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
        private List<ServerInfo> _servers = null;
        private List<DomainInfo> _domains = null;

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
            lbServer.ContextMenuStrip= _lbServerContextMenu;

            UserConfig.Load();

            if (UserConfig.Settings != null)
            {
                if (UserConfig.Settings.Servers != null)
                {
                    _servers = UserConfig.Settings.Servers.ConvertAll(x => new ServerInfo()
                    {
                        ServerId = x.ServerId,
                        Name = x.Name,
                        Url = x.Url,
                        Username = x.Username,
                        Password = x.Password
                    });

                    foreach (var serverInfo in _servers)
                    {
                        lbServer.Items.Add(serverInfo);
                    }
                }
                if (UserConfig.Settings.Domains != null)
                {
                    _domains = UserConfig.Settings.Domains.ConvertAll(x => new DomainInfo()
                    {
                        Url = x.Url,
                        ServerId = x.ServerId
                    });

                    foreach (var domainInfo in _domains)
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

        /// <summary>
        /// 서버 정보 수정 이벤트 핸들러
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lbServerEditMenu_Click(object sender, EventArgs e)
        {
            int index = lbServer.SelectedIndex;
            if (index >= 0)
            {
                string serverId = lbServer.SelectedValue.ToString();
                var serverInfo = UpdateServer(serverId);
                if (serverInfo != null)
                {
                    lbServer.Text = serverInfo.Name;
                }
            }
        }

        /// <summary>
        /// 서버 정보 삭제 이벤트 핸들러
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lbServerDelMenu_Click(object sender, EventArgs e)
        {
            int index = lbServer.SelectedIndex;
            if (index >= 0)
            {
                lbServer.Items.RemoveAt(index);
                string serverId = lbServer.SelectedValue.ToString();
                int itemIndex = _servers.FindIndex(x => serverId.Equals(x.ServerId));
                _servers.RemoveAt(itemIndex);
            }
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
                    form.ServerInfo = _servers.Find(x => x.ServerId.Equals(serverId));
                }
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _dataChanged = true;
                    if (String.IsNullOrWhiteSpace(serverId))
                    {
                        _servers.Add(form.ServerInfo);
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
                UserConfig.Settings.Servers = _servers;
                UserConfig.Settings.Domains = _domains;
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
