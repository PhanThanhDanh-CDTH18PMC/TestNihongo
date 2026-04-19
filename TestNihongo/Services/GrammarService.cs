using NihongoVocabTrainer.Models;
using System.Text;

namespace NihongoVocabTrainer.Services
{
	/// <summary>
	/// 文法データの読み込み、保存処理を行います。
	/// </summary>
	public class GrammarService
	{
		/// <summary>
		/// Dataフォルダ内の grammar_*.csv をすべて読み込みます。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <returns>文法一覧</returns>
		public List<GrammarItem> LoadAllFromDataDirectory(string dataDirectoryPath)
		{
			#region 全文法CSV読み込み

			var grammarItems = new List<GrammarItem>();

			if (!Directory.Exists(dataDirectoryPath))
			{
				Directory.CreateDirectory(dataDirectoryPath);
				return grammarItems;
			}

			string[] csvFiles = Directory.GetFiles(dataDirectoryPath, "grammar_*.csv");

			foreach (string csvFile in csvFiles)
			{
				grammarItems.AddRange(LoadFromCsv(csvFile));
			}

			return grammarItems;

			#endregion
		}

		/// <summary>
		/// CSVファイルから文法一覧を読み込みます。
		/// 複数行にまたがるCSV項目にも対応します。
		/// </summary>
		/// <param name="filePath">CSVファイルパス</param>
		/// <returns>文法一覧</returns>
		public List<GrammarItem> LoadFromCsv(string filePath)
		{
			#region CSV読み込み

			var grammarItems = new List<GrammarItem>();

			if (!File.Exists(filePath))
			{
				return grammarItems;
			}

			string csvText = File.ReadAllText(filePath, Encoding.UTF8);

			List<string> records = SplitCsvRecords(csvText);

			foreach (string record in records.Skip(1))
			{
				if (string.IsNullOrWhiteSpace(record))
				{
					continue;
				}

				string[] parts = SplitCsvLine(record);

				if (parts.Length < 6)
				{
					continue;
				}

				var grammarItem = new GrammarItem
				{
					Pattern = parts[0],
					Meaning = parts[1],
					Level = parts[2],
					Usage = parts[3],
					ExampleJapanese = parts[4],
					ExampleVietnamese = parts[5],
					CorrectCount = parts.Length > 6 && int.TryParse(parts[6], out int correctCount) ? correctCount : 0,
					WrongCount = parts.Length > 7 && int.TryParse(parts[7], out int wrongCount) ? wrongCount : 0,
					IsDifficult = parts.Length > 8 && bool.TryParse(parts[8], out bool isDifficult) && isDifficult
				};

				grammarItems.Add(grammarItem);
			}

			return grammarItems;

			#endregion
		}

		/// <summary>
		/// CSVテキストをレコード単位に分割します。
		/// ダブルクォーテーション内の改行は同一レコードとして扱います。
		/// </summary>
		/// <param name="csvText">CSV全体の文字列</param>
		/// <returns>CSVレコード一覧</returns>
		private List<string> SplitCsvRecords(string csvText)
		{
			#region CSVレコード分割

			var records = new List<string>();
			var current = new StringBuilder();

			bool isInQuotes = false;

			for (int i = 0; i < csvText.Length; i++)
			{
				char character = csvText[i];

				if (character == '"')
				{
					if (isInQuotes && i + 1 < csvText.Length && csvText[i + 1] == '"')
					{
						current.Append('"');
						i++;
					}
					else
					{
						isInQuotes = !isInQuotes;
						current.Append(character);
					}
				}
				else if ((character == '\r' || character == '\n') && !isInQuotes)
				{
					if (character == '\r' && i + 1 < csvText.Length && csvText[i + 1] == '\n')
					{
						i++;
					}

					if (!string.IsNullOrWhiteSpace(current.ToString()))
					{
						records.Add(current.ToString());
						current.Clear();
					}
				}
				else
				{
					current.Append(character);
				}
			}

			if (!string.IsNullOrWhiteSpace(current.ToString()))
			{
				records.Add(current.ToString());
			}

			return records;

			#endregion
		}

		/// <summary>
		/// 文法一覧をレベル別CSVファイルに保存します。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <param name="grammarItems">文法一覧</param>
		public void SaveAllByLevel(string dataDirectoryPath, List<GrammarItem> grammarItems)
		{
			#region レベル別CSV保存

			if (!Directory.Exists(dataDirectoryPath))
			{
				Directory.CreateDirectory(dataDirectoryPath);
			}

			var levelGroups = grammarItems.GroupBy(x => NormalizeLevel(x.Level));

			foreach (var group in levelGroups)
			{
				string filePath = GetLevelCsvPath(dataDirectoryPath, group.Key);

				var lines = new List<string>
				{
					"Pattern,Meaning,Level,Usage,ExampleJapanese,ExampleVietnamese,CorrectCount,WrongCount,IsDifficult"
				};

				foreach (GrammarItem grammarItem in group)
				{
					lines.Add(ToCsvLine(grammarItem));
				}

				File.WriteAllLines(filePath, lines, Encoding.UTF8);
			}

			#endregion
		}

		/// <summary>
		/// レベルに対応する文法CSVファイルパスを取得します。
		/// </summary>
		/// <param name="dataDirectoryPath">Dataフォルダパス</param>
		/// <param name="level">JLPTレベル</param>
		/// <returns>CSVファイルパス</returns>
		private string GetLevelCsvPath(string dataDirectoryPath, string level)
		{
			#region レベルCSVパス取得

			string normalizedLevel = NormalizeLevel(level).ToLower();

			return Path.Combine(dataDirectoryPath, $"grammar_{normalizedLevel}.csv");

			#endregion
		}

		/// <summary>
		/// 文法情報をCSVの1行に変換します。
		/// </summary>
		/// <param name="grammarItem">文法情報</param>
		/// <returns>CSV行</returns>
		private string ToCsvLine(GrammarItem grammarItem)
		{
			#region CSV行変換

			return string.Join(",",
				EscapeCsvValue(grammarItem.Pattern),
				EscapeCsvValue(grammarItem.Meaning),
				EscapeCsvValue(grammarItem.Level),
				EscapeCsvValue(grammarItem.Usage),
				EscapeCsvValue(grammarItem.ExampleJapanese),
				EscapeCsvValue(grammarItem.ExampleVietnamese),
				grammarItem.CorrectCount.ToString(),
				grammarItem.WrongCount.ToString(),
				grammarItem.IsDifficult.ToString());

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
		/// CSVファイルの1行を分割します。
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
