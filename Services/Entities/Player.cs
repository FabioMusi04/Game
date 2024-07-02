using System.Drawing;
using System.Windows.Forms;
using static GameProject.Utils.Constants;
using GameProject.Utils;
using System;

namespace GameProject.Services.Entities
{
    public class Player : Entity
    {
        private Bitmap[,] animations;
        private const int width = 48, height = 48;
        private const float playerSpeed = 1.5f;
        private const float playerAcceleration = 1.5f;

        private int animationTick, animationIndex, animationSpeed = 30;
        private int playerAction = PlayerConstants.IDLE_DOWN;
        private bool _left, _right, _up, _down;
        private bool _wasLeft, _wasRight, _wasUp, _wasDown;
        private bool isMoving = false;

        public bool Left
        {
            get => _left; set
            {
                _left = value;
                if (_left)
                {
                    _wasLeft = true;
                    _wasRight = false;
                    _wasUp = false;
                    _wasDown = false;
                }
            }
        }
        public bool Right
        {
            get => _right; set
            {
                _right = value;
                if (_right)
                {
                    _wasRight = true;
                    _wasLeft = false;
                    _wasUp = false;
                    _wasDown = false;
                }
            }
        }
        public bool Up
        {
            get => _up; set
            {
                _up = value;
                if (_up)
                {
                    _wasUp = true;
                    _wasRight = false;
                    _wasLeft = false;
                    _wasDown = false;
                }
            }
        }
        public bool Down
        {
            get => _down; set
            {
                _down = value;
                if (_down)
                {
                    _wasDown = true;
                    _wasRight = false;
                    _wasLeft = false;
                    _wasUp = false;
                }
            }
        }

        public Player(float x, float y) : base(x, y)
        {
            LoadAnimations();
        }

        public void Update()
        {
            UpdatePosition();
            UpdateAnimationTick();
            SetAnimation();
        }

        public void Render(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (this.Left || this._wasLeft)
            {
                g.DrawImage(animations[animationIndex, playerAction], (int)this._x + width, (int)this._y, -width, height);
            }
            else
            {
                g.DrawImage(animations[animationIndex, playerAction], (int)this._x, (int)this._y, width, height);
            }
        }

        private void LoadAnimations()
        {
            Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.PLAYER_ATLAS);
            animations = new Bitmap[6, 10];
            for (int i = 0; i < animations.GetLength(0); i++)
            {
                for (int j = 0; j < animations.GetLength(1); j++)
                {
                    animations[i, j] = img.Clone(new Rectangle(i * width, j * height, width, height), img.PixelFormat);
                }
            }
        }

        private void UpdateAnimationTick()
        {
            animationTick++;
            if (animationTick >= animationSpeed)
            {
                animationTick = 0;
                animationIndex++;
                if (animationIndex >= PlayerConstants.GetSpriteAmount(playerAction))
                {
                    animationIndex = 0;
                }
            }
        }

        private void SetAnimation()
        {
            if (isMoving)
            {
                if (Left)
                {
                    playerAction = PlayerConstants.RUNNING_LEFT;
                }
                else if (Right)
                {
                    playerAction = PlayerConstants.RUNNING_RIGHT;
                }
                else if (Up)
                {
                    playerAction = PlayerConstants.RUNNING_UP;
                }
                else if (Down)
                {
                    playerAction = PlayerConstants.RUNNING_DOWN;
                }
            }
            else
            {
                if (this._wasDown)
                {
                    playerAction = PlayerConstants.IDLE_DOWN;
                }
                else if (this._wasLeft)
                {
                    playerAction = PlayerConstants.IDLE_LEFT;
                }
                else if (this._wasRight)
                {
                    playerAction = PlayerConstants.IDLE_RIGHT;
                }
                else if (this._wasUp)
                {
                    playerAction = PlayerConstants.IDLE_UP;
                }
            }
        }

        private void UpdatePosition()
        {
            this.isMoving = false;

            if (Left && !Right && !Up && !Down)
            {
                this._x -= playerSpeed;
                this.isMoving = true;
            }
            else if (Right && !Left && !Up && !Down)
            {
                this._x += playerSpeed;
                this.isMoving = true;
            }
            if (Up && !Down && !Left && !Right)
            {
                this._y -= playerSpeed;
                this.isMoving = true;
            }
            if (Down && !Up && !Left && !Right)
            {
                this._y += playerSpeed;
                this.isMoving = true;
            }
        }
        public void ResetDirection()
        {
            this.Left = false;
            this.Right = false;
            this.Up = false;
            this.Down = false;
        }
    }
}