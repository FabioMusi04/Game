using System.Windows.Forms;
using GameProject.Services.Window;
using GameProject.Services.States;

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
            switch (GameStateManager.GameState)
            {
                case GameState.MENU:
                    this._gamePanel.GetGame().GetMenu().KeyPress(sender, e);
                    break;
                case GameState.PLAYING:
                    this._gamePanel.GetGame().GetPlaying().KeyPress(sender, e);
                    break;
                case GameState.PAUSED:
                    this._gamePanel.GetGame().GetPause().KeyPress(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void KeyboardInputs_KeyUp(object sender, KeyEventArgs e)
        {
            switch (GameStateManager.GameState)
            {
                case GameState.MENU:
                    this._gamePanel.GetGame().GetMenu().KeyUp(sender, e);
                    break;
                case GameState.PLAYING:
                    this._gamePanel.GetGame().GetPlaying().KeyUp(sender, e);
                    break;
                default:
                    break;
            }
        }
    }
}