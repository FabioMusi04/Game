using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Game;
using GameProject.Services.Inputs;

namespace GameProject.Services.Window
{
    public class GamePanel : Panel
    {
        private readonly GameSetup _game;
        private readonly KeyboardInputs _keyboardInputs;
        private readonly MouseInputs _mouseInputs;
        public GamePanel(GameSetup game)
        {
            this._keyboardInputs = new KeyboardInputs(this);
            this._mouseInputs = new MouseInputs(this);
            this._game = game;

            SetPanelSize();
        }
        private void SetPanelSize()
        {
            Size size = new(GameSetup.GAME_WIDTH, GameSetup.GAME_HEIGHT);
            //this.DoubleBuffered = true;
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.UserPaint |
               ControlStyles.DoubleBuffer, true);
            this.Dock = DockStyle.Fill;
            this.MinimumSize = size;
            this.Size = size;
            this.MaximumSize = size;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this._game.Render(e);
        }
        public void UpdateGame()
        {

        }

        public GameSetup GetGame()
        {
            return this._game;
        }
    }
}
