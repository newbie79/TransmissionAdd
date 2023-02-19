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
        private ContextMenuStrip _lbDomainContextMenu;
        private List<ServerInfo> _servers = null;
        private List<DomainInfo> _domains = null;
        public bool ShowServerConfigForm { get; set; }

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

            // lbDomain Context menu
            var lbDomainEditMenu = new ToolStripMenuItem { Text = "수정" };
            lbDomainEditMenu.Click += lbDomainEditMenu_Click;
            var lbDomainDelMenu = new ToolStripMenuItem { Text = "삭제" };
            lbDomainDelMenu.Click += lbDomainDelMenu_Click;
            _lbDomainContextMenu = new ContextMenuStrip();
            _lbDomainContextMenu.Items.AddRange(new ToolStripItem[] { lbDomainEditMenu, lbDomainDelMenu });
            lbDomain.ContextMenuStrip = _lbDomainContextMenu;

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
                        Domain = x.Domain,
                        ServerId = x.ServerId
                    });

                    foreach (var domainInfo in _domains)
                    {
                        lbDomain.Items.Add(domainInfo);
                    }
                }
            }

            if (this.ShowServerConfigForm)
            {
                var serverInfo = UpdateServer(null);
                if (serverInfo != null)
                {
                    lbServer.Items.Add(serverInfo);
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
            var serverInfo = lbServer.SelectedItem as ServerInfo;
            if (serverInfo != null)
            {
                var newServerInfo = UpdateServer(serverInfo);
                if (newServerInfo != null)
                {
                    lbServer.Items[lbServer.SelectedIndex] = newServerInfo;
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
            var serverInfo = lbServer.SelectedItem as ServerInfo;
            if (serverInfo != null)
            {
                lbServer.Items.Remove(serverInfo);
                _servers.Remove(serverInfo);
                _dataChanged = true;
            }
        }

        private void lbDomain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lbDomain.SelectedIndex = lbDomain.IndexFromPoint(e.Location);
                if (lbDomain.SelectedIndex != -1)
                {
                    _lbDomainContextMenu.Show();
                }
            }
        }

        /// <summary>
        /// 도메인 정보 수정 이벤트 핸들러
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lbDomainEditMenu_Click(object sender, EventArgs e)
        {
            var domainInfo = lbDomain.SelectedItem as DomainInfo;
            if (domainInfo != null)
            {
                var newDomainInfo = UpdateDomain(domainInfo);
                if (newDomainInfo != null)
                {
                    lbDomain.Items[lbDomain.SelectedIndex] = newDomainInfo;
                }
            }
        }

        /// <summary>
        /// 도메인 정보 삭제 이벤트 핸들러
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lbDomainDelMenu_Click(object sender, EventArgs e)
        {
            var domainInfo = lbDomain.SelectedItem as DomainInfo;
            if (domainInfo != null)
            {
                lbDomain.Items.Remove(domainInfo);
                _domains.Remove(domainInfo);
                _dataChanged = true;
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

        private ServerInfo UpdateServer(ServerInfo serverInfo)
        {
            using (var form = new ServerConfigForm())
            {
                if (serverInfo != null)
                {
                    form.ServerInfo = serverInfo;
                }
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _dataChanged = true;
                    if (serverInfo == null)
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

        private void btnDomainAdd_Click(object sender, EventArgs e)
        {
            var domainInfo = UpdateDomain(null);
            if (domainInfo != null)
            {
                lbDomain.Items.Add(domainInfo);
            }
        }

        private DomainInfo UpdateDomain(DomainInfo domainInfo)
        {
            using (var form = new DomainConfigForm())
            {
                form.Servers = _servers;
                if (domainInfo != null)
                {
                    form.DomainInfo = domainInfo;
                }
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _dataChanged = true;
                    if (domainInfo == null)
                    {
                        _domains.Add(form.DomainInfo);
                    }
                    return form.DomainInfo;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
