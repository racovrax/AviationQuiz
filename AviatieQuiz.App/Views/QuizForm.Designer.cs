namespace AviatieQuiz.App.Views
{
    partial class QuizForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblQuestionText = new System.Windows.Forms.Label();
            this.lblQuestionProgress = new System.Windows.Forms.Label();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.btnNextQuestion = new System.Windows.Forms.Button();
            this.lblTimerDisplay = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblLiveScoreDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblQuestionText
            // 
            this.lblQuestionText.AutoSize = true;
            this.lblQuestionText.Location = new System.Drawing.Point(53, 77);
            this.lblQuestionText.Name = "lblQuestionText";
            this.lblQuestionText.Size = new System.Drawing.Size(80, 16);
            this.lblQuestionText.TabIndex = 0;
            this.lblQuestionText.Text = "textIntrebare";
            // 
            // lblQuestionProgress
            // 
            this.lblQuestionProgress.AutoSize = true;
            this.lblQuestionProgress.Location = new System.Drawing.Point(331, 40);
            this.lblQuestionProgress.Name = "lblQuestionProgress";
            this.lblQuestionProgress.Size = new System.Drawing.Size(88, 16);
            this.lblQuestionProgress.TabIndex = 1;
            this.lblQuestionProgress.Text = "Intrebarea x/y";
            // 
            // lblFeedback
            // 
            this.lblFeedback.AutoSize = true;
            this.lblFeedback.Location = new System.Drawing.Point(69, 466);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(64, 16);
            this.lblFeedback.TabIndex = 2;
            this.lblFeedback.Text = "feedback";
            this.lblFeedback.Visible = false;
            // 
            // gbOptions
            // 
            this.gbOptions.Location = new System.Drawing.Point(56, 109);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(683, 252);
            this.gbOptions.TabIndex = 3;
            this.gbOptions.TabStop = false;
            // 
            // btnNextQuestion
            // 
            this.btnNextQuestion.Location = new System.Drawing.Point(273, 397);
            this.btnNextQuestion.Name = "btnNextQuestion";
            this.btnNextQuestion.Size = new System.Drawing.Size(254, 38);
            this.btnNextQuestion.TabIndex = 5;
            this.btnNextQuestion.Text = "Urmatoarea Intrebare";
            this.btnNextQuestion.UseVisualStyleBackColor = true;
            // 
            // lblTimerDisplay
            // 
            this.lblTimerDisplay.AutoSize = true;
            this.lblTimerDisplay.Location = new System.Drawing.Point(740, 20);
            this.lblTimerDisplay.Name = "lblTimerDisplay";
            this.lblTimerDisplay.Size = new System.Drawing.Size(38, 16);
            this.lblTimerDisplay.TabIndex = 6;
            this.lblTimerDisplay.Text = "30:00";
            this.lblTimerDisplay.Visible = false;
            this.lblTimerDisplay.Click += new System.EventHandler(this.lblTimerDisplay_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // lblLiveScoreDisplay
            // 
            this.lblLiveScoreDisplay.AutoSize = true;
            this.lblLiveScoreDisplay.Location = new System.Drawing.Point(28, 20);
            this.lblLiveScoreDisplay.Name = "lblLiveScoreDisplay";
            this.lblLiveScoreDisplay.Size = new System.Drawing.Size(33, 16);
            this.lblLiveScoreDisplay.TabIndex = 7;
            this.lblLiveScoreDisplay.Text = "scor";
            // 
            // QuizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 513);
            this.Controls.Add(this.lblLiveScoreDisplay);
            this.Controls.Add(this.lblTimerDisplay);
            this.Controls.Add(this.btnNextQuestion);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.lblQuestionProgress);
            this.Controls.Add(this.lblQuestionText);
            this.Name = "QuizForm";
            this.Text = "QuizForm";
            this.Load += new System.EventHandler(this.QuizForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.Label lblQuestionProgress;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Button btnNextQuestion;
        private System.Windows.Forms.Label lblTimerDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblLiveScoreDisplay;
    }
}