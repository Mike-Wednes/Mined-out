namespace WinFormsUI.Layers
{
    partial class WorkShop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkShop));
            this.FieldArea = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.heightNumeric = new System.Windows.Forms.NumericUpDown();
            this.widthNumeric = new System.Windows.Forms.NumericUpDown();
            this.borderPicture = new System.Windows.Forms.PictureBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldArea
            // 
            this.FieldArea.Image = ((System.Drawing.Image)(resources.GetObject("FieldArea.Image")));
            this.FieldArea.Location = new System.Drawing.Point(283, 133);
            this.FieldArea.Name = "FieldArea";
            this.FieldArea.Size = new System.Drawing.Size(635, 635);
            this.FieldArea.TabIndex = 1;
            this.FieldArea.TabStop = false;
            this.FieldArea.Click += new System.EventHandler(this.FieldArea_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(40, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "heigh";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bauhaus 93", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(40, 375);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "width";
            // 
            // heightNumeric
            // 
            this.heightNumeric.Location = new System.Drawing.Point(40, 310);
            this.heightNumeric.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.heightNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.heightNumeric.Name = "heightNumeric";
            this.heightNumeric.Size = new System.Drawing.Size(84, 27);
            this.heightNumeric.TabIndex = 7;
            this.heightNumeric.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.heightNumeric.ValueChanged += new System.EventHandler(this.heighNumeric_ValueChanged);
            // 
            // widthNumeric
            // 
            this.widthNumeric.Location = new System.Drawing.Point(40, 435);
            this.widthNumeric.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.widthNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.widthNumeric.Name = "widthNumeric";
            this.widthNumeric.Size = new System.Drawing.Size(84, 27);
            this.widthNumeric.TabIndex = 8;
            this.widthNumeric.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.widthNumeric.ValueChanged += new System.EventHandler(this.widthNumeric_ValueChanged);
            // 
            // borderPicture
            // 
            this.borderPicture.Image = ((System.Drawing.Image)(resources.GetObject("borderPicture.Image")));
            this.borderPicture.Location = new System.Drawing.Point(1088, 133);
            this.borderPicture.Name = "borderPicture";
            this.borderPicture.Size = new System.Drawing.Size(60, 60);
            this.borderPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.borderPicture.TabIndex = 9;
            this.borderPicture.TabStop = false;
            this.borderPicture.Click += new System.EventHandler(this.borderPicture_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(40, 180);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(107, 27);
            this.nameBox.TabIndex = 10;
            this.nameBox.Text = "new level";
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bauhaus 93", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(40, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 32);
            this.label3.TabIndex = 11;
            this.label3.Text = "name";
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.saveButton.Font = new System.Drawing.Font("Bauhaus 93", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.saveButton.Location = new System.Drawing.Point(40, 710);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(162, 58);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // openButton
            // 
            this.openButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.openButton.Font = new System.Drawing.Font("Bauhaus 93", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.openButton.Location = new System.Drawing.Point(40, 576);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(162, 58);
            this.openButton.TabIndex = 13;
            this.openButton.Text = "open";
            this.openButton.UseVisualStyleBackColor = false;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // WorkShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.borderPicture);
            this.Controls.Add(this.widthNumeric);
            this.Controls.Add(this.heightNumeric);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FieldArea);
            this.Name = "WorkShop";
            this.Size = new System.Drawing.Size(1200, 900);
            this.Load += new System.EventHandler(this.WorkShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox FieldArea;
        private Label label1;
        private Label label2;
        private NumericUpDown heightNumeric;
        private NumericUpDown widthNumeric;
        private PictureBox borderPicture;
        private TextBox nameBox;
        private Label label3;
        private Button saveButton;
        private Button openButton;
    }
}
