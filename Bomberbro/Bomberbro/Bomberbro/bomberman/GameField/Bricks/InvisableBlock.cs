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
    public class InvisableBlock :Brick
    {
        public InvisableBlock() : base()
        {
            
        }


        public override void LoadContent(ContentManager content)
        {
            _brickRectangle = new Rectangle(0, 0, 1, 1);
            _brickTextures = content.Load<Texture2D>("blocks");
            _brick = new SpriteHelper(_brickTextures, _brickRectangle);
        }
    }
}
