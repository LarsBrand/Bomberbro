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
    public class BrickBreakAble : Brick
    {
        private Animation _brickDestroyAnimation;
        private bool _destroying;
        private int _destroyTime;

        public bool Destroyed
        {
            get { return (_destroyTime < 0); }

        }
        public override void LoadContent(ContentManager content)
        {
            _brickTextures = content.Load<Texture2D>("blocks");
            _brickRectangle = new Rectangle(64, 4, 125 - 64, 65 - 4);
            _brick = new SpriteHelper(_brickTextures, _brickRectangle);

            //explosions take 1500 ms to disappear, 1500/4 frames = 375 ms.
            List<SpriteHelper> destroyAnimationFrames = new List<SpriteHelper>();
            destroyAnimationFrames.Add(_brick);

            Rectangle destroy1Rectangle = new Rectangle(125, 4, 186 - 125, 65 - 4);
            SpriteHelper destroy1 = new SpriteHelper(_brickTextures, destroy1Rectangle);
            destroyAnimationFrames.Add(destroy1);

            Rectangle destroy2Rectangle = new Rectangle(186, 4, 247 - 186, 65 - 4);
            SpriteHelper destroy2 = new SpriteHelper(_brickTextures, destroy2Rectangle);
            destroyAnimationFrames.Add(destroy2);

            Rectangle destroy3Rectangle = new Rectangle(247, 4, 308 - 247, 65 - 4);
            SpriteHelper destroy3 = new SpriteHelper(_brickTextures, destroy3Rectangle);
            destroyAnimationFrames.Add(destroy3);

            _brickDestroyAnimation = new Animation(375, destroyAnimationFrames, true);

            CollisionType = CollisionTypes.BlockBreakable;
            _destroying = false;
            _destroyTime = 1500;


        }

        public override void Update(GameTime gameTime)
        {
            if (_destroying)
            {
                _brickDestroyAnimation.Update(gameTime);
                _destroyTime -= gameTime.ElapsedGameTime.Milliseconds;
            }
            base.Update(gameTime);
        }

        public override void Draw(Rectangle rect, GameTime gameTime)
        {
            if (_destroying)
            {
                _brickDestroyAnimation.Draw(rect);
            }
            else
            {
                _brick.Render(rect);
            }
        }

        public void DestroyBlock()
        {
            _destroying = true;
        }
    }
}
