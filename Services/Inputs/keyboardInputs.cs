using System;
using System.Windows.Forms;
using GameProject.Services.Window;

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
                    this._gamePanel.changeYDelta(-5);
                    break;
                case 'a':
                    this._gamePanel.changeXDelta(-5);
                    break;
                case 's':
                    this._gamePanel.changeYDelta(5);
                    break;
                case 'd':
                    this._gamePanel.changeXDelta(5);
                    break;
            }
        }

        private void KeyboardInputs_KeyUp(object sender, KeyEventArgs e)
        {
            // Handle key up event here
        }
    }
}