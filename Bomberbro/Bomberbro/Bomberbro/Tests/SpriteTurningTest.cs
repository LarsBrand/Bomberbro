using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.bomberman;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberbro.Helpers;

namespace Bomberbro.Tests
{
    public class SpriteTurningTest : IGameState
    {
        private ContentManager _content;
        private GraphicsDeviceManager _graphics;

        private SpriteHelper _explosionCross;
        private SpriteHelper _explosionRight;
        private SpriteHelper _explosionRightEnd;
        private SpriteHelper _explosionUp;
        private SpriteHelper _explosionUpEnd;
        private SpriteHelper _explosionLeft;
        private SpriteHelper _explosionLeftEnd;
        private SpriteHelper _explosionDown;
        private SpriteHelper _explosionDownEnd;
        private Rectangle explosionCrossRectangle;
        public SpriteTurningTest(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
            _graphics.PreferredBackBufferWidth = 840;
            _graphics.PreferredBackBufferHeight = 690;
        }

        public void Initialize()
        {
         //   throw new NotImplementedException();
        }

        public void LoadContent()
        {
            Texture2D explosionTexture = _content.Load<Texture2D>("fire");
            explosionCrossRectangle = new Rectangle(10, 10, 69 - 10, 69 - 10);
            _explosionCross = new SpriteHelper(explosionTexture, explosionCrossRectangle);

            Rectangle explosionRightRectangle = new Rectangle(71, 10, 130 - 71, 69 - 10);
            _explosionRight = new SpriteHelper(explosionTexture, explosionRightRectangle);
            Rectangle explosionRightEndRectangle = new Rectangle(132, 10 , 191 - 132, 69 - 10);
            _explosionRightEnd = new SpriteHelper(explosionTexture, explosionRightEndRectangle);

            Texture2D explosionTextureLeft = SpriteHelper.Flip(explosionTexture, false, true);
            Rectangle explosionLeftRectangle = new Rectangle(explosionTextureLeft.Width - 130, 10, 130-71 , 69 - 10);
            _explosionLeft = new SpriteHelper(explosionTextureLeft, explosionLeftRectangle);
            Rectangle explosionLeftEndRectangle = new Rectangle(explosionTextureLeft.Width - 191, 10, 191 - 132, 69 - 10);
            _explosionLeftEnd = new SpriteHelper(explosionTextureLeft, explosionLeftEndRectangle);

            Texture2D explosionTextureDown = SpriteHelper.RotateTexture90Degrees(explosionTexture);
            Rectangle explosionDownRectangle = new Rectangle(10, 71, 69 - 10, 130 - 71);
            _explosionDown = new SpriteHelper(explosionTextureDown, explosionDownRectangle);
            Rectangle explosionDownEndRectangle = new Rectangle(10, 132, 69 - 10, 191 - 132);
            _explosionDownEnd = new SpriteHelper(explosionTextureDown, explosionDownEndRectangle);

            Texture2D explosionTextureUp = SpriteHelper.Flip(explosionTextureDown, true, false);
            Rectangle explosionUpRectangle = new Rectangle(10, explosionTextureUp.Height - 130, 69 - 10, 130- 71);
            _explosionUp = new SpriteHelper(explosionTextureUp, explosionUpRectangle);
            Rectangle explosionUpEndRectangle = new Rectangle(10, explosionTextureUp.Height - 191, 69 - 10, 191 - 132);
            _explosionUpEnd = new SpriteHelper(explosionTextureUp, explosionUpEndRectangle);

            

        }

        public void UnloadContent()
        {
         //   throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
           // throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            int xpos = 300, ypos = 300;
            Rectangle differ = explosionCrossRectangle;
          _explosionCross.Render(xpos,ypos);
           _explosionUp.Render(xpos,ypos-differ.Height);
           _explosionUpEnd.Render(xpos, ypos - differ.Height*2); 
          _explosionDown.Render(xpos,ypos+differ.Height);
          _explosionDownEnd.Render(xpos, ypos + differ.Height*2);
            _explosionLeft.Render(xpos-differ.Width,ypos);
            _explosionLeftEnd.Render(xpos - differ.Width*2, ypos);
            _explosionRight.Render(xpos+differ.Width,ypos);
            _explosionRightEnd.Render(xpos + differ.Width*2, ypos);
          SpriteHelper.DrawSprites(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
        }
    }
}
