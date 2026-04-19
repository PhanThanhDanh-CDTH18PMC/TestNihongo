namespace NihongoVocabTrainer.Models
{
    /// <summary>
    /// 日本語文法の情報を保持します。
    /// </summary>
    public class GrammarItem
    {
        /// <summary>
        /// 文法パターン
        /// </summary>
        public string Pattern { get; set; } = string.Empty;

        /// <summary>
        /// ベトナム語の意味
        /// </summary>
        public string Meaning { get; set; } = string.Empty;

        /// <summary>
        /// JLPTレベル
        /// </summary>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// 使い方
        /// </summary>
        public string Usage { get; set; } = string.Empty;

        /// <summary>
        /// 日本語例文
        /// </summary>
        public string ExampleJapanese { get; set; } = string.Empty;

        /// <summary>
        /// ベトナム語例文
        /// </summary>
        public string ExampleVietnamese { get; set; } = string.Empty;

        /// <summary>
        /// 正解回数
        /// </summary>
        public int CorrectCount { get; set; }

        /// <summary>
        /// 不正解回数
        /// </summary>
        public int WrongCount { get; set; }

        /// <summary>
        /// 苦手文法かどうか
        /// </summary>
        public bool IsDifficult { get; set; }
    }
}
