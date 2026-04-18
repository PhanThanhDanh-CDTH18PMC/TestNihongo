using NihongoVocabTrainer.Models;
using NihongoVocabTrainer.Services;

namespace NihongoVocabTrainer.Forms
{
	public partial class ImportWordForm : Form
	{
		private readonly VocabularyService _vocabularyService = new VocabularyService();

		public List<Vocabulary> ImportedVocabularies { get; private set; } = new List<Vocabulary>();

		/// <summary>
		/// 単語インポート画面を初期化します。
		/// </summary>
		public ImportWordForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// CSVファイルを選択します。
		/// </summary>
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			#region CSVファイル選択

			using var dialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
				Title = "単語CSVファイルを選択してください"
			};

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			txtFilePath.Text = dialog.FileName;
			PreviewCsv(dialog.FileName);

			#endregion
		}

		/// <summary>
		/// CSVファイルの内容をプレビュー表示します。
		/// </summary>
		/// <param name="filePath">CSVファイルパス</param>
		private void PreviewCsv(string filePath)
		{
			#region CSVプレビュー

			ImportedVocabularies = _vocabularyService.LoadFromCsv(filePath);

			dgvPreview.DataSource = null;
			dgvPreview.AutoGenerateColumns = true;
			dgvPreview.DataSource = ImportedVocabularies;

			lblCount.Text = $"読み込み件数: {ImportedVocabularies.Count}";

			#endregion
		}

		/// <summary>
		/// インポートを確定します。
		/// </summary>
		private void btnImport_Click(object sender, EventArgs e)
		{
			#region インポート確定

			if (ImportedVocabularies.Count == 0)
			{
				MessageBox.Show("インポートする単語がありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult = DialogResult.OK;
			Close();

			#endregion
		}

		/// <summary>
		/// 画面を閉じます。
		/// </summary>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			#region キャンセル

			DialogResult = DialogResult.Cancel;
			Close();

			#endregion
		}
	}
}
