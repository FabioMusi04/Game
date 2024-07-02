using System.Windows.Forms;
using GameProject.Services.Window;
using static GameProject.Utils.Constants;

namespace GameProject.Services.Inputs
{
    public class KeyboardInputs
    {
        private GamePanel _gamePanel;
        public KeyboardInputs(GamePanel gamePanel)
        {
            this._gamePanel = gamePanel;
            this._gamePanel.KeyDown += KeyboardInputs_KeyDown;
            this._gamePanel.KeyPress += KeyboardInputs_KeyPress;
            this._gamePanel.KeyUp += KeyboardInputs_KeyUp;
        }

        private void KeyboardInputs_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle key down event here
        }

        private void KeyboardInputs_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle key press event here
            switch (char.ToLower(e.KeyChar))
            {
                case 'w':
                    this._gamePanel.GetGame().GetPlayer().Up = true;
                    break;
                case 'a':
                    this._gamePanel.GetGame().GetPlayer().Left = true;
                    break;
                case 's':
                    this._gamePanel.GetGame().GetPlayer().Down = true;
                    break;
                case 'd':
                    this._gamePanel.GetGame().GetPlayer().Right = true;
                    break;
            }
        }

        private void KeyboardInputs_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    this._gamePanel.GetGame().GetPlayer().Up = false;
                    break;
                case Keys.A:
                    this._gamePanel.GetGame().GetPlayer().Left = false;
                    break;
                case Keys.S:
                    this._gamePanel.GetGame().GetPlayer().Down = false;
                    break;
                case Keys.D:
                    this._gamePanel.GetGame().GetPlayer().Right = false;
                    break;
            }
        }
    }
}