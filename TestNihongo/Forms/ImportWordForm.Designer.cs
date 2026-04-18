namespace NihongoVocabTrainer.Forms
{
    partial class ImportWordForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblDescription;
        private TextBox txtFilePath;
        private Button btnBrowse;
        private DataGridView dgvPreview;
        private Label lblCount;
        private Button btnImport;
        private Button btnCancel;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージドリソースを破棄する場合は true。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		/// <summary>
		/// デザイナーで必要なメソッドです。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportWordForm));
			lblTitle = new Label();
			lblDescription = new Label();
			txtFilePath = new TextBox();
			btnBrowse = new Button();
			dgvPreview = new DataGridView();
			lblCount = new Label();
			btnImport = new Button();
			btnCancel = new Button();
			((System.ComponentModel.ISupportInitialize)dgvPreview).BeginInit();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblTitle.Location = new Point(24, 20);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(209, 30);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Import Vocabulary";
			// 
			// lblDescription
			// 
			lblDescription.AutoSize = true;
			lblDescription.Location = new Point(27, 60);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(393, 15);
			lblDescription.TabIndex = 1;
			lblDescription.Text = "CSV形式: Kanji,Hiragana,Meaning,Level,Example の順で作成してください。";
			// 
			// txtFilePath
			// 
			txtFilePath.Location = new Point(27, 94);
			txtFilePath.Name = "txtFilePath";
			txtFilePath.ReadOnly = true;
			txtFilePath.Size = new Size(600, 23);
			txtFilePath.TabIndex = 2;
			// 
			// btnBrowse
			// 
			btnBrowse.Location = new Point(645, 92);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new Size(110, 27);
			btnBrowse.TabIndex = 3;
			btnBrowse.Text = "Browse";
			btnBrowse.UseVisualStyleBackColor = true;
			btnBrowse.Click += btnBrowse_Click;
			// 
			// dgvPreview
			// 
			dgvPreview.AllowUserToAddRows = false;
			dgvPreview.AllowUserToDeleteRows = false;
			dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPreview.Location = new Point(27, 140);
			dgvPreview.MultiSelect = false;
			dgvPreview.Name = "dgvPreview";
			dgvPreview.ReadOnly = true;
			dgvPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvPreview.Size = new Size(728, 310);
			dgvPreview.TabIndex = 4;
			// 
			// lblCount
			// 
			lblCount.AutoSize = true;
			lblCount.Location = new Point(27, 466);
			lblCount.Name = "lblCount";
			lblCount.Size = new Size(95, 15);
			lblCount.TabIndex = 5;
			lblCount.Text = "読み込み件数: 0";
			// 
			// btnImport
			// 
			btnImport.Location = new Point(535, 460);
			btnImport.Name = "btnImport";
			btnImport.Size = new Size(105, 32);
			btnImport.TabIndex = 6;
			btnImport.Text = "Import";
			btnImport.UseVisualStyleBackColor = true;
			btnImport.Click += btnImport_Click;
			// 
			// btnCancel
			// 
			btnCancel.Location = new Point(650, 460);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(105, 32);
			btnCancel.TabIndex = 7;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// ImportWordForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 511);
			Controls.Add(btnCancel);
			Controls.Add(btnImport);
			Controls.Add(lblCount);
			Controls.Add(dgvPreview);
			Controls.Add(btnBrowse);
			Controls.Add(txtFilePath);
			Controls.Add(lblDescription);
			Controls.Add(lblTitle);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ImportWordForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Import Vocabulary";
			((System.ComponentModel.ISupportInitialize)dgvPreview).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
