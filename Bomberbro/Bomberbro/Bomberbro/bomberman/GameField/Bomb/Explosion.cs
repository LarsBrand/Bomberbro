using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberbro.bomberman
{
    public class Explosion : GamefieldItem
    {
        private SpriteHelper _explosionCross;
        private SpriteHelper _explosionRight;
        private SpriteHelper _explosionRightEnd;
        private SpriteHelper _explosionUp;
        private SpriteHelper _explosionUpEnd;
        private SpriteHelper _explosionLeft;
        private SpriteHelper _explosionLeftEnd;
        private SpriteHelper _explosionDown;
        private SpriteHelper _explosionDownEnd;

        private ExplosionTypes _explosionType;
        private int _explosionTime;
        bool _fixedExplosionType;

        public bool CanRemove
        {
            get { return (_explosionTime <= 0); }
        }


        public ExplosionTypes ExplosionType
        {
            get { return _explosionType; }
            set
            {
                if (!_fixedExplosionType)
                {
                    _explosionType = value;

                }
            }
        }

        public override void Draw(Rectangle rect, GameTime gameTime)
        {
            switch (ExplosionType)
            {
                case ExplosionTypes.Up:
                    _explosionUp.Render(rect);
                    break;
                case ExplosionTypes.Down:
                    _explosionDown.Render(rect);
                    break;
                case ExplosionTypes.Left:
                    _explosionLeft.Render(rect);
                    break;
                case ExplosionTypes.Right:
                    _explosionRight.Render(rect);
                    break;
                case ExplosionTypes.UpEnd:
                    _explosionUpEnd.Render(rect);
                    break;
                case ExplosionTypes.DownEnd:
                    _explosionDownEnd.Render(rect);
                    break;
                case ExplosionTypes.LeftEnd:
                    _explosionLeftEnd.Render(rect);
                    break;
                case ExplosionTypes.RightEnd:
                    _explosionRightEnd.Render(rect);
                    break;
                case ExplosionTypes.Cross:
                    _explosionCross.Render(rect);
                    break;                    
                default:
                    throw new Exception("none set");
                    break;

            }
        }



        public override void Update(GameTime gameTime)
        {
            _explosionTime -= gameTime.ElapsedGameTime.Milliseconds;
        }



        public override void LoadContent(ContentManager content)
        {
            Texture2D explosionTexture = content.Load<Texture2D>("fire");
            Rectangle explosionCrossRectangle = new Rectangle(10, 10, 69 - 10, 69 - 10);
            _explosionCross = new SpriteHelper(explosionTexture, explosionCrossRectangle);

            Rectangle explosionRightRectangle = new Rectangle(71, 10, 130 - 71, 69 - 10);
            _explosionRight = new SpriteHelper(explosionTexture, explosionRightRectangle);
            Rectangle explosionRightEndRectangle = new Rectangle(132, 10, 191 - 132, 69 - 10);
            _explosionRightEnd = new SpriteHelper(explosionTexture, explosionRightEndRectangle);

            Texture2D explosionTextureLeft = SpriteHelper.Flip(explosionTexture, false, true);
            Rectangle explosionLeftRectangle = new Rectangle(explosionTextureLeft.Width - 130, 10, 130 - 71, 69 - 10);
            _explosionLeft = new SpriteHelper(explosionTextureLeft, explosionLeftRectangle);
            Rectangle explosionLeftEndRectangle = new Rectangle(explosionTextureLeft.Width - 191, 10, 191 - 132, 69 - 10);
            _explosionLeftEnd = new SpriteHelper(explosionTextureLeft, explosionLeftEndRectangle);

            Texture2D explosionTextureDown = SpriteHelper.RotateTexture90Degrees(explosionTexture);
            Rectangle explosionDownRectangle = new Rectangle(10, 71, 69 - 10, 130 - 71);
            _explosionDown = new SpriteHelper(explosionTextureDown, explosionDownRectangle);
            Rectangle explosionDownEndRectangle = new Rectangle(10, 132, 69 - 10, 191 - 132);
            _explosionDownEnd = new SpriteHelper(explosionTextureDown, explosionDownEndRectangle);

            Texture2D explosionTextureUp = SpriteHelper.Flip(explosionTextureDown, true, false);
            Rectangle explosionUpRectangle = new Rectangle(10, explosionTextureUp.Height - 130, 69 - 10, 130 - 71);
            _explosionUp = new SpriteHelper(explosionTextureUp, explosionUpRectangle);
            Rectangle explosionUpEndRectangle = new Rectangle(10, explosionTextureUp.Height - 191, 69 - 10, 191 - 132);
            _explosionUpEnd = new SpriteHelper(explosionTextureUp, explosionUpEndRectangle);

            CollisionType = CollisionTypes.Explosion;
            _explosionTime = 2500;

        }
        public Explosion Copy(ExplosionTypes explosionType)
        {
            Explosion output = new Explosion();
            output._explosionType = explosionType;
            output.CollisionType = CollisionType;
            output._fixedExplosionType = true;

            output._explosionCross = _explosionCross;
            output._explosionDown = _explosionDown;
            output._explosionDownEnd = _explosionDownEnd;
            output._explosionLeft = _explosionLeft;
            output._explosionLeftEnd = _explosionLeftEnd;
            output._explosionRight = _explosionRight;
            output._explosionRightEnd = _explosionRightEnd;
            output._explosionUp = _explosionUp;
            output._explosionUpEnd = _explosionUpEnd;
            output._explosionTime = _explosionTime;
            
            return output;
        }

        public Explosion Copy()
        {
            Explosion output = Copy(_explosionType);
            output._fixedExplosionType = false;

            return output;
        }
    }
    public enum ExplosionTypes
    {
        Cross, Up, Down, Left, Right, UpEnd, DownEnd, LeftEnd, RightEnd
    }
}
