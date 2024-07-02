using System;
namespace GameProject.Services.Entities {
    public abstract class Entity {
        protected float _x, _y;

        public Entity(float x, float y) {
            this._x = x;
            this._y = y;
        }
    }
}