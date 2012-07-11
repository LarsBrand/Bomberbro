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
    public class BomberManGuy
    {

        Rectangle _playerRectangle = new Rectangle(0, 0, 75, 79);
        private Texture2D _playerTexture;
        private SpriteHelper _player;
        private Vector2 _position;
        private int _playerHitBoxWidth;
        private int _playerHitBoxHeight;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int PlayerHitBoxWidth
        {
            get { return _playerHitBoxWidth; }
            set { _playerHitBoxWidth = value; }
        }

        public int PlayerHitBoxHeight
        {
            get { return _playerHitBoxHeight; }
            set { _playerHitBoxHeight = value; }
        }

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {

            _hitBoxTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _hitBoxTexture.SetData(new Color[] { Color.Red });
            _hitBox = new SpriteHelper(_hitBoxTexture, new Rectangle(0, 0, 1, 1));

            //_centerTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            //_centerTexture.SetData(new Color[] { Color.Lime });
            //_center = new SpriteHelper(_centerTexture, new Rectangle(0, 0, 1, 1));

            //_centerTexture.SetData(new Color[] { Color.CornflowerBlue });

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
            _player.Render(new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(_playerRectangle.Width * scale), Convert.ToInt32(_playerRectangle.Height * scale)));
            drawHitBox(scale);
            //drawCenter(scale);
        }

        private Texture2D _hitBoxTexture;
        private SpriteHelper _hitBox;
        void drawHitBox(float scale)
        {
            SpriteHelper.DrawSprites(840, 690);
            Rectangle bgrec = GetBombermanGuyPositionedRectangle(scale);
            _hitBox.Render(bgrec);
            SpriteHelper.DrawSprites(840, 690);
        }



        private Texture2D _centerTexture;
        private SpriteHelper _center;
        void drawCenter(float scale)
        {
            SpriteHelper.DrawSprites(840, 690);
            Rectangle bgrec = new Rectangle(Convert.ToInt32(_position.X - 1), Convert.ToInt32(_position.Y - 1), 2, 2);
            _center.Render(bgrec);
            SpriteHelper.DrawSprites(840, 690);
        }



        public Rectangle GetBombermanGuyPositionedRectangle(float scale)
        {
            return new Rectangle((int)(_position.X) + Convert.ToInt32(((_playerRectangle.Width * scale) - PlayerHitBoxWidth) / 2), (int)(_position.Y) + Convert.ToInt32((_playerRectangle.Height * scale) - PlayerHitBoxHeight), PlayerHitBoxWidth, PlayerHitBoxHeight);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
