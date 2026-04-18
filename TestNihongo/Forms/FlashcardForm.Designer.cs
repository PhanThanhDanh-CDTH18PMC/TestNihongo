namespace NihongoVocabTrainer.Forms
{
	partial class FlashcardForm
	{
		private System.ComponentModel.IContainer components = null;
		private Label lblTitle;
		private Label lblProgress;
		private Label lblQuestion;
		private Label lblAnswer;
		private CheckBox chkOnlyDifficult;
		private Button btnShowAnswer;
		private Button btnKnow;
		private Button btnDontKnow;
		private Button btnNext;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashcardForm));
			lblTitle = new Label();
			lblProgress = new Label();
			lblQuestion = new Label();
			lblAnswer = new Label();
			chkOnlyDifficult = new CheckBox();
			btnShowAnswer = new Button();
			btnKnow = new Button();
			btnDontKnow = new Button();
			btnNext = new Button();
			btnClose = new Button();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblTitle.Location = new Point(30, 20);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(720, 36);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Flashcard";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblProgress
			// 
			lblProgress.Font = new Font("Segoe UI", 10F);
			lblProgress.Location = new Point(48, 64);
			lblProgress.Name = "lblProgress";
			lblProgress.Size = new Size(450, 24);
			lblProgress.TabIndex = 1;
			lblProgress.Text = "0 問学習済み / 0 語";
			lblProgress.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblQuestion
			// 
			lblQuestion.BorderStyle = BorderStyle.FixedSingle;
			lblQuestion.Font = new Font("Yu Gothic UI", 34F, FontStyle.Bold);
			lblQuestion.Location = new Point(48, 100);
			lblQuestion.Name = "lblQuestion";
			lblQuestion.Size = new Size(684, 120);
			lblQuestion.TabIndex = 3;
			lblQuestion.Text = "勉強";
			lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblAnswer
			// 
			lblAnswer.BorderStyle = BorderStyle.FixedSingle;
			lblAnswer.Font = new Font("Segoe UI", 13F);
			lblAnswer.Location = new Point(48, 238);
			lblAnswer.Name = "lblAnswer";
			lblAnswer.Size = new Size(684, 138);
			lblAnswer.TabIndex = 4;
			lblAnswer.Text = "答えを表示してください。";
			lblAnswer.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// chkOnlyDifficult
			// 
			chkOnlyDifficult.AutoSize = true;
			chkOnlyDifficult.Font = new Font("Segoe UI", 10F);
			chkOnlyDifficult.Location = new Point(620, 64);
			chkOnlyDifficult.Name = "chkOnlyDifficult";
			chkOnlyDifficult.Size = new Size(105, 23);
			chkOnlyDifficult.TabIndex = 2;
			chkOnlyDifficult.Text = "Only difficult";
			chkOnlyDifficult.UseVisualStyleBackColor = true;
			chkOnlyDifficult.CheckedChanged += chkOnlyDifficult_CheckedChanged;
			// 
			// btnShowAnswer
			// 
			btnShowAnswer.Font = new Font("Segoe UI", 10.5F);
			btnShowAnswer.Location = new Point(48, 402);
			btnShowAnswer.Name = "btnShowAnswer";
			btnShowAnswer.Size = new Size(132, 42);
			btnShowAnswer.TabIndex = 5;
			btnShowAnswer.Text = "Show Answer";
			btnShowAnswer.UseVisualStyleBackColor = true;
			btnShowAnswer.Click += btnShowAnswer_Click;
			// 
			// btnKnow
			// 
			btnKnow.Font = new Font("Segoe UI", 10.5F);
			btnKnow.Location = new Point(194, 402);
			btnKnow.Name = "btnKnow";
			btnKnow.Size = new Size(132, 42);
			btnKnow.TabIndex = 6;
			btnKnow.Text = "Know";
			btnKnow.UseVisualStyleBackColor = true;
			btnKnow.Click += btnKnow_Click;
			// 
			// btnDontKnow
			// 
			btnDontKnow.Font = new Font("Segoe UI", 10.5F);
			btnDontKnow.Location = new Point(340, 402);
			btnDontKnow.Name = "btnDontKnow";
			btnDontKnow.Size = new Size(132, 42);
			btnDontKnow.TabIndex = 7;
			btnDontKnow.Text = "Don't Know";
			btnDontKnow.UseVisualStyleBackColor = true;
			btnDontKnow.Click += btnDontKnow_Click;
			// 
			// btnNext
			// 
			btnNext.Font = new Font("Segoe UI", 10.5F);
			btnNext.Location = new Point(486, 402);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(112, 42);
			btnNext.TabIndex = 8;
			btnNext.Text = "Next";
			btnNext.UseVisualStyleBackColor = true;
			btnNext.Click += btnNext_Click;
			// 
			// btnClose
			// 
			btnClose.Font = new Font("Segoe UI", 10.5F);
			btnClose.Location = new Point(620, 402);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(112, 42);
			btnClose.TabIndex = 9;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// FlashcardForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 471);
			Controls.Add(btnClose);
			Controls.Add(btnNext);
			Controls.Add(btnDontKnow);
			Controls.Add(btnKnow);
			Controls.Add(btnShowAnswer);
			Controls.Add(lblAnswer);
			Controls.Add(lblQuestion);
			Controls.Add(chkOnlyDifficult);
			Controls.Add(lblProgress);
			Controls.Add(lblTitle);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "FlashcardForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Flashcard";
			Load += FlashcardForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}
	}
}