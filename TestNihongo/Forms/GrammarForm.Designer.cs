namespace NihongoVocabTrainer.Forms
{
    partial class GrammarForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtSearch;
        private ComboBox cboGrammarLevel;
        private Button btnSearch;
        private Button btnReset;
        private DataGridView dgvGrammar;
        private Label lblPattern;
        private Label lblMeaning;
        private TextBox txtUsage;
        private TextBox txtExampleJapanese;
        private TextBox txtExampleVietnamese;
        private Label lblUsageTitle;
        private Label lblExampleJapaneseTitle;
        private Label lblExampleVietnameseTitle;
        private Button btnKnow;
        private Button btnDontKnow;
        private Button btnClose;
        private Label lblSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtSearch = new TextBox();
            cboGrammarLevel = new ComboBox();
            btnSearch = new Button();
            btnReset = new Button();
            dgvGrammar = new DataGridView();
            lblPattern = new Label();
            lblMeaning = new Label();
            txtUsage = new TextBox();
            txtExampleJapanese = new TextBox();
            txtExampleVietnamese = new TextBox();
            lblUsageTitle = new Label();
            lblExampleJapaneseTitle = new Label();
            lblExampleVietnameseTitle = new Label();
            btnKnow = new Button();
            btnDontKnow = new Button();
            btnClose = new Button();
            lblSearch = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvGrammar).BeginInit();
            SuspendLayout();
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.Location = new Point(24, 29);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(69, 19);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(105, 26);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(430, 23);
            txtSearch.TabIndex = 1;
            // 
            // cboGrammarLevel
            // 
            cboGrammarLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGrammarLevel.FormattingEnabled = true;
            cboGrammarLevel.Location = new Point(552, 26);
            cboGrammarLevel.Name = "cboGrammarLevel";
            cboGrammarLevel.Size = new Size(120, 23);
            cboGrammarLevel.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(694, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(130, 36);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Search 🔍";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(840, 20);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(130, 36);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset ♻️";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // dgvGrammar
            // 
            dgvGrammar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrammar.Location = new Point(24, 76);
            dgvGrammar.Name = "dgvGrammar";
            dgvGrammar.Size = new Size(500, 485);
            dgvGrammar.TabIndex = 5;
            dgvGrammar.SelectionChanged += dgvGrammar_SelectionChanged;
            // 
            // lblPattern
            // 
            lblPattern.BorderStyle = BorderStyle.FixedSingle;
            lblPattern.Location = new Point(548, 76);
            lblPattern.Name = "lblPattern";
            lblPattern.Size = new Size(500, 56);
            lblPattern.TabIndex = 6;
            lblPattern.Text = "文法";
            lblPattern.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMeaning
            // 
            lblMeaning.BorderStyle = BorderStyle.FixedSingle;
            lblMeaning.Location = new Point(548, 144);
            lblMeaning.Name = "lblMeaning";
            lblMeaning.Size = new Size(500, 52);
            lblMeaning.TabIndex = 7;
            lblMeaning.Text = "意味";
            lblMeaning.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsageTitle
            // 
            lblUsageTitle.AutoSize = true;
            lblUsageTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsageTitle.Location = new Point(548, 215);
            lblUsageTitle.Name = "lblUsageTitle";
            lblUsageTitle.Size = new Size(73, 19);
            lblUsageTitle.TabIndex = 8;
            lblUsageTitle.Text = "Cách dùng";
            // 
            // txtUsage
            // 
            txtUsage.Location = new Point(548, 238);
            txtUsage.Multiline = true;
            txtUsage.Name = "txtUsage";
            txtUsage.ReadOnly = true;
            txtUsage.ScrollBars = ScrollBars.Vertical;
            txtUsage.Size = new Size(500, 80);
            txtUsage.TabIndex = 9;
            // 
            // lblExampleJapaneseTitle
            // 
            lblExampleJapaneseTitle.AutoSize = true;
            lblExampleJapaneseTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblExampleJapaneseTitle.Location = new Point(548, 334);
            lblExampleJapaneseTitle.Name = "lblExampleJapaneseTitle";
            lblExampleJapaneseTitle.Size = new Size(121, 19);
            lblExampleJapaneseTitle.TabIndex = 10;
            lblExampleJapaneseTitle.Text = "Ví dụ tiếng Nhật";
            // 
            // txtExampleJapanese
            // 
            txtExampleJapanese.Location = new Point(548, 357);
            txtExampleJapanese.Multiline = true;
            txtExampleJapanese.Name = "txtExampleJapanese";
            txtExampleJapanese.ReadOnly = true;
            txtExampleJapanese.ScrollBars = ScrollBars.Vertical;
            txtExampleJapanese.Size = new Size(500, 80);
            txtExampleJapanese.TabIndex = 11;
            // 
            // lblExampleVietnameseTitle
            // 
            lblExampleVietnameseTitle.AutoSize = true;
            lblExampleVietnameseTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblExampleVietnameseTitle.Location = new Point(548, 453);
            lblExampleVietnameseTitle.Name = "lblExampleVietnameseTitle";
            lblExampleVietnameseTitle.Size = new Size(100, 19);
            lblExampleVietnameseTitle.TabIndex = 12;
            lblExampleVietnameseTitle.Text = "Dịch tiếng Việt";
            // 
            // txtExampleVietnamese
            // 
            txtExampleVietnamese.Location = new Point(548, 476);
            txtExampleVietnamese.Multiline = true;
            txtExampleVietnamese.Name = "txtExampleVietnamese";
            txtExampleVietnamese.ReadOnly = true;
            txtExampleVietnamese.ScrollBars = ScrollBars.Vertical;
            txtExampleVietnamese.Size = new Size(500, 85);
            txtExampleVietnamese.TabIndex = 13;
            // 
            // btnKnow
            // 
            btnKnow.Location = new Point(548, 585);
            btnKnow.Name = "btnKnow";
            btnKnow.Size = new Size(140, 38);
            btnKnow.TabIndex = 14;
            btnKnow.Text = "Know 🌱";
            btnKnow.UseVisualStyleBackColor = true;
            btnKnow.Click += btnKnow_Click;
            // 
            // btnDontKnow
            // 
            btnDontKnow.Location = new Point(708, 585);
            btnDontKnow.Name = "btnDontKnow";
            btnDontKnow.Size = new Size(160, 38);
            btnDontKnow.TabIndex = 15;
            btnDontKnow.Text = "Don't Know 💭";
            btnDontKnow.UseVisualStyleBackColor = true;
            btnDontKnow.Click += btnDontKnow_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(888, 585);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(160, 38);
            btnClose.TabIndex = 16;
            btnClose.Text = "Close 🐾";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // GrammarForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 651);
            Controls.Add(btnClose);
            Controls.Add(btnDontKnow);
            Controls.Add(btnKnow);
            Controls.Add(txtExampleVietnamese);
            Controls.Add(lblExampleVietnameseTitle);
            Controls.Add(txtExampleJapanese);
            Controls.Add(lblExampleJapaneseTitle);
            Controls.Add(txtUsage);
            Controls.Add(lblUsageTitle);
            Controls.Add(lblMeaning);
            Controls.Add(lblPattern);
            Controls.Add(dgvGrammar);
            Controls.Add(btnReset);
            Controls.Add(btnSearch);
            Controls.Add(cboGrammarLevel);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Name = "GrammarForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Grammar Study";
            Load += GrammarForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGrammar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
