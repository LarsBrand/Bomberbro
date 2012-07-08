using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Bomberbro.bomberman
{
    public class GameField
    {
        private const int widthOfOneBlock = 60;
        private const int heigthOfOneBlock = 60;

        private GamefieldItems[,] _gamefield;
        private Texture2D _backgroundTexture;
        private SpriteHelper _background;
        private float _fieldScale;
        private Vector2 _fieldSize;
        private Vector2 _totalSize;
        private Vector2 _position;



        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public GameField(GamefieldItems[,] gamefield, Vector2 totalSize, Vector2 fieldSize, Vector2 position)
        {
            _gamefield = gamefield;
            _fieldSize = fieldSize;
            _totalSize = totalSize;
            _position = position;
            float TempfieldScale = fieldSize.X/(widthOfOneBlock*gamefield.GetLength(0));
            _fieldScale = fieldSize.Y / (heigthOfOneBlock*gamefield.GetLength(1));
            if (TempfieldScale != _fieldScale)
            {
                MessageBox(new IntPtr(0), "OH no, The fields ratio's are off, this might look ugly", "Made a new gamefield", 0);
            }
        }

        public void DrawGameField(List<bomberManGuy> players, GameTime gameTime)
        {
            //Rectangle bgrec = new Rectangle((int)_position.X, (int)_position.Y, (int)_fieldSize.X, (int)_fieldSize.Y);
            //_background.Render(bgrec);
            int xLenght = _gamefield.GetLength(0);
            int yLenght = _gamefield.GetLength(1);
            int blockWidth = (int)(_fieldSize.X / xLenght);
            int blockHeight = (int)(_fieldSize.Y / yLenght);

            //determing when to end the spritebatch calls based on player positions
            Dictionary<int, int> playerHeigts = new Dictionary<int, int>();
            for (int i = 0; i < players.Count; i++)
            {
                int row = GetRowHeight(_fieldSize.Y, players[i].Position.Y - _position.Y, blockHeight);
                // Debug.WriteLine(row.ToString());
                playerHeigts.Add(i, row);
            }

            //Draw the game field
            for (int j = 0; j < yLenght; j++)//draw verticals
            {
                float drawPercantageY = j == 0 ? 0 : (float)(Convert.ToDouble(j) / Convert.ToDouble(yLenght));
                for (int i = 0; i < xLenght; i++)//draw horizontally
                {
                    float drawPercantageX = i == 0 ? 0 : (float)(Convert.ToDouble(i) / Convert.ToDouble(xLenght));
                    Rectangle drawRectangle = new Rectangle(Convert.ToInt32(_fieldSize.X * drawPercantageX + _position.X), Convert.ToInt32(_fieldSize.Y * drawPercantageY + _position.Y), blockWidth, blockHeight);
                    _gamefield[i, j].DrawAllItems(drawRectangle, gameTime);

                    foreach (KeyValuePair<int, int> playerNumberAndRowHeight in playerHeigts)
                    {
                        if (playerNumberAndRowHeight.Value == j)//The player row is being drawn. should draw player else he is placed in front of the blocks.
                        {
                            SpriteHelper.DrawSprites((int)_totalSize.X, (int)_totalSize.Y);
                            players[playerNumberAndRowHeight.Key].Draw(_fieldScale);
                            SpriteHelper.DrawSprites((int)_totalSize.X, (int)_totalSize.Y);
                        }
                    }
                }

            }
            SpriteHelper.DrawSprites((int)_totalSize.X, (int)_totalSize.Y);
        }

        public int GetRowHeight(float fieldHeigt, float currentHeight, int blockSize)
        {
            int rowNumber = 0;
            for (int i = 0; i * blockSize < currentHeight; i++)
            {
                rowNumber = i;
            }
            return rowNumber;
        }

        public void LoadAllFieldContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            _backgroundTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _backgroundTexture.SetData(new Color[] { Color.CornflowerBlue });
            _background = new SpriteHelper(_backgroundTexture, new Rectangle(0, 0, 1, 1));

            for (int i = 0; i < _gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < _gamefield.GetLength(1); j++)
                {
                    _gamefield[i, j].LoadAllItems(content);
                }

            }
        }
    }

    public class GamefieldItems
    {
        private List<GamefieldItem> items;

        public GamefieldItems(List<GamefieldItem> items)
        {
            this.items = items;
        }

        public void DrawAllItems(Rectangle rect, GameTime gameTime)
        {
            foreach (var gamefieldItem in items)
            {
                gamefieldItem.Draw(rect, gameTime);
            }
        }
        public void LoadAllItems(ContentManager content)
        {
            foreach (var gamefieldItem in items)
            {
                gamefieldItem.LoadContent(content);
            }
        }
    }



    public abstract class GamefieldItem
    {

        public abstract void Draw(Rectangle rect, GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent(ContentManager content);
    }
}
