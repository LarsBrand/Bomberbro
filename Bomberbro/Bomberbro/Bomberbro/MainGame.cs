using System;
using System.Collections.Generic;
using System.Linq;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bomberbro
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {

        private static Rectangle
            _backgroundRectangle = new Rectangle(0, 0, 840, 690),
            _sollidBrickRectangle = new Rectangle(90, 180, 60, 60),
            _playerRectangle = new Rectangle(0, 0, 75, 79);

        GraphicsDeviceManager _graphics;
        private int _width, _height;
        private Texture2D _backgroundTexture,  _sollidBrickTexture, _playerTexture;
        private Helpers.SpriteHelper _background, _sollidBrick,_player;
        private Vector2 _playerPos;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _width = _graphics.GraphicsDevice.Viewport.Width;
            _height = _graphics.GraphicsDevice.Viewport.Height;
            _playerPos = new Vector2(0, 0);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _backgroundTexture = Content.Load<Texture2D>("layout_level_empty");
            _background = new SpriteHelper(_backgroundTexture, _backgroundRectangle);

            _sollidBrickTexture = Content.Load<Texture2D>("layout_level_indestructible_stones");
            _sollidBrick= new SpriteHelper(_sollidBrickTexture,_sollidBrickRectangle);

            _playerTexture = Content.Load<Texture2D>("player1_spreadsheet_temp");
            _player= new SpriteHelper(_playerTexture,_playerRectangle);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit

            Input.Update();
            if (Input.KeyboardDownPressed)
            {
                _playerPos.Y = _playerPos.Y + 0.001f *gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Input.KeyboardUpPressed)
            {
                _playerPos.Y = _playerPos.Y - 0.001f *gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Input.KeyboardLeftPressed)
            {
                _playerPos.X = _playerPos.X - 0.001f * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Input.KeyboardRightPressed)
            {
                _playerPos.X = _playerPos.X + 0.001f * gameTime.ElapsedGameTime.Milliseconds;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _background.Render();
            SpriteHelper.DrawSprites(_width,_height);

            
            _sollidBrick.Render(80,180);
            _sollidBrick.Render(200, 180);
            _sollidBrick.Render(320, 180);
            _sollidBrick.Render(80, 300);
            _sollidBrick.Render(80, 420);
            SpriteHelper.DrawSprites(_width, _height);
            _player.Render(Convert.ToInt32(_playerPos.X*_width), Convert.ToInt32(_playerPos.Y*_height));
            SpriteHelper.DrawSprites(_width, _height);
            _sollidBrick.Render(200, 300);
            _sollidBrick.Render(320, 300);
            _sollidBrick.Render(200, 420);
            _sollidBrick.Render(320, 420);
            SpriteHelper.DrawSprites(_width,_height);


            base.Draw(gameTime);
        }
    }
}
