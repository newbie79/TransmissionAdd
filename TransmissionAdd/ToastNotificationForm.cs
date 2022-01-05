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
    public partial class ToastNotificationForm : Form
    {
        public ToastNotificationForm(string message, int showSecond = 5)
        {
            InitializeComponent();

            timerClose.Interval = showSecond * 1000;
            lblMessage.Text = message;
        }

        public ToastNotificationForm(string message, Color fontColor, Color bgColor, int showSecond = 5)
        {
            InitializeComponent();

            timerClose.Interval = showSecond * 1000;
            ForeColor = fontColor;
            BackColor = bgColor;
            lblMessage.Text = message;
        }

        private void ToastNotification_Load(object sender, EventArgs e)
        {
            Top = 20;
            Left = Screen.PrimaryScreen.Bounds.Width - Width - 20;

            timerClose.Start();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
