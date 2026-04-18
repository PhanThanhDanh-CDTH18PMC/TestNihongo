namespace NihongoVocabTrainer.Forms
{
    partial class MockTestForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblProgress;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MockTestForm));
			lblTitle = new Label();
			lblProgress = new Label();
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
			lblTitle.Location = new Point(30, 20);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(720, 38);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "📝 Mock Test 📝";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblProgress
			// 
			lblProgress.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
			lblProgress.Location = new Point(48, 70);
			lblProgress.Name = "lblProgress";
			lblProgress.Size = new Size(684, 28);
			lblProgress.TabIndex = 1;
			lblProgress.Text = "Question 1 / 20";
			lblProgress.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblQuestion
			// 
			lblQuestion.BorderStyle = BorderStyle.FixedSingle;
			lblQuestion.Font = new Font("Yu Gothic UI", 30F, FontStyle.Bold);
			lblQuestion.Location = new Point(48, 110);
			lblQuestion.Name = "lblQuestion";
			lblQuestion.Size = new Size(684, 110);
			lblQuestion.TabIndex = 2;
			lblQuestion.Text = "勉強";
			lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// rdoA
			// 
			rdoA.Font = new Font("Yu Gothic UI", 11.5F);
			rdoA.Location = new Point(80, 245);
			rdoA.Name = "rdoA";
			rdoA.Size = new Size(620, 34);
			rdoA.TabIndex = 3;
			rdoA.TabStop = true;
			rdoA.Text = "A";
			rdoA.UseVisualStyleBackColor = true;
			rdoA.CheckedChanged += AnswerRadioButton_CheckedChanged;
			// 
			// rdoB
			// 
			rdoB.Font = new Font("Yu Gothic UI", 11.5F);
			rdoB.Location = new Point(80, 290);
			rdoB.Name = "rdoB";
			rdoB.Size = new Size(620, 34);
			rdoB.TabIndex = 4;
			rdoB.TabStop = true;
			rdoB.Text = "B";
			rdoB.UseVisualStyleBackColor = true;
			rdoB.CheckedChanged += AnswerRadioButton_CheckedChanged;
			// 
			// rdoC
			// 
			rdoC.Font = new Font("Yu Gothic UI", 11.5F);
			rdoC.Location = new Point(80, 335);
			rdoC.Name = "rdoC";
			rdoC.Size = new Size(620, 34);
			rdoC.TabIndex = 5;
			rdoC.TabStop = true;
			rdoC.Text = "C";
			rdoC.UseVisualStyleBackColor = true;
			rdoC.CheckedChanged += AnswerRadioButton_CheckedChanged;
			// 
			// rdoD
			// 
			rdoD.Font = new Font("Yu Gothic UI", 11.5F);
			rdoD.Location = new Point(80, 380);
			rdoD.Name = "rdoD";
			rdoD.Size = new Size(620, 34);
			rdoD.TabIndex = 6;
			rdoD.TabStop = true;
			rdoD.Text = "D";
			rdoD.UseVisualStyleBackColor = true;
			rdoD.CheckedChanged += AnswerRadioButton_CheckedChanged;
			// 
			// lblResult
			// 
			lblResult.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
			lblResult.Location = new Point(48, 426);
			lblResult.Name = "lblResult";
			lblResult.Size = new Size(684, 30);
			lblResult.TabIndex = 7;
			lblResult.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnPrevious
			// 
			btnPrevious.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnPrevious.Location = new Point(112, 475);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new Size(130, 42);
			btnPrevious.TabIndex = 8;
			btnPrevious.Text = "Previous ⬅️";
			btnPrevious.UseVisualStyleBackColor = true;
			btnPrevious.Click += btnPrevious_Click;
			// 
			// btnNext
			// 
			btnNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnNext.Location = new Point(260, 475);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(130, 42);
			btnNext.TabIndex = 9;
			btnNext.Text = "Next ➡️";
			btnNext.UseVisualStyleBackColor = true;
			btnNext.Click += btnNext_Click;
			// 
			// btnSubmitTest
			// 
			btnSubmitTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnSubmitTest.Location = new Point(408, 475);
			btnSubmitTest.Name = "btnSubmitTest";
			btnSubmitTest.Size = new Size(150, 42);
			btnSubmitTest.TabIndex = 10;
			btnSubmitTest.Text = "Submit Test 📝";
			btnSubmitTest.UseVisualStyleBackColor = true;
			btnSubmitTest.Click += btnSubmitTest_Click;
			// 
			// btnClose
			// 
			btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnClose.Location = new Point(576, 475);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(120, 42);
			btnClose.TabIndex = 11;
			btnClose.Text = "Close 🐾";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// MockTestForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 545);
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
			Controls.Add(lblProgress);
			Controls.Add(lblTitle);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "MockTestForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Mock Test";
			Load += MockTestForm_Load;
			ResumeLayout(false);
		}
	}
}
