namespace NihongoVocabTrainer.Models
{
	/// <summary>
	/// 日本語の単語情報を保持します。
	/// </summary>
	public class Vocabulary
	{
		/// <summary>
		/// 漢字
		/// </summary>
		public string Kanji { get; set; } = string.Empty;

		/// <summary>
		/// ひらがな
		/// </summary>
		public string Hiragana { get; set; } = string.Empty;

		/// <summary>
		/// ベトナム語の意味
		/// </summary>
		public string Meaning { get; set; } = string.Empty;

		/// <summary>
		/// JLPTレベル
		/// </summary>
		public string Level { get; set; } = string.Empty;

		/// <summary>
		/// 例文
		/// </summary>
		public string Example { get; set; } = string.Empty;

		/// <summary>
		/// 正解回数
		/// </summary>
		public int CorrectCount { get; set; }

		/// <summary>
		/// 不正解回数
		/// </summary>
		public int WrongCount { get; set; }

		/// <summary>
		/// 難しい単語かどうか
		/// </summary>
		public bool IsDifficult { get; set; }
	}
}