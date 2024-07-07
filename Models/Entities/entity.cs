using System;
using System.Drawing;
using System.Windows.Forms;
namespace GameProject.Services.Entities
{
    public abstract class Entity
    {
        protected float _x, _y;
        protected int _width, _height;
        protected RectangleF _bounds;

        public Entity(float x, float y, int width, int height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }
        protected void DrawHitBox(Graphics g, int _xLvlOffset, int _yLvlOffset)
        {
            g.DrawRectangle(new Pen(Color.Red), 
            this._bounds.X - _xLvlOffset, 
            this._bounds.Y - _yLvlOffset, 
            this._bounds.Width, 
            this._bounds.Height);
        }
        public void InitHitBox(float x, float y, int width, int height)
        {
            this._bounds = new RectangleF(x, y, width, height);
        }

        public RectangleF GetBounds()
        {
            return this._bounds;
        }
    }
}