using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransmissionAdd
{
    public partial class ServerConfigForm : Form
    {
        public ServerInfo ServerInfo { get; set; }

        public ServerConfigForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Transmission Name을 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (String.IsNullOrWhiteSpace(tbUrl.Text))
            {
                MessageBox.Show("Transmission Url을 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (String.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show("Username을 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (String.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password을 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.ServerInfo == null)
            {
                this.ServerInfo = new ServerInfo()
                {
                    ServerId = Guid.NewGuid().ToString(),
                    Name = tbName.Text.Trim(),
                    Url = tbUrl.Text.Trim(),
                    Username = tbUsername.Text.Trim(),
                    Password = tbPassword.Text.Trim()
                };
            }
            else
            {
                this.ServerInfo.Name = tbName.Text.Trim();
                this.ServerInfo.Url = tbUrl.Text.Trim();
                this.ServerInfo.Username = tbUsername.Text.Trim();
                this.ServerInfo.Password = tbPassword.Text.Trim();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            if (this.ServerInfo != null)
            {
                tbName.Text = this.ServerInfo.Name;
                tbUrl.Text = this.ServerInfo.Url;
                tbUsername.Text = this.ServerInfo.Username;
            }
        }
    }
}
