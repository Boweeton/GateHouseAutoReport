namespace GHAR_WindowsApp
{
    partial class MainScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreenForm));
            this.laodTodaysGuestlistButton = new System.Windows.Forms.Button();
            this.createToursAndTeasButton = new System.Windows.Forms.Button();
            this.createOvernightsButton = new System.Windows.Forms.Button();
            this.lastRunTextBox = new System.Windows.Forms.TextBox();
            this.buttonsGroup = new System.Windows.Forms.GroupBox();
            this.lastRunBox = new System.Windows.Forms.GroupBox();
            this.buttonsGroup.SuspendLayout();
            this.lastRunBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // laodTodaysGuestlistButton
            // 
            this.laodTodaysGuestlistButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.laodTodaysGuestlistButton.Location = new System.Drawing.Point(6, 22);
            this.laodTodaysGuestlistButton.Name = "laodTodaysGuestlistButton";
            this.laodTodaysGuestlistButton.Size = new System.Drawing.Size(228, 53);
            this.laodTodaysGuestlistButton.TabIndex = 0;
            this.laodTodaysGuestlistButton.Text = "Load Today\'s Guests";
            this.laodTodaysGuestlistButton.UseVisualStyleBackColor = false;
            this.laodTodaysGuestlistButton.Click += new System.EventHandler(this.OnLaodTodaysGuestlistButtonClick);
            // 
            // createToursAndTeasButton
            // 
            this.createToursAndTeasButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.createToursAndTeasButton.Location = new System.Drawing.Point(6, 81);
            this.createToursAndTeasButton.Name = "createToursAndTeasButton";
            this.createToursAndTeasButton.Size = new System.Drawing.Size(228, 53);
            this.createToursAndTeasButton.TabIndex = 1;
            this.createToursAndTeasButton.Text = "Create Tours and Teas Report";
            this.createToursAndTeasButton.UseVisualStyleBackColor = false;
            this.createToursAndTeasButton.Click += new System.EventHandler(this.OnCreateToursAndTeasButtonClick);
            // 
            // createOvernightsButton
            // 
            this.createOvernightsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.createOvernightsButton.Location = new System.Drawing.Point(6, 140);
            this.createOvernightsButton.Name = "createOvernightsButton";
            this.createOvernightsButton.Size = new System.Drawing.Size(228, 53);
            this.createOvernightsButton.TabIndex = 2;
            this.createOvernightsButton.Text = "Create Overnights Report";
            this.createOvernightsButton.UseVisualStyleBackColor = false;
            this.createOvernightsButton.Click += new System.EventHandler(this.OnCreateOvernightsButtonClick);
            // 
            // lastRunTextBox
            // 
            this.lastRunTextBox.BackColor = System.Drawing.Color.White;
            this.lastRunTextBox.Location = new System.Drawing.Point(6, 21);
            this.lastRunTextBox.Name = "lastRunTextBox";
            this.lastRunTextBox.ReadOnly = true;
            this.lastRunTextBox.Size = new System.Drawing.Size(180, 22);
            this.lastRunTextBox.TabIndex = 0;
            // 
            // buttonsGroup
            // 
            this.buttonsGroup.Controls.Add(this.createToursAndTeasButton);
            this.buttonsGroup.Controls.Add(this.laodTodaysGuestlistButton);
            this.buttonsGroup.Controls.Add(this.createOvernightsButton);
            this.buttonsGroup.Location = new System.Drawing.Point(211, 12);
            this.buttonsGroup.Name = "buttonsGroup";
            this.buttonsGroup.Size = new System.Drawing.Size(240, 202);
            this.buttonsGroup.TabIndex = 4;
            this.buttonsGroup.TabStop = false;
            this.buttonsGroup.Text = "Commands";
            // 
            // lastRunBox
            // 
            this.lastRunBox.Controls.Add(this.lastRunTextBox);
            this.lastRunBox.Location = new System.Drawing.Point(12, 12);
            this.lastRunBox.Name = "lastRunBox";
            this.lastRunBox.Size = new System.Drawing.Size(193, 53);
            this.lastRunBox.TabIndex = 5;
            this.lastRunBox.TabStop = false;
            this.lastRunBox.Text = "Last Run On";
            // 
            // MainScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 228);
            this.Controls.Add(this.lastRunBox);
            this.Controls.Add(this.buttonsGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gate House Auto-Reporter";
            this.Load += new System.EventHandler(this.OnProgramLoad);
            this.buttonsGroup.ResumeLayout(false);
            this.lastRunBox.ResumeLayout(false);
            this.lastRunBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button laodTodaysGuestlistButton;
        private System.Windows.Forms.Button createToursAndTeasButton;
        private System.Windows.Forms.Button createOvernightsButton;
        private System.Windows.Forms.TextBox lastRunTextBox;
        private System.Windows.Forms.GroupBox buttonsGroup;
        private System.Windows.Forms.GroupBox lastRunBox;
    }
}

