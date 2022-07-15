namespace WinFormsUI.Layers
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
            this.logoTimer = new System.Windows.Forms.Timer(this.components);
            this.GameOverLabel1 = new System.Windows.Forms.Label();
            this.GameOverLabel2 = new System.Windows.Forms.Label();
            this.FinishLabel = new System.Windows.Forms.Label();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.standartButton = new System.Windows.Forms.Button();
            this.customButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldArea
            // 
            this.FieldArea.Image = ((System.Drawing.Image)(resources.GetObject("FieldArea.Image")));
            this.FieldArea.Location = new System.Drawing.Point(283, 133);
            this.FieldArea.Name = "FieldArea";
            this.FieldArea.Size = new System.Drawing.Size(630, 630);
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
            this.HeadLogo.Visible = false;
            // 
            // logoTimer
            // 
            this.logoTimer.Interval = 800;
            this.logoTimer.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.loadingLabel.Visible = false;
            // 
            // standartButton
            // 
            this.standartButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.standartButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.standartButton.Font = new System.Drawing.Font("Bauhaus 93", 40.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.standartButton.Location = new System.Drawing.Point(434, 158);
            this.standartButton.Name = "standartButton";
            this.standartButton.Size = new System.Drawing.Size(339, 160);
            this.standartButton.TabIndex = 7;
            this.standartButton.Text = "standart";
            this.standartButton.UseVisualStyleBackColor = false;
            this.standartButton.Click += new System.EventHandler(this.standartButton_Click);
            // 
            // customButton
            // 
            this.customButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.customButton.Font = new System.Drawing.Font("Bauhaus 93", 40.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.customButton.Location = new System.Drawing.Point(434, 498);
            this.customButton.Name = "customButton";
            this.customButton.Size = new System.Drawing.Size(339, 160);
            this.customButton.TabIndex = 8;
            this.customButton.Text = "custom";
            this.customButton.UseVisualStyleBackColor = false;
            this.customButton.Click += new System.EventHandler(this.customButton_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.customButton);
            this.Controls.Add(this.standartButton);
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
        private System.Windows.Forms.Timer logoTimer;
        private Label GameOverLabel1;
        private Label GameOverLabel2;
        private Label FinishLabel;
        private Label loadingLabel;
        private Button standartButton;
        private Button customButton;
    }
}
