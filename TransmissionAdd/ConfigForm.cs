using Microsoft.VisualBasic.ApplicationServices;
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
        private bool _dataChanged = false;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            UserConfig.Load();
            if (UserConfig.Settings != null)
            {
                if (UserConfig.Settings.Servers != null)
                {
                    lbServer.DataSource = UserConfig.Settings.Servers;
                }
                if (UserConfig.Settings.Domains != null)
                {
                    lbDomain.DataSource= UserConfig.Settings.Domains;
                }
            }
        }

        private void btnServerAdd_Click(object sender, EventArgs e)
        {
            var serverInfo = UpdateServer(null);
            if (serverInfo!= null)
            {
                lbServer.Items.Add(serverInfo);
            }
        }

        private ServerInfo UpdateServer(string serverId)
        {
            using (var form = new ServerConfigForm())
            {
                if (serverId != null)
                {
                    form.ServerInfo = UserConfig.Settings.Servers.Find(x => x.ServerId.Equals(serverId));
                }
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (String.IsNullOrWhiteSpace(serverId))
                    {
                        UserConfig.Settings.Servers.Add(form.ServerInfo);
                    }
                    return form.ServerInfo;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
