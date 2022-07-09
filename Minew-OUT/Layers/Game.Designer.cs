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
            this.FieldArea = new System.Windows.Forms.PictureBox();
            this.HeadPicture = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GameOverLabel1 = new System.Windows.Forms.Label();
            this.GameOverLabel2 = new System.Windows.Forms.Label();
            this.FinishLabel = new System.Windows.Forms.Label();
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
            this.FieldArea.Paint += new System.Windows.Forms.PaintEventHandler(this.FieldArea_Paint_1);
            // 
            // HeadPicture
            // 
            this.HeadPicture.Location = new System.Drawing.Point(283, 30);
            this.HeadPicture.Name = "HeadPicture";
            this.HeadPicture.Size = new System.Drawing.Size(635, 97);
            this.HeadPicture.TabIndex = 1;
            this.HeadPicture.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Font = new System.Drawing.Font("Bauhaus 93", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(44, 614);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 152);
            this.button1.TabIndex = 2;
            this.button1.Text = "click here";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.FinishLabel);
            this.Controls.Add(this.GameOverLabel2);
            this.Controls.Add(this.GameOverLabel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HeadPicture);
            this.Controls.Add(this.FieldArea);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(1200, 900);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox FieldArea;
        private PictureBox HeadPicture;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
        private Label GameOverLabel1;
        private Label GameOverLabel2;
        private Label FinishLabel;
    }
}
