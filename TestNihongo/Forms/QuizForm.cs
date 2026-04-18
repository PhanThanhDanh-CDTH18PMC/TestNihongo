using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Forms
{
	public partial class QuizForm : Form
	{
		private readonly List<Vocabulary> _vocabularies;

		private readonly Random _random = new Random();

		private Vocabulary? _currentVocabulary;

		private Vocabulary? _previousVocabulary;

		private string _correctAnswer = string.Empty;

		private int _correctCount;

		private int _questionCount;

		private bool _answered;

		/// <summary>
		/// 学習データが変更されたかどうかを取得します。
		/// </summary>
		public bool IsChanged { get; private set; }

		/// <summary>
		/// クイズ画面を初期化します。
		/// </summary>
		/// <param name="vocabularies">単語一覧</param>
		public QuizForm(List<Vocabulary> vocabularies)
		{
			InitializeComponent();

			_vocabularies = vocabularies;
		}

		/// <summary>
		/// 画面読み込み時に最初の問題を表示します。
		/// </summary>
		private void QuizForm_Load(object sender, EventArgs e)
		{
			#region 初期表示

			ApplyCuteDesign();

			ShowNextQuestion();

			#endregion
		}

		/// <summary>
		/// 次の問題を表示します。
		/// </summary>
		private void ShowNextQuestion()
		{
			#region 問題表示

			if (_vocabularies.Count < 4)
			{
				lblQuestion.Text = "単語が4件以上必要です。";
				lblResult.Text = string.Empty;
				SetAnswerControlsEnabled(false);
				return;
			}

			ResetAnswerState();

			_currentVocabulary = GetRandomVocabulary(_vocabularies);

			if (_currentVocabulary == null)
			{
				return;
			}

			_correctAnswer = _currentVocabulary.Meaning;

			lblQuestion.Text = string.IsNullOrWhiteSpace(_currentVocabulary.Kanji)
				? _currentVocabulary.Hiragana
				: _currentVocabulary.Kanji;

			List<string> answers = CreateAnswerOptions(_currentVocabulary);

			rdoA.Text = answers[0];
			rdoB.Text = answers[1];
			rdoC.Text = answers[2];
			rdoD.Text = answers[3];

			UpdateScore();

			#endregion
		}

		/// <summary>
		/// 回答状態を初期化します。
		/// </summary>
		private void ResetAnswerState()
		{
			#region 回答状態初期化

			rdoA.Checked = false;
			rdoB.Checked = false;
			rdoC.Checked = false;
			rdoD.Checked = false;

			lblResult.Text = string.Empty;

			_answered = false;

			btnSubmit.Enabled = true;
			btnNext.Enabled = false;

			SetAnswerControlsEnabled(true);

			#endregion
		}

		/// <summary>
		/// 前回と同じ単語を避けて、ランダムに単語を1件取得します。
		/// </summary>
		/// <param name="vocabularies">単語一覧</param>
		/// <returns>単語</returns>
		private Vocabulary? GetRandomVocabulary(List<Vocabulary> vocabularies)
		{
			#region ランダム単語取得

			if (vocabularies.Count == 0)
			{
				return null;
			}

			if (vocabularies.Count == 1)
			{
				return vocabularies[0];
			}

			List<Vocabulary> candidates = vocabularies
				.Where(x =>
					_previousVocabulary == null ||
					x.Kanji != _previousVocabulary.Kanji ||
					x.Hiragana != _previousVocabulary.Hiragana ||
					x.Meaning != _previousVocabulary.Meaning)
				.ToList();

			int index = _random.Next(candidates.Count);

			Vocabulary selectedVocabulary = candidates[index];

			_previousVocabulary = selectedVocabulary;

			return selectedVocabulary;

			#endregion
		}

		/// <summary>
		/// 4択の回答候補を作成します。
		/// </summary>
		/// <param name="correctVocabulary">正解の単語</param>
		/// <returns>回答候補</returns>
		private List<string> CreateAnswerOptions(Vocabulary correctVocabulary)
		{
			#region 回答候補作成

			var answers = new List<string>
			{
				correctVocabulary.Meaning
			};

			List<Vocabulary> wrongOptions = _vocabularies
				.Where(x => x.Meaning != correctVocabulary.Meaning)
				.OrderBy(x => _random.Next())
				.Take(3)
				.ToList();

			foreach (Vocabulary option in wrongOptions)
			{
				answers.Add(option.Meaning);
			}

			return answers
				.OrderBy(x => _random.Next())
				.ToList();

			#endregion
		}

		/// <summary>
		/// 回答をチェックします。
		/// 正解の場合は自動で次の問題を表示します。
		/// 不正解の場合は正解を表示し、Nextボタンで次の問題へ進みます。
		/// </summary>
		private void btnSubmit_Click(object sender, EventArgs e)
		{
			#region 回答チェック

			if (_currentVocabulary == null || _answered)
			{
				return;
			}

			string selectedAnswer = GetSelectedAnswer();

			if (string.IsNullOrWhiteSpace(selectedAnswer))
			{
				MessageBox.Show("回答を選択してください。");
				return;
			}

			_questionCount++;
			_answered = true;

			if (selectedAnswer == _correctAnswer)
			{
				_correctCount++;

				_currentVocabulary.CorrectCount++;
				_currentVocabulary.IsDifficult = false;

				IsChanged = true;

				UpdateScore();

				ShowNextQuestion();
				return;
			}

			_currentVocabulary.WrongCount++;
			_currentVocabulary.IsDifficult = true;

			lblResult.Text = $"不正解です。正解：{_correctAnswer}";

			IsChanged = true;

			btnSubmit.Enabled = false;
			btnNext.Enabled = true;
			SetAnswerControlsEnabled(false);

			UpdateScore();

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
		/// スコア表示を更新します。
		/// </summary>
		private void UpdateScore()
		{
			#region スコア更新

			lblScore.Text = $"Score: {_correctCount} / {_questionCount}";

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

			#endregion
		}

		/// <summary>
		/// 次の問題を表示します。
		/// </summary>
		private void btnNext_Click(object sender, EventArgs e)
		{
			#region 次問題表示

			ShowNextQuestion();

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

			BackColor = Color.FromArgb(255, 247, 250);
			Font = new Font("Segoe UI", 10F);

			lblTitle.Text = "✨ Quiz Time ✨";
			lblTitle.ForeColor = Color.FromArgb(55, 65, 81);

			lblQuestion.BackColor = Color.White;
			lblQuestion.ForeColor = Color.FromArgb(55, 65, 81);

			lblScore.ForeColor = Color.FromArgb(55, 65, 81);
			lblResult.ForeColor = Color.FromArgb(219, 39, 119);

			rdoA.ForeColor = Color.FromArgb(55, 65, 81);
			rdoB.ForeColor = Color.FromArgb(55, 65, 81);
			rdoC.ForeColor = Color.FromArgb(55, 65, 81);
			rdoD.ForeColor = Color.FromArgb(55, 65, 81);

			btnSubmit.Text = "Submit 🌱";
			btnNext.Text = "Next ➡️";
			btnClose.Text = "Close 🐾";

			StyleButton(btnSubmit, Color.FromArgb(187, 247, 208));
			StyleButton(btnNext, Color.FromArgb(191, 219, 254));
			StyleButton(btnClose, Color.FromArgb(229, 231, 235));

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
			button.ForeColor = Color.FromArgb(55, 65, 81);
			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderSize = 0;
			button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			button.Cursor = Cursors.Hand;

			#endregion
		}
	}
}