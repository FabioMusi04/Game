using System;
using System.Drawing;
using System.Windows.Forms;

using GameProject.Services.Game;

namespace GameProject.Services.Window
{
    public class GameWindow
    {
        public Form form;
        public GameWindow(GamePanel gamePanel)
        {
            CreateWindow(gamePanel);
        }

        [STAThread]
        public void CreateWindow(GamePanel gamePanel)
        {
            this.form = new Form
            {
                Text = "Game",
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = true,
                StartPosition = FormStartPosition.CenterScreen
            };

            this.form.Deactivate += (sender, e) =>
            {
                Console.WriteLine("Lost focus");
                gamePanel.GetGame().GetPlayer().ResetDirection();
            };

            this.form.FormClosing += new FormClosingEventHandler(OnFormClosing);

            this.form.Shown += (sender, e) =>
            {
                gamePanel.Focus();
            };

            this.form.Size = gamePanel.Size;
            this.form.Controls.Add(gamePanel);

            this.form.Show();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
