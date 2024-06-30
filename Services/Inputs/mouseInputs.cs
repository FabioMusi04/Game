using System;
using System.Windows.Forms;
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
            this._gamePanel.setRectPosition(e.X, e.Y);
        }
        private void MouseInputs_MouseClick(object sender, MouseEventArgs e)
        {
            // Handle mouse up event here
        }

        
    }
}