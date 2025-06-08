namespace AviatieQuiz.App.Views
{
    partial class MainForm
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
            this.lblWelcomeMessage = new System.Windows.Forms.Label();
            this.lstDisciplines = new System.Windows.Forms.ListBox();
            this.btnStartQuiz = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnReloadQuestions = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWelcomeMessage
            // 
            this.lblWelcomeMessage.AutoSize = true;
            this.lblWelcomeMessage.Location = new System.Drawing.Point(202, 72);
            this.lblWelcomeMessage.Name = "lblWelcomeMessage";
            this.lblWelcomeMessage.Size = new System.Drawing.Size(0, 16);
            this.lblWelcomeMessage.TabIndex = 0;
            // 
            // lstDisciplines
            // 
            this.lstDisciplines.FormattingEnabled = true;
            this.lstDisciplines.ItemHeight = 16;
            this.lstDisciplines.Location = new System.Drawing.Point(210, 102);
            this.lstDisciplines.Name = "lstDisciplines";
            this.lstDisciplines.Size = new System.Drawing.Size(380, 212);
            this.lstDisciplines.TabIndex = 1;
            // 
            // btnStartQuiz
            // 
            this.btnStartQuiz.Location = new System.Drawing.Point(296, 351);
            this.btnStartQuiz.Name = "btnStartQuiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(214, 70);
            this.btnStartQuiz.TabIndex = 2;
            this.btnStartQuiz.Text = "Incepe Testul";
            this.btnStartQuiz.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(639, 28);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(149, 25);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Delogare";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(654, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 70);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ajutor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnReloadQuestions
            // 
            this.btnReloadQuestions.Location = new System.Drawing.Point(17, 369);
            this.btnReloadQuestions.Name = "btnReloadQuestions";
            this.btnReloadQuestions.Size = new System.Drawing.Size(134, 52);
            this.btnReloadQuestions.TabIndex = 6;
            this.btnReloadQuestions.Text = "Reincarca Intrebarile";
            this.btnReloadQuestions.UseVisualStyleBackColor = true;
            this.btnReloadQuestions.Visible = false;
            this.btnReloadQuestions.Click += new System.EventHandler(this.btnReloadQuestions_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Bine ai venit!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(291, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Selecteaza Disciplina";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReloadQuestions);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnStartQuiz);
            this.Controls.Add(this.lstDisciplines);
            this.Controls.Add(this.lblWelcomeMessage);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcomeMessage;
        private System.Windows.Forms.ListBox lstDisciplines;
        private System.Windows.Forms.Button btnStartQuiz;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReloadQuestions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}