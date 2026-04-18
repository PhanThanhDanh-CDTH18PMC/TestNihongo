using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Forms
{
	/// <summary>
	/// 単語の追加・編集画面です。
	/// </summary>
	public partial class AddWordForm : Form
	{
		/// <summary>
		/// 入力された単語情報を取得します。
		/// </summary>
		public Vocabulary Vocabulary { get; private set; } = new Vocabulary();

		/// <summary>
		/// 単語追加画面を初期化します。
		/// </summary>
		public AddWordForm()
		{
			InitializeComponent();
			cboLevel.SelectedIndex = 0;
		}

		/// <summary>
		/// 単語編集画面を初期化します。
		/// </summary>
		/// <param name="vocabulary">編集対象の単語</param>
		public AddWordForm(Vocabulary vocabulary) : this()
		{
			#region 編集データ表示

			Vocabulary = vocabulary;

			txtKanji.Text = vocabulary.Kanji;
			txtHiragana.Text = vocabulary.Hiragana;
			txtMeaning.Text = vocabulary.Meaning;
			cboLevel.Text = vocabulary.Level;
			txtExample.Text = vocabulary.Example;

			this.Text = "Edit Word";

			#endregion
		}

		/// <summary>
		/// 入力内容を検証し、単語情報を保存します。
		/// </summary>
		private void btnSave_Click(object sender, EventArgs e)
		{
			#region 入力チェック

			if (string.IsNullOrWhiteSpace(txtKanji.Text))
			{
				MessageBox.Show("Kanji を入力してください。", "入力チェック", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtKanji.Focus();
				return;
			}

			if (string.IsNullOrWhiteSpace(txtHiragana.Text))
			{
				MessageBox.Show("Hiragana を入力してください。", "入力チェック", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtHiragana.Focus();
				return;
			}

			if (string.IsNullOrWhiteSpace(txtMeaning.Text))
			{
				MessageBox.Show("Meaning を入力してください。", "入力チェック", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMeaning.Focus();
				return;
			}

			#endregion

			#region 単語情報設定

			Vocabulary.Kanji = txtKanji.Text.Trim();
			Vocabulary.Hiragana = txtHiragana.Text.Trim();
			Vocabulary.Meaning = txtMeaning.Text.Trim();
			Vocabulary.Level = cboLevel.Text.Trim();
			Vocabulary.Example = txtExample.Text.Trim();

			this.DialogResult = DialogResult.OK;
			this.Close();

			#endregion
		}

		/// <summary>
		/// 画面を閉じます。
		/// </summary>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			#region キャンセル

			this.DialogResult = DialogResult.Cancel;
			this.Close();

			#endregion
		}
	}
}
