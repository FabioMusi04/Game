using System;
using System.Windows.Forms;
using GameProject.Services.States;
using GameProject.Services.Window;

namespace GameProject.Services.Inputs
{
    public class MouseInputs
    {
        private GamePanel _gamePanel;
        public MouseInputs(GamePanel gamePanel)
        {
            this._gamePanel = gamePanel;
            this._gamePanel.MouseMove += MouseInputs_MouseMove;
            this._gamePanel.MouseClick += MouseInputs_MouseClick;
        }

        private void MouseInputs_MouseMove(object sender, MouseEventArgs e)
        {
            // Handle mouse down event here
        }
        private void MouseInputs_MouseClick(object sender, MouseEventArgs e)
        {
            switch (GameStateManager.GameState)
            {
                case GameState.MENU:
                    this._gamePanel.GetGame().GetMenu().MouseClick(sender, e);
                    break;
                case GameState.PLAYING:
                    this._gamePanel.GetGame().GetPlaying().MouseClick(sender, e);
                    break;
                default:
                    break;

            }
        }

        
    }
}