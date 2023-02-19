using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransmissionAdd
{
    public partial class DomainConfigForm : Form
    {
        public List<ServerInfo> Servers { get; set; }

        public DomainInfo DomainInfo { get; set; }

        public DomainConfigForm()
        {
            InitializeComponent();
        }

        private void DomainConfigForm_Load(object sender, EventArgs e)
        {
            if (this.Servers == null || this.Servers.Count == 0)
            {
                cbServers.Enabled = false;
                tbDomain.Enabled = false;
                btnOK.Enabled = false;
                btnCancel.Enabled = true;
                return;
            }

            cbServers.DataSource = Servers;
            if (this.DomainInfo != null)
            {
                cbServers.SelectedValue = this.DomainInfo.ServerId;
                tbDomain.Text = this.DomainInfo.Domain;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbDomain.Text))
            {
                MessageBox.Show("Domain을 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.DomainInfo == null)
            {
                this.DomainInfo = new DomainInfo()
                {
                    Domain = tbDomain.Text.Trim(),
                    ServerId = (cbServers.SelectedItem as ServerInfo).ServerId
                };
            }
            else
            {
                this.DomainInfo.Domain = tbDomain.Text.Trim();
                this.DomainInfo.ServerId = (cbServers.SelectedItem as ServerInfo).ServerId;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
