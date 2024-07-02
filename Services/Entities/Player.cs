using System.Drawing;
using System.Windows.Forms;
using static GameProject.Utils.Constants;
using GameProject.Utils;

namespace GameProject.Services.Entities
{
    public class Player : Entity
    {
        private Bitmap[,] animations;
        private const int width = 32, height = 48;
        private const float playerSpeed = 1.5f;
        private const float playerAcceleration = 1.5f;

        private int animationTick, animationIndex, animationSpeed = 30;
        private int playerAction = PlayerConstants.RUNNING_DOWN;
        private bool _left, _right, _up, _down;
        private bool isMoving = false;

        public bool Left { get => _left; set => _left = value; }
        public bool Right { get => _right; set => _right = value; }
        public bool Up { get => _up; set => _up = value; }
        public bool Down { get => _down; set => _down = value; }

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

            g.DrawImage(animations[animationIndex, playerAction], (int)this._x, (int)this._y, width, height);
        }

        private void LoadAnimations()
        {
            Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.PLAYER_ATLAS);
            animations = new Bitmap[4, 4];
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
                animationIndex = 0;
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