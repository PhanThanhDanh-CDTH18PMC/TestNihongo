namespace NihongoVocabTrainer.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private Label lblSearch;
		private TextBox txtSearch;
		private ComboBox cboLevel;
		private Button btnSearch;
		private Button btnReset;
		private DataGridView dgvVocabulary;
		private Button btnAdd;
		private Button btnEdit;
		private Button btnDelete;
		private Button btnFlashcard;
		private Button btnQuiz;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true、それ以外の場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。
		/// このメソッドの内容をコード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			lblSearch = new Label();
			txtSearch = new TextBox();
			cboLevel = new ComboBox();
			btnSearch = new Button();
			btnReset = new Button();
			dgvVocabulary = new DataGridView();
			btnAdd = new Button();
			btnEdit = new Button();
			btnDelete = new Button();
			btnFlashcard = new Button();
			btnQuiz = new Button();
			btnImport = new Button();
			btnImportJTestHtml = new Button();
			btnMockTest = new Button();
			btnImportTestPdf = new Button();
			btnImportTestCsv = new Button();
			btnExamMockTest = new Button();
			((System.ComponentModel.ISupportInitialize)dgvVocabulary).BeginInit();
			SuspendLayout();
			// 
			// lblSearch
			// 
			lblSearch.AutoSize = true;
			lblSearch.Font = new Font("Segoe UI", 12F);
			lblSearch.Location = new Point(24, 52);
			lblSearch.Name = "lblSearch";
			lblSearch.Size = new Size(77, 21);
			lblSearch.TabIndex = 0;
			lblSearch.Text = "Tìm kiếm:";
			// 
			// txtSearch
			// 
			txtSearch.Font = new Font("Segoe UI", 12F);
			txtSearch.Location = new Point(122, 50);
			txtSearch.Margin = new Padding(3, 2, 3, 2);
			txtSearch.Name = "txtSearch";
			txtSearch.Size = new Size(490, 29);
			txtSearch.TabIndex = 1;
			// 
			// cboLevel
			// 
			cboLevel.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLevel.Font = new Font("Segoe UI", 12F);
			cboLevel.FormattingEnabled = true;
			cboLevel.Items.AddRange(new object[] { "All", "N5", "N4", "N3", "N2", "N1" });
			cboLevel.Location = new Point(626, 50);
			cboLevel.Margin = new Padding(3, 2, 3, 2);
			cboLevel.Name = "cboLevel";
			cboLevel.Size = new Size(140, 29);
			cboLevel.TabIndex = 2;
			// 
			// btnSearch
			// 
			btnSearch.Font = new Font("Segoe UI", 12F);
			btnSearch.Location = new Point(779, 50);
			btnSearch.Margin = new Padding(3, 2, 3, 2);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(127, 30);
			btnSearch.TabIndex = 3;
			btnSearch.Text = "Search";
			btnSearch.UseVisualStyleBackColor = true;
			btnSearch.Click += btnSearch_Click;
			// 
			// btnReset
			// 
			btnReset.Font = new Font("Segoe UI", 12F);
			btnReset.Location = new Point(919, 50);
			btnReset.Margin = new Padding(3, 2, 3, 2);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(127, 30);
			btnReset.TabIndex = 4;
			btnReset.Text = "Reset";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += btnReset_Click;
			// 
			// dgvVocabulary
			// 
			dgvVocabulary.AllowUserToAddRows = false;
			dgvVocabulary.AllowUserToDeleteRows = false;
			dgvVocabulary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvVocabulary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvVocabulary.BackgroundColor = SystemColors.ControlLightLight;
			dgvVocabulary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvVocabulary.Location = new Point(26, 112);
			dgvVocabulary.Margin = new Padding(3, 2, 3, 2);
			dgvVocabulary.MultiSelect = false;
			dgvVocabulary.Name = "dgvVocabulary";
			dgvVocabulary.ReadOnly = true;
			dgvVocabulary.RowHeadersWidth = 51;
			dgvVocabulary.RowTemplate.Height = 33;
			dgvVocabulary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvVocabulary.Size = new Size(1027, 462);
			dgvVocabulary.TabIndex = 5;
			// 
			// btnAdd
			// 
			btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnAdd.Font = new Font("Segoe UI", 12F);
			btnAdd.Location = new Point(26, 587);
			btnAdd.Margin = new Padding(3, 2, 3, 2);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(88, 32);
			btnAdd.TabIndex = 6;
			btnAdd.Text = "Add";
			btnAdd.UseVisualStyleBackColor = true;
			btnAdd.Click += btnAdd_Click;
			// 
			// btnEdit
			// 
			btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnEdit.Font = new Font("Segoe UI", 12F);
			btnEdit.Location = new Point(154, 587);
			btnEdit.Margin = new Padding(3, 2, 3, 2);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new Size(88, 32);
			btnEdit.TabIndex = 7;
			btnEdit.Text = "Edit";
			btnEdit.UseVisualStyleBackColor = true;
			btnEdit.Click += btnEdit_Click;
			// 
			// btnDelete
			// 
			btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnDelete.Font = new Font("Segoe UI", 12F);
			btnDelete.Location = new Point(282, 587);
			btnDelete.Margin = new Padding(3, 2, 3, 2);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(88, 32);
			btnDelete.TabIndex = 8;
			btnDelete.Text = "Delete";
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// btnFlashcard
			// 
			btnFlashcard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnFlashcard.Font = new Font("Segoe UI", 12F);
			btnFlashcard.Location = new Point(410, 587);
			btnFlashcard.Margin = new Padding(3, 2, 3, 2);
			btnFlashcard.Name = "btnFlashcard";
			btnFlashcard.Size = new Size(140, 32);
			btnFlashcard.TabIndex = 9;
			btnFlashcard.Text = "Flashcard";
			btnFlashcard.UseVisualStyleBackColor = true;
			btnFlashcard.Click += btnFlashcard_Click;
			// 
			// btnQuiz
			// 
			btnQuiz.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnQuiz.Font = new Font("Segoe UI", 12F);
			btnQuiz.Location = new Point(590, 587);
			btnQuiz.Margin = new Padding(3, 2, 3, 2);
			btnQuiz.Name = "btnQuiz";
			btnQuiz.Size = new Size(88, 32);
			btnQuiz.TabIndex = 10;
			btnQuiz.Text = "Quiz";
			btnQuiz.UseVisualStyleBackColor = true;
			btnQuiz.Click += btnQuiz_Click;
			// 
			// btnImport
			// 
			btnImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnImport.Font = new Font("Segoe UI", 12F);
			btnImport.Location = new Point(718, 587);
			btnImport.Margin = new Padding(3, 2, 3, 2);
			btnImport.Name = "btnImport";
			btnImport.Size = new Size(140, 32);
			btnImport.TabIndex = 10;
			btnImport.Text = "Import CSV";
			btnImport.UseVisualStyleBackColor = true;
			btnImport.Click += btnImport_Click;
			// 
			// btnImportJTestHtml
			// 
			btnImportJTestHtml.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnImportJTestHtml.Font = new Font("Segoe UI", 12F);
			btnImportJTestHtml.Location = new Point(898, 586);
			btnImportJTestHtml.Margin = new Padding(3, 2, 3, 2);
			btnImportJTestHtml.Name = "btnImportJTestHtml";
			btnImportJTestHtml.Size = new Size(151, 32);
			btnImportJTestHtml.TabIndex = 10;
			btnImportJTestHtml.Text = "Import JTest HTML";
			btnImportJTestHtml.UseVisualStyleBackColor = true;
			btnImportJTestHtml.Click += btnImportJTestHtml_Click;
			// 
			// btnMockTest
			// 
			btnMockTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnMockTest.Font = new Font("Segoe UI", 12F);
			btnMockTest.Location = new Point(26, 624);
			btnMockTest.Margin = new Padding(3, 2, 3, 2);
			btnMockTest.Name = "btnMockTest";
			btnMockTest.Size = new Size(88, 32);
			btnMockTest.TabIndex = 11;
			btnMockTest.Text = "Mock Test";
			btnMockTest.UseVisualStyleBackColor = true;
			btnMockTest.Click += btnMockTest_Click;
			// 
			// btnImportTestPdf
			// 
			btnImportTestPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnImportTestPdf.Font = new Font("Segoe UI", 12F);
			btnImportTestPdf.Location = new Point(154, 624);
			btnImportTestPdf.Margin = new Padding(3, 2, 3, 2);
			btnImportTestPdf.Name = "btnImportTestPdf";
			btnImportTestPdf.Size = new Size(120, 32);
			btnImportTestPdf.TabIndex = 7;
			btnImportTestPdf.Text = "Import PDF";
			btnImportTestPdf.UseVisualStyleBackColor = true;
			btnImportTestPdf.Click += btnImportTestPdf_Click;
			// 
			// btnImportTestCsv
			// 
			btnImportTestCsv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnImportTestCsv.Font = new Font("Segoe UI", 12F);
			btnImportTestCsv.Location = new Point(282, 624);
			btnImportTestCsv.Margin = new Padding(3, 2, 3, 2);
			btnImportTestCsv.Name = "btnImportTestCsv";
			btnImportTestCsv.Size = new Size(140, 32);
			btnImportTestCsv.TabIndex = 8;
			btnImportTestCsv.Text = "Import Test CSV";
			btnImportTestCsv.UseVisualStyleBackColor = true;
			btnImportTestCsv.Click += btnImportTestCsv_Click;
			// 
			// btnExamMockTest
			// 
			btnExamMockTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnExamMockTest.Font = new Font("Segoe UI", 12F);
			btnExamMockTest.Location = new Point(428, 624);
			btnExamMockTest.Margin = new Padding(3, 2, 3, 2);
			btnExamMockTest.Name = "btnExamMockTest";
			btnExamMockTest.Size = new Size(140, 32);
			btnExamMockTest.TabIndex = 9;
			btnExamMockTest.Text = "Exam Test";
			btnExamMockTest.UseVisualStyleBackColor = true;
			btnExamMockTest.Click += btnExamMockTest_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1084, 661);
			Controls.Add(btnMockTest);
			Controls.Add(btnImportJTestHtml);
			Controls.Add(btnImport);
			Controls.Add(btnQuiz);
			Controls.Add(btnExamMockTest);
			Controls.Add(btnFlashcard);
			Controls.Add(btnImportTestCsv);
			Controls.Add(btnImportTestPdf);
			Controls.Add(btnDelete);
			Controls.Add(btnEdit);
			Controls.Add(btnAdd);
			Controls.Add(dgvVocabulary);
			Controls.Add(btnReset);
			Controls.Add(btnSearch);
			Controls.Add(cboLevel);
			Controls.Add(txtSearch);
			Controls.Add(lblSearch);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(3, 2, 3, 2);
			MinimumSize = new Size(1000, 600);
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "NihongoVocabTrainer";
			Load += MainForm_Load;
			((System.ComponentModel.ISupportInitialize)dgvVocabulary).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnImport;
		private Button btnImportJTestHtml;
		private Button btnMockTest;
		private Button btnImportTestPdf;
		private Button btnImportTestCsv;
		private Button btnExamMockTest;
	}
}
