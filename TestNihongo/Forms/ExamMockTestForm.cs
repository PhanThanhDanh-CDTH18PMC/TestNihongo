using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Forms
{
    public partial class ExamMockTestForm : Form
    {
        private readonly List<ExamQuestion> _questions;

        private readonly Dictionary<int, string> _userAnswers = new Dictionary<int, string>();

        private int _currentIndex;

        /// <summary>
        /// 試験データが採点されたかどうかを取得します。
        /// </summary>
        public bool IsSubmitted { get; private set; }

        /// <summary>
        /// 模擬試験画面を初期化します。
        /// </summary>
        /// <param name="questions">問題一覧</param>
        public ExamMockTestForm(List<ExamQuestion> questions)
        {
            InitializeComponent();

            _questions = questions;
        }

        /// <summary>
        /// 画面読み込み時にデザインを設定し、最初の問題を表示します。
        /// </summary>
        private void ExamMockTestForm_Load(object sender, EventArgs e)
        {
            #region 初期表示

            ApplyCuteDesign();
            ShowQuestion();

            #endregion
        }

        /// <summary>
        /// 現在の問題を表示します。
        /// </summary>
        private void ShowQuestion()
        {
            #region 問題表示

            if (_questions.Count == 0)
            {
                lblQuestion.Text = "問題データがありません。";
                SetAnswerControlsEnabled(false);
                return;
            }

            ExamQuestion question = _questions[_currentIndex];

            lblProgress.Text = $"Question {_currentIndex + 1} / {_questions.Count}";
            lblScore.Text = $"Answered: {_userAnswers.Count} / {_questions.Count}";

            lblQuestion.Text = question.Question;
            rdoA.Text = $"A. {question.OptionA}";
            rdoB.Text = $"B. {question.OptionB}";
            rdoC.Text = $"C. {question.OptionC}";
            rdoD.Text = $"D. {question.OptionD}";

            RestoreSelectedAnswer();

            lblResult.Text = string.Empty;
            btnPrevious.Enabled = _currentIndex > 0;
            btnNext.Enabled = _currentIndex < _questions.Count - 1;

            #endregion
        }

        /// <summary>
        /// 選択済み回答を画面に復元します。
        /// </summary>
        private void RestoreSelectedAnswer()
        {
            #region 選択回答復元

            rdoA.CheckedChanged -= Answer_CheckedChanged;
            rdoB.CheckedChanged -= Answer_CheckedChanged;
            rdoC.CheckedChanged -= Answer_CheckedChanged;
            rdoD.CheckedChanged -= Answer_CheckedChanged;

            rdoA.Checked = false;
            rdoB.Checked = false;
            rdoC.Checked = false;
            rdoD.Checked = false;

            if (_userAnswers.TryGetValue(_currentIndex, out string? answer))
            {
                rdoA.Checked = answer == "A";
                rdoB.Checked = answer == "B";
                rdoC.Checked = answer == "C";
                rdoD.Checked = answer == "D";
            }

            rdoA.CheckedChanged += Answer_CheckedChanged;
            rdoB.CheckedChanged += Answer_CheckedChanged;
            rdoC.CheckedChanged += Answer_CheckedChanged;
            rdoD.CheckedChanged += Answer_CheckedChanged;

            #endregion
        }

        /// <summary>
        /// 回答選択時にユーザー回答を保存します。
        /// </summary>
        private void Answer_CheckedChanged(object? sender, EventArgs e)
        {
            #region 回答保存

            string selectedAnswer = GetSelectedOption();

            if (!string.IsNullOrWhiteSpace(selectedAnswer))
            {
                _userAnswers[_currentIndex] = selectedAnswer;
                lblScore.Text = $"Answered: {_userAnswers.Count} / {_questions.Count}";
            }

            #endregion
        }

        /// <summary>
        /// 現在選択されている選択肢を取得します。
        /// </summary>
        /// <returns>選択肢</returns>
        private string GetSelectedOption()
        {
            #region 選択肢取得

            if (rdoA.Checked)
            {
                return "A";
            }

            if (rdoB.Checked)
            {
                return "B";
            }

            if (rdoC.Checked)
            {
                return "C";
            }

            if (rdoD.Checked)
            {
                return "D";
            }

            return string.Empty;

            #endregion
        }

        /// <summary>
        /// 前の問題を表示します。
        /// </summary>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            #region 前問題表示

            if (_currentIndex <= 0)
            {
                return;
            }

            _currentIndex--;
            ShowQuestion();

            #endregion
        }

        /// <summary>
        /// 次の問題を表示します。
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            #region 次問題表示

            if (_currentIndex >= _questions.Count - 1)
            {
                return;
            }

            _currentIndex++;
            ShowQuestion();

            #endregion
        }

        /// <summary>
        /// 試験を採点します。
        /// </summary>
        private void btnSubmitTest_Click(object sender, EventArgs e)
        {
            #region 採点処理

            int unansweredCount = _questions.Count - _userAnswers.Count;

            if (unansweredCount > 0)
            {
                DialogResult result = MessageBox.Show(
                    $"未回答の問題が {unansweredCount} 件あります。採点しますか？",
                    "採点確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            int correctCount = 0;
            var wrongQuestionTexts = new List<string>();

            for (int i = 0; i < _questions.Count; i++)
            {
                ExamQuestion question = _questions[i];
                string userAnswer = _userAnswers.ContainsKey(i) ? _userAnswers[i] : string.Empty;

                if (userAnswer == question.CorrectOption)
                {
                    correctCount++;
                }
                else
                {
                    wrongQuestionTexts.Add(
                        $"Q{question.QuestionNo}: 正解 {question.CorrectOption}" +
                        (string.IsNullOrWhiteSpace(question.Explanation) ? string.Empty : $" - {question.Explanation}"));
                }
            }

            int wrongCount = _questions.Count - correctCount;
            double rate = _questions.Count == 0 ? 0 : (double)correctCount / _questions.Count * 100;

            IsSubmitted = true;

            string message =
                $"結果：{correctCount} / {_questions.Count}" + Environment.NewLine +
                $"正解率：{rate:F1}%" + Environment.NewLine +
                $"不正解：{wrongCount} 件";

            if (wrongQuestionTexts.Count > 0)
            {
                message += Environment.NewLine + Environment.NewLine +
                           "間違えた問題：" + Environment.NewLine +
                           string.Join(Environment.NewLine, wrongQuestionTexts.Take(10));

                if (wrongQuestionTexts.Count > 10)
                {
                    message += Environment.NewLine + "...";
                }
            }

            MessageBox.Show(message, "試験結果", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblResult.Text = $"Result: {correctCount} / {_questions.Count} ({rate:F1}%)";

            #endregion
        }

        /// <summary>
        /// 画面を閉じます。
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            #region 画面終了

            Close();

            #endregion
        }

        /// <summary>
        /// 回答選択肢の有効／無効を切り替えます。
        /// </summary>
        /// <param name="enabled">有効にする場合は true</param>
        private void SetAnswerControlsEnabled(bool enabled)
        {
            #region 回答選択肢切替

            rdoA.Enabled = enabled;
            rdoB.Enabled = enabled;
            rdoC.Enabled = enabled;
            rdoD.Enabled = enabled;
            btnPrevious.Enabled = enabled;
            btnNext.Enabled = enabled;
            btnSubmitTest.Enabled = enabled;

            #endregion
        }

        /// <summary>
        /// 画面全体のかわいいデザインを設定します。
        /// </summary>
        private void ApplyCuteDesign()
        {
            #region デザイン設定

            BackColor = Color.FromArgb(255, 250, 245);
            Font = new Font("Segoe UI", 10F);

            lblTitle.ForeColor = Color.FromArgb(31, 41, 55);
            lblProgress.ForeColor = Color.FromArgb(31, 41, 55);
            lblScore.ForeColor = Color.FromArgb(31, 41, 55);
            lblQuestion.BackColor = Color.White;
            lblQuestion.ForeColor = Color.FromArgb(31, 41, 55);
            lblResult.ForeColor = Color.FromArgb(219, 39, 119);

            StyleRadioButton(rdoA);
            StyleRadioButton(rdoB);
            StyleRadioButton(rdoC);
            StyleRadioButton(rdoD);

            StyleButton(btnPrevious, Color.FromArgb(226, 232, 240));
            StyleButton(btnNext, Color.FromArgb(186, 230, 253));
            StyleButton(btnSubmitTest, Color.FromArgb(221, 214, 254));
            StyleButton(btnClose, Color.FromArgb(245, 245, 244));

            #endregion
        }

        /// <summary>
        /// ラジオボタンのデザインを設定します。
        /// </summary>
        /// <param name="radioButton">対象ラジオボタン</param>
        private void StyleRadioButton(RadioButton radioButton)
        {
            #region ラジオボタンデザイン

            radioButton.BackColor = Color.FromArgb(255, 250, 245);
            radioButton.ForeColor = Color.FromArgb(31, 41, 55);
            radioButton.Font = new Font("Yu Gothic UI", 12F);

            #endregion
        }

        /// <summary>
        /// ボタンのデザインを設定します。
        /// </summary>
        /// <param name="button">対象ボタン</param>
        /// <param name="backColor">背景色</param>
        private void StyleButton(Button button, Color backColor)
        {
            #region ボタンデザイン

            button.BackColor = backColor;
            button.ForeColor = Color.FromArgb(31, 41, 55);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;

            #endregion
        }
    }
}
