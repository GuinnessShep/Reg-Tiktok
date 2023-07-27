
namespace Reg_Tiktok
{
    partial class fCheckLive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCheckLive));
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rduidfull = new System.Windows.Forms.RadioButton();
            this.rduid = new System.Windows.Forms.RadioButton();
            this.btnclear = new System.Windows.Forms.Button();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grKhongCheckDuoc = new System.Windows.Forms.GroupBox();
            this.txtKhongCheckDuoc = new System.Windows.Forms.RichTextBox();
            this.grChuaTao = new System.Windows.Forms.GroupBox();
            this.txtDie = new System.Windows.Forms.RichTextBox();
            this.grDaTao = new System.Windows.Forms.GroupBox();
            this.txtLive = new System.Windows.Forms.RichTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            this.grKhongCheckDuoc.SuspendLayout();
            this.grChuaTao.SuspendLayout();
            this.grDaTao.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(457, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 34);
            this.btnAdd.TabIndex = 158;
            this.btnAdd.Text = "Check";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rduidfull);
            this.groupBox1.Controls.Add(this.rduid);
            this.groupBox1.Location = new System.Drawing.Point(971, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 81);
            this.groupBox1.TabIndex = 156;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // rduidfull
            // 
            this.rduidfull.AutoSize = true;
            this.rduidfull.Location = new System.Drawing.Point(17, 48);
            this.rduidfull.Name = "rduidfull";
            this.rduidfull.Size = new System.Drawing.Size(96, 17);
            this.rduidfull.TabIndex = 75;
            this.rduidfull.Text = "Username|XXX";
            this.rduidfull.UseVisualStyleBackColor = true;
            // 
            // rduid
            // 
            this.rduid.AutoSize = true;
            this.rduid.Checked = true;
            this.rduid.Location = new System.Drawing.Point(17, 25);
            this.rduid.Name = "rduid";
            this.rduid.Size = new System.Drawing.Size(73, 17);
            this.rduid.TabIndex = 74;
            this.rduid.TabStop = true;
            this.rduid.Text = "Username";
            this.rduid.UseVisualStyleBackColor = true;
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.Red;
            this.btnclear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnclear.FlatAppearance.BorderSize = 0;
            this.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.ForeColor = System.Drawing.Color.White;
            this.btnclear.Location = new System.Drawing.Point(971, 405);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(135, 36);
            this.btnclear.TabIndex = 155;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // nudThread
            // 
            this.nudThread.Location = new System.Drawing.Point(356, 22);
            this.nudThread.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudThread.Name = "nudThread";
            this.nudThread.Size = new System.Drawing.Size(69, 20);
            this.nudThread.TabIndex = 154;
            this.nudThread.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 153;
            this.label2.Text = "Threads:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(613, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(61, 13);
            this.lblStatus.TabIndex = 152;
            this.lblStatus.Text = "Checking...";
            this.lblStatus.Visible = false;
            // 
            // grKhongCheckDuoc
            // 
            this.grKhongCheckDuoc.Controls.Add(this.txtKhongCheckDuoc);
            this.grKhongCheckDuoc.ForeColor = System.Drawing.Color.Red;
            this.grKhongCheckDuoc.Location = new System.Drawing.Point(744, 53);
            this.grKhongCheckDuoc.Name = "grKhongCheckDuoc";
            this.grKhongCheckDuoc.Size = new System.Drawing.Size(221, 391);
            this.grKhongCheckDuoc.TabIndex = 148;
            this.grKhongCheckDuoc.TabStop = false;
            this.grKhongCheckDuoc.Text = "Can\'t check (0)";
            // 
            // txtKhongCheckDuoc
            // 
            this.txtKhongCheckDuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKhongCheckDuoc.Location = new System.Drawing.Point(3, 16);
            this.txtKhongCheckDuoc.Name = "txtKhongCheckDuoc";
            this.txtKhongCheckDuoc.Size = new System.Drawing.Size(215, 372);
            this.txtKhongCheckDuoc.TabIndex = 50;
            this.txtKhongCheckDuoc.Text = "";
            this.txtKhongCheckDuoc.WordWrap = false;
            this.txtKhongCheckDuoc.TextChanged += new System.EventHandler(this.txtKhongCheckDuoc_TextChanged);
            // 
            // grChuaTao
            // 
            this.grChuaTao.Controls.Add(this.txtDie);
            this.grChuaTao.ForeColor = System.Drawing.Color.DarkRed;
            this.grChuaTao.Location = new System.Drawing.Point(517, 53);
            this.grChuaTao.Name = "grChuaTao";
            this.grChuaTao.Size = new System.Drawing.Size(221, 391);
            this.grChuaTao.TabIndex = 149;
            this.grChuaTao.TabStop = false;
            this.grChuaTao.Text = "DIE (0)";
            // 
            // txtDie
            // 
            this.txtDie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDie.Location = new System.Drawing.Point(3, 16);
            this.txtDie.Name = "txtDie";
            this.txtDie.Size = new System.Drawing.Size(215, 372);
            this.txtDie.TabIndex = 50;
            this.txtDie.Text = "";
            this.txtDie.WordWrap = false;
            this.txtDie.TextChanged += new System.EventHandler(this.txtDie_TextChanged);
            // 
            // grDaTao
            // 
            this.grDaTao.Controls.Add(this.txtLive);
            this.grDaTao.ForeColor = System.Drawing.Color.DarkGreen;
            this.grDaTao.Location = new System.Drawing.Point(290, 53);
            this.grDaTao.Name = "grDaTao";
            this.grDaTao.Size = new System.Drawing.Size(221, 394);
            this.grDaTao.TabIndex = 150;
            this.grDaTao.TabStop = false;
            this.grDaTao.Text = "LIVE (0)";
            // 
            // txtLive
            // 
            this.txtLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLive.Location = new System.Drawing.Point(3, 16);
            this.txtLive.Name = "txtLive";
            this.txtLive.Size = new System.Drawing.Size(215, 375);
            this.txtLive.TabIndex = 50;
            this.txtLive.Text = "";
            this.txtLive.WordWrap = false;
            this.txtLive.TextChanged += new System.EventHandler(this.txtLive_TextChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtInput);
            this.groupBox7.Location = new System.Drawing.Point(2, 2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(282, 445);
            this.groupBox7.TabIndex = 151;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "List Uid (0)";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(0, 12);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(267, 426);
            this.txtInput.TabIndex = 50;
            this.txtInput.Text = "";
            this.txtInput.WordWrap = false;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // fCheckLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 448);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.nudThread);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grKhongCheckDuoc);
            this.Controls.Add(this.grChuaTao);
            this.Controls.Add(this.grDaTao);
            this.Controls.Add(this.groupBox7);
            this.Name = "fCheckLive";
            this.Text = "CheckLive";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            this.grKhongCheckDuoc.ResumeLayout(false);
            this.grChuaTao.ResumeLayout(false);
            this.grDaTao.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rduidfull;
        private System.Windows.Forms.RadioButton rduid;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grKhongCheckDuoc;
        private System.Windows.Forms.RichTextBox txtKhongCheckDuoc;
        private System.Windows.Forms.GroupBox grChuaTao;
        private System.Windows.Forms.RichTextBox txtDie;
        private System.Windows.Forms.GroupBox grDaTao;
        private System.Windows.Forms.RichTextBox txtLive;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox txtInput;
    }
}