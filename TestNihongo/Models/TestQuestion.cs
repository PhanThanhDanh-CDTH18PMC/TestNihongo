using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Models
{
    /// <summary>
    /// 模擬テストの1問分の情報を保持します。
    /// </summary>
    public class TestQuestion
    {
        /// <summary>
        /// 出題対象の単語
        /// </summary>
        public Vocabulary Vocabulary { get; set; } = new Vocabulary();

        /// <summary>
        /// 回答候補
        /// </summary>
        public List<string> Options { get; set; } = new List<string>();

        /// <summary>
        /// 正解
        /// </summary>
        public string CorrectAnswer { get; set; } = string.Empty;

        /// <summary>
        /// ユーザーが選択した回答
        /// </summary>
        public string SelectedAnswer { get; set; } = string.Empty;

        /// <summary>
        /// 回答済みかどうか
        /// </summary>
        public bool IsAnswered => !string.IsNullOrWhiteSpace(SelectedAnswer);

        /// <summary>
        /// 正解かどうか
        /// </summary>
        public bool IsCorrect => SelectedAnswer == CorrectAnswer;
    }
}
