﻿namespace GHAR_WindowsApp
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
            this.createToursAndTeasButton = new System.Windows.Forms.Button();
            this.createOvernightsButton = new System.Windows.Forms.Button();
            this.lastRunTextBox = new System.Windows.Forms.TextBox();
            this.buttonsGroup = new System.Windows.Forms.GroupBox();
            this.manuallyGeneratePathButton = new System.Windows.Forms.Button();
            this.lastRunBox = new System.Windows.Forms.GroupBox();
            this.nothingChangedMessage = new System.Windows.Forms.TextBox();
            this.resultsDisplayGroupBox = new System.Windows.Forms.GroupBox();
            this.testButton = new System.Windows.Forms.Button();
            this.buttonsGroup.SuspendLayout();
            this.lastRunBox.SuspendLayout();
            this.resultsDisplayGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // createToursAndTeasButton
            // 
            this.createToursAndTeasButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.createToursAndTeasButton.Location = new System.Drawing.Point(4, 75);
            this.createToursAndTeasButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createToursAndTeasButton.Name = "createToursAndTeasButton";
            this.createToursAndTeasButton.Size = new System.Drawing.Size(171, 43);
            this.createToursAndTeasButton.TabIndex = 1;
            this.createToursAndTeasButton.Text = "Create Tours and Teas Report";
            this.createToursAndTeasButton.UseVisualStyleBackColor = false;
            this.createToursAndTeasButton.Click += new System.EventHandler(this.OnCreateToursAndTeasButtonClick);
            // 
            // createOvernightsButton
            // 
            this.createOvernightsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.createOvernightsButton.Location = new System.Drawing.Point(4, 123);
            this.createOvernightsButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createOvernightsButton.Name = "createOvernightsButton";
            this.createOvernightsButton.Size = new System.Drawing.Size(171, 43);
            this.createOvernightsButton.TabIndex = 2;
            this.createOvernightsButton.Text = "Create Overnights Report";
            this.createOvernightsButton.UseVisualStyleBackColor = false;
            this.createOvernightsButton.Click += new System.EventHandler(this.OnCreateOvernightsButtonClick);
            // 
            // lastRunTextBox
            // 
            this.lastRunTextBox.BackColor = System.Drawing.Color.White;
            this.lastRunTextBox.Location = new System.Drawing.Point(4, 17);
            this.lastRunTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastRunTextBox.Name = "lastRunTextBox";
            this.lastRunTextBox.ReadOnly = true;
            this.lastRunTextBox.Size = new System.Drawing.Size(171, 20);
            this.lastRunTextBox.TabIndex = 0;
            this.lastRunTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonsGroup
            // 
            this.buttonsGroup.Controls.Add(this.createToursAndTeasButton);
            this.buttonsGroup.Controls.Add(this.manuallyGeneratePathButton);
            this.buttonsGroup.Controls.Add(this.createOvernightsButton);
            this.buttonsGroup.Location = new System.Drawing.Point(20, 116);
            this.buttonsGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonsGroup.Name = "buttonsGroup";
            this.buttonsGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonsGroup.Size = new System.Drawing.Size(180, 175);
            this.buttonsGroup.TabIndex = 4;
            this.buttonsGroup.TabStop = false;
            this.buttonsGroup.Text = "Commands";
            // 
            // manuallyGeneratePathButton
            // 
            this.manuallyGeneratePathButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.manuallyGeneratePathButton.Location = new System.Drawing.Point(4, 28);
            this.manuallyGeneratePathButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.manuallyGeneratePathButton.Name = "manuallyGeneratePathButton";
            this.manuallyGeneratePathButton.Size = new System.Drawing.Size(171, 43);
            this.manuallyGeneratePathButton.TabIndex = 0;
            this.manuallyGeneratePathButton.Text = "Manually Generate Path";
            this.manuallyGeneratePathButton.UseVisualStyleBackColor = false;
            this.manuallyGeneratePathButton.Click += new System.EventHandler(this.OnManuallyGeneratePathButtonClick);
            // 
            // lastRunBox
            // 
            this.lastRunBox.Controls.Add(this.lastRunTextBox);
            this.lastRunBox.Location = new System.Drawing.Point(20, 14);
            this.lastRunBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastRunBox.Name = "lastRunBox";
            this.lastRunBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastRunBox.Size = new System.Drawing.Size(180, 43);
            this.lastRunBox.TabIndex = 5;
            this.lastRunBox.TabStop = false;
            this.lastRunBox.Text = "Last Run At";
            // 
            // nothingChangedMessage
            // 
            this.nothingChangedMessage.Location = new System.Drawing.Point(4, 19);
            this.nothingChangedMessage.Name = "nothingChangedMessage";
            this.nothingChangedMessage.Size = new System.Drawing.Size(171, 20);
            this.nothingChangedMessage.TabIndex = 7;
            this.nothingChangedMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // resultsDisplayGroupBox
            // 
            this.resultsDisplayGroupBox.Controls.Add(this.nothingChangedMessage);
            this.resultsDisplayGroupBox.Location = new System.Drawing.Point(20, 62);
            this.resultsDisplayGroupBox.Name = "resultsDisplayGroupBox";
            this.resultsDisplayGroupBox.Size = new System.Drawing.Size(180, 49);
            this.resultsDisplayGroupBox.TabIndex = 8;
            this.resultsDisplayGroupBox.TabStop = false;
            this.resultsDisplayGroupBox.Text = "Results";
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(46, 314);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 9;
            this.testButton.Text = "test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // MainScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 394);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.resultsDisplayGroupBox);
            this.Controls.Add(this.lastRunBox);
            this.Controls.Add(this.buttonsGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "G.H.A.R.";
            this.Load += new System.EventHandler(this.OnProgramLoad);
            this.buttonsGroup.ResumeLayout(false);
            this.lastRunBox.ResumeLayout(false);
            this.lastRunBox.PerformLayout();
            this.resultsDisplayGroupBox.ResumeLayout(false);
            this.resultsDisplayGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button createToursAndTeasButton;
        private System.Windows.Forms.Button createOvernightsButton;
        private System.Windows.Forms.TextBox lastRunTextBox;
        private System.Windows.Forms.GroupBox buttonsGroup;
        private System.Windows.Forms.GroupBox lastRunBox;
        private System.Windows.Forms.TextBox nothingChangedMessage;
        private System.Windows.Forms.GroupBox resultsDisplayGroupBox;
        private System.Windows.Forms.Button manuallyGeneratePathButton;
        private System.Windows.Forms.Button testButton;
    }
}

