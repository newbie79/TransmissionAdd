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
    public partial class ServerSelectForm : Form
    {
        public ServerInfo ServerInfo { get; set; }

        public ServerSelectForm()
        {
            InitializeComponent();
        }

        private void ServerSelectForm_Load(object sender, EventArgs e)
        {
            cbServers.DataSource = UserConfig.Settings.Servers;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ServerInfo = cbServers.SelectedItem as ServerInfo;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
