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
        public ToastNotificationForm(string message, int displayTime = 5)
        {
            InitializeComponent();
            Init(message, Color.White, Color.FromArgb(0, 120, 215), displayTime);
        }

        public ToastNotificationForm(string message, Color fontColor, Color bgColor, int displayTime = 5)
        {
            InitializeComponent();
            Init(message, fontColor, bgColor, displayTime);
        }

        private void Init(string message, Color fontColor, Color bgColor, int displayTime)
        {
            lblMessage.Text = message;
            ForeColor = fontColor;
            BackColor = bgColor;
            timerClose.Interval = displayTime * 1000;
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

        private void ToastNotificationForm_Click(object sender, EventArgs e)
        {
            timerClose.Stop();
            Close();
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
            timerClose.Stop();
            Close();
        }
    }
}
