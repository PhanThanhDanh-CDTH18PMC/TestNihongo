using NihongoVocabTrainer.Models;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NihongoVocabTrainer.Services
{
	/// <summary>
	/// JTest のHTMLファイルから単語データを抽出する処理を行います。
	/// </summary>
	public class JTestHtmlParserService
	{
		/// <summary>
		/// JTest のHTMLファイルから単語一覧を抽出します。
		/// </summary>
		/// <param name="htmlFilePath">HTMLファイルパス</param>
		/// <param name="level">JLPTレベル</param>
		/// <returns>単語一覧</returns>
		public List<Vocabulary> ParseFromHtmlFile(string htmlFilePath, string level)
		{
			#region HTML解析

			var vocabularies = new List<Vocabulary>();

			if (!File.Exists(htmlFilePath))
			{
				MessageBox.Show("HTMLファイルが見つかりません。");
				return vocabularies;
			}

			string html = File.ReadAllText(htmlFilePath, Encoding.UTF8);

			string text = ConvertHtmlToText(html);

			string[] lines = text
				.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
				.Select(x => x.Trim())
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.ToArray();

			for (int i = 0; i < lines.Length; i++)
			{
				if (!IsVocabularyNumber(lines[i]))
				{
					continue;
				}

				if (i + 3 >= lines.Length)
				{
					continue;
				}

				string kanji = lines[i + 1].Trim();
				string line2 = lines[i + 2].Trim();
				string line3 = lines[i + 3].Trim();

				string hiragana = string.Empty;
				string meaning = string.Empty;
				string example = string.Empty;

				if (IsJapaneseReading(line2))
				{
					hiragana = line2;

					if (i + 4 >= lines.Length)
					{
						continue;
					}

					meaning = lines[i + 3];
					example = lines[i + 4];
				}
				else
				{
					meaning = line2;
					example = line3;

					// 漢字がない単語の場合は、読み仮名にも同じ値を設定します。
					if (IsJapaneseReading(kanji))
					{
						hiragana = kanji;
					}
				}

				if (string.IsNullOrWhiteSpace(kanji) ||
					string.IsNullOrWhiteSpace(meaning))
				{
					continue;
				}

				vocabularies.Add(new Vocabulary
				{
					Kanji = kanji,
					Hiragana = hiragana,
					Meaning = meaning,
					Level = level,
					Example = example
				});
			}

			return vocabularies;

			#endregion
		}

		/// <summary>
		/// HTMLをテキストに変換します。
		/// </summary>
		/// <param name="html">HTML文字列</param>
		/// <returns>テキスト</returns>
		private string ConvertHtmlToText(string html)
		{
			#region HTMLテキスト変換

			string text = html;

			text = Regex.Replace(text, "<br\\s*/?>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</p>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</div>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</h1>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</h2>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</h3>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</h4>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</h5>", "\n", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "</li>", "\n", RegexOptions.IgnoreCase);

			text = Regex.Replace(text, "<.*?>", string.Empty);
			text = WebUtility.HtmlDecode(text);

			return text;

			#endregion
		}

		/// <summary>
		/// 単語番号かどうかを判定します。
		/// </summary>
		/// <param name="value">文字列</param>
		/// <returns>単語番号の場合 true</returns>
		private bool IsVocabularyNumber(string value)
		{
			#region 単語番号判定

			return int.TryParse(value, out int number) &&
				   number > 0 &&
				   number < 5000;

			#endregion
		}

		/// <summary>
		/// 読み仮名らしい文字列かどうかを判定します。
		/// ひらがな、カタカナ、長音、括弧、および「＜する＞」「<する>」形式を許可します。
		/// </summary>
		/// <param name="value">文字列</param>
		/// <returns>読み仮名の場合 true</returns>
		private bool IsJapaneseReading(string value)
		{
			#region 読み仮名判定

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			string normalizedValue = value.Trim();

			return Regex.IsMatch(
				normalizedValue,
				@"^[ぁ-んァ-ンー・･（）\(\)＜＞<>［］\[\]【】「」『』〜~\s]+$");

			#endregion
		}
	}
}