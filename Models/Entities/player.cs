using System.Drawing;
using System.Windows.Forms;
using GameProject.Utils;
using static GameProject.Utils.HelpMethods;
using static GameProject.Utils.Constants;
using GameProject.Services.Game;

namespace GameProject.Services.Entities
{
    public class Player : Entity
    {
        private Bitmap[,] animations;
        private const float _playerSpeed = 0.5f;
        //private const float playerAcceleration = 1.5f;
        private int _animationTick;
        private int _animationIndex;
        private readonly int _animationSpeed = 30;
        private int _playerAction = PlayerConstants.IDLE_DOWN;
        private bool _left, _right, _up, _down, _attack;
        private bool _wasLeft, _wasRight, _wasUp, _wasDown;
        private bool _isMoving = false;

        private const float _xDrawOffset = 15 * GameSetup.SCALE;
        private const float _yDrawOffset = 15 * GameSetup.SCALE;

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
        public bool Attack { get => _attack; set => _attack = value; }
        private int[,] _lvlData;

        public Player(float x, float y, int width, int height) : base(x, y, width, height)
        {
            LoadAnimations();

            this.InitHitBox(this._x, this._y, 20 * GameSetup.SCALE, 30 * GameSetup.SCALE);
        }

        public void Update()
        {
            UpdatePosition();
            UpdateAnimationTick();
            SetAnimation();
        }

        public void Render(Graphics g)
        {

            if (this.Left || this._wasLeft)
            {
                g.DrawImage(animations[this._animationIndex, this._playerAction],
                (int)(this._bounds.X - _xDrawOffset) + this._width,
                (int)(this._bounds.Y - _yDrawOffset),
                -this._width,
                this._height);
            }
            else
            {
                g.DrawImage(animations[this._animationIndex, this._playerAction],
                (int)(this._bounds.X - _xDrawOffset),
                (int)(this._bounds.Y - _yDrawOffset),
                this._width,
                this._height);
            }
            DrawHitBox(g);
        }

        private void LoadAnimations()
        {
            Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.PLAYER_ATLAS);
            animations = new Bitmap[6, 10];
            for (int i = 0; i < animations.GetLength(0); i++)
            {
                for (int j = 0; j < animations.GetLength(1); j++)
                {
                    animations[i, j] = img.Clone(new Rectangle(i * 48,
                    j * 48,
                    48,
                    48), img.PixelFormat);
                }
            }
        }
        public void LoadLevelData(int[,] lvlData)
        {
            this._lvlData = lvlData;
        }
        private void UpdateAnimationTick()
        {
            this._animationTick++;
            if (this._animationTick >= this._animationSpeed)
            {
                this._animationTick = 0;
                this._animationIndex++;
                if (this._animationIndex >= PlayerConstants.GetSpriteAmount(this._playerAction))
                {
                    this._animationIndex = 0;
                    this._attack = false;
                }
            }
        }

        private void SetAnimation()
        {
            int startAni = this._playerAction;
            if (_isMoving)
            {
                if (Left)
                {
                    this._playerAction = PlayerConstants.RUNNING_LEFT;
                }
                else if (Right)
                {
                    this._playerAction = PlayerConstants.RUNNING_RIGHT;
                }
                else if (Up)
                {
                    this._playerAction = PlayerConstants.RUNNING_UP;
                }
                else if (Down)
                {
                    this._playerAction = PlayerConstants.RUNNING_DOWN;
                }
            }
            else
            {
                if (this._wasDown)
                {
                    this._playerAction = PlayerConstants.IDLE_DOWN;
                }
                else if (this._wasLeft)
                {
                    this._playerAction = PlayerConstants.IDLE_LEFT;
                }
                else if (this._wasRight)
                {
                    this._playerAction = PlayerConstants.IDLE_RIGHT;
                }
                else if (this._wasUp)
                {
                    this._playerAction = PlayerConstants.IDLE_UP;
                }
            }
            if(this._attack) {
                if (this._wasDown)
                {
                    this._playerAction = PlayerConstants.ATTACKING_DOWN;
                }
                else if (this._wasLeft)
                {
                    this._playerAction = PlayerConstants.ATTACKING_LEFT;
                }
                else if (this._wasRight)
                {
                    this._playerAction = PlayerConstants.ATTACKING_RIGHT;
                }
                else if (this._wasUp)
                {
                    this._playerAction = PlayerConstants.ATTACKING_UP;
                }
                else {
                    this._playerAction = PlayerConstants.ATTACKING_DOWN;
                }
            }

            if (startAni != this._playerAction)
            {
                this._animationTick = 0;
                this._animationIndex = 0;
            }
        }

        private void UpdatePosition()
        {
            this._isMoving = false;

            if (!Left && !Right && !Up && !Down)
            {
                return;
            }

            float xSpeed = 0, ySpeed = 0;

            if (Left && !Right && !Up && !Down)
            {
                xSpeed = -_playerSpeed;
            }
            else if (Right && !Left && !Up && !Down)
            {
                xSpeed = +_playerSpeed;
            }
            if (Up && !Down && !Left && !Right)
            {
                ySpeed = -_playerSpeed;
            }
            if (Down && !Up && !Left && !Right)
            {
                ySpeed = +_playerSpeed;
            }

            if (CanMoveHere(this._bounds.X + xSpeed,
            this._bounds.Y,
            this._bounds.Width,
            this._bounds.Height,
            this._lvlData,
            this._bounds.X,
            this._bounds.Y,
            xSpeed,
            ySpeed))
            {
                this._bounds.X += xSpeed;
                this._bounds.Y += ySpeed;
                this._isMoving = true;
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