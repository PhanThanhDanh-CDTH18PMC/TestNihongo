namespace NihongoVocabTrainer.Forms
{
    partial class AddWordForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblKanji;
        private Label lblHiragana;
        private Label lblMeaning;
        private Label lblLevel;
        private Label lblExample;
        private TextBox txtKanji;
        private TextBox txtHiragana;
        private TextBox txtMeaning;
        private ComboBox cboLevel;
        private TextBox txtExample;
        private Button btnSave;
        private Button btnCancel;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWordForm));
			lblKanji = new Label();
			lblHiragana = new Label();
			lblMeaning = new Label();
			lblLevel = new Label();
			lblExample = new Label();
			txtKanji = new TextBox();
			txtHiragana = new TextBox();
			txtMeaning = new TextBox();
			cboLevel = new ComboBox();
			txtExample = new TextBox();
			btnSave = new Button();
			btnCancel = new Button();
			SuspendLayout();
			// 
			// lblKanji
			// 
			lblKanji.AutoSize = true;
			lblKanji.Font = new Font("Segoe UI", 11F);
			lblKanji.Location = new Point(32, 32);
			lblKanji.Name = "lblKanji";
			lblKanji.Size = new Size(42, 20);
			lblKanji.TabIndex = 0;
			lblKanji.Text = "Kanji";
			// 
			// lblHiragana
			// 
			lblHiragana.AutoSize = true;
			lblHiragana.Font = new Font("Segoe UI", 11F);
			lblHiragana.Location = new Point(32, 84);
			lblHiragana.Name = "lblHiragana";
			lblHiragana.Size = new Size(70, 20);
			lblHiragana.TabIndex = 1;
			lblHiragana.Text = "Hiragana";
			// 
			// lblMeaning
			// 
			lblMeaning.AutoSize = true;
			lblMeaning.Font = new Font("Segoe UI", 11F);
			lblMeaning.Location = new Point(32, 136);
			lblMeaning.Name = "lblMeaning";
			lblMeaning.Size = new Size(67, 20);
			lblMeaning.TabIndex = 2;
			lblMeaning.Text = "Meaning";
			// 
			// lblLevel
			// 
			lblLevel.AutoSize = true;
			lblLevel.Font = new Font("Segoe UI", 11F);
			lblLevel.Location = new Point(32, 188);
			lblLevel.Name = "lblLevel";
			lblLevel.Size = new Size(43, 20);
			lblLevel.TabIndex = 3;
			lblLevel.Text = "Level";
			// 
			// lblExample
			// 
			lblExample.AutoSize = true;
			lblExample.Font = new Font("Segoe UI", 11F);
			lblExample.Location = new Point(32, 240);
			lblExample.Name = "lblExample";
			lblExample.Size = new Size(66, 20);
			lblExample.TabIndex = 4;
			lblExample.Text = "Example";
			// 
			// txtKanji
			// 
			txtKanji.Font = new Font("Segoe UI", 11F);
			txtKanji.Location = new Point(132, 29);
			txtKanji.Name = "txtKanji";
			txtKanji.Size = new Size(430, 27);
			txtKanji.TabIndex = 0;
			// 
			// txtHiragana
			// 
			txtHiragana.Font = new Font("Segoe UI", 11F);
			txtHiragana.Location = new Point(132, 81);
			txtHiragana.Name = "txtHiragana";
			txtHiragana.Size = new Size(430, 27);
			txtHiragana.TabIndex = 1;
			// 
			// txtMeaning
			// 
			txtMeaning.Font = new Font("Segoe UI", 11F);
			txtMeaning.Location = new Point(132, 133);
			txtMeaning.Name = "txtMeaning";
			txtMeaning.Size = new Size(430, 27);
			txtMeaning.TabIndex = 2;
			// 
			// cboLevel
			// 
			cboLevel.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLevel.Font = new Font("Segoe UI", 11F);
			cboLevel.FormattingEnabled = true;
			cboLevel.Items.AddRange(new object[] { "N5", "N4", "N3", "N2", "N1" });
			cboLevel.Location = new Point(132, 185);
			cboLevel.Name = "cboLevel";
			cboLevel.Size = new Size(160, 28);
			cboLevel.TabIndex = 3;
			// 
			// txtExample
			// 
			txtExample.Font = new Font("Segoe UI", 11F);
			txtExample.Location = new Point(132, 237);
			txtExample.Multiline = true;
			txtExample.Name = "txtExample";
			txtExample.Size = new Size(430, 96);
			txtExample.TabIndex = 4;
			// 
			// btnSave
			// 
			btnSave.Font = new Font("Segoe UI", 11F);
			btnSave.Location = new Point(334, 358);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(108, 38);
			btnSave.TabIndex = 5;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnCancel
			// 
			btnCancel.Font = new Font("Segoe UI", 11F);
			btnCancel.Location = new Point(454, 358);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(108, 38);
			btnCancel.TabIndex = 6;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// AddWordForm
			// 
			AcceptButton = btnSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(604, 421);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(txtExample);
			Controls.Add(cboLevel);
			Controls.Add(txtMeaning);
			Controls.Add(txtHiragana);
			Controls.Add(txtKanji);
			Controls.Add(lblExample);
			Controls.Add(lblLevel);
			Controls.Add(lblMeaning);
			Controls.Add(lblHiragana);
			Controls.Add(lblKanji);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "AddWordForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Add Word";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
