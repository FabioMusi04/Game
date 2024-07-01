using System;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Inputs;
using static GameProject.Utils.Constants;

namespace GameProject.Services.Window
{
    public class GamePanel : Panel
    {
        private int xDelta, yDelta = 0;
        private int width = 32, height = 48;
        private Bitmap img;
        private Bitmap[,] animations;
        private int animationTick, animationIndex, animationSpeed = 30;
        private int playerAction = PlayerConstants.RUNNING_DOWN;
        private int playerDirection = -1;
        private bool isMoving = false;
        public GamePanel()
        {
            _ = new KeyboardInputs(this);
            _ = new MouseInputs(this);

            ImportImage();
            LoadAnimations();

            SetPanelSize();
        }
        private void ImportImage()
        {
            img = new Bitmap("../../../Res/NPC/hero.png");
        }
        private void LoadAnimations()
        {
            animations = new Bitmap[4, 4];
            for (int i = 0; i < animations.GetLength(0); i++)
            {
                for (int j = 0; j < animations.GetLength(1); j++)
                {
                    animations[i, j] = img.Clone(new Rectangle(i * width, j * height, width, height), img.PixelFormat);
                }
            }
        }
        private void SetPanelSize()
        {
            Size size = new(1280, 800);
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            this.Dock = DockStyle.Fill;
            this.MinimumSize = size;
            this.Size = size;
            this.MaximumSize = size;
        }

        public void SetDirection(int dir)
        {
            this.playerDirection = dir;
            this.isMoving = true;
        }

        public void SetMoving(bool moving)
        {
            this.isMoving = moving;
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
                switch (playerDirection)
                {
                    case Directions.DOWN:
                        playerAction = PlayerConstants.RUNNING_DOWN;
                        break;
                    case Directions.LEFT:
                        playerAction = PlayerConstants.RUNNING_LEFT;
                        break;
                    case Directions.RIGHT:
                        playerAction = PlayerConstants.RUNNING_RIGHT;
                        break;
                    case Directions.UP:
                        playerAction = PlayerConstants.RUNNING_UP;
                        break;
                }
            }
        }

        private void UpdatePosition()
        {
            if (this.isMoving)
            {
                switch (playerDirection)
                {
                    case Directions.DOWN:
                        yDelta += 2;
                        break;
                    case Directions.LEFT:
                        xDelta -= 2;
                        break;
                    case Directions.RIGHT:
                        xDelta += 2;
                        break;
                    case Directions.UP:
                        yDelta -= 2;
                        break;
                }
            } else {
                // Reset the animation index to the first frame
                animationIndex = 0;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;


            g.DrawImage(animations[animationIndex, playerAction], xDelta, yDelta, width, height);
        }

        public void UpdateGame()
        {
            UpdateAnimationTick();
            SetAnimation();
            UpdatePosition();
        }
    }
}
