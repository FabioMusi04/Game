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
        protected void DrawHitBox(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Red), 
            this._bounds.X, 
            this._bounds.Y, 
            this._bounds.Width, 
            this._bounds.Height);
        }
        public void InitHitBox(float x, float y, float width, float height)
        {
            this._bounds = new RectangleF(x, y, width, height);
        }
        /*  protected void UpdateHitBox() {
             this._bounds.X = (int)this._x;
             this._bounds.Y = (int)this._y;
         } */

        public RectangleF GetBounds()
        {
            return this._bounds;
        }
    }
}