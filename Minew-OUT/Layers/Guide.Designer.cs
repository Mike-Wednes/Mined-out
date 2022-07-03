namespace Minew_OUT.Layers
{
    partial class Guide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Guide));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.introducingLabel = new System.Windows.Forms.Label();
            this.MovingLabel = new System.Windows.Forms.Label();
            this.MinesCountLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.MarkLabel = new System.Windows.Forms.Label();
            this.LastLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(130, 169);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 357);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // NextButton
            // 
            this.NextButton.AutoSize = true;
            this.NextButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.NextButton.Font = new System.Drawing.Font("Bauhaus 93", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NextButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NextButton.Location = new System.Drawing.Point(478, 663);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(220, 105);
            this.NextButton.TabIndex = 1;
            this.NextButton.Text = "Click";
            this.NextButton.UseVisualStyleBackColor = false;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // introducingLabel
            // 
            this.introducingLabel.AutoSize = true;
            this.introducingLabel.Font = new System.Drawing.Font("Bauhaus 93", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.introducingLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.introducingLabel.Location = new System.Drawing.Point(187, 324);
            this.introducingLabel.Name = "introducingLabel";
            this.introducingLabel.Size = new System.Drawing.Size(823, 68);
            this.introducingLabel.TabIndex = 2;
            this.introducingLabel.Text = "Let me introduce a little guide";
            this.introducingLabel.Visible = false;
            // 
            // MovingLabel
            // 
            this.MovingLabel.AutoSize = true;
            this.MovingLabel.Font = new System.Drawing.Font("Bauhaus 93", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovingLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MovingLabel.Location = new System.Drawing.Point(404, 217);
            this.MovingLabel.Name = "MovingLabel";
            this.MovingLabel.Size = new System.Drawing.Size(577, 53);
            this.MovingLabel.TabIndex = 3;
            this.MovingLabel.Text = "you can move using arrows";
            this.MovingLabel.Visible = false;
            // 
            // MinesCountLabel
            // 
            this.MinesCountLabel.AutoEllipsis = true;
            this.MinesCountLabel.AutoSize = true;
            this.MinesCountLabel.Font = new System.Drawing.Font("Bauhaus 93", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinesCountLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MinesCountLabel.Location = new System.Drawing.Point(404, 392);
            this.MinesCountLabel.Name = "MinesCountLabel";
            this.MinesCountLabel.Size = new System.Drawing.Size(555, 106);
            this.MinesCountLabel.TabIndex = 4;
            this.MinesCountLabel.Text = "your number shows mines \r\namount near you";
            this.MinesCountLabel.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(818, 262);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(154, 149);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // MarkLabel
            // 
            this.MarkLabel.AutoSize = true;
            this.MarkLabel.Font = new System.Drawing.Font("Bauhaus 93", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MarkLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MarkLabel.Location = new System.Drawing.Point(238, 286);
            this.MarkLabel.Name = "MarkLabel";
            this.MarkLabel.Size = new System.Drawing.Size(397, 106);
            this.MarkLabel.TabIndex = 6;
            this.MarkLabel.Text = "use WASD to mark\r\npotensional mines";
            this.MarkLabel.Visible = false;
            // 
            // LastLabel
            // 
            this.LastLabel.AutoSize = true;
            this.LastLabel.Font = new System.Drawing.Font("Bauhaus 93", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LastLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LastLabel.Location = new System.Drawing.Point(261, 274);
            this.LastLabel.Name = "LastLabel";
            this.LastLabel.Size = new System.Drawing.Size(670, 136);
            this.LastLabel.TabIndex = 7;
            this.LastLabel.Text = "           good luck\r\nwaiting you at the finish";
            this.LastLabel.Visible = false;
            // 
            // Guide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.LastLabel);
            this.Controls.Add(this.MarkLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.MinesCountLabel);
            this.Controls.Add(this.MovingLabel);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.introducingLabel);
            this.Name = "Guide";
            this.Size = new System.Drawing.Size(1200, 900);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Button NextButton;
        private Label introducingLabel;
        private Label MovingLabel;
        private Label MinesCountLabel;
        private PictureBox pictureBox2;
        private Label MarkLabel;
        private Label LastLabel;
    }
}
