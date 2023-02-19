namespace TransmissionAdd
{
    partial class ConfigForm
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
            this.lbServer = new System.Windows.Forms.ListBox();
            this.btnServerAdd = new System.Windows.Forms.Button();
            this.btnDomainAdd = new System.Windows.Forms.Button();
            this.lbDomain = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "서버:";
            // 
            // lbServer
            // 
            this.lbServer.DisplayMember = "Name";
            this.lbServer.FormattingEnabled = true;
            this.lbServer.ItemHeight = 25;
            this.lbServer.Location = new System.Drawing.Point(177, 22);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(702, 204);
            this.lbServer.TabIndex = 4;
            this.lbServer.ValueMember = "ServerId";
            this.lbServer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbServer_MouseDown);
            // 
            // btnServerAdd
            // 
            this.btnServerAdd.Location = new System.Drawing.Point(757, 232);
            this.btnServerAdd.Name = "btnServerAdd";
            this.btnServerAdd.Size = new System.Drawing.Size(120, 48);
            this.btnServerAdd.TabIndex = 7;
            this.btnServerAdd.Text = "추가";
            this.btnServerAdd.UseVisualStyleBackColor = true;
            this.btnServerAdd.Click += new System.EventHandler(this.btnServerAdd_Click);
            // 
            // btnDomainAdd
            // 
            this.btnDomainAdd.Location = new System.Drawing.Point(757, 527);
            this.btnDomainAdd.Name = "btnDomainAdd";
            this.btnDomainAdd.Size = new System.Drawing.Size(120, 48);
            this.btnDomainAdd.TabIndex = 10;
            this.btnDomainAdd.Text = "추가";
            this.btnDomainAdd.UseVisualStyleBackColor = true;
            this.btnDomainAdd.Click += new System.EventHandler(this.btnDomainAdd_Click);
            // 
            // lbDomain
            // 
            this.lbDomain.DisplayMember = "Url";
            this.lbDomain.FormattingEnabled = true;
            this.lbDomain.ItemHeight = 25;
            this.lbDomain.Location = new System.Drawing.Point(177, 317);
            this.lbDomain.Name = "lbDomain";
            this.lbDomain.Size = new System.Drawing.Size(702, 204);
            this.lbDomain.TabIndex = 9;
            this.lbDomain.ValueMember = "Url";
            this.lbDomain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbDomain_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "검색 사이트:";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(17, 593);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(884, 13);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(757, 628);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 48);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(630, 628);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 48);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 695);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDomainAdd);
            this.Controls.Add(this.lbDomain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnServerAdd);
            this.Controls.Add(this.lbServer);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "TransmissionAdd 설정";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbServer;
        private System.Windows.Forms.Button btnServerAdd;
        private System.Windows.Forms.Button btnDomainAdd;
        private System.Windows.Forms.ListBox lbDomain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}