using NihongoVocabTrainer.Models;
using NihongoVocabTrainer.Services;
using System.Drawing.Drawing2D;

namespace NihongoVocabTrainer.Forms
{
	public partial class MainForm : Form
	{
		private readonly VocabularyService _vocabularyService = new VocabularyService();

		private List<Vocabulary> _vocabularies = new List<Vocabulary>();

		private string _dataDirectoryPath = string.Empty;

		private readonly JTestHtmlParserService _jTestHtmlParserService = new JTestHtmlParserService();
		private readonly ExamQuestionService _examQuestionService = new ExamQuestionService();
		private string _testDataDirectoryPath = string.Empty;


		/// <summary>
		/// メイン画面を初期化します。
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 画面読み込み時にDataフォルダ内の単語データを読み込み、一覧に表示します。
		/// </summary>
		private void MainForm_Load(object sender, EventArgs e)
		{
			#region 初期表示

			ApplyCuteDesign();
			EnableDoubleBuffering();

			cboLevel.SelectedIndex = 0;

			_dataDirectoryPath = GetProjectDataDirectoryPath();

			_vocabularies = _vocabularyService.LoadAllFromDataDirectory(_dataDirectoryPath);

			DisplayVocabulary(_vocabularies);

			_testDataDirectoryPath = GetProjectTestDataDirectoryPath();
			_examQuestionService.CreateSampleQuestionCsv(_testDataDirectoryPath);

			#endregion
		}

		/// <summary>
		/// DataGridView のちらつきとスクロールの重さを軽減します。
		/// </summary>
		private void EnableDoubleBuffering()
		{
			#region ダブルバッファ設定

			typeof(DataGridView)
				.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
				?.SetValue(dgvVocabulary, true, null);

			#endregion
		}

		/// <summary>
		/// Dataフォルダパスを取得します。
		/// DEBUG時はプロジェクト直下のDataフォルダ、
		/// RELEASE時は実行ファイル直下のDataフォルダを使用します。
		/// </summary>
		/// <returns>Dataフォルダパス</returns>
		private string GetProjectDataDirectoryPath()
		{
			#region Dataフォルダ取得

			#if DEBUG
			DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);

			while (directory != null)
			{
				bool hasProjectFile = directory
					.GetFiles("*.csproj")
					.Any();

				if (hasProjectFile)
				{
					string projectDataPath = Path.Combine(directory.FullName, "Data");

					if (!Directory.Exists(projectDataPath))
					{
						Directory.CreateDirectory(projectDataPath);
					}

					return projectDataPath;
				}

				directory = directory.Parent;
			}
			#endif

			string exeDataPath = Path.Combine(AppContext.BaseDirectory, "Data");

			if (!Directory.Exists(exeDataPath))
			{
				Directory.CreateDirectory(exeDataPath);
			}

			return exeDataPath;

			#endregion
		}

		/// <summary>
		/// 単語一覧を画面に表示します。
		/// </summary>
		/// <param name="vocabularies">表示対象の単語一覧</param>
		private void DisplayVocabulary(List<Vocabulary> vocabularies)
		{
			#region 一覧表示

			dgvVocabulary.SuspendLayout();

			try
			{
				dgvVocabulary.DataSource = null;
				dgvVocabulary.AutoGenerateColumns = true;
				dgvVocabulary.DataSource = vocabularies;

				ApplyDataGridViewColumnSettings();
			}
			finally
			{
				dgvVocabulary.ResumeLayout();
			}

			#endregion
		}

		/// <summary>
		/// 入力されたキーワードおよびJLPTレベルで単語を検索します。
		/// </summary>
		private void btnSearch_Click(object? sender, EventArgs e)
		{
			#region 単語検索

			string keyword = txtSearch.Text.Trim();
			string level = cboLevel.SelectedItem?.ToString() ?? "All";

			var result = _vocabularies
				.Where(x =>
					string.IsNullOrWhiteSpace(keyword) ||
					x.Kanji.Contains(keyword) ||
					x.Hiragana.Contains(keyword) ||
					x.Meaning.Contains(keyword) ||
					x.Level.Contains(keyword) ||
					x.Example.Contains(keyword))
				.Where(x => level == "All" || x.Level == level)
				.ToList();

			DisplayVocabulary(result);

			#endregion
		}

		/// <summary>
		/// 検索条件を解除し、全単語を表示します。
		/// </summary>
		private void btnReset_Click(object? sender, EventArgs e)
		{
			#region 検索解除

			txtSearch.Text = string.Empty;
			cboLevel.SelectedIndex = 0;

			DisplayVocabulary(_vocabularies);

			#endregion
		}

		/// <summary>
		/// 現在の単語一覧をレベル別CSVファイルに保存します。
		/// </summary>
		private void SaveVocabulary()
		{
			#region CSV保存

			_vocabularyService.SaveAllByLevel(_dataDirectoryPath, _vocabularies);

			#endregion
		}

		/// <summary>
		/// 単語追加画面を開きます。
		/// </summary>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			#region 単語追加

			using var form = new AddWordForm();

			if (form.ShowDialog() == DialogResult.OK)
			{
				Vocabulary newVocabulary = form.Vocabulary;

				bool exists = _vocabularies.Any(x =>
					NormalizeText(x.Kanji) == NormalizeText(newVocabulary.Kanji) &&
					NormalizeText(x.Hiragana) == NormalizeText(newVocabulary.Hiragana) &&
					NormalizeText(x.Level) == NormalizeText(newVocabulary.Level));

				if (exists)
				{
					MessageBox.Show("既に存在する単語です。");
					return;
				}

				_vocabularies.Add(newVocabulary);

				_vocabularyService.AppendToLevelCsv(_dataDirectoryPath, newVocabulary);

				DisplayVocabulary(_vocabularies);
			}

			#endregion
		}

		/// <summary>
		/// 選択中の単語を編集します。
		/// </summary>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			#region 単語編集

			if (dgvVocabulary.CurrentRow?.DataBoundItem is not Vocabulary selectedVocabulary)
			{
				MessageBox.Show("編集する単語を選択してください。");
				return;
			}

			using var form = new AddWordForm(selectedVocabulary);

			if (form.ShowDialog() == DialogResult.OK)
			{
				SaveVocabulary();
				DisplayVocabulary(_vocabularies);
			}

			#endregion
		}

		/// <summary>
		/// 選択中の単語を削除します。
		/// </summary>
		private void btnDelete_Click(object sender, EventArgs e)
		{
			#region 単語削除

			if (dgvVocabulary.CurrentRow?.DataBoundItem is not Vocabulary selectedVocabulary)
			{
				MessageBox.Show("削除する単語を選択してください。");
				return;
			}

			DialogResult result = MessageBox.Show(
				"選択した単語を削除しますか？",
				"削除確認",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				_vocabularies.Remove(selectedVocabulary);
				SaveVocabulary();
				DisplayVocabulary(_vocabularies);
			}

			#endregion
		}

		/// <summary>
		/// フラッシュカード画面を開きます。
		/// 選択中のJLPTレベルに応じて学習対象を絞り込みます。
		/// </summary>
		private void btnFlashcard_Click(object sender, EventArgs e)
		{
			#region フラッシュカード表示

			string selectedLevel = cboLevel.SelectedItem?.ToString() ?? "All";

			List<Vocabulary> flashcardVocabularies = selectedLevel == "All"
				? _vocabularies
				: _vocabularies
					.Where(x => NormalizeText(x.Level) == NormalizeText(selectedLevel))
					.ToList();

			if (flashcardVocabularies.Count == 0)
			{
				MessageBox.Show("選択したレベルの単語がありません。");
				return;
			}

			using var form = new FlashcardForm(flashcardVocabularies);
			form.ShowDialog();

			if (form.IsChanged)
			{
				SaveVocabulary();
				DisplayVocabulary(_vocabularies);
			}

			#endregion
		}

		/// <summary>
		/// クイズ画面を開きます。
		/// 選択中のJLPTレベルに応じて出題対象を絞り込みます。
		/// </summary>
		private void btnQuiz_Click(object sender, EventArgs e)
		{
			#region クイズ表示

			string selectedLevel = cboLevel.SelectedItem?.ToString() ?? "All";

			List<Vocabulary> quizVocabularies = selectedLevel == "All"
				? _vocabularies
				: _vocabularies
					.Where(x => NormalizeText(x.Level) == NormalizeText(selectedLevel))
					.ToList();

			if (quizVocabularies.Count < 4)
			{
				MessageBox.Show("クイズを開始するには、選択したレベルの単語が4件以上必要です。");
				return;
			}

			using var form = new QuizForm(quizVocabularies);
			form.ShowDialog();

			if (form.IsChanged)
			{
				SaveVocabulary();
				DisplayVocabulary(_vocabularies);
			}

			#endregion
		}

		/// <summary>
		/// CSVファイルから単語データを一括インポートします。
		/// 既に存在する単語は追加せず、重複としてスキップします。
		/// </summary>
		private void btnImport_Click(object sender, EventArgs e)
		{
			#region CSVインポート

			using var openFileDialog = new OpenFileDialog
			{
				Title = "インポートするCSVファイルを選択してください。",
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
			};

			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			List<Vocabulary> importedVocabularies = _vocabularyService.LoadFromCsv(openFileDialog.FileName);

			if (importedVocabularies.Count == 0)
			{
				MessageBox.Show("インポートできる単語がありません。");
				return;
			}

			int addedCount = 0;
			int skippedCount = 0;

			foreach (Vocabulary importedVocabulary in importedVocabularies)
			{
				bool exists = _vocabularies.Any(x =>
					NormalizeText(x.Kanji) == NormalizeText(importedVocabulary.Kanji) &&
					NormalizeText(x.Hiragana) == NormalizeText(importedVocabulary.Hiragana) &&
					NormalizeText(x.Level) == NormalizeText(importedVocabulary.Level));

				if (exists)
				{
					skippedCount++;
					continue;
				}

				_vocabularies.Add(importedVocabulary);

				_vocabularyService.AppendToLevelCsv(_dataDirectoryPath, importedVocabulary);

				addedCount++;
			}

			DisplayVocabulary(_vocabularies);

			MessageBox.Show(
				$"{addedCount}件の単語をインポートしました。{Environment.NewLine}" +
				$"{skippedCount}件の重複単語をスキップしました。");

			#endregion
		}

		/// <summary>
		/// JTest のHTMLファイル、またはフォルダ内のHTMLファイルを一括インポートします。
		/// インポート後、選択元のファイルまたはフォルダを削除するか確認します。
		/// </summary>
		private void btnImportJTestHtml_Click(object sender, EventArgs e)
		{
			#region JTest HTMLインポート方式選択

			DialogResult importTypeResult = MessageBox.Show(
				"インポート方法を選択してください。" + Environment.NewLine +
				Environment.NewLine +
				"はい：HTMLファイルを選択" + Environment.NewLine +
				"いいえ：HTMLフォルダを選択" + Environment.NewLine +
				"キャンセル：中止",
				"JTest HTMLインポート",
				MessageBoxButtons.YesNoCancel,
				MessageBoxIcon.Question);

			if (importTypeResult == DialogResult.Cancel)
			{
				return;
			}

			string[] htmlFiles;
			string selectedFolderPath = string.Empty;
			bool isFolderImport = false;

			if (importTypeResult == DialogResult.Yes)
			{
				htmlFiles = SelectHtmlFiles();
			}
			else
			{
				htmlFiles = SelectHtmlFilesFromFolder(out selectedFolderPath);
				isFolderImport = true;
			}

			if (htmlFiles.Length == 0)
			{
				return;
			}

			bool bError = false;

			ImportJTestHtmlFiles(htmlFiles, ref bError);

			if (bError)
			{
				return;
			}

			ConfirmAndDeleteImportSource(htmlFiles, selectedFolderPath, isFolderImport);

			#endregion
		}

		/// <summary>
		/// ファイル名からJLPTレベルを取得します。
		/// N1～N5 のみ対象とします。
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>取得できたJLPTレベル。取得できない場合は空文字。</returns>
		private string GetLevelFromFileName(string filePath)
		{
			#region ファイル名レベル取得

			string fileName = Path.GetFileNameWithoutExtension(filePath).ToUpper();

			for (int level = 1; level <= 5; level++)
			{
				string levelText = $"N{level}";

				if (fileName.Contains(levelText))
				{
					return levelText;
				}
			}

			return string.Empty;

			#endregion
		}

		/// <summary>
		/// HTMLファイルのインポートレベルを決定します。
		/// ファイル名にN1～N5が含まれる場合はそれを優先し、
		/// 含まれない場合は画面で選択されたレベルを使用します。
		/// </summary>
		/// <param name="htmlFilePath">HTMLファイルパス</param>
		/// <param name="bError">エラーが発生した場合は true</param>
		/// <returns>JLPTレベル</returns>
		private string ResolveHtmlImportLevel(string htmlFilePath, ref bool bError)
		{
			#region HTMLインポートレベル決定

			string levelFromFileName = GetLevelFromFileName(htmlFilePath);

			if (!string.IsNullOrWhiteSpace(levelFromFileName))
			{
				return levelFromFileName;
			}

			string selectedLevel = GetSelectedImportLevel();

			if (string.IsNullOrWhiteSpace(selectedLevel))
			{
				bError = true;
				return string.Empty;
			}

			return selectedLevel;

			#endregion
		}

		/// <summary>
		/// インポート対象のHTMLファイルを複数選択します。
		/// </summary>
		/// <returns>HTMLファイルパス配列</returns>
		private string[] SelectHtmlFiles()
		{
			#region HTMLファイル選択

			using var openFileDialog = new OpenFileDialog
			{
				Title = "JTest のHTMLファイルを選択してください。",
				Filter = "HTML files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*",
				Multiselect = true
			};

			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return Array.Empty<string>();
			}

			return openFileDialog.FileNames;

			#endregion
		}

		/// <summary>
		/// フォルダを選択し、フォルダ内のHTMLファイルを取得します。
		/// </summary>
		/// <param name="selectedFolderPath">選択されたフォルダパス</param>
		/// <returns>HTMLファイルパス配列</returns>
		private string[] SelectHtmlFilesFromFolder(out string selectedFolderPath)
		{
			#region HTMLフォルダ選択

			selectedFolderPath = string.Empty;

			using var folderBrowserDialog = new FolderBrowserDialog
			{
				Description = "JTest HTMLファイルが保存されているフォルダを選択してください。",
				UseDescriptionForTitle = true
			};

			if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
			{
				return Array.Empty<string>();
			}

			selectedFolderPath = folderBrowserDialog.SelectedPath;

			string[] htmlFiles = Directory
				.GetFiles(selectedFolderPath, "*.*", SearchOption.TopDirectoryOnly)
				.Where(x =>
					string.Equals(Path.GetExtension(x), ".html", StringComparison.OrdinalIgnoreCase) ||
					string.Equals(Path.GetExtension(x), ".htm", StringComparison.OrdinalIgnoreCase))
				.ToArray();

			if (htmlFiles.Length == 0)
			{
				MessageBox.Show("選択したフォルダにHTMLファイルがありません。");
				return Array.Empty<string>();
			}

			return htmlFiles;

			#endregion
		}

		/// <summary>
		/// インポート元のHTMLファイルまたはフォルダを削除するか確認し、必要に応じて削除します。
		/// </summary>
		/// <param name="htmlFiles">インポート対象HTMLファイル一覧</param>
		/// <param name="selectedFolderPath">選択されたフォルダパス</param>
		/// <param name="isFolderImport">フォルダインポートかどうか</param>
		private void ConfirmAndDeleteImportSource(
			string[] htmlFiles,
			string selectedFolderPath,
			bool isFolderImport)
		{
			#region インポート元削除確認

			DialogResult deleteResult = MessageBox.Show(
				"インポート元のHTMLデータを削除しますか？" + Environment.NewLine +
				Environment.NewLine +
				"はい：削除する" + Environment.NewLine +
				"いいえ：削除しない",
				"インポート元削除確認",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (deleteResult != DialogResult.Yes)
			{
				return;
			}

			try
			{
				if (isFolderImport)
				{
					DeleteImportedFolder(selectedFolderPath);
				}
				else
				{
					DeleteImportedFiles(htmlFiles);
				}

				MessageBox.Show("インポート元のHTMLデータを削除しました。");
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"インポート元の削除中にエラーが発生しました。" + Environment.NewLine +
					ex.Message,
					"削除エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

			#endregion
		}

		/// <summary>
		/// インポート済みHTMLファイルを削除します。
		/// </summary>
		/// <param name="htmlFiles">HTMLファイルパス配列</param>
		private void DeleteImportedFiles(string[] htmlFiles)
		{
			#region HTMLファイル削除

			foreach (string htmlFile in htmlFiles)
			{
				if (!File.Exists(htmlFile))
				{
					continue;
				}

				File.Delete(htmlFile);
			}

			#endregion
		}

		/// <summary>
		/// インポート元フォルダの中身をすべて削除します。
		/// 選択されたフォルダ自体は削除しません。
		/// </summary>
		/// <param name="selectedFolderPath">対象フォルダパス</param>
		private void DeleteImportedFolder(string selectedFolderPath)
		{
			#region フォルダ内全削除

			if (string.IsNullOrWhiteSpace(selectedFolderPath) ||
				!Directory.Exists(selectedFolderPath))
			{
				return;
			}

			// フォルダ直下のファイルを削除します。
			string[] files = Directory.GetFiles(
				selectedFolderPath,
				"*.*",
				SearchOption.TopDirectoryOnly);

			foreach (string file in files)
			{
				if (!File.Exists(file))
				{
					continue;
				}

				File.Delete(file);
			}

			// フォルダ直下のサブフォルダを削除します。
			string[] directories = Directory.GetDirectories(
				selectedFolderPath,
				"*",
				SearchOption.TopDirectoryOnly);

			foreach (string directory in directories)
			{
				if (!Directory.Exists(directory))
				{
					continue;
				}

				Directory.Delete(directory, recursive: true);
			}

			#endregion
		}

		/// <summary>
		/// 指定されたJTest HTMLファイル一覧から単語データを一括インポートします。
		/// ファイル名に N1～N5 が含まれる場合は、そのレベルを優先して設定します。
		/// </summary>
		/// <param name="htmlFiles">HTMLファイルパス配列</param>
		/// <param name="bError">エラーが発生した場合は true</param>
		private void ImportJTestHtmlFiles(string[] htmlFiles, ref bool bError)
		{
			#region JTest HTML共通インポート

			bError = false;

			int addedCount = 0;
			int skippedCount = 0;
			int parsedFileCount = 0;

			try
			{
				foreach (string htmlFilePath in htmlFiles)
				{
					string level = ResolveHtmlImportLevel(htmlFilePath, ref bError);

					if (bError)
					{
						return;
					}

					List<Vocabulary> importedVocabularies =
						_jTestHtmlParserService.ParseFromHtmlFile(htmlFilePath, level);

					if (importedVocabularies.Count == 0)
					{
						continue;
					}

					parsedFileCount++;

					foreach (Vocabulary importedVocabulary in importedVocabularies)
					{
						bool exists = _vocabularies.Any(x =>
							NormalizeText(x.Kanji) == NormalizeText(importedVocabulary.Kanji) &&
							NormalizeText(x.Hiragana) == NormalizeText(importedVocabulary.Hiragana) &&
							NormalizeText(x.Level) == NormalizeText(importedVocabulary.Level));

						if (exists)
						{
							skippedCount++;
							continue;
						}

						_vocabularies.Add(importedVocabulary);
						addedCount++;
					}
				}

				if (addedCount > 0)
				{
					SaveVocabulary();
				}

				DisplayVocabulary(_vocabularies);

				MessageBox.Show(
					$"{htmlFiles.Length}件のHTMLファイルを選択しました。{Environment.NewLine}" +
					$"{parsedFileCount}件のHTMLファイルから単語を取得しました。{Environment.NewLine}" +
					$"{addedCount}件の単語をインポートしました。{Environment.NewLine}" +
					$"{skippedCount}件の重複単語をスキップしました。");
			}
			catch (Exception ex)
			{
				bError = true;

				MessageBox.Show(
					"HTMLインポート中にエラーが発生しました。" + Environment.NewLine +
					ex.Message,
					"インポートエラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

			#endregion
		}

		/// <summary>
		/// HTMLインポート用のJLPTレベルを取得します。
		/// All の場合はインポートできないようにします。
		/// </summary>
		/// <returns>JLPTレベル</returns>
		private string GetSelectedImportLevel()
		{
			#region インポートレベル取得

			string level = cboLevel.SelectedItem?.ToString()?.Trim() ?? string.Empty;

			if (string.IsNullOrWhiteSpace(level) || level == "All")
			{
				MessageBox.Show(
					"HTMLをインポートする前に、JLPTレベルを選択してください。" + Environment.NewLine +
					"例：N4",
					"レベル選択",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				return string.Empty;
			}

			return level;

			#endregion
		}

		/// <summary>
		/// 比較用に文字列を正規化します。
		/// </summary>
		/// <param name="value">文字列</param>
		/// <returns>正規化後の文字列</returns>
		private string NormalizeText(string value)
		{
			#region 文字列正規化

			return value
				.Replace("\r", string.Empty)
				.Replace("\n", string.Empty)
				.Trim();

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

			Text = "🌸 Nihongo Vocab Trainer 🌸";

			btnSearch.Text = "Search 🔍";
			btnReset.Text = "Reset ♻️";
			btnAdd.Text = "Add 🌱";
			btnEdit.Text = "Edit ✏️";
			btnDelete.Text = "Delete 🗑️";
			btnImport.Text = "Import CSV 📄";
			btnImportJTestHtml.Text = "Import HTML 🌐";
			btnFlashcard.Text = "Flashcard 🌸";
			btnQuiz.Text = "Quiz ✨";
			btnMockTest.Text = "Mock Test 📝";
			btnImportTestPdf.Text = "Import PDF 📚";
			btnImportTestCsv.Text = "Import Test CSV 🧾";
			btnExamMockTest.Text = "Exam Test 📝";

			Color primary = Color.FromArgb(186, 230, 253);
			Color resetColor = Color.FromArgb(226, 232, 240);
			Color study = Color.FromArgb(221, 214, 254);
			Color add = Color.FromArgb(187, 247, 208);
			Color editColor = Color.FromArgb(253, 224, 71);
			Color danger = Color.FromArgb(254, 202, 202);
			Color importColor = Color.FromArgb(216, 180, 254);

			StyleButton(btnSearch, primary);
			StyleButton(btnReset, resetColor);

			StyleButton(btnAdd, add);
			StyleButton(btnEdit, editColor);
			StyleButton(btnDelete, danger);

			StyleButton(btnFlashcard, study);
			StyleButton(btnQuiz, study);

			StyleButton(btnImport, importColor);
			StyleButton(btnImportJTestHtml, importColor);
			StyleButton(btnMockTest, Color.FromArgb(253, 224, 71));

			StyleButton(btnImportTestPdf, Color.FromArgb(216, 180, 254));
			StyleButton(btnImportTestCsv, Color.FromArgb(216, 180, 254));
			StyleButton(btnExamMockTest, Color.FromArgb(221, 214, 254));

			StyleSearchBox();
			StyleComboBox();
			StyleDataGridView();

			#endregion
		}

		/// <summary>
		/// JLPTレベル選択コンボボックスのデザインを設定します。
		/// </summary>
		private void StyleComboBox()
		{
			#region コンボボックスデザイン

			cboLevel.BackColor = Color.FromArgb(255, 255, 255);
			cboLevel.ForeColor = Color.FromArgb(31, 41, 55);
			cboLevel.FlatStyle = FlatStyle.Popup;
			cboLevel.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
			cboLevel.DropDownStyle = ComboBoxStyle.DropDownList;

			#endregion
		}

		/// <summary>
		/// 検索テキストボックスのデザインを設定します。
		/// </summary>
		private void StyleSearchBox()
		{
			#region 検索ボックスデザイン

			txtSearch.BackColor = Color.FromArgb(255, 255, 255);
			txtSearch.ForeColor = Color.FromArgb(31, 41, 55);
			txtSearch.BorderStyle = BorderStyle.FixedSingle;
			txtSearch.Font = new Font("Segoe UI", 10.5F);

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

			SetRoundedRegion(button, 14);

			#endregion
		}

		/// <summary>
		/// 一覧のデザインを設定します。
		/// 大量データでもスクロールが重くならないように、自動行高調整は使用しません。
		/// </summary>
		private void StyleDataGridView()
		{
			#region 一覧デザイン

			dgvVocabulary.BackgroundColor = Color.White;
			dgvVocabulary.BorderStyle = BorderStyle.FixedSingle;
			dgvVocabulary.GridColor = Color.FromArgb(229, 231, 235);

			dgvVocabulary.EnableHeadersVisualStyles = false;

			dgvVocabulary.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 237, 213);
			dgvVocabulary.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
			dgvVocabulary.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold);
			dgvVocabulary.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 237, 213);
			dgvVocabulary.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);

			dgvVocabulary.DefaultCellStyle.BackColor = Color.White;
			dgvVocabulary.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
			dgvVocabulary.DefaultCellStyle.Font = new Font("Yu Gothic UI", 10F);
			dgvVocabulary.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 215, 170);
			dgvVocabulary.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);

			// 大量データ対策：折り返し・自動行高調整は重いため無効化します。
			dgvVocabulary.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
			dgvVocabulary.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dgvVocabulary.RowTemplate.Height = 32;

			dgvVocabulary.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 247, 237);

			dgvVocabulary.RowHeadersVisible = false;
			dgvVocabulary.AllowUserToAddRows = false;
			dgvVocabulary.AllowUserToResizeRows = false;
			dgvVocabulary.AllowUserToResizeColumns = true;

			dgvVocabulary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvVocabulary.MultiSelect = false;
			dgvVocabulary.ReadOnly = true;

			#endregion
		}

		/// <summary>
		/// コントロールの角丸を設定します。
		/// </summary>
		/// <param name="control">対象コントロール</param>
		/// <param name="radius">角丸半径</param>
		private void SetRoundedRegion(Control control, int radius)
		{
			#region 角丸設定

			if (control.Width <= 0 || control.Height <= 0)
			{
				return;
			}

			var path = new GraphicsPath();

			path.AddArc(0, 0, radius, radius, 180, 90);
			path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
			path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
			path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
			path.CloseFigure();

			control.Region = new Region(path);

			#endregion
		}

		/// <summary>
		/// 一覧の列名と表示設定を調整します。
		/// </summary>
		private void ApplyDataGridViewColumnSettings()
		{
			#region 一覧列設定

			dgvVocabulary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

			if (dgvVocabulary.Columns["Kanji"] != null)
			{
				dgvVocabulary.Columns["Kanji"].HeaderText = "単語";
				dgvVocabulary.Columns["Kanji"].Width = 150;
			}

			if (dgvVocabulary.Columns["Hiragana"] != null)
			{
				dgvVocabulary.Columns["Hiragana"].HeaderText = "読み方";
				dgvVocabulary.Columns["Hiragana"].Width = 150;
			}

			if (dgvVocabulary.Columns["Meaning"] != null)
			{
				dgvVocabulary.Columns["Meaning"].HeaderText = "意味";
				dgvVocabulary.Columns["Meaning"].Width = 260;
			}

			if (dgvVocabulary.Columns["Level"] != null)
			{
				dgvVocabulary.Columns["Level"].HeaderText = "レベル";
				dgvVocabulary.Columns["Level"].Width = 70;
			}

			if (dgvVocabulary.Columns["Example"] != null)
			{
				dgvVocabulary.Columns["Example"].HeaderText = "例文";
				dgvVocabulary.Columns["Example"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dgvVocabulary.Columns["Example"].MinimumWidth = 300;
			}

			if (dgvVocabulary.Columns["CorrectCount"] != null)
			{
				dgvVocabulary.Columns["CorrectCount"].Visible = false;
			}

			if (dgvVocabulary.Columns["WrongCount"] != null)
			{
				dgvVocabulary.Columns["WrongCount"].Visible = false;
			}

			if (dgvVocabulary.Columns["IsDifficult"] != null)
			{
				dgvVocabulary.Columns["IsDifficult"].Visible = false;
			}

			#endregion
		}

		/// <summary>
		/// 模擬テスト画面を開きます。
		/// 選択中のJLPTレベルに応じて出題対象を絞り込みます。
		/// </summary>
		private void btnMockTest_Click(object sender, EventArgs e)
		{
			#region 模擬テスト表示

			string selectedLevel = cboLevel.SelectedItem?.ToString() ?? "All";

			List<Vocabulary> testVocabularies = selectedLevel == "All"
				? _vocabularies
				: _vocabularies
					.Where(x => NormalizeText(x.Level) == NormalizeText(selectedLevel))
					.ToList();

			if (testVocabularies.Count < 4)
			{
				MessageBox.Show("模擬テストを開始するには、選択したレベルの単語が4件以上必要です。");
				return;
			}

			int questionCount = GetMockTestQuestionCount(testVocabularies.Count);

			if (questionCount == 0)
			{
				return;
			}

			using var form = new MockTestForm(testVocabularies, questionCount);
			form.ShowDialog();

			if (form.IsChanged)
			{
				SaveVocabulary();
				DisplayVocabulary(_vocabularies);
			}

			#endregion
		}

		/// <summary>
		/// 模擬テストの出題数を取得します。
		/// </summary>
		/// <param name="maxCount">最大出題数</param>
		/// <returns>出題数</returns>
		private int GetMockTestQuestionCount(int maxCount)
		{
			#region 出題数取得

			string input = Microsoft.VisualBasic.Interaction.InputBox(
				$"問題数を入力してください。最大 {maxCount} 問まで。",
				"Mock Test",
				Math.Min(20, maxCount).ToString());

			if (string.IsNullOrWhiteSpace(input))
			{
				return 0;
			}

			if (!int.TryParse(input, out int questionCount))
			{
				MessageBox.Show("問題数は数字で入力してください。");
				return 0;
			}

			if (questionCount < 1)
			{
				MessageBox.Show("問題数は1以上で入力してください。");
				return 0;
			}

			if (questionCount > maxCount)
			{
				questionCount = maxCount;
			}

			return questionCount;

			#endregion
		}

		/// <summary>
		/// プロジェクト直下のTestDataフォルダパスを取得します。
		/// </summary>
		/// <returns>TestDataフォルダパス</returns>
		private string GetProjectTestDataDirectoryPath()
		{
			#region TestDataフォルダ取得

			DirectoryInfo? directory = new DirectoryInfo(Application.StartupPath);

			while (directory != null)
			{
				bool hasProjectFile = directory
					.GetFiles("*.csproj")
					.Any();

				if (hasProjectFile)
				{
					string testDataDirectoryPath = Path.Combine(directory.FullName, "TestData");
					string pdfDirectoryPath = Path.Combine(testDataDirectoryPath, "Pdf");
					string csvDirectoryPath = Path.Combine(testDataDirectoryPath, "Csv");

					Directory.CreateDirectory(pdfDirectoryPath);
					Directory.CreateDirectory(csvDirectoryPath);

					return testDataDirectoryPath;
				}

				directory = directory.Parent;
			}

			string fallbackPath = Path.Combine(Application.StartupPath, "TestData");
			Directory.CreateDirectory(Path.Combine(fallbackPath, "Pdf"));
			Directory.CreateDirectory(Path.Combine(fallbackPath, "Csv"));

			return fallbackPath;

			#endregion
		}

		/// <summary>
		/// PDFの模擬試験ファイルをTestData/Pdfに取り込みます。
		/// </summary>
		private void btnImportTestPdf_Click(object sender, EventArgs e)
		{
			#region 試験PDF取込

			using var openFileDialog = new OpenFileDialog
			{
				Title = "模擬試験PDFを選択してください。",
				Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
				Multiselect = true
			};

			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			int importedCount = 0;

			foreach (string pdfPath in openFileDialog.FileNames)
			{
				_examQuestionService.ImportPdf(_testDataDirectoryPath, pdfPath);
				importedCount++;
			}

			MessageBox.Show($"{importedCount}件のPDFを取り込みました。\nPDFはTestData/Pdfに保存されます。\n\n注意：PDFは原本保管用です。試験に使う問題はTestData/CsvにCSVとして登録してください。");

			#endregion
		}

		/// <summary>
		/// 模擬試験用の問題CSVをTestData/Csvに取り込みます。
		/// </summary>
		private void btnImportTestCsv_Click(object sender, EventArgs e)
		{
			#region 試験CSV取込

			using var openFileDialog = new OpenFileDialog
			{
				Title = "模擬試験CSVを選択してください。",
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
				Multiselect = true
			};

			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			int importedCount = 0;

			foreach (string csvPath in openFileDialog.FileNames)
			{
				_examQuestionService.ImportQuestionCsv(_testDataDirectoryPath, csvPath);
				importedCount++;
			}

			MessageBox.Show($"{importedCount}件の問題CSVを取り込みました。\nCSVはTestData/Csvに保存されます。");

			#endregion
		}

		/// <summary>
		/// TestData/Csv の問題データから模擬試験を開始します。
		/// </summary>
		private void btnExamMockTest_Click(object sender, EventArgs e)
		{
			#region 試験モックテスト開始

			string selectedLevel = cboLevel.SelectedItem?.ToString() ?? "All";

			List<ExamQuestion> allQuestions = _examQuestionService.LoadAllQuestions(_testDataDirectoryPath);

			List<ExamQuestion> targetQuestions = selectedLevel == "All"
				? allQuestions
				: allQuestions
					.Where(x => NormalizeText(x.Level) == NormalizeText(selectedLevel))
					.ToList();

			if (targetQuestions.Count == 0)
			{
				MessageBox.Show("選択したレベルの試験問題がありません。TestData/Csvに問題CSVを追加してください。");
				return;
			}

			using var form = new ExamMockTestForm(targetQuestions);
			form.ShowDialog();

			#endregion
		}
	}
}
