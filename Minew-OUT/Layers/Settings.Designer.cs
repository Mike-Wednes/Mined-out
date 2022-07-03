namespace Minew_OUT.Layers
{
    partial class Settings
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
            this.SizeLabel = new System.Windows.Forms.Label();
            this.DifficultyLabel = new System.Windows.Forms.Label();
            this.difficultyTrackBar = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.sizeTrackBar = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.difficultyTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Font = new System.Drawing.Font("Bauhaus 93", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SizeLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SizeLabel.Location = new System.Drawing.Point(210, 189);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(359, 91);
            this.SizeLabel.TabIndex = 0;
            this.SizeLabel.Text = "field size";
            // 
            // DifficultyLabel
            // 
            this.DifficultyLabel.AutoSize = true;
            this.DifficultyLabel.Font = new System.Drawing.Font("Bauhaus 93", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DifficultyLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DifficultyLabel.Location = new System.Drawing.Point(210, 487);
            this.DifficultyLabel.Name = "DifficultyLabel";
            this.DifficultyLabel.Size = new System.Drawing.Size(368, 91);
            this.DifficultyLabel.TabIndex = 1;
            this.DifficultyLabel.Text = "difficulty";
            // 
            // difficultyTrackBar
            // 
            this.difficultyTrackBar.LargeChange = 1;
            this.difficultyTrackBar.Location = new System.Drawing.Point(727, 510);
            this.difficultyTrackBar.Maximum = 3;
            this.difficultyTrackBar.Minimum = 1;
            this.difficultyTrackBar.Name = "difficultyTrackBar";
            this.difficultyTrackBar.Size = new System.Drawing.Size(285, 56);
            this.difficultyTrackBar.TabIndex = 4;
            this.difficultyTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.difficultyTrackBar.Value = 1;
            this.difficultyTrackBar.Scroll += new System.EventHandler(this.difficultyTrackBar_Scroll);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(721, 501);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(298, 71);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // sizeTrackBar
            // 
            this.sizeTrackBar.LargeChange = 1;
            this.sizeTrackBar.Location = new System.Drawing.Point(727, 210);
            this.sizeTrackBar.Maximum = 3;
            this.sizeTrackBar.Minimum = 1;
            this.sizeTrackBar.Name = "sizeTrackBar";
            this.sizeTrackBar.Size = new System.Drawing.Size(285, 56);
            this.sizeTrackBar.TabIndex = 6;
            this.sizeTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sizeTrackBar.Value = 1;
            this.sizeTrackBar.Scroll += new System.EventHandler(this.sizeTrackBar_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(721, 201);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 71);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Fuchsia;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.backButton.Font = new System.Drawing.Font("Bauhaus 93", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.backButton.Location = new System.Drawing.Point(51, 678);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(296, 89);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.sizeTrackBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.difficultyTrackBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.DifficultyLabel);
            this.Controls.Add(this.SizeLabel);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(1200, 900);
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.difficultyTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label SizeLabel;
        private Label DifficultyLabel;
        private TrackBar difficultyTrackBar;
        private PictureBox pictureBox2;
        private TrackBar sizeTrackBar;
        private PictureBox pictureBox1;
        private Button backButton;
    }
}
