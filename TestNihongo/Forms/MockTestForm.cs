using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Forms
{
	public partial class MockTestForm : Form
	{
		private readonly List<Vocabulary> _vocabularies;

		private readonly Random _random = new Random();

		private readonly int _questionCountLimit;

		private List<TestQuestion> _questions = new List<TestQuestion>();

		private int _currentQuestionIndex;

		private bool _isSubmitted;

		/// <summary>
		/// 学習データが変更されたかどうかを取得します。
		/// </summary>
		public bool IsChanged { get; private set; }

		/// <summary>
		/// 模擬テスト画面を初期化します。
		/// </summary>
		/// <param name="vocabularies">単語一覧</param>
		/// <param name="questionCountLimit">出題数</param>
		public MockTestForm(List<Vocabulary> vocabularies, int questionCountLimit)
		{
			InitializeComponent();

			_vocabularies = vocabularies;
			_questionCountLimit = questionCountLimit;
		}

		/// <summary>
		/// 画面読み込み時に模擬テストを開始します。
		/// </summary>
		private void MockTestForm_Load(object sender, EventArgs e)
		{
			#region 初期表示

			ApplyCuteDesign();
			CreateQuestions();
			ShowCurrentQuestion();

			#endregion
		}

		/// <summary>
		/// 模擬テストの問題を作成します。
		/// </summary>
		private void CreateQuestions()
		{
			#region 問題作成

			if (_vocabularies.Count < 4)
			{
				lblQuestion.Text = "単語が4件以上必要です。";
				SetAnswerControlsEnabled(false);
				btnPrevious.Enabled = false;
				btnNext.Enabled = false;
				btnSubmitTest.Enabled = false;
				return;
			}

			int questionCount = Math.Min(_questionCountLimit, _vocabularies.Count);

			List<Vocabulary> selectedVocabularies = _vocabularies
				.OrderBy(x => _random.Next())
				.Take(questionCount)
				.ToList();

			_questions = selectedVocabularies
				.Select(CreateQuestion)
				.ToList();

			_currentQuestionIndex = 0;

			#endregion
		}

		/// <summary>
		/// 単語から1問分の問題を作成します。
		/// </summary>
		/// <param name="vocabulary">出題単語</param>
		/// <returns>問題</returns>
		private TestQuestion CreateQuestion(Vocabulary vocabulary)
		{
			#region 1問作成

			var options = new List<string>
			{
				vocabulary.Meaning
			};

			List<string> wrongOptions = _vocabularies
				.Where(x => x.Meaning != vocabulary.Meaning)
				.Select(x => x.Meaning)
				.Distinct()
				.OrderBy(x => _random.Next())
				.Take(3)
				.ToList();

			options.AddRange(wrongOptions);

			while (options.Count < 4)
			{
				options.Add("-");
			}

			options = options
				.OrderBy(x => _random.Next())
				.ToList();

			return new TestQuestion
			{
				Vocabulary = vocabulary,
				Options = options,
				CorrectAnswer = vocabulary.Meaning
			};

			#endregion
		}

		/// <summary>
		/// 現在の問題を表示します。
		/// </summary>
		private void ShowCurrentQuestion()
		{
			#region 問題表示

			if (_questions.Count == 0)
			{
				return;
			}

			TestQuestion question = _questions[_currentQuestionIndex];

			lblProgress.Text = $"Question {_currentQuestionIndex + 1} / {_questions.Count}";
			lblQuestion.Text = string.IsNullOrWhiteSpace(question.Vocabulary.Kanji)
				? question.Vocabulary.Hiragana
				: question.Vocabulary.Kanji;

			rdoA.Text = question.Options[0];
			rdoB.Text = question.Options[1];
			rdoC.Text = question.Options[2];
			rdoD.Text = question.Options[3];

			SetSelectedAnswer(question.SelectedAnswer);

			lblResult.Text = question.IsAnswered
				? "回答済みです。"
				: "";

			btnPrevious.Enabled = _currentQuestionIndex > 0;
			btnNext.Enabled = _currentQuestionIndex < _questions.Count - 1;
			btnSubmitTest.Enabled = !_isSubmitted;
			SetAnswerControlsEnabled(!_isSubmitted);

			#endregion
		}

		/// <summary>
		/// 選択中の回答を問題に保存します。
		/// </summary>
		private void SaveSelectedAnswer()
		{
			#region 選択回答保存

			if (_questions.Count == 0 || _isSubmitted)
			{
				return;
			}

			_questions[_currentQuestionIndex].SelectedAnswer = GetSelectedAnswer();

			#endregion
		}

		/// <summary>
		/// 選択された回答を取得します。
		/// </summary>
		/// <returns>選択された回答</returns>
		private string GetSelectedAnswer()
		{
			#region 選択回答取得

			if (rdoA.Checked)
			{
				return rdoA.Text;
			}

			if (rdoB.Checked)
			{
				return rdoB.Text;
			}

			if (rdoC.Checked)
			{
				return rdoC.Text;
			}

			if (rdoD.Checked)
			{
				return rdoD.Text;
			}

			return string.Empty;

			#endregion
		}

		/// <summary>
		/// 保存済み回答を画面に反映します。
		/// </summary>
		/// <param name="selectedAnswer">保存済み回答</param>
		private void SetSelectedAnswer(string selectedAnswer)
		{
			#region 選択回答反映

			rdoA.Checked = rdoA.Text == selectedAnswer;
			rdoB.Checked = rdoB.Text == selectedAnswer;
			rdoC.Checked = rdoC.Text == selectedAnswer;
			rdoD.Checked = rdoD.Text == selectedAnswer;

			#endregion
		}

		/// <summary>
		/// 回答選択肢の有効／無効を切り替えます。
		/// </summary>
		/// <param name="enabled">有効にする場合 true</param>
		private void SetAnswerControlsEnabled(bool enabled)
		{
			#region 回答選択肢切替

			rdoA.Enabled = enabled;
			rdoB.Enabled = enabled;
			rdoC.Enabled = enabled;
			rdoD.Enabled = enabled;

			#endregion
		}

		/// <summary>
		/// 回答選択時に現在の回答を保存します。
		/// </summary>
		private void AnswerRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			#region 回答選択

			SaveSelectedAnswer();

			if (!_isSubmitted && !string.IsNullOrWhiteSpace(GetSelectedAnswer()))
			{
				lblResult.Text = "回答を保存しました。";
			}

			#endregion
		}

		/// <summary>
		/// 前の問題を表示します。
		/// </summary>
		private void btnPrevious_Click(object sender, EventArgs e)
		{
			#region 前問題表示

			SaveSelectedAnswer();

			if (_currentQuestionIndex > 0)
			{
				_currentQuestionIndex--;
				ShowCurrentQuestion();
			}

			#endregion
		}

		/// <summary>
		/// 次の問題を表示します。
		/// </summary>
		private void btnNext_Click(object sender, EventArgs e)
		{
			#region 次問題表示

			SaveSelectedAnswer();

			if (_currentQuestionIndex < _questions.Count - 1)
			{
				_currentQuestionIndex++;
				ShowCurrentQuestion();
			}

			#endregion
		}

		/// <summary>
		/// 模擬テストを採点します。
		/// </summary>
		private void btnSubmitTest_Click(object sender, EventArgs e)
		{
			#region テスト採点

			SaveSelectedAnswer();

			int unansweredCount = _questions.Count(x => !x.IsAnswered);

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

			foreach (TestQuestion question in _questions)
			{
				if (question.IsCorrect)
				{
					correctCount++;
					question.Vocabulary.CorrectCount++;
					question.Vocabulary.IsDifficult = false;
				}
				else
				{
					question.Vocabulary.WrongCount++;
					question.Vocabulary.IsDifficult = true;
				}
			}

			_isSubmitted = true;
			IsChanged = true;

			int wrongCount = _questions.Count - correctCount;
			decimal correctRate = _questions.Count == 0
				? 0
				: Math.Round((decimal)correctCount * 100 / _questions.Count, 1);

			lblResult.Text = $"Result: {correctCount} / {_questions.Count} ({correctRate}%)";

			SetAnswerControlsEnabled(false);
			btnSubmitTest.Enabled = false;

			MessageBox.Show(
				$"模擬テスト結果{Environment.NewLine}{Environment.NewLine}" +
				$"正解：{correctCount} 件{Environment.NewLine}" +
				$"不正解：{wrongCount} 件{Environment.NewLine}" +
				$"正答率：{correctRate}%{Environment.NewLine}{Environment.NewLine}" +
				CreateWrongAnswerSummary(),
				"Mock Test Result",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

			#endregion
		}

		/// <summary>
		/// 間違えた問題の概要を作成します。
		/// </summary>
		/// <returns>間違えた問題の概要</returns>
		private string CreateWrongAnswerSummary()
		{
			#region 不正解概要作成

			List<TestQuestion> wrongQuestions = _questions
				.Where(x => !x.IsCorrect)
				.Take(10)
				.ToList();

			if (wrongQuestions.Count == 0)
			{
				return "全問正解です！🌸";
			}

			var lines = new List<string>
			{
				"間違えた問題："
			};

			foreach (TestQuestion question in wrongQuestions)
			{
				string word = string.IsNullOrWhiteSpace(question.Vocabulary.Kanji)
					? question.Vocabulary.Hiragana
					: question.Vocabulary.Kanji;

				lines.Add($"・{word} → {question.CorrectAnswer}");
			}

			if (_questions.Count(x => !x.IsCorrect) > wrongQuestions.Count)
			{
				lines.Add("...");
			}

			return string.Join(Environment.NewLine, lines);

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
		/// 画面全体のかわいいデザインを設定します。
		/// </summary>
		private void ApplyCuteDesign()
		{
			#region デザイン設定

			BackColor = Color.FromArgb(255, 250, 245);
			Font = new Font("Segoe UI", 10F);
			Text = "📝 Mock Test 📝";

			lblTitle.Text = "📝 Mock Test 📝";
			lblTitle.ForeColor = Color.FromArgb(55, 65, 81);

			lblProgress.ForeColor = Color.FromArgb(55, 65, 81);
			lblQuestion.BackColor = Color.White;
			lblQuestion.ForeColor = Color.FromArgb(55, 65, 81);
			lblResult.ForeColor = Color.FromArgb(219, 39, 119);

			rdoA.ForeColor = Color.FromArgb(55, 65, 81);
			rdoB.ForeColor = Color.FromArgb(55, 65, 81);
			rdoC.ForeColor = Color.FromArgb(55, 65, 81);
			rdoD.ForeColor = Color.FromArgb(55, 65, 81);

			StyleButton(btnPrevious, Color.FromArgb(226, 232, 240));
			StyleButton(btnNext, Color.FromArgb(186, 230, 253));
			StyleButton(btnSubmitTest, Color.FromArgb(187, 247, 208));
			StyleButton(btnClose, Color.FromArgb(254, 202, 202));

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
