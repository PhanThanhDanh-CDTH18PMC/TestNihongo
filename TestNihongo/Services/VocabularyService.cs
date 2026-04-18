using NihongoVocabTrainer.Models;
using System.Text;

namespace NihongoVocabTrainer.Services
{
	/// <summary>
	/// 単語データの読み込み、保存処理を行います。
	/// </summary>
	public class VocabularyService
	{
		/// <summary>
		/// Dataフォルダ内の vocab_*.csv をすべて読み込みます。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <returns>単語一覧</returns>
		public List<Vocabulary> LoadAllFromDataDirectory(string dataDirectoryPath)
		{
			#region 全CSV読み込み

			var vocabularies = new List<Vocabulary>();

			if (!Directory.Exists(dataDirectoryPath))
			{
				Directory.CreateDirectory(dataDirectoryPath);
				return vocabularies;
			}

			string[] csvFiles = Directory.GetFiles(dataDirectoryPath, "vocab_*.csv");

			foreach (string csvFile in csvFiles)
			{
				vocabularies.AddRange(LoadFromCsv(csvFile));
			}

			return vocabularies;

			#endregion
		}

		/// <summary>
		/// CSVファイルから単語一覧を読み込みます。
		/// </summary>
		/// <param name="filePath">CSVファイルパス</param>
		/// <returns>単語一覧</returns>
		public List<Vocabulary> LoadFromCsv(string filePath)
		{
			#region CSV読み込み

			var vocabularies = new List<Vocabulary>();

			if (!File.Exists(filePath))
			{
				return vocabularies;
			}

			string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

			foreach (string line in lines.Skip(1))
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					continue;
				}

				string[] parts = SplitCsvLine(line);

				if (parts.Length < 5)
				{
					continue;
				}

				vocabularies.Add(new Vocabulary
				{
					Kanji = parts[0],
					Hiragana = parts[1],
					Meaning = parts[2],
					Level = parts[3],
					Example = parts[4],
					CorrectCount = parts.Length > 5 && int.TryParse(parts[5], out int correctCount) ? correctCount : 0,
					WrongCount = parts.Length > 6 && int.TryParse(parts[6], out int wrongCount) ? wrongCount : 0,
					IsDifficult = parts.Length > 7 && bool.TryParse(parts[7], out bool isDifficult) && isDifficult
				});
			}

			return vocabularies;

			#endregion
		}

		/// <summary>
		/// 単語をレベル別CSVファイルに追記します。
		/// ファイルが存在しない場合は新規作成します。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <param name="vocabulary">単語</param>
		public void AppendToLevelCsv(string dataDirectoryPath, Vocabulary vocabulary)
		{
			#region レベル別CSV追記

			if (!Directory.Exists(dataDirectoryPath))
			{
				Directory.CreateDirectory(dataDirectoryPath);
			}

			string filePath = GetLevelCsvPath(dataDirectoryPath, vocabulary.Level);

			bool needsHeader = !File.Exists(filePath) || new FileInfo(filePath).Length == 0;

			using var writer = new StreamWriter(filePath, append: true, Encoding.UTF8);

			if (needsHeader)
			{
				writer.WriteLine("Kanji,Hiragana,Meaning,Level,Example,CorrectCount,WrongCount,IsDifficult");
			}

			writer.WriteLine(ToCsvLine(vocabulary));

			#endregion
		}

		/// <summary>
		/// 単語一覧をレベル別CSVファイルに保存します。
		/// 既存ファイルは上書きされます。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <param name="vocabularies">単語一覧</param>
		public void SaveAllByLevel(string dataDirectoryPath, List<Vocabulary> vocabularies)
		{
			#region レベル別CSV保存

			if (!Directory.Exists(dataDirectoryPath))
			{
				Directory.CreateDirectory(dataDirectoryPath);
			}

			var levelGroups = vocabularies
				.GroupBy(x => NormalizeLevel(x.Level));

			foreach (var group in levelGroups)
			{
				string filePath = GetLevelCsvPath(dataDirectoryPath, group.Key);

				var lines = new List<string>
				{
					"Kanji,Hiragana,Meaning,Level,Example,CorrectCount,WrongCount,IsDifficult"
				};

				foreach (Vocabulary vocabulary in group)
				{
					lines.Add(ToCsvLine(vocabulary));
				}

				File.WriteAllLines(filePath, lines, Encoding.UTF8);
			}

			#endregion
		}

		/// <summary>
		/// レベルに対応するCSVファイルパスを取得します。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <param name="level">JLPTレベル</param>
		/// <returns>CSVファイルパス</returns>
		private string GetLevelCsvPath(string dataDirectoryPath, string level)
		{
			#region レベルCSVパス取得

			string normalizedLevel = NormalizeLevel(level).ToLower();

			return Path.Combine(dataDirectoryPath, $"vocab_{normalizedLevel}.csv");

			#endregion
		}

		/// <summary>
		/// JLPTレベルを正規化します。
		/// </summary>
		/// <param name="level">JLPTレベル</param>
		/// <returns>正規化後のJLPTレベル</returns>
		private string NormalizeLevel(string level)
		{
			#region レベル正規化

			string normalizedLevel = level
				.Replace("\r", string.Empty)
				.Replace("\n", string.Empty)
				.Trim()
				.ToUpper();

			if (string.IsNullOrWhiteSpace(normalizedLevel) || normalizedLevel == "ALL")
			{
				return "N5";
			}

			return normalizedLevel;

			#endregion
		}

		/// <summary>
		/// 単語をCSVの1行に変換します。
		/// </summary>
		/// <param name="vocabulary">単語</param>
		/// <returns>CSV行</returns>
		private string ToCsvLine(Vocabulary vocabulary)
		{
			#region CSV行変換

			return string.Join(",",
				EscapeCsvValue(vocabulary.Kanji),
				EscapeCsvValue(vocabulary.Hiragana),
				EscapeCsvValue(vocabulary.Meaning),
				EscapeCsvValue(vocabulary.Level),
				EscapeCsvValue(vocabulary.Example),
				vocabulary.CorrectCount.ToString(),
				vocabulary.WrongCount.ToString(),
				vocabulary.IsDifficult.ToString());

			#endregion
		}

		/// <summary>
		/// CSVファイルの1行をカンマ区切りで分割します。
		/// ダブルクォーテーション内のカンマは分割対象外にします。
		/// </summary>
		/// <param name="line">CSVの1行</param>
		/// <returns>分割後の値</returns>
		private string[] SplitCsvLine(string line)
		{
			#region CSV行分割

			var result = new List<string>();
			var current = new StringBuilder();

			bool isInQuotes = false;

			for (int i = 0; i < line.Length; i++)
			{
				char character = line[i];

				if (character == '"')
				{
					if (isInQuotes && i + 1 < line.Length && line[i + 1] == '"')
					{
						current.Append('"');
						i++;
					}
					else
					{
						isInQuotes = !isInQuotes;
					}
				}
				else if (character == ',' && !isInQuotes)
				{
					result.Add(current.ToString());
					current.Clear();
				}
				else
				{
					current.Append(character);
				}
			}

			result.Add(current.ToString());

			return result.ToArray();

			#endregion
		}

		/// <summary>
		/// CSV保存用に値をエスケープします。
		/// </summary>
		/// <param name="value">値</param>
		/// <returns>CSV用の値</returns>
		private string EscapeCsvValue(string value)
		{
			#region CSV値エスケープ

			if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
			{
				return "\"" + value.Replace("\"", "\"\"") + "\"";
			}

			return value;

			#endregion
		}
	}
}