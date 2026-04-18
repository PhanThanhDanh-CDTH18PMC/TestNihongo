namespace NihongoVocabTrainer.Forms
{
	partial class QuizForm
	{
		private System.ComponentModel.IContainer components = null;
		private Label lblTitle;
		private Label lblScore;
		private Label lblQuestion;
		private RadioButton rdoA;
		private RadioButton rdoB;
		private RadioButton rdoC;
		private RadioButton rdoD;
		private Button btnSubmit;
		private Button btnNext;
		private Button btnClose;
		private Label lblResult;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuizForm));
			lblTitle = new Label();
			lblScore = new Label();
			lblQuestion = new Label();
			rdoA = new RadioButton();
			rdoB = new RadioButton();
			rdoC = new RadioButton();
			rdoD = new RadioButton();
			btnSubmit = new Button();
			btnNext = new Button();
			btnClose = new Button();
			lblResult = new Label();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblTitle.Location = new Point(30, 20);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(720, 36);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Quiz";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblScore
			// 
			lblScore.Font = new Font("Segoe UI", 10F);
			lblScore.Location = new Point(48, 64);
			lblScore.Name = "lblScore";
			lblScore.Size = new Size(684, 28);
			lblScore.TabIndex = 1;
			lblScore.Text = "Score: 0 / 0";
			lblScore.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblQuestion
			// 
			lblQuestion.BorderStyle = BorderStyle.FixedSingle;
			lblQuestion.Font = new Font("Yu Gothic UI", 30F, FontStyle.Bold);
			lblQuestion.Location = new Point(48, 104);
			lblQuestion.Name = "lblQuestion";
			lblQuestion.Size = new Size(684, 110);
			lblQuestion.TabIndex = 2;
			lblQuestion.Text = "勉強";
			lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// rdoA
			// 
			rdoA.Font = new Font("Segoe UI", 12F);
			rdoA.Location = new Point(80, 238);
			rdoA.Name = "rdoA";
			rdoA.Size = new Size(620, 34);
			rdoA.TabIndex = 3;
			rdoA.TabStop = true;
			rdoA.Text = "A";
			rdoA.UseVisualStyleBackColor = true;
			// 
			// rdoB
			// 
			rdoB.Font = new Font("Segoe UI", 12F);
			rdoB.Location = new Point(80, 282);
			rdoB.Name = "rdoB";
			rdoB.Size = new Size(620, 34);
			rdoB.TabIndex = 4;
			rdoB.TabStop = true;
			rdoB.Text = "B";
			rdoB.UseVisualStyleBackColor = true;
			// 
			// rdoC
			// 
			rdoC.Font = new Font("Segoe UI", 12F);
			rdoC.Location = new Point(80, 326);
			rdoC.Name = "rdoC";
			rdoC.Size = new Size(620, 34);
			rdoC.TabIndex = 5;
			rdoC.TabStop = true;
			rdoC.Text = "C";
			rdoC.UseVisualStyleBackColor = true;
			// 
			// rdoD
			// 
			rdoD.Font = new Font("Segoe UI", 12F);
			rdoD.Location = new Point(80, 370);
			rdoD.Name = "rdoD";
			rdoD.Size = new Size(620, 34);
			rdoD.TabIndex = 6;
			rdoD.TabStop = true;
			rdoD.Text = "D";
			rdoD.UseVisualStyleBackColor = true;
			// 
			// btnSubmit
			// 
			btnSubmit.Font = new Font("Segoe UI", 10.5F);
			btnSubmit.Location = new Point(190, 462);
			btnSubmit.Name = "btnSubmit";
			btnSubmit.Size = new Size(120, 42);
			btnSubmit.TabIndex = 8;
			btnSubmit.Text = "Submit";
			btnSubmit.UseVisualStyleBackColor = true;
			btnSubmit.Click += btnSubmit_Click;
			// 
			// btnNext
			// 
			btnNext.Font = new Font("Segoe UI", 10.5F);
			btnNext.Location = new Point(330, 462);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(120, 42);
			btnNext.TabIndex = 9;
			btnNext.Text = "Next";
			btnNext.UseVisualStyleBackColor = true;
			btnNext.Click += btnNext_Click;
			// 
			// btnClose
			// 
			btnClose.Font = new Font("Segoe UI", 10.5F);
			btnClose.Location = new Point(470, 462);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(120, 42);
			btnClose.TabIndex = 10;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// lblResult
			// 
			lblResult.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblResult.Location = new Point(48, 418);
			lblResult.Name = "lblResult";
			lblResult.Size = new Size(684, 28);
			lblResult.TabIndex = 7;
			lblResult.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// QuizForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 531);
			Controls.Add(btnClose);
			Controls.Add(btnNext);
			Controls.Add(btnSubmit);
			Controls.Add(lblResult);
			Controls.Add(rdoD);
			Controls.Add(rdoC);
			Controls.Add(rdoB);
			Controls.Add(rdoA);
			Controls.Add(lblQuestion);
			Controls.Add(lblScore);
			Controls.Add(lblTitle);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "QuizForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Quiz";
			Load += QuizForm_Load;
			ResumeLayout(false);
		}
	}
}