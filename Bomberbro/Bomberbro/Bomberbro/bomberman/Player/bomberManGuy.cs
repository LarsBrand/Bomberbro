using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberbro.Helpers;

namespace Bomberbro.bomberman
{
    public class BomberManGuy
    {

        Rectangle _playerRectangle = new Rectangle(6, 7, 63 - 6, 84 - 7);
        private Texture2D _playerTexture;
        private SpriteHelper _player;
        private Vector2 _position;
        private int _playerHitBoxWidth;
        private int _playerHitBoxHeight;
        private Bomb bombProtoType;
        private Rectangle _bombermanGuyPositionedHitBoxPreviousUpdate;
        private bool _dead;
        private int _amountOfAllowedBombs;
        private List<Bomb> _placedBombs;

        private Animation _currentAnimation;
        private Animation _idleAnimation;
        private Animation _leftAnimation;
        private Animation _rightAnimation;
        private Animation _downAnimation;
        private Animation _upAnimation;
        private Animation _deadAnimation;
        private Animation _normalAnimation;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int PlayerHitBoxWidth
        {
            get { return _playerHitBoxWidth; }
            set { _playerHitBoxWidth = value; }
        }

        public int PlayerHitBoxHeight
        {
            get { return _playerHitBoxHeight; }
            set { _playerHitBoxHeight = value; }
        }

        public Rectangle BombermanGuyPositionedHitBoxPreviousUpdate
        {
            get { return _bombermanGuyPositionedHitBoxPreviousUpdate; }
            set { _bombermanGuyPositionedHitBoxPreviousUpdate = value; }
        }

        public bool Dead
        {
            get { return _dead; }
            set
            {
                if (!value)
                {
                    _deadAnimation.ResetAnimation();
                }
                _dead = value;
            }
        }

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {

            _hitBoxTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _hitBoxTexture.SetData(new Color[] { Color.Red });
            _hitBox = new SpriteHelper(_hitBoxTexture, new Rectangle(0, 0, 1, 1));

            _playerTexture = content.Load<Texture2D>("character1_sprites");
            _player = new SpriteHelper(_playerTexture, _playerRectangle);
            #region AnnimationInit
            Rectangle idleFrame1Rectangle = new Rectangle(6, 7, 63 - 6, 84 - 7);
            Rectangle idleFrame2Rectangle = new Rectangle(63, 7, 120 - 63, 84 - 7);
            List<SpriteHelper> idleAnimationFrames = new List<SpriteHelper>();
            idleAnimationFrames.Add(new SpriteHelper(_playerTexture, idleFrame1Rectangle));
            idleAnimationFrames.Add(new SpriteHelper(_playerTexture, idleFrame2Rectangle));
            _idleAnimation = new Animation(300, idleAnimationFrames);

            Rectangle leftFrame1Rectangle = new Rectangle(6, 84, 67 - 6, 165 - 84);
            Rectangle leftFrame2Rectangle = new Rectangle(68, 84, 129 - 68, 165 - 84);
            Rectangle leftFrame3Rectangle = new Rectangle(129, 84, 189 - 129, 165 - 84);
            List<SpriteHelper> leftAnimationFrames = new List<SpriteHelper>();
            leftAnimationFrames.Add(new SpriteHelper(_playerTexture, leftFrame1Rectangle));
            leftAnimationFrames.Add(new SpriteHelper(_playerTexture, leftFrame2Rectangle));
            leftAnimationFrames.Add(new SpriteHelper(_playerTexture, leftFrame3Rectangle));
            _leftAnimation = new Animation(190, leftAnimationFrames);

            Texture2D vertFlipPlayer = SpriteHelper.Flip(_playerTexture, false, true);
            Rectangle rightFrame1Rectangle = new Rectangle(vertFlipPlayer.Width - 67, 84, 67 - 6, 165 - 84);
            Rectangle rightFrame2Rectangle = new Rectangle(vertFlipPlayer.Width - 129, 84, 129 - 68, 165 - 84);
            Rectangle rightFrame3Rectangle = new Rectangle(vertFlipPlayer.Width - 189, 84, 189 - 129, 165 - 84);
            List<SpriteHelper> rightAnimationFrames = new List<SpriteHelper>();
            rightAnimationFrames.Add(new SpriteHelper(vertFlipPlayer, rightFrame1Rectangle));
            rightAnimationFrames.Add(new SpriteHelper(vertFlipPlayer, rightFrame2Rectangle));
            rightAnimationFrames.Add(new SpriteHelper(vertFlipPlayer, rightFrame3Rectangle));
            _rightAnimation = new Animation(190, rightAnimationFrames);

            Rectangle downFrame1Rectangle = new Rectangle(6, 165, 65 - 6, 247 - 165);
            Rectangle downFrame2Rectangle = new Rectangle(65, 165, 124 - 65, 247 - 165);
            Rectangle downFrame3Rectangle = new Rectangle(124, 165, 183 - 124, 247 - 165);
            List<SpriteHelper> downAnimationFrames = new List<SpriteHelper>();
            downAnimationFrames.Add(new SpriteHelper(_playerTexture, downFrame1Rectangle));
            downAnimationFrames.Add(new SpriteHelper(_playerTexture, downFrame2Rectangle));
            downAnimationFrames.Add(new SpriteHelper(_playerTexture, downFrame3Rectangle));
            _downAnimation = new Animation(190, downAnimationFrames);

            Rectangle upFrame1Rectangle = new Rectangle(6, 247, 64 - 6, 328 - 247);
            Rectangle upFrame2Rectangle = new Rectangle(64, 247, 124 - 65, 328 - 247);
            Rectangle upFrame3Rectangle = new Rectangle(123, 247, 182 - 124, 328 - 247);
            List<SpriteHelper> upAnimationFrames = new List<SpriteHelper>();
            upAnimationFrames.Add(new SpriteHelper(_playerTexture, upFrame1Rectangle));
            upAnimationFrames.Add(new SpriteHelper(_playerTexture, upFrame2Rectangle));
            upAnimationFrames.Add(new SpriteHelper(_playerTexture, upFrame3Rectangle));
            _upAnimation = new Animation(190, upAnimationFrames);

            Rectangle deadFrame1Rectangle = new Rectangle(6, 328, 79 - 6, 408 - 328);
            Rectangle deadFrame2Rectangle = new Rectangle(79, 328, 152 - 79, 408 - 328);
            Rectangle deadFrame3Rectangle = new Rectangle(152, 328, 225 - 152, 408 - 328);
            Rectangle deadFrame4Rectangle = new Rectangle(225, 328, 299 - 225, 408 - 328);
            List<SpriteHelper> deadAnimationFrames = new List<SpriteHelper>();
            deadAnimationFrames.Add(new SpriteHelper(_playerTexture, deadFrame1Rectangle));
            deadAnimationFrames.Add(new SpriteHelper(_playerTexture, deadFrame2Rectangle));
            deadAnimationFrames.Add(new SpriteHelper(_playerTexture, deadFrame3Rectangle));
            deadAnimationFrames.Add(new SpriteHelper(_playerTexture, deadFrame4Rectangle));
            _deadAnimation = new Animation(190, deadAnimationFrames, true);

            Rectangle normalFrame1Rectangle = new Rectangle(6, 7, 63 - 6, 84 - 7);
            List<SpriteHelper> normalAnimationFrames = new List<SpriteHelper>();
            normalAnimationFrames.Add(new SpriteHelper(_playerTexture, normalFrame1Rectangle));
            _normalAnimation = new Animation(1000, normalAnimationFrames);
            #endregion


            bombProtoType = new Bomb();
            bombProtoType.LoadContent(content);
            _amountOfAllowedBombs = 3;
            _placedBombs = new List<Bomb>();
            _currentAnimation = _normalAnimation;
        }

