using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.bomberman;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberbro.Tests
{
    public class FontTest : IGameState
    {
        SpriteFont UVfont;
        SpriteFont UVfont2;
        SpriteBatch spritebatch;
        private ContentManager _content;
        private GraphicsDeviceManager _graphics;

        public FontTest(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }
        public void Initialize()
        {
        //    throw new NotImplementedException();
        }

        public void LoadContent()
        {
            spritebatch = new SpriteBatch(_graphics.GraphicsDevice);
            UVfont = _content.Load<SpriteFont>("TestFont");
            UVfont2 = _content.Load<SpriteFont>("TestFont");
            


        }

        public void UnloadContent()
        {
            //throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime)
        {
            spritebatch.Begin();
            //spritebatch.DrawString(UVfont, "Hello World", Vector2.Zero, Color.White);
            spritebatch.DrawString(UVfont, "Now that there is the Tec-9, \r\n a crappy spray gun from South Miami. \r\nThis gun is advertised as the most popular gun in American crime. \r\nDo you believe that shit? \r\nIt actually says that in the little book that comes with it: \r\nthe most popular gun in American crime. \r\nLike they're actually proud of that shit.", Vector2.Zero, Color.White);
            spritebatch.End(); 
        }
    }
}
