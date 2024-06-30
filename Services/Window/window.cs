using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameProject.Services.Window
{
    public class GameWindow
    {
        public Form form;
        public GameWindow(GamePanel gamePanel)
        {
            CreateWindow(gamePanel);
            Application.EnableVisualStyles();
            Application.Run(form);
        }

        [STAThread]
        public void CreateWindow(GamePanel gamePanel)
        {
            form = new Form
            {
                Text = "Game",
                Size = new Size(400, 400),
                FormBorderStyle = FormBorderStyle.Sizable,
                MaximizeBox = true,
                MinimizeBox = true,
                StartPosition = FormStartPosition.CenterScreen
            };

            form.FormClosing += new FormClosingEventHandler(OnFormClosing);
            form.Shown += (sender, e) =>
            {
                gamePanel.Focus();
            };

            form.Controls.Add(gamePanel);

            form.Show();
            /* form.Paint += new PaintEventHandler(OnPaint);
            form.KeyDown += new KeyEventHandler(OnKeyDown);
            form.KeyUp += new KeyEventHandler(OnKeyUp);
            form.Show(); */
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
