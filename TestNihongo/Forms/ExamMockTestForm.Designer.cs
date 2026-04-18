namespace NihongoVocabTrainer.Forms
{
    partial class ExamMockTestForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblProgress;
        private Label lblScore;
        private Label lblQuestion;
        private RadioButton rdoA;
        private RadioButton rdoB;
        private RadioButton rdoC;
        private RadioButton rdoD;
        private Label lblResult;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnSubmitTest;
        private Button btnClose;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamMockTestForm));
			lblTitle = new Label();
			lblProgress = new Label();
			lblScore = new Label();
			lblQuestion = new Label();
			rdoA = new RadioButton();
			rdoB = new RadioButton();
			rdoC = new RadioButton();
			rdoD = new RadioButton();
			lblResult = new Label();
			btnPrevious = new Button();
			btnNext = new Button();
			btnSubmitTest = new Button();
			btnClose = new Button();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblTitle.Location = new Point(24, 18);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(836, 38);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "📝 Exam Mock Test 📝";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblProgress
			// 
			lblProgress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblProgress.Location = new Point(42, 68);
			lblProgress.Name = "lblProgress";
			lblProgress.Size = new Size(320, 26);
			lblProgress.TabIndex = 1;
			lblProgress.Text = "Question 1 / 20";
			lblProgress.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblScore
			// 
			lblScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblScore.Location = new Point(540, 68);
			lblScore.Name = "lblScore";
			lblScore.Size = new Size(300, 26);
			lblScore.TabIndex = 2;
			lblScore.Text = "Answered: 0";
			lblScore.TextAlign = ContentAlignment.MiddleRight;
			// 
			// lblQuestion
			// 
			lblQuestion.BorderStyle = BorderStyle.FixedSingle;
			lblQuestion.Font = new Font("Yu Gothic UI", 17F, FontStyle.Bold);
			lblQuestion.Location = new Point(42, 108);
			lblQuestion.Name = "lblQuestion";
			lblQuestion.Size = new Size(800, 126);
			lblQuestion.TabIndex = 3;
			lblQuestion.Text = "Question";
			lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// rdoA
			// 
			rdoA.Font = new Font("Yu Gothic UI", 12F);
			rdoA.Location = new Point(76, 258);
			rdoA.Name = "rdoA";
			rdoA.Size = new Size(730, 38);
			rdoA.TabIndex = 4;
			rdoA.TabStop = true;
			rdoA.Text = "A";
			rdoA.UseVisualStyleBackColor = true;
			rdoA.CheckedChanged += Answer_CheckedChanged;
			// 
			// rdoB
			// 
			rdoB.Font = new Font("Yu Gothic UI", 12F);
			rdoB.Location = new Point(76, 306);
			rdoB.Name = "rdoB";
			rdoB.Size = new Size(730, 38);
			rdoB.TabIndex = 5;
			rdoB.TabStop = true;
			rdoB.Text = "B";
			rdoB.UseVisualStyleBackColor = true;
			rdoB.CheckedChanged += Answer_CheckedChanged;
			// 
			// rdoC
			// 
			rdoC.Font = new Font("Yu Gothic UI", 12F);
			rdoC.Location = new Point(76, 354);
			rdoC.Name = "rdoC";
			rdoC.Size = new Size(730, 38);
			rdoC.TabIndex = 6;
			rdoC.TabStop = true;
			rdoC.Text = "C";
			rdoC.UseVisualStyleBackColor = true;
			rdoC.CheckedChanged += Answer_CheckedChanged;
			// 
			// rdoD
			// 
			rdoD.Font = new Font("Yu Gothic UI", 12F);
			rdoD.Location = new Point(76, 402);
			rdoD.Name = "rdoD";
			rdoD.Size = new Size(730, 38);
			rdoD.TabIndex = 7;
			rdoD.TabStop = true;
			rdoD.Text = "D";
			rdoD.UseVisualStyleBackColor = true;
			rdoD.CheckedChanged += Answer_CheckedChanged;
			// 
			// lblResult
			// 
			lblResult.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblResult.Location = new Point(42, 456);
			lblResult.Name = "lblResult";
			lblResult.Size = new Size(800, 36);
			lblResult.TabIndex = 8;
			lblResult.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnPrevious
			// 
			btnPrevious.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnPrevious.Location = new Point(120, 512);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new Size(130, 42);
			btnPrevious.TabIndex = 9;
			btnPrevious.Text = "Previous ⬅️";
			btnPrevious.UseVisualStyleBackColor = true;
			btnPrevious.Click += btnPrevious_Click;
			// 
			// btnNext
			// 
			btnNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnNext.Location = new Point(278, 512);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(130, 42);
			btnNext.TabIndex = 10;
			btnNext.Text = "Next ➡️";
			btnNext.UseVisualStyleBackColor = true;
			btnNext.Click += btnNext_Click;
			// 
			// btnSubmitTest
			// 
			btnSubmitTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnSubmitTest.Location = new Point(436, 512);
			btnSubmitTest.Name = "btnSubmitTest";
			btnSubmitTest.Size = new Size(150, 42);
			btnSubmitTest.TabIndex = 11;
			btnSubmitTest.Text = "Submit Test 📝";
			btnSubmitTest.UseVisualStyleBackColor = true;
			btnSubmitTest.Click += btnSubmitTest_Click;
			// 
			// btnClose
			// 
			btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnClose.Location = new Point(614, 512);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(130, 42);
			btnClose.TabIndex = 12;
			btnClose.Text = "Close 🐾";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// ExamMockTestForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(884, 581);
			Controls.Add(btnClose);
			Controls.Add(btnSubmitTest);
			Controls.Add(btnNext);
			Controls.Add(btnPrevious);
			Controls.Add(lblResult);
			Controls.Add(rdoD);
			Controls.Add(rdoC);
			Controls.Add(rdoB);
			Controls.Add(rdoA);
			Controls.Add(lblQuestion);
			Controls.Add(lblScore);
			Controls.Add(lblProgress);
			Controls.Add(lblTitle);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "ExamMockTestForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Exam Mock Test";
			Load += ExamMockTestForm_Load;
			ResumeLayout(false);
		}
	}
}
