using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Forms
{
	public partial class FlashcardForm : Form
	{
		private readonly List<Vocabulary> _vocabularies;

		private readonly Random _random = new Random();

		private Vocabulary? _currentVocabulary;

		private int _studiedCount;

		/// <summary>
		/// 学習データが変更されたかどうかを取得します。
		/// </summary>
		public bool IsChanged { get; private set; }

		/// <summary>
		/// フラッシュカード画面を初期化します。
		/// </summary>
		/// <param name="vocabularies">単語一覧</param>
		public FlashcardForm(List<Vocabulary> vocabularies)
		{
			InitializeComponent();

			_vocabularies = vocabularies;
		}

		/// <summary>
		/// 画面読み込み時に最初の単語を表示します。
		/// </summary>
		private void FlashcardForm_Load(object sender, EventArgs e)
		{
			#region 初期表示

			ApplyCuteDesign();

			lblAnswer.Text = string.Empty;
			ShowNextCard();

			#endregion
		}

		/// <summary>
		/// 次のカードを表示します。
		/// </summary>
		private void ShowNextCard()
		{
			#region カード表示

			List<Vocabulary> targetVocabularies = GetTargetVocabularies();

			if (targetVocabularies.Count == 0)
			{
				lblQuestion.Text = "単語データがありません。";
				lblAnswer.Text = string.Empty;
				lblProgress.Text = "0 / 0";
				return;
			}

			int index = _random.Next(targetVocabularies.Count);

			_currentVocabulary = targetVocabularies[index];

			lblQuestion.Text = string.IsNullOrWhiteSpace(_currentVocabulary.Kanji)
				? _currentVocabulary.Hiragana
				: _currentVocabulary.Kanji;

			lblAnswer.Text = string.Empty;
			lblProgress.Text = $"{_studiedCount} 問学習済み / {targetVocabularies.Count} 語";

			#endregion
		}

		/// <summary>
		/// 学習対象の単語一覧を取得します。
		/// </summary>
		/// <returns>学習対象の単語一覧</returns>
		private List<Vocabulary> GetTargetVocabularies()
		{
			#region 学習対象取得

			if (chkOnlyDifficult.Checked)
			{
				return _vocabularies
					.Where(x => x.IsDifficult)
					.ToList();
			}

			return _vocabularies;

			#endregion
		}

		/// <summary>
		/// 答えを表示します。
		/// </summary>
		private void btnShowAnswer_Click(object sender, EventArgs e)
		{
			#region 答え表示

			if (_currentVocabulary == null)
			{
				return;
			}

			lblAnswer.Text =
				$"読み方：{_currentVocabulary.Hiragana}{Environment.NewLine}" +
				$"意味：{_currentVocabulary.Meaning}{Environment.NewLine}" +
				$"レベル：{_currentVocabulary.Level}{Environment.NewLine}" +
				$"例文：{_currentVocabulary.Example}{Environment.NewLine}" +
				$"正解：{_currentVocabulary.CorrectCount} / 不正解：{_currentVocabulary.WrongCount}";

			#endregion
		}

		/// <summary>
		/// 正解として記録し、次のカードを表示します。
		/// </summary>
		private void btnKnow_Click(object sender, EventArgs e)
		{
			#region 正解記録

			if (_currentVocabulary == null)
			{
				return;
			}

			_currentVocabulary.CorrectCount++;
			_currentVocabulary.IsDifficult = false;

			_studiedCount++;
			IsChanged = true;

			ShowNextCard();

			#endregion
		}

		/// <summary>
		/// 不正解として記録し、難しい単語に設定します。
		/// </summary>
		private void btnDontKnow_Click(object sender, EventArgs e)
		{
			#region 不正解記録

			if (_currentVocabulary == null)
			{
				return;
			}

			_currentVocabulary.WrongCount++;
			_currentVocabulary.IsDifficult = true;

			_studiedCount++;
			IsChanged = true;

			ShowNextCard();

			#endregion
		}

		/// <summary>
		/// 次のカードを表示します。
		/// </summary>
		private void btnNext_Click(object sender, EventArgs e)
		{
			#region 次カード表示

			ShowNextCard();

			#endregion
		}

		/// <summary>
		/// 難しい単語のみの表示条件を切り替えます。
		/// </summary>
		private void chkOnlyDifficult_CheckedChanged(object sender, EventArgs e)
		{
			#region 表示条件変更

			ShowNextCard();

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

			lblTitle.Text = "🌸 Flashcard 🌸";
			lblTitle.ForeColor = Color.FromArgb(55, 65, 81);

			lblQuestion.BackColor = Color.White;
			lblQuestion.ForeColor = Color.FromArgb(55, 65, 81);

			lblAnswer.BackColor = Color.White;
			lblAnswer.ForeColor = Color.FromArgb(55, 65, 81);

			btnShowAnswer.Text = "Show Answer 👀";
			btnKnow.Text = "Know 🌱";
			btnDontKnow.Text = "Don't Know 💭";
			btnNext.Text = "Next ➡️";
			btnClose.Text = "Close 🐾";

			StyleButton(btnShowAnswer, Color.FromArgb(191, 219, 254));
			StyleButton(btnKnow, Color.FromArgb(187, 247, 208));
			StyleButton(btnDontKnow, Color.FromArgb(252, 165, 165));
			StyleButton(btnNext, Color.FromArgb(221, 214, 254));
			StyleButton(btnClose, Color.FromArgb(229, 231, 235));

			chkOnlyDifficult.Text = "Only difficult 💔";
			chkOnlyDifficult.ForeColor = Color.FromArgb(55, 65, 81);

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