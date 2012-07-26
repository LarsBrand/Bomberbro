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
   public  class BrickSollid :Brick
    {
       public BrickSollid() :base()
       {
           
       }

        public override void LoadContent(ContentManager content)
        {
            _brickRectangle = new Rectangle(3,4,64-3,65-4);
            _brickTextures = content.Load<Texture2D>("blocks");
            _brick = new SpriteHelper(_brickTextures,_brickRectangle);

            
        }
    }
}
