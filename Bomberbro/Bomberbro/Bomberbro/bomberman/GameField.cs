using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Bomberbro.bomberman
{
    public class GameField
    {
        private GamefieldItems[,] gamefield;

        public GameField(GamefieldItems[,] gamefield)
        {
            this.gamefield = gamefield;
        }

        public void DrawGameField(Vector2 fieldSize, Vector2 TotalSize, Vector4 Offsets, List<bomberManGuy> players, GameTime gameTime)
        {
            //determing when to end the spritebatch calls based on player positions
            int xLenght = gamefield.GetLength(1);
            int yLenght = gamefield.GetLength(0);
            for (int i = 0; i < xLenght; i++)//draw horizontally
            {
                float drawPercantageX = i == 0 ? 0 : (float)Convert.ToDouble(xLenght) / i;
                for (int j = 0; j < yLenght; j++)//draw verticals
                {
                    float drawPercantageY = j == 0 ? 0 : (float)Convert.ToDouble(yLenght) / j;
                    gamefield[i, j].DrawAllItems(Convert.ToInt32(fieldSize.X * drawPercantageX) + (int)Offsets.X, Convert.ToInt32(fieldSize.Y * drawPercantageY) + (int)Offsets.Y, gameTime);
                }

            }
            SpriteHelper.DrawSprites((int)TotalSize.X, (int)TotalSize.Y);
        }

        public void LoadAllFieldContent(ContentManager content)
        {
            for (int i = 0; i < gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < gamefield.GetLength(1); j++)
                {
                    gamefield[i,j].LoadAllItems(content);
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

        public void DrawAllItems(int positionX, int positionY, GameTime gameTime)
        {
            foreach (var gamefieldItem in items)
            {
                gamefieldItem.Draw(positionX, positionY, gameTime);
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

        public abstract void Draw(int postionX, int posistionY, GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent(ContentManager content);
    }
}