        public void Draw()
        {
            Draw(1);
            //_player.Render(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y));
        }

        public void Draw(float scale)
        {
            //_player.Render(new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(_playerRectangle.Width * scale), Convert.ToInt32(_playerRectangle.Height * scale)));

            _currentAnimation.Draw(new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(_playerRectangle.Width * scale), Convert.ToInt32(_playerRectangle.Height * scale)));
            //drawHitBox(scale);

        }

        private Texture2D _hitBoxTexture;
        private SpriteHelper _hitBox;
        void drawHitBox(float scale)
        {
            SpriteHelper.DrawSprites(840, 690);
            Rectangle bgrec = GetBombermanGuyPositionedHitBox(scale);
            _hitBox.Render(bgrec);
            SpriteHelper.DrawSprites(840, 690);
        }

        public Rectangle GetBombermanGuyPositionedHitBox(float scale)
        {
            return new Rectangle((int)(_position.X) + Convert.ToInt32(((_playerRectangle.Width * scale) - PlayerHitBoxWidth) / 2), (int)(_position.Y) + Convert.ToInt32((_playerRectangle.Height * scale) - PlayerHitBoxHeight - 10), PlayerHitBoxWidth, PlayerHitBoxHeight);
        }



        public Vector2 GetCenterOfPositionedHitBox(float scale)
        {
            Point center = GetBombermanGuyPositionedHitBox(scale).Center;
            return new Vector2(center.X, center.Y);
        }

        public void Update(GameTime gameTime)
        {
            _currentAnimation.Update(gameTime);


        }

        /// <summary>
        /// returns a bomb if it's possible and allowed, else returns null
        /// </summary>
        /// <returns>a bomb, or null of not allowed</returns>
        public Bomb GetBomb()
        {
            for (int i = _placedBombs.Count-1; i >=0 ; i--)
            {
                if (_placedBombs[i].Exploded)
                {
                    _placedBombs.RemoveAt(i);
                }
            }
             if (_amountOfAllowedBombs > _placedBombs.Count)
            {
                Bomb outputBomb = bombProtoType.copy();
                _placedBombs.Add(outputBomb);
            return outputBomb;
            }
            return null;
        }

        public void SetAnimation(playerAnimations animationType)
        {
            switch (animationType)
            {
                case playerAnimations.normal:
                    _currentAnimation = _normalAnimation;
                    break;
                case playerAnimations.idle:
                    _currentAnimation = _idleAnimation;
                    break;
                case playerAnimations.up:
                    _currentAnimation = _upAnimation;
                    break;
                case playerAnimations.down:
                    _currentAnimation = _downAnimation;
                    break;
                case playerAnimations.left:
                    _currentAnimation = _leftAnimation;
                    break;
                case playerAnimations.right:
                    _currentAnimation = _rightAnimation;
                    break;
                case playerAnimations.dead:
                    _currentAnimation = _deadAnimation;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("animationType");
            }

        }
    }

    public enum playerAnimations
    {
        normal, idle, up, down, left, right, dead
    }
}
