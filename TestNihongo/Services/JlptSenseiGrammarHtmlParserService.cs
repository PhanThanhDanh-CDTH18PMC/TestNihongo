using NihongoVocabTrainer.Models;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NihongoVocabTrainer.Services
{
	/// <summary>
	/// JLPT Sensei の文法一覧HTMLから文法データを抽出する処理を行います。
	/// </summary>
	public class JlptSenseiGrammarHtmlParserService
	{
		/// <summary>
		/// HTMLファイルから文法一覧を抽出します。
		/// </summary>
		/// <param name="htmlFilePath">HTMLファイルパス</param>
		/// <param name="level">JLPTレベル</param>
		/// <returns>文法一覧</returns>
		public List<GrammarItem> ParseFromHtmlFile(string htmlFilePath, string level)
		{
			#region HTML解析

			var grammarItems = new List<GrammarItem>();

			if (!File.Exists(htmlFilePath))
			{
				MessageBox.Show("HTMLファイルが見つかりません。");
				return grammarItems;
			}

			string html = File.ReadAllText(htmlFilePath, Encoding.UTF8);

			MatchCollection rowMatches = Regex.Matches(
				html,
				@"<tr[^>]*>(.*?)</tr>",
				RegexOptions.IgnoreCase | RegexOptions.Singleline);

			foreach (Match rowMatch in rowMatches)
			{
				string rowHtml = rowMatch.Groups[1].Value;

				List<string> cells = ExtractTableCells(rowHtml);

				if (cells.Count < 4)
				{
					continue;
				}

				// 0: 番号
				// 1: ローマ字の文法
				// 2: 日本語の文法
				// 3: ベトナム語の意味
				string no = cells[0].Trim();
				string romajiPattern = cells[1].Trim();
				string japanesePattern = cells[2].Trim();
				string meaning = cells[3].Trim();

				if (!int.TryParse(no, out _))
				{
					continue;
				}

				if (string.IsNullOrWhiteSpace(japanesePattern) ||
					string.IsNullOrWhiteSpace(meaning))
				{
					continue;
				}

				grammarItems.Add(new GrammarItem
				{
					Pattern = japanesePattern,
					Meaning = meaning,
					Level = level,
					Usage = romajiPattern,
					ExampleJapanese = string.Empty,
					ExampleVietnamese = string.Empty,
					CorrectCount = 0,
					WrongCount = 0,
					IsDifficult = false
				});
			}

			return grammarItems;

			#endregion
		}

		/// <summary>
		/// テーブル行HTMLからセル文字列を抽出します。
		/// </summary>
		/// <param name="rowHtml">tr 内のHTML</param>
		/// <returns>セル一覧</returns>
		private List<string> ExtractTableCells(string rowHtml)
		{
			#region セル抽出

			var cells = new List<string>();

			MatchCollection cellMatches = Regex.Matches(
				rowHtml,
				@"<t[dh][^>]*>(.*?)</t[dh]>",
				RegexOptions.IgnoreCase | RegexOptions.Singleline);

			foreach (Match cellMatch in cellMatches)
			{
				string cellHtml = cellMatch.Groups[1].Value;
				string cellText = ConvertHtmlToText(cellHtml);

				cells.Add(cellText);
			}

			return cells;

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
			text = Regex.Replace(text, "<.*?>", string.Empty, RegexOptions.Singleline);
			text = WebUtility.HtmlDecode(text);

			text = text
				.Replace("\r", string.Empty)
				.Replace("\n", " ")
				.Replace("\t", string.Empty)
				.Trim();

			while (text.Contains("  "))
			{
				text = text.Replace("  ", " ");
			}

			return text;

			#endregion
		}
	}
}