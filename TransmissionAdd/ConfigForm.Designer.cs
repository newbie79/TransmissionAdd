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
            label1 = new System.Windows.Forms.Label();
            lbServer = new System.Windows.Forms.ListBox();
            btnServerAdd = new System.Windows.Forms.Button();
            btnDomainAdd = new System.Windows.Forms.Button();
            lbDomain = new System.Windows.Forms.ListBox();
            label2 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(25, 13);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 15);
            label1.TabIndex = 3;
            label1.Text = "서버:";
            // 
            // lbServer
            // 
            lbServer.DisplayMember = "Name";
            lbServer.FormattingEnabled = true;
            lbServer.ItemHeight = 15;
            lbServer.Location = new System.Drawing.Point(124, 13);
            lbServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            lbServer.Name = "lbServer";
            lbServer.Size = new System.Drawing.Size(493, 124);
            lbServer.TabIndex = 4;
            lbServer.ValueMember = "ServerId";
            lbServer.MouseDown += lbServer_MouseDown;
            // 
            // btnServerAdd
            // 
            btnServerAdd.Location = new System.Drawing.Point(530, 139);
            btnServerAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnServerAdd.Name = "btnServerAdd";
            btnServerAdd.Size = new System.Drawing.Size(84, 29);
            btnServerAdd.TabIndex = 7;
            btnServerAdd.Text = "추가";
            btnServerAdd.UseVisualStyleBackColor = true;
            btnServerAdd.Click += btnServerAdd_Click;
            // 
            // btnDomainAdd
            // 
            btnDomainAdd.Location = new System.Drawing.Point(530, 316);
            btnDomainAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnDomainAdd.Name = "btnDomainAdd";
            btnDomainAdd.Size = new System.Drawing.Size(84, 29);
            btnDomainAdd.TabIndex = 10;
            btnDomainAdd.Text = "추가";
            btnDomainAdd.UseVisualStyleBackColor = true;
            btnDomainAdd.Click += btnDomainAdd_Click;
            // 
            // lbDomain
            // 
            lbDomain.DisplayMember = "Domain";
            lbDomain.FormattingEnabled = true;
            lbDomain.ItemHeight = 15;
            lbDomain.Location = new System.Drawing.Point(124, 190);
            lbDomain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            lbDomain.Name = "lbDomain";
            lbDomain.Size = new System.Drawing.Size(493, 124);
            lbDomain.TabIndex = 9;
            lbDomain.ValueMember = "Domain";
            lbDomain.MouseDown += lbDomain_MouseDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(25, 190);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(74, 15);
            label2.TabIndex = 8;
            label2.Text = "검색 사이트:";
            // 
            // groupBox1
            // 
            groupBox1.Location = new System.Drawing.Point(12, 356);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(619, 8);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(530, 377);
            btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(84, 29);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(441, 377);
            btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(84, 29);
            btnOK.TabIndex = 12;
            btnOK.Text = "확인";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(643, 417);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(groupBox1);
            Controls.Add(btnDomainAdd);
            Controls.Add(lbDomain);
            Controls.Add(label2);
            Controls.Add(btnServerAdd);
            Controls.Add(lbServer);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            Text = "TransmissionAdd 설정";
            Load += ConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
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