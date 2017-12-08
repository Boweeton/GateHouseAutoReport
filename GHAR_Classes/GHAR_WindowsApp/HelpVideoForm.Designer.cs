namespace GHAR_WindowsApp
{
    partial class HelpVideoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpVideoForm));
            this.helpVideoMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.helpVideoMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // helpVideoMediaPlayer
            // 
            this.helpVideoMediaPlayer.Enabled = true;
            this.helpVideoMediaPlayer.Location = new System.Drawing.Point(13, 13);
            this.helpVideoMediaPlayer.Name = "helpVideoMediaPlayer";
            this.helpVideoMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("helpVideoMediaPlayer.OcxState")));
            this.helpVideoMediaPlayer.Size = new System.Drawing.Size(911, 449);
            this.helpVideoMediaPlayer.TabIndex = 0;
            // 
            // HelpVideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 474);
            this.Controls.Add(this.helpVideoMediaPlayer);
            this.Name = "HelpVideoForm";
            this.Text = "HelpForm";
            this.Load += new System.EventHandler(this.HelpVideoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.helpVideoMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer helpVideoMediaPlayer;
    }
}