﻿namespace WinFormsUI.Layers
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.visibilityPicture = new System.Windows.Forms.PictureBox();
            this.fieldParametersGroup = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuStip = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPicture = new System.Windows.Forms.PictureBox();
            this.tools = new System.Windows.Forms.GroupBox();
            this.highlighterBox1 = new System.Windows.Forms.PictureBox();
            this.highlighterBox2 = new System.Windows.Forms.PictureBox();
            this.cursors = new System.Windows.Forms.GroupBox();
            this.lineCursorBox = new System.Windows.Forms.PictureBox();
            this.dotCursorBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visibilityPicture)).BeginInit();
            this.fieldParametersGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearPicture)).BeginInit();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.highlighterBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlighterBox2)).BeginInit();
            this.cursors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lineCursorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotCursorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldArea
            // 
            this.FieldArea.Image = ((System.Drawing.Image)(resources.GetObject("FieldArea.Image")));
            this.FieldArea.Location = new System.Drawing.Point(283, 133);
            this.FieldArea.Name = "FieldArea";
            this.FieldArea.Size = new System.Drawing.Size(630, 630);
            this.FieldArea.TabIndex = 1;
            this.FieldArea.TabStop = false;
            this.FieldArea.Click += new System.EventHandler(this.FieldArea_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(19, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bauhaus 93", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(19, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "width";
            // 
            // heightNumeric
            // 
            this.heightNumeric.Location = new System.Drawing.Point(25, 217);
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
            this.heightNumeric.Size = new System.Drawing.Size(47, 27);
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
            this.widthNumeric.Location = new System.Drawing.Point(25, 346);
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
            this.widthNumeric.Size = new System.Drawing.Size(47, 27);
            this.widthNumeric.TabIndex = 8;
            this.widthNumeric.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.widthNumeric.ValueChanged += new System.EventHandler(this.widthNumeric_ValueChanged);
            // 
            // nameBox
            // 
            this.nameBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nameBox.Location = new System.Drawing.Point(19, 83);
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
            this.label3.Location = new System.Drawing.Point(19, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 32);
            this.label3.TabIndex = 11;
            this.label3.Text = "name";
            // 
            // typeBox
            // 
            this.typeBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "Border",
            "Mine",
            "Space",
            "Finish",
            "Player"});
            this.typeBox.Location = new System.Drawing.Point(19, 87);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(127, 28);
            this.typeBox.TabIndex = 15;
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.typeBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bauhaus 93", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(19, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 32);
            this.label4.TabIndex = 16;
            this.label4.Text = "add cell";
            // 
            // visibilityPicture
            // 
            this.visibilityPicture.Image = ((System.Drawing.Image)(resources.GetObject("visibilityPicture.Image")));
            this.visibilityPicture.Location = new System.Drawing.Point(76, 290);
            this.visibilityPicture.Name = "visibilityPicture";
            this.visibilityPicture.Size = new System.Drawing.Size(60, 60);
            this.visibilityPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.visibilityPicture.TabIndex = 17;
            this.visibilityPicture.TabStop = false;
            this.visibilityPicture.Click += new System.EventHandler(this.visibilityPicture_Click);
            // 
            // fieldParametersGroup
            // 
            this.fieldParametersGroup.Controls.Add(this.label3);
            this.fieldParametersGroup.Controls.Add(this.nameBox);
            this.fieldParametersGroup.Controls.Add(this.label2);
            this.fieldParametersGroup.Controls.Add(this.label1);
            this.fieldParametersGroup.Controls.Add(this.heightNumeric);
            this.fieldParametersGroup.Controls.Add(this.widthNumeric);
            this.fieldParametersGroup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.fieldParametersGroup.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fieldParametersGroup.Location = new System.Drawing.Point(40, 70);
            this.fieldParametersGroup.Name = "fieldParametersGroup";
            this.fieldParametersGroup.Size = new System.Drawing.Size(160, 400);
            this.fieldParametersGroup.TabIndex = 19;
            this.fieldParametersGroup.TabStop = false;
            this.fieldParametersGroup.Text = "field parameters";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backMenuItem,
            this.OpenMenuStrip,
            this.saveMenuStip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 28);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backMenuItem
            // 
            this.backMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.backMenuItem.Name = "backMenuItem";
            this.backMenuItem.Size = new System.Drawing.Size(56, 24);
            this.backMenuItem.Text = "Back";
            this.backMenuItem.Click += new System.EventHandler(this.backMenuItem_Click);
            // 
            // OpenMenuStrip
            // 
            this.OpenMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.OpenMenuStrip.Name = "OpenMenuStrip";
            this.OpenMenuStrip.Size = new System.Drawing.Size(60, 24);
            this.OpenMenuStrip.Text = "Open";
            this.OpenMenuStrip.Click += new System.EventHandler(this.OpenMenuStrip_Click);
            // 
            // saveMenuStip
            // 
            this.saveMenuStip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveMenuStip.Name = "saveMenuStip";
            this.saveMenuStip.Size = new System.Drawing.Size(55, 24);
            this.saveMenuStip.Text = "Save";
            this.saveMenuStip.Click += new System.EventHandler(this.saveMenuStip_Click);
            // 
            // clearPicture
            // 
            this.clearPicture.Image = ((System.Drawing.Image)(resources.GetObject("clearPicture.Image")));
            this.clearPicture.Location = new System.Drawing.Point(76, 159);
            this.clearPicture.Name = "clearPicture";
            this.clearPicture.Size = new System.Drawing.Size(60, 60);
            this.clearPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.clearPicture.TabIndex = 22;
            this.clearPicture.TabStop = false;
            this.clearPicture.Click += new System.EventHandler(this.clearPicture_Click);
            // 
            // tools
            // 
            this.tools.Controls.Add(this.label4);
            this.tools.Controls.Add(this.visibilityPicture);
            this.tools.Controls.Add(this.typeBox);
            this.tools.Controls.Add(this.clearPicture);
            this.tools.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tools.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tools.Location = new System.Drawing.Point(983, 70);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(160, 400);
            this.tools.TabIndex = 21;
            this.tools.TabStop = false;
            this.tools.Text = "tools";
            // 
            // highlighterBox1
            // 
            this.highlighterBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.highlighterBox1.Location = new System.Drawing.Point(722, 517);
            this.highlighterBox1.Name = "highlighterBox1";
            this.highlighterBox1.Size = new System.Drawing.Size(116, 127);
            this.highlighterBox1.TabIndex = 22;
            this.highlighterBox1.TabStop = false;
            this.highlighterBox1.Visible = false;
            // 
            // highlighterBox2
            // 
            this.highlighterBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.highlighterBox2.Location = new System.Drawing.Point(796, 229);
            this.highlighterBox2.Name = "highlighterBox2";
            this.highlighterBox2.Size = new System.Drawing.Size(55, 68);
            this.highlighterBox2.TabIndex = 23;
            this.highlighterBox2.TabStop = false;
            this.highlighterBox2.Visible = false;
            // 
            // cursors
            // 
            this.cursors.Controls.Add(this.lineCursorBox);
            this.cursors.Controls.Add(this.dotCursorBox);
            this.cursors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cursors.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cursors.Location = new System.Drawing.Point(983, 506);
            this.cursors.Name = "cursors";
            this.cursors.Size = new System.Drawing.Size(160, 257);
            this.cursors.TabIndex = 23;
            this.cursors.TabStop = false;
            this.cursors.Text = "cursors";
            // 
            // lineCursorBox
            // 
            this.lineCursorBox.Image = ((System.Drawing.Image)(resources.GetObject("lineCursorBox.Image")));
            this.lineCursorBox.Location = new System.Drawing.Point(76, 166);
            this.lineCursorBox.Name = "lineCursorBox";
            this.lineCursorBox.Size = new System.Drawing.Size(60, 60);
            this.lineCursorBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.lineCursorBox.TabIndex = 17;
            this.lineCursorBox.TabStop = false;
            this.lineCursorBox.Click += new System.EventHandler(this.lineCursorBox_Click);
            // 
            // dotCursorBox
            // 
            this.dotCursorBox.Image = ((System.Drawing.Image)(resources.GetObject("dotCursorBox.Image")));
            this.dotCursorBox.Location = new System.Drawing.Point(76, 43);
            this.dotCursorBox.Name = "dotCursorBox";
            this.dotCursorBox.Size = new System.Drawing.Size(60, 60);
            this.dotCursorBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dotCursorBox.TabIndex = 22;
            this.dotCursorBox.TabStop = false;
            this.dotCursorBox.Click += new System.EventHandler(this.dotCursorBox_Click);
            // 
            // WorkShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.cursors);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.fieldParametersGroup);
            this.Controls.Add(this.FieldArea);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.highlighterBox2);
            this.Controls.Add(this.highlighterBox1);
            this.Name = "WorkShop";
            this.Size = new System.Drawing.Size(1200, 900);
            this.Load += new System.EventHandler(this.WorkShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FieldArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visibilityPicture)).EndInit();
            this.fieldParametersGroup.ResumeLayout(false);
            this.fieldParametersGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearPicture)).EndInit();
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.highlighterBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlighterBox2)).EndInit();
            this.cursors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lineCursorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotCursorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox FieldArea;
        private Label label1;
        private Label label2;
        private NumericUpDown heightNumeric;
        private NumericUpDown widthNumeric;
        private TextBox nameBox;
        private Label label3;
        private ComboBox typeBox;
        private Label label4;
        private PictureBox visibilityPicture;
        private GroupBox fieldParametersGroup;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem backMenuItem;
        private ToolStripMenuItem OpenMenuStrip;
        private ToolStripMenuItem saveMenuStip;
        private PictureBox clearPicture;
        private GroupBox tools;
        private PictureBox highlighterBox1;
        private PictureBox highlighterBox2;
        private GroupBox cursors;
        private PictureBox lineCursorBox;
        private PictureBox dotCursorBox;
    }
}