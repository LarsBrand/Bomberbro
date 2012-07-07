using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberbro.bomberman
{
    public class GameField
    {
        private GamefieldItems[,] gamefield;
        private Texture2D _backgroundTexture;
        private SpriteHelper _background;

        public GameField(GamefieldItems[,] gamefield)
        {
            this.gamefield = gamefield;
        }

        public void DrawGameField(Vector2 fieldSize, Vector2 totalSize, Vector2 position, List<bomberManGuy> players, GameTime gameTime)
        {
            //Rectangle bgrec = new Rectangle((int)position.X, (int)position.Y, (int)fieldSize.X, (int)fieldSize.Y);
            //_background.Render(bgrec);
            int xLenght = gamefield.GetLength(0);
            int yLenght = gamefield.GetLength(1);
            int blockWidth = (int)(fieldSize.X / xLenght);
            int blockHeight = (int)(fieldSize.Y / yLenght);

            //determing when to end the spritebatch calls based on player positions
            Dictionary<int, int> playerHeigts = new Dictionary<int, int>();
            for (int i = 0; i < players.Count; i++)
            {
                int row = GetRowHeight(fieldSize.Y, players[i].Position.Y - position.Y, blockHeight);
                Debug.WriteLine(row.ToString());
                playerHeigts.Add(i, row);
            }

            //Draw the game field
            for (int j = 0; j < yLenght; j++)//draw verticals
            {
                float drawPercantageY = j == 0 ? 0 : (float)(Convert.ToDouble(j) / Convert.ToDouble(yLenght));
                for (int i = 0; i < xLenght; i++)//draw horizontally
                {
                    float drawPercantageX = i == 0 ? 0 : (float)(Convert.ToDouble(i) / Convert.ToDouble(xLenght));
                    Rectangle drawRectangle = new Rectangle(Convert.ToInt32(fieldSize.X * drawPercantageX + position.X), Convert.ToInt32(fieldSize.Y * drawPercantageY + position.Y), blockWidth, blockHeight);
                    gamefield[i, j].DrawAllItems(drawRectangle, gameTime);

                    foreach (KeyValuePair<int, int> playerNumberAndRowHeight in playerHeigts)
                    {
                        if (playerNumberAndRowHeight.Value == j)//The player row is being drawn. should draw player else he is placed in front of the blocks.
                        {
                            SpriteHelper.DrawSprites((int)totalSize.X, (int)totalSize.Y);
                            players[playerNumberAndRowHeight.Key].draw();
                            SpriteHelper.DrawSprites((int)totalSize.X, (int)totalSize.Y);
                        }
                    }
                }

            }
            SpriteHelper.DrawSprites((int)totalSize.X, (int)totalSize.Y);
        }

        public int GetRowHeight(float fieldHeigt, float currentHeight, int blockSize)
        {
            int rowNumber = 0;
            for (int i = 0; i * blockSize < currentHeight; i++)
            {
                rowNumber = i;
            }
            return rowNumber + 1;
        }

        public void LoadAllFieldContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            _backgroundTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _backgroundTexture.SetData(new Color[] { Color.CornflowerBlue });
            _background = new SpriteHelper(_backgroundTexture, new Rectangle(0, 0, 1, 1));

            for (int i = 0; i < gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < gamefield.GetLength(1); j++)
                {
                    gamefield[i, j].LoadAllItems(content);
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
