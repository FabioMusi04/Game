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
            this._gamePanel.MouseDown += MouseInputs_MousePress;
            this._gamePanel.MouseUp += MouseInputs_MouseRelease;
        }

        private void MouseInputs_MouseMove(object sender, MouseEventArgs e)
        {
            switch (GameStateManager.GameState)
            {
                case GameState.MENU:
                    this._gamePanel.GetGame().GetMenu().MouseMove(sender, e);
                    break;
                default:
                    break;

            }
        }
        private void MouseInputs_MouseClick(object sender, MouseEventArgs e)
        {
            switch (GameStateManager.GameState)
            {
                case GameState.PLAYING:
                    this._gamePanel.GetGame().GetPlaying().MouseClick(sender, e);
                    break;
                case GameState.PAUSED:
                    this._gamePanel.GetGame().GetPause().MouseClick(sender, e);
                    break;
                default:
                    break;

            }
        }

        private void MouseInputs_MousePress(object sender, MouseEventArgs e)
        {
            switch (GameStateManager.GameState)
            {
                case GameState.MENU:
                    this._gamePanel.GetGame().GetMenu().MousePress(sender, e);
                    break;
                default:
                    break;

            }
        }

        private void MouseInputs_MouseRelease(object sender, MouseEventArgs e)
        {
            switch (GameStateManager.GameState)
            {
                case GameState.MENU:
                    this._gamePanel.GetGame().GetMenu().MouseRelease(sender, e);
                    break;
                default:
                    break;

            }
        }
    }
}