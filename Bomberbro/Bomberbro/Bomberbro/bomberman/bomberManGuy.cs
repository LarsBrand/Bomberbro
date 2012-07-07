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
    public class bomberManGuy
    {

        Rectangle _playerRectangle = new Rectangle(0, 0, 75, 79);
        private Texture2D _playerTexture ;
        private SpriteHelper _player;

        public void LoadContent(ContentManager content)
        {
            _playerTexture = content.Load<Texture2D>("player1_spreadsheet_temp");
            _player = new SpriteHelper(_playerTexture, _playerRectangle);
        }

        public void draw(int positionX, int positionY)
        {
            _player.Render(positionX,positionY);            
        }
    }
}
