using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bomberbro.bomberman
{
    public class BombermanGame : IGameState
    {
        GraphicsDeviceManager _graphics;
        private ContentManager _content;
        private int _width, _height;

        private static Rectangle
            _backgroundRectangle = new Rectangle(0, 0, 840, 690);



        private Texture2D _backgroundTexture;
        private Helpers.SpriteHelper _background;
        private Vector2 _fieldSize;
        private Vector2 _totalSize;
        private Vector2 _fieldPosition;
        private List<BomberManGuy> _players;
        private GameField _gameField;
        private BomberManInput _bomberManInput;

        public BombermanGame(ContentManager content, GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            _content = content;
            _graphics.PreferredBackBufferWidth = 840;
            _graphics.PreferredBackBufferHeight = 690;
        }
        public void Initialize()
        {

            _width = _graphics.GraphicsDevice.Viewport.Width;
            _height = _graphics.GraphicsDevice.Viewport.Height;


            float fieldWidth = 840;
            float fieldHeight = 600;
            _fieldSize = new Vector2(fieldWidth, fieldHeight);
            _totalSize = new Vector2(_width, _height);
            _fieldPosition = new Vector2(0, 90);

            buildGameField();

            _players = new List<BomberManGuy>();
            _players.Add(new BomberManGuy());
            _players[0].PlayerHitBoxHeight =Convert.ToInt32( _gameField.BlockHeight*0.8f);
            _players[0].PlayerHitBoxWidth = Convert.ToInt32(_gameField.BlockWidth*0.8f);

            _players[0].Position = new Vector2((_fieldPosition.X + _gameField.BlockWidth) +1, (_fieldPosition.Y + _gameField.BlockHeight) + 1);

            _bomberManInput = new BomberManInput(_players, _gameField);
        }

        private void buildGameField()
        {
            int sizeX = 11;
            int sizeY = 9;

            GamefieldItems[,] buildingField = new GamefieldItems[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    List<GamefieldItem> items = new List<GamefieldItem>();
                    if (i % 2 == 0 && j % 2 == 0)
                        items.Add(new BrickSollid());

                    GamefieldItems gamefieldItems = new GamefieldItems(items);
                    buildingField[i, j] = gamefieldItems;
                }
            }
            List<GamefieldItem> brickBorderItem = new List<GamefieldItem>();
            brickBorderItem.Add(new BrickSollid());
            GamefieldItems brickBorderItems = new GamefieldItems(brickBorderItem);

            for (int i = 0; i < sizeX; i++)
            {
                buildingField[i, 0] = brickBorderItems;
                buildingField[i, sizeY - 1] = brickBorderItems;
            }
            for (int i = 0; i < sizeY; i++)
            {
                buildingField[0, i] = brickBorderItems;
                buildingField[sizeX - 1, i] = brickBorderItems;
            }
            _gameField = new GameField(buildingField, _totalSize, _fieldSize, _fieldPosition);
        }

        public void LoadContent()
        {
            _backgroundTexture = _content.Load<Texture2D>("layout_level_empty");
            _background = new SpriteHelper(_backgroundTexture, _backgroundRectangle);


            foreach (var bomberManGuy in _players)
            {
                bomberManGuy.LoadContent(_content,_graphics);
            }
            _gameField.LoadAllFieldContent(_content, _graphics);

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            Input.Update();

            _bomberManInput.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {

            _background.Render();
            SpriteHelper.DrawSprites(_width, _height);
            _gameField.DrawGameField(_players, gameTime);

            //foreach (var bomberManGuy in _players)
            //{
            //    bomberManGuy.draw();
            //}
            SpriteHelper.DrawSprites(_width, _height);

        }
    }
}
