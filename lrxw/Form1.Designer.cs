namespace lrxw
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.importRadio = new System.Windows.Forms.RadioButton();
            this.alignRadio = new System.Windows.Forms.RadioButton();
            this.exportRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceLocRes = new System.Windows.Forms.TextBox();
            this.sourceButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.targetLocRes = new System.Windows.Forms.TextBox();
            this.targetButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.xliff = new System.Windows.Forms.TextBox();
            this.xliffButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.sourceLang = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.targetLang = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.locResFormat = new System.Windows.Forms.ComboBox();
            this.runButton = new System.Windows.Forms.Button();
            this.sourceOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.targetOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.sourceSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.targetSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.xliffOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.xliffSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Operation:";
            // 
            // importRadio
            // 
            this.importRadio.AutoSize = true;
            this.importRadio.Checked = true;
            this.importRadio.Location = new System.Drawing.Point(30, 3);
            this.importRadio.Name = "importRadio";
            this.importRadio.Size = new System.Drawing.Size(310, 21);
            this.importRadio.TabIndex = 5;
            this.importRadio.TabStop = true;
            this.importRadio.Text = "Import one LocRes (of source language) to XLIFF";
            this.importRadio.UseVisualStyleBackColor = true;
            this.importRadio.CheckedChanged += new System.EventHandler(this.operation_CheckedChanged);
            // 
            // alignRadio
            // 
            this.alignRadio.AutoSize = true;
            this.alignRadio.Location = new System.Drawing.Point(30, 30);
            this.alignRadio.Name = "alignRadio";
            this.alignRadio.Size = new System.Drawing.Size(359, 21);
            this.alignRadio.TabIndex = 6;
            this.alignRadio.Text = "Align and import two LocRes (source and target) to XLIFF";
            this.alignRadio.UseVisualStyleBackColor = true;
            this.alignRadio.CheckedChanged += new System.EventHandler(this.operation_CheckedChanged);
            // 
            // exportRadio
            // 
            this.exportRadio.AutoSize = true;
            this.exportRadio.Location = new System.Drawing.Point(30, 57);
            this.exportRadio.Name = "exportRadio";
            this.exportRadio.Size = new System.Drawing.Size(231, 21);
            this.exportRadio.TabIndex = 7;
            this.exportRadio.Text = "Export translated XLIFF to a LocRes";
            this.exportRadio.UseVisualStyleBackColor = true;
            this.exportRadio.CheckedChanged += new System.EventHandler(this.operation_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 8;
            this.label2.Tag = "IA";
            this.label2.Text = "Source LocRes:";
            // 
            // sourceLocRes
            // 
            this.sourceLocRes.Location = new System.Drawing.Point(42, 164);
            this.sourceLocRes.Name = "sourceLocRes";
            this.sourceLocRes.ReadOnly = true;
            this.sourceLocRes.Size = new System.Drawing.Size(486, 25);
            this.sourceLocRes.TabIndex = 9;
            this.sourceLocRes.Tag = "IA";
            // 
            // sourceButton
            // 
            this.sourceButton.Location = new System.Drawing.Point(534, 164);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(40, 23);
            this.sourceButton.TabIndex = 10;
            this.sourceButton.Tag = "IA";
            this.sourceButton.Text = "...";
            this.sourceButton.UseVisualStyleBackColor = true;
            this.sourceButton.Click += new System.EventHandler(this.sourceButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 11;
            this.label3.Tag = "AE";
            this.label3.Text = "Target LocRes:";
            // 
            // targetLocRes
            // 
            this.targetLocRes.Location = new System.Drawing.Point(42, 212);
            this.targetLocRes.Name = "targetLocRes";
            this.targetLocRes.ReadOnly = true;
            this.targetLocRes.Size = new System.Drawing.Size(486, 25);
            this.targetLocRes.TabIndex = 12;
            this.targetLocRes.Tag = "AE";
            // 
            // targetButton
            // 
            this.targetButton.Location = new System.Drawing.Point(534, 212);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(40, 23);
            this.targetButton.TabIndex = 13;
            this.targetButton.Tag = "AE";
            this.targetButton.Text = "...";
            this.targetButton.UseVisualStyleBackColor = true;
            this.targetButton.Click += new System.EventHandler(this.targetButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 14;
            this.label4.Tag = "IAE";
            this.label4.Text = "XLIFF:";
            // 
            // xliff
            // 
            this.xliff.Location = new System.Drawing.Point(42, 260);
            this.xliff.Name = "xliff";
            this.xliff.ReadOnly = true;
            this.xliff.Size = new System.Drawing.Size(486, 25);
            this.xliff.TabIndex = 15;
            this.xliff.Tag = "IAE";
            // 
            // xliffButton
            // 
            this.xliffButton.Location = new System.Drawing.Point(534, 260);
            this.xliffButton.Name = "xliffButton";
            this.xliffButton.Size = new System.Drawing.Size(40, 23);
            this.xliffButton.TabIndex = 16;
            this.xliffButton.Tag = "IAE";
            this.xliffButton.Text = "...";
            this.xliffButton.UseVisualStyleBackColor = true;
            this.xliffButton.Click += new System.EventHandler(this.xliffButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 288);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 17);
            this.label5.TabIndex = 17;
            this.label5.Tag = "IA";
            this.label5.Text = "Source language code:";
            // 
            // sourceLang
            // 
            this.sourceLang.Location = new System.Drawing.Point(64, 308);
            this.sourceLang.Name = "sourceLang";
            this.sourceLang.Size = new System.Drawing.Size(100, 25);
            this.sourceLang.TabIndex = 18;
            this.sourceLang.Tag = "IA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 17);
            this.label6.TabIndex = 19;
            this.label6.Tag = "IA";
            this.label6.Text = "Target language code:";
            // 
            // targetLang
            // 
            this.targetLang.Location = new System.Drawing.Point(64, 356);
            this.targetLang.Name = "targetLang";
            this.targetLang.Size = new System.Drawing.Size(100, 25);
            this.targetLang.TabIndex = 20;
            this.targetLang.Tag = "IA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 392);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 21;
            this.label7.Tag = "E";
            this.label7.Text = "LocRes format:";
            // 
            // locResFormat
            // 
            this.locResFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.locResFormat.Items.AddRange(new object[] {
            "Default",
            "Force old format",
            "Force new format"});
            this.locResFormat.Location = new System.Drawing.Point(42, 412);
            this.locResFormat.Name = "locResFormat";
            this.locResFormat.Size = new System.Drawing.Size(486, 25);
            this.locResFormat.TabIndex = 22;
            this.locResFormat.Tag = "E";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(497, 476);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 23;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // sourceOpenDialog
            // 
            this.sourceOpenDialog.Filter = "LocRes|*.locres";
            // 
            // targetOpenDialog
            // 
            this.targetOpenDialog.Filter = "LocRes|*.locres";
            // 
            // sourceSaveDialog
            // 
            this.sourceSaveDialog.DefaultExt = "locres";
            this.sourceSaveDialog.Filter = "LocRes|*.locres";
            // 
            // targetSaveDialog
            // 
            this.targetSaveDialog.DefaultExt = "locres";
            this.targetSaveDialog.Filter = "LocRes|*.locres";
            // 
            // xliffOpenDialog
            // 
            this.xliffOpenDialog.Filter = "XLIFF|*.xliff";
            // 
            // xliffSaveDialog
            // 
            this.xliffSaveDialog.DefaultExt = "xliff";
            this.xliffSaveDialog.Filter = "XLIFF|*.xliff";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.importRadio);
            this.panel1.Controls.Add(this.alignRadio);
            this.panel1.Controls.Add(this.exportRadio);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 83);
            this.panel1.TabIndex = 24;
            // 
            // Form1
            // 
            this.AcceptButton = this.runButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 511);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.locResFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.targetLang);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sourceLang);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.xliffButton);
            this.Controls.Add(this.xliff);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.targetButton);
            this.Controls.Add(this.targetLocRes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sourceButton);
            this.Controls.Add(this.sourceLocRes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "lrxw";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton importRadio;
        private System.Windows.Forms.RadioButton alignRadio;
        private System.Windows.Forms.RadioButton exportRadio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sourceLocRes;
        private System.Windows.Forms.Button sourceButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox targetLocRes;
        private System.Windows.Forms.Button targetButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox xliff;
        private System.Windows.Forms.Button xliffButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sourceLang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox targetLang;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox locResFormat;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.OpenFileDialog sourceOpenDialog;
        private System.Windows.Forms.OpenFileDialog targetOpenDialog;
        private System.Windows.Forms.SaveFileDialog sourceSaveDialog;
        private System.Windows.Forms.SaveFileDialog targetSaveDialog;
        private System.Windows.Forms.OpenFileDialog xliffOpenDialog;
        private System.Windows.Forms.SaveFileDialog xliffSaveDialog;
        private System.Windows.Forms.Panel panel1;
    }
}

