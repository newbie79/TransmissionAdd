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
        private ContextMenuStrip _lbServerContextMenu;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // lbServer Context menu
            var lbServerEditMenu = new ToolStripMenuItem { Text = "수정" };
            lbServerEditMenu.Click += lbServerEditMenu_Click;
            var lbServerDelMenu = new ToolStripMenuItem { Text = "삭제" };
            lbServerDelMenu.Click += lbServerDelMenu_Click;
            _lbServerContextMenu = new ContextMenuStrip();
            _lbServerContextMenu.Items.AddRange(new ToolStripItem[] { lbServerEditMenu, lbServerDelMenu });

            UserConfig.Load();

            if (UserConfig.Settings != null)
            {
                if (UserConfig.Settings.Servers != null)
                {
                    foreach (var serverInfo in UserConfig.Settings.Servers)
                    {
                        lbServer.Items.Add(serverInfo);
                    }
                }
                if (UserConfig.Settings.Domains != null)
                {
                    foreach (var domainInfo in UserConfig.Settings.Domains)
                    {
                        lbDomain.Items.Add(domainInfo);
                    }
                }
            }
        }

        private void lbServer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lbServer.SelectedIndex = lbServer.IndexFromPoint(e.Location);
                if (lbServer.SelectedIndex != -1)
                {
                    _lbServerContextMenu.Show();
                }
            }
        }

        private void lbServerEditMenu_Click(object sender, EventArgs e)
        {

        }

        private void lbServerDelMenu_Click(object sender, EventArgs e)
        {

        }

        private void btnServerAdd_Click(object sender, EventArgs e)
        {
            var serverInfo = UpdateServer(null);
            if (serverInfo != null)
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
                    _dataChanged = true;
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_dataChanged)
            {
                UserConfig.Save();
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
