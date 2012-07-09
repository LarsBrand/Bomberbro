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
            _brickRectangle = new Rectangle(90,180,60,60);
            _brickTextures = content.Load<Texture2D>("layout_level_indestructible_stones");
            _brick = new SpriteHelper(_brickTextures,_brickRectangle);

            
        }
    }
}
