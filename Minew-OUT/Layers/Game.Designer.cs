namespace Minew_OUT.Layers
{
    partial class Game
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FieldArea = new System.Windows.Forms.PictureBox();
            this.HeadPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldArea
            // 
            this.FieldArea.Location = new System.Drawing.Point(283, 133);
            this.FieldArea.Name = "FieldArea";
            this.FieldArea.Size = new System.Drawing.Size(635, 635);
            this.FieldArea.TabIndex = 0;
            this.FieldArea.TabStop = false;
            // 
            // HeadPicture
            // 
            this.HeadPicture.Location = new System.Drawing.Point(283, 30);
            this.HeadPicture.Name = "HeadPicture";
            this.HeadPicture.Size = new System.Drawing.Size(635, 97);
            this.HeadPicture.TabIndex = 1;
            this.HeadPicture.TabStop = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.HeadPicture);
            this.Controls.Add(this.FieldArea);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(1200, 900);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox FieldArea;
        private PictureBox HeadPicture;
    }
}
