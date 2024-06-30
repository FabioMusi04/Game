using System;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Inputs;

namespace GameProject.Services.Window
{
    public class GamePanel : Panel
    {
        private int xDelta, yDelta = 0;
        private int frames = 0;
        private long lastTime = 0;
        public GamePanel()
        {
            _ = new KeyboardInputs(this);
            _ = new MouseInputs(this);
            this.Size = new Size(400, 400);
        }

        public void changeXDelta(int x)
        {
            this.xDelta += x;
        }

        public void changeYDelta(int y)
        {
            this.yDelta += y;
        }

        public void setRectPosition(int x, int y)
        {
            this.xDelta = x;
            this.yDelta = y;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);



            Graphics gfx = e.Graphics;
            gfx.FillRectangle(Brushes.Black, xDelta, yDelta, 50, 50);
            
            frames++;

            if ( DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastTime > 1000)
            {
                lastTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine(frames);
                frames = 0;
            }

            this.Refresh();
        }
    }
}
