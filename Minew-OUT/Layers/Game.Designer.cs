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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.FieldArea = new System.Windows.Forms.PictureBox();
            this.HeadLogo = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GameOverLabel1 = new System.Windows.Forms.Label();
            this.GameOverLabel2 = new System.Windows.Forms.Label();
            this.FinishLabel = new System.Windows.Forms.Label();
            this.loadingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldArea
            // 
            this.FieldArea.Image = ((System.Drawing.Image)(resources.GetObject("FieldArea.Image")));
            this.FieldArea.Location = new System.Drawing.Point(283, 133);
            this.FieldArea.Name = "FieldArea";
            this.FieldArea.Size = new System.Drawing.Size(635, 635);
            this.FieldArea.TabIndex = 0;
            this.FieldArea.TabStop = false;
            this.FieldArea.Visible = false;
            // 
            // HeadLogo
            // 
            this.HeadLogo.Location = new System.Drawing.Point(283, 30);
            this.HeadLogo.Name = "HeadLogo";
            this.HeadLogo.Size = new System.Drawing.Size(635, 97);
            this.HeadLogo.TabIndex = 1;
            this.HeadLogo.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 800;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameOverLabel1
            // 
            this.GameOverLabel1.AutoSize = true;
            this.GameOverLabel1.Font = new System.Drawing.Font("Bauhaus 93", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameOverLabel1.ForeColor = System.Drawing.Color.Red;
            this.GameOverLabel1.Location = new System.Drawing.Point(303, 185);
            this.GameOverLabel1.Name = "GameOverLabel1";
            this.GameOverLabel1.Size = new System.Drawing.Size(594, 227);
            this.GameOverLabel1.TabIndex = 3;
            this.GameOverLabel1.Text = "game\r\n";
            this.GameOverLabel1.Visible = false;
            // 
            // GameOverLabel2
            // 
            this.GameOverLabel2.AutoSize = true;
            this.GameOverLabel2.Font = new System.Drawing.Font("Bauhaus 93", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameOverLabel2.ForeColor = System.Drawing.Color.Red;
            this.GameOverLabel2.Location = new System.Drawing.Point(360, 434);
            this.GameOverLabel2.Name = "GameOverLabel2";
            this.GameOverLabel2.Size = new System.Drawing.Size(459, 227);
            this.GameOverLabel2.TabIndex = 4;
            this.GameOverLabel2.Text = "over";
            this.GameOverLabel2.Visible = false;
            // 
            // FinishLabel
            // 
            this.FinishLabel.AutoSize = true;
            this.FinishLabel.Font = new System.Drawing.Font("Bauhaus 93", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FinishLabel.ForeColor = System.Drawing.Color.Yellow;
            this.FinishLabel.Location = new System.Drawing.Point(303, 243);
            this.FinishLabel.Name = "FinishLabel";
            this.FinishLabel.Size = new System.Drawing.Size(568, 227);
            this.FinishLabel.TabIndex = 5;
            this.FinishLabel.Text = "finish";
            this.FinishLabel.Visible = false;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.BackColor = System.Drawing.SystemColors.Desktop;
            this.loadingLabel.Font = new System.Drawing.Font("Bauhaus 93", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loadingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.loadingLabel.Location = new System.Drawing.Point(112, 268);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(1008, 227);
            this.loadingLabel.TabIndex = 6;
            this.loadingLabel.Text = "loading...";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.FinishLabel);
            this.Controls.Add(this.GameOverLabel2);
            this.Controls.Add(this.GameOverLabel1);
            this.Controls.Add(this.HeadLogo);
            this.Controls.Add(this.FieldArea);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(1200, 900);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox FieldArea;
        private PictureBox HeadLogo;
        private System.Windows.Forms.Timer timer1;
        private Label GameOverLabel1;
        private Label GameOverLabel2;
        private Label FinishLabel;
        private Label loadingLabel;
    }
}
