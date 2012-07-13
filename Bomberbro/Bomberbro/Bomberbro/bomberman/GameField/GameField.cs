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
        public static int WidthOfOneBlock = 60;
        public static int HeigthOfOneBlock = 60;

        private GamefieldItems[,] _gamefield;
        private Texture2D _backgroundTexture;
        private SpriteHelper _background;
        private float _fieldScale;
        private Vector2 _fieldSize;
        private Vector2 _totalSize;
        private Vector2 _position;
        private int xLenght;
        private int yLenght;

        public int BlockWidth
        {
            get { return Convert.ToInt32(_fieldSize.X/xLenght); }
        }
        public  int BlockHeight
        {
            get { return Convert.ToInt32(_fieldSize.Y / yLenght); }
        }
        



        public GamefieldItems[,] Gamefield
        {
            get { return _gamefield; }
            set { _gamefield = value; }
        }

        public float FieldScale
        {
            get { return _fieldScale; }
            set { _fieldScale = value; }
        }

        public int XLenght
        {
            get { return xLenght; }
            set { xLenght = value; }
        }

        public int YLenght
        {
            get { return yLenght; }
            set { yLenght = value; }
        }

        public Rectangle GetBlocksRectange(Vector2 blockPoint)
        {
            return new Rectangle(Convert.ToInt32((blockPoint.X * BlockWidth) + _position.X), Convert.ToInt32((blockPoint.Y * BlockHeight) + _position.Y), BlockWidth, BlockHeight);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public GameField(GamefieldItems[,] gamefield, Vector2 totalSize, Vector2 fieldSize, Vector2 position)
        {
            Gamefield = gamefield;
            _fieldSize = fieldSize;
            _totalSize = totalSize;
            _position = position;
            float tempfieldScale = fieldSize.X / (WidthOfOneBlock * gamefield.GetLength(0));
            FieldScale = fieldSize.Y / (HeigthOfOneBlock * gamefield.GetLength(1));
            if (Math.Abs(tempfieldScale - FieldScale) > 0.2f)
            {
                MessageBox(new IntPtr(0), "OH no, The fields ratio's are off, this might look ugly", "Made a new gamefield", 0);
            }

            XLenght = gamefield.GetLength(0);
            YLenght = gamefield.GetLength(1);
        }

        public void DrawGameField(List<BomberManGuy> players, GameTime gameTime)
        {
            //Rectangle bgrec = new Rectangle((int)_position.X, (int)_position.Y, (int)_fieldSize.X, (int)_fieldSize.Y);
            //_background.Render(bgrec);
        
           

            //determing when to end the spritebatch calls based on player positions
            Dictionary<int, Vector2> playerPositionsInGrid = new Dictionary<int, Vector2>();
            for (int i = 0; i < players.Count; i++)
            {

                Vector2 playerPosition = GetPosistionInGrid(players[i].Position);

                playerPositionsInGrid.Add(i, playerPosition);
            }
            //Draw the game field
            for (int j = 0; j < YLenght; j++)//draw verticals
            {
                float drawPercantageY = j == 0 ? 0 : (float)(Convert.ToDouble(j) / Convert.ToDouble(YLenght));
                for (int i = 0; i < XLenght; i++)//draw horizontally
                {
                    float drawPercantageX = i == 0 ? 0 : (float)(Convert.ToDouble(i) / Convert.ToDouble(XLenght));
                    Rectangle drawRectangle = new Rectangle(Convert.ToInt32(_fieldSize.X * drawPercantageX + _position.X), Convert.ToInt32(_fieldSize.Y * drawPercantageY + _position.Y), BlockWidth, BlockHeight);
                    Gamefield[i, j].DrawAllItems(drawRectangle, gameTime);

                    foreach (KeyValuePair<int, Vector2> playerNumberAndRowHeight in playerPositionsInGrid)
                    {
                        if (playerNumberAndRowHeight.Value.Y == j && i == xLenght-1)//The player row is being drawn. should draw player else he is placed in front of the blocks.
                        {   //Note: && i==ylength, We want to do this only once
                            SpriteHelper.DrawSprites((int)_totalSize.X, (int)_totalSize.Y);
                            players[playerNumberAndRowHeight.Key].Draw(FieldScale);
                            SpriteHelper.DrawSprites((int)_totalSize.X, (int)_totalSize.Y);
                        }
                    }
                }

            }
            SpriteHelper.DrawSprites((int)_totalSize.X, (int)_totalSize.Y);
        }

        public Vector2 GetPosistionInGrid(Vector2 screenPoint)
        {
            Vector2 pointOnGrid = new Vector2();

            //determine row
            float yCord = screenPoint.Y - _position.Y;
            if (yCord < 0 || yCord > _fieldSize.Y)
            {   //Outside of the grid
                pointOnGrid.Y = -1;
            }
            else
            {
                float trying = yCord / BlockHeight;
                pointOnGrid.Y = (int)Math.Floor(yCord / BlockHeight);
            }

            float xCord = screenPoint.X - _position.X;
            if (xCord < 0 || xCord > _fieldSize.X)
            {   //Outside of the grid
                pointOnGrid.X = -1;
            }
            else
            {
                float trying = xCord / (BlockWidth);
                pointOnGrid.X = (int)Math.Floor(xCord / (BlockWidth ));
            }
            //  Console.WriteLine(pointOnGrid);
            return pointOnGrid;
        }

        public List<Vector2> getPosistionOfRectInGrid(Rectangle rect)
        {
            List<Vector2> thePlaces = new List<Vector2>();
            Vector2 topRigt = new Vector2(rect.Right, rect.Top);
            Vector2 topLeft = new Vector2(rect.Left, rect.Top);
            Vector2 bottomRigt = new Vector2(rect.Right, rect.Bottom);
            Vector2 bottomLeft = new Vector2(rect.Left, rect.Bottom);
            Vector2 gridTopright = GetPosistionInGrid(topRigt);
            Vector2 gridTopLeft = GetPosistionInGrid(topLeft);
            Vector2 gridBottomRight = GetPosistionInGrid(bottomRigt);
            Vector2 gridBottomLeft = GetPosistionInGrid(bottomLeft);
            //We got all the places, we remove double values because of performance
            thePlaces.Add(gridTopLeft);
            thePlaces = addVectorIfNotAlreadyIn(thePlaces, gridTopright);
            thePlaces = addVectorIfNotAlreadyIn(thePlaces, gridBottomLeft);
            thePlaces = addVectorIfNotAlreadyIn(thePlaces, gridBottomRight);
            return thePlaces;
        }

        private List<Vector2> addVectorIfNotAlreadyIn(List<Vector2> theList, Vector2 theVector)
        {
            bool isEqual = false;
            foreach (Vector2 vectorInList in theList)
            {
                if (vectorInList.X == theVector.X && vectorInList.Y == theVector.Y)
                    isEqual = true;
            }
            if (!isEqual)
            {
                theList.Add(theVector);
            }
            return theList;
        }

        #region oldCode
        //OldCode to dertmine what the row was
        //public int GetRowHeight(float fieldHeigt, float currentHeight, int blockSize)
        //{
        //    int rowNumber = 0;
        //    for (int i = 0; i * blockSize < currentHeight; i++)
        //    {
        //        rowNumber = i;
        //    }
        //    return rowNumber;
        //}

        #endregion
        public void LoadAllFieldContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            //_backgroundTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            //_backgroundTexture.SetData(new Color[] { Color.CornflowerBlue });
            //_background = new SpriteHelper(_backgroundTexture, new Rectangle(0, 0, 1, 1));

            for (int i = 0; i < Gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < Gamefield.GetLength(1); j++)
                {
                    Gamefield[i, j].LoadAllItems(content);
                }

            }
        }
    }

    public class GamefieldItems
    {
        private List<GamefieldItem> _items;



        public GamefieldItems(List<GamefieldItem> items)
        {
            Items = items;
        }

        public List<GamefieldItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void DrawAllItems(Rectangle rect, GameTime gameTime)
        {
            foreach (var gamefieldItem in Items)
            {
                gamefieldItem.Draw(rect, gameTime);
            }
        }
        public void LoadAllItems(ContentManager content)
        {
            foreach (var gamefieldItem in Items)
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
        private CollisionTypes _collisionType;

        public CollisionTypes CollisionType
        {
            get { return _collisionType; }
            set { _collisionType = value; }
        }
    }
    public enum CollisionTypes
    {
        Block, Empty, Moveable, PowerUp
    }
}
