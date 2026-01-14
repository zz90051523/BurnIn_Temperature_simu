using System;
using System.Drawing;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace BurnIn_Temperature_simu
{
    public partial class UpdateForm : Form
    {
        private UpdateInfoEventArgs _args;

        public UpdateForm(UpdateInfoEventArgs args)
        {
            InitializeComponent();
            _args = args;
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            // Set data
            lblVersion.Text = $"v{_args.CurrentVersion}";
            txtChangelog.Text = "Release Notes:\r\n" + _args.ChangelogURL; // Since we don't have raw text changelog easily, we might link or just show generic text.
            // Actually, args.ChangelogURL is a URL.
            // If args.Mandatory.Value is true, hide "Remind Later"
            if (_args.Mandatory.Value)
            {
                btnRemindLater.Visible = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRemindLater_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Window Dragging
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
