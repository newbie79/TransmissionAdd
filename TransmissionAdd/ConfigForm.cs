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
    public partial class ConfigForm : Form
    {
        public string TransmissionUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbUrl.Text))
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

            this.TransmissionUrl = tbUrl.Text.Trim();
            this.Username = tbUsername.Text.Trim();
            this.Password = tbPassword.Text.Trim();

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
            tbUrl.Text = this.TransmissionUrl;
            tbUsername.Text = this.Username;
        }
    }
}
