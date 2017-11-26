namespace GHAR_WindowsApp
{
    partial class ManualPathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualPathForm));
            this.calculateButton = new System.Windows.Forms.Button();
            this.otherArrivalsReportPathTextBox = new System.Windows.Forms.TextBox();
            this.toursReportPathGroupBox = new System.Windows.Forms.GroupBox();
            this.toursReportPathCopyButton = new System.Windows.Forms.Button();
            this.toursReportPathTextBox = new System.Windows.Forms.TextBox();
            this.otherArrivalsReportPathGroupBox = new System.Windows.Forms.GroupBox();
            this.otherArrivalsReportPathCopyButton = new System.Windows.Forms.Button();
            this.toursReportPathGroupBox.SuspendLayout();
            this.otherArrivalsReportPathGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(304, 124);
            this.calculateButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(78, 37);
            this.calculateButton.TabIndex = 0;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.OnCalculateButtonClick);
            // 
            // otherArrivalsReportPathTextBox
            // 
            this.otherArrivalsReportPathTextBox.BackColor = System.Drawing.Color.White;
            this.otherArrivalsReportPathTextBox.Location = new System.Drawing.Point(5, 18);
            this.otherArrivalsReportPathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.otherArrivalsReportPathTextBox.Name = "otherArrivalsReportPathTextBox";
            this.otherArrivalsReportPathTextBox.ReadOnly = true;
            this.otherArrivalsReportPathTextBox.Size = new System.Drawing.Size(258, 20);
            this.otherArrivalsReportPathTextBox.TabIndex = 1;
            this.otherArrivalsReportPathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            // 
            // toursReportPathGroupBox
            // 
            this.toursReportPathGroupBox.Controls.Add(this.toursReportPathCopyButton);
            this.toursReportPathGroupBox.Controls.Add(this.toursReportPathTextBox);
            this.toursReportPathGroupBox.Location = new System.Drawing.Point(10, 11);
            this.toursReportPathGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toursReportPathGroupBox.Name = "toursReportPathGroupBox";
            this.toursReportPathGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toursReportPathGroupBox.Size = new System.Drawing.Size(379, 47);
            this.toursReportPathGroupBox.TabIndex = 2;
            this.toursReportPathGroupBox.TabStop = false;
            this.toursReportPathGroupBox.Text = "Tours Report Path";
            // 
            // toursReportPathCopyButton
            // 
            this.toursReportPathCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toursReportPathCopyButton.Location = new System.Drawing.Point(270, 14);
            this.toursReportPathCopyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toursReportPathCopyButton.Name = "toursReportPathCopyButton";
            this.toursReportPathCopyButton.Size = new System.Drawing.Size(104, 25);
            this.toursReportPathCopyButton.TabIndex = 3;
            this.toursReportPathCopyButton.Text = "Copy to Clipboard";
            this.toursReportPathCopyButton.UseVisualStyleBackColor = true;
            this.toursReportPathCopyButton.Click += new System.EventHandler(this.OnToursReportPathCopyButtonClick);
            // 
            // toursReportPathTextBox
            // 
            this.toursReportPathTextBox.BackColor = System.Drawing.Color.White;
            this.toursReportPathTextBox.Location = new System.Drawing.Point(4, 18);
            this.toursReportPathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toursReportPathTextBox.Name = "toursReportPathTextBox";
            this.toursReportPathTextBox.ReadOnly = true;
            this.toursReportPathTextBox.Size = new System.Drawing.Size(258, 20);
            this.toursReportPathTextBox.TabIndex = 1;
            this.toursReportPathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            // 
            // otherArrivalsReportPathGroupBox
            // 
            this.otherArrivalsReportPathGroupBox.Controls.Add(this.otherArrivalsReportPathCopyButton);
            this.otherArrivalsReportPathGroupBox.Controls.Add(this.otherArrivalsReportPathTextBox);
            this.otherArrivalsReportPathGroupBox.Location = new System.Drawing.Point(9, 63);
            this.otherArrivalsReportPathGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.otherArrivalsReportPathGroupBox.Name = "otherArrivalsReportPathGroupBox";
            this.otherArrivalsReportPathGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.otherArrivalsReportPathGroupBox.Size = new System.Drawing.Size(379, 47);
            this.otherArrivalsReportPathGroupBox.TabIndex = 2;
            this.otherArrivalsReportPathGroupBox.TabStop = false;
            this.otherArrivalsReportPathGroupBox.Text = "Other Arrivals Report Path";
            // 
            // otherArrivalsReportPathCopyButton
            // 
            this.otherArrivalsReportPathCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otherArrivalsReportPathCopyButton.Location = new System.Drawing.Point(270, 14);
            this.otherArrivalsReportPathCopyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.otherArrivalsReportPathCopyButton.Name = "otherArrivalsReportPathCopyButton";
            this.otherArrivalsReportPathCopyButton.Size = new System.Drawing.Size(104, 25);
            this.otherArrivalsReportPathCopyButton.TabIndex = 3;
            this.otherArrivalsReportPathCopyButton.Text = "Copy to Clipboard";
            this.otherArrivalsReportPathCopyButton.UseVisualStyleBackColor = true;
            this.otherArrivalsReportPathCopyButton.Click += new System.EventHandler(this.OnOtherArrivalsReportPathCopyButtonClick);
            // 
            // ManualPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 171);
            this.Controls.Add(this.otherArrivalsReportPathGroupBox);
            this.Controls.Add(this.toursReportPathGroupBox);
            this.Controls.Add(this.calculateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualPathForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manually Generate Path";
            this.Load += new System.EventHandler(this.ManualPathForm_Load);
            this.toursReportPathGroupBox.ResumeLayout(false);
            this.toursReportPathGroupBox.PerformLayout();
            this.otherArrivalsReportPathGroupBox.ResumeLayout(false);
            this.otherArrivalsReportPathGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.TextBox otherArrivalsReportPathTextBox;
        private System.Windows.Forms.GroupBox toursReportPathGroupBox;
        private System.Windows.Forms.Button toursReportPathCopyButton;
        private System.Windows.Forms.GroupBox otherArrivalsReportPathGroupBox;
        private System.Windows.Forms.Button otherArrivalsReportPathCopyButton;
        private System.Windows.Forms.TextBox toursReportPathTextBox;
    }
}