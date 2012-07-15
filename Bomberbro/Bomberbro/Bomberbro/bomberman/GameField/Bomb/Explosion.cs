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
    public class Explosion: GamefieldItem
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

        public ExplosionTypes ExplosionType
        {
            get { return _explosionType; }
            set { _explosionType = value; }
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
                default:
                    _explosionCross.Render(rect);
                    break;
                    
            }
        }



        public override void Update(GameTime gameTime)
        {
            
        }



        public override void LoadContent(ContentManager content)
        {
            Texture2D explosionTexture = content.Load<Texture2D>("fire");
           Rectangle explosionCrossRectangle = new Rectangle(9, 9, 70 - 9, 70 - 9);
            _explosionCross = new SpriteHelper(explosionTexture, explosionCrossRectangle);

            Rectangle explosionRightRectangle = new Rectangle(70, 9, 131 - 70, 70 - 9);
            _explosionRight = new SpriteHelper(explosionTexture, explosionRightRectangle);
            Rectangle explosionRightEndRectangle = new Rectangle(131, 9, 192 - 131, 70 - 10);
            _explosionRightEnd = new SpriteHelper(explosionTexture, explosionRightEndRectangle);

            Texture2D explosionTextureLeft = SpriteHelper.Flip(explosionTexture, false, true);
            Rectangle explosionLeftRectangle = new Rectangle(explosionTextureLeft.Width - 131, 9, 131 - 70, 70 - 9);
            _explosionLeft = new SpriteHelper(explosionTextureLeft, explosionLeftRectangle);
            Rectangle explosionLeftEndRectangle = new Rectangle(explosionTextureLeft.Width - 192, 9, 192 - 131, 70 - 9);
            _explosionLeftEnd = new SpriteHelper(explosionTextureLeft, explosionLeftEndRectangle);

            Texture2D explosionTextureDown = SpriteHelper.RotateTexture90Degrees(explosionTexture);
            Rectangle explosionDownRectangle = new Rectangle(9, 70, 70 - 9, 131 - 70);
            _explosionDown = new SpriteHelper(explosionTextureDown, explosionDownRectangle);
            Rectangle explosionDownEndRectangle = new Rectangle(9, 131, 70 - 9, 192 - 131);
            _explosionDownEnd = new SpriteHelper(explosionTextureDown, explosionDownEndRectangle);

            Texture2D explosionTextureUp = SpriteHelper.Flip(explosionTextureDown, true, false);
            Rectangle explosionUpRectangle = new Rectangle(9, explosionTextureUp.Height - 131, 70 - 9, 131 - 70);
            _explosionUp = new SpriteHelper(explosionTextureUp, explosionUpRectangle);
            Rectangle explosionUpEndRectangle = new Rectangle(9, explosionTextureUp.Height - 192, 70 - 9, 192 - 131);
            _explosionUpEnd = new SpriteHelper(explosionTextureUp, explosionUpEndRectangle);

            CollisionType = CollisionTypes.Explosion;
            

        }

        public Explosion Copy()
        {
            Explosion output = new Explosion();
            output._explosionType = _explosionType;
            output.CollisionType = CollisionType;


            output._explosionCross = _explosionCross;
            output._explosionDown = _explosionDown;
            output._explosionDownEnd = _explosionDownEnd;
            output._explosionLeft = _explosionLeft;
            output._explosionLeftEnd = _explosionLeftEnd;
            output._explosionRight = _explosionRight;
            output._explosionRightEnd = _explosionRightEnd;
            output._explosionUp = _explosionUp;
            output._explosionUpEnd = _explosionUpEnd;
            return output;
        }
    }
    public enum ExplosionTypes
    {
        Cross,Up,Down,Left,Right,UpEnd,DownEnd,LeftEnd,RightEnd
    }
}
