namespace NihongoVocabTrainer.Models
{
    /// <summary>
    /// 模擬試験の問題情報を保持します。
    /// </summary>
    public class ExamQuestion
    {
        /// <summary>
        /// テストID
        /// </summary>
        public string TestId { get; set; } = string.Empty;

        /// <summary>
        /// JLPTレベル
        /// </summary>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// 問題番号
        /// </summary>
        public int QuestionNo { get; set; }

        /// <summary>
        /// 問題文
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// 選択肢A
        /// </summary>
        public string OptionA { get; set; } = string.Empty;

        /// <summary>
        /// 選択肢B
        /// </summary>
        public string OptionB { get; set; } = string.Empty;

        /// <summary>
        /// 選択肢C
        /// </summary>
        public string OptionC { get; set; } = string.Empty;

        /// <summary>
        /// 選択肢D
        /// </summary>
        public string OptionD { get; set; } = string.Empty;

        /// <summary>
        /// 正解選択肢（A/B/C/D）
        /// </summary>
        public string CorrectOption { get; set; } = string.Empty;

        /// <summary>
        /// 解説
        /// </summary>
        public string Explanation { get; set; } = string.Empty;
    }
}
