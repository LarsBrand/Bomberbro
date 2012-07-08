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
        private Texture2D _playerTexture;
        private SpriteHelper _player;
        private Vector2 position;
        private Rectangle gfxRectangle;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void LoadContent(ContentManager content)
        {
            _playerTexture = content.Load<Texture2D>("player1_spreadsheet_temp");
            _player = new SpriteHelper(_playerTexture, _playerRectangle);
            //gfxRectangle= new Rectangle(0,0,_playerTexture.Width,_playerTexture.Height);
        }

        public void Draw()
        {
            Draw(1);
            //_player.Render(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y));
        }

        public void Draw(float scale)
        {
        //    int width = Convert.ToInt32(scale*_playerRectangle.Width);
        //    int heigtht = Convert.ToInt32(scale*_playerRectangle.Height);
            
        //    _player.Render(new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y),width,heigtht));
            _player.RenderCentered(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y),scale);
        }

        public void Update(GameTime gameTime)
        {
            if (Input.KeyboardDownPressed)
            {
                position.Y = position.Y + 0.2f * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Input.KeyboardUpPressed)
            {
                position.Y = position.Y - 0.2f * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Input.KeyboardLeftPressed)
            {
                position.X = position.X - 0.2f * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Input.KeyboardRightPressed)
            {
                position.X = position.X + 0.2f * gameTime.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
