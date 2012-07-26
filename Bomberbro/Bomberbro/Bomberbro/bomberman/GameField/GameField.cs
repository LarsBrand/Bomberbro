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
        private Explosion _explosionProtoType;


        public int BlockWidth
        {
            get { return Convert.ToInt32(_fieldSize.X / xLenght); }
        }
        public int BlockHeight
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
            _explosionProtoType = new Explosion();
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
            for (int yPoint = 0; yPoint < yLenght; yPoint++)//draw verticals
            {
                float drawPercantageY = yPoint == 0 ? 0 : (float)(Convert.ToDouble(yPoint) / Convert.ToDouble(YLenght));
                for (int xPoint = 0; xPoint < xLenght; xPoint++)//draw horizontally
                {
                    float drawPercantageX = xPoint == 0 ? 0 : (float)(Convert.ToDouble(xPoint) / Convert.ToDouble(XLenght));
                    Rectangle drawRectangle = new Rectangle(Convert.ToInt32(_fieldSize.X * drawPercantageX + _position.X), Convert.ToInt32(_fieldSize.Y * drawPercantageY + _position.Y), BlockWidth, BlockHeight);
                    Gamefield[xPoint, yPoint].DrawAllItems(drawRectangle, gameTime);

                    foreach (KeyValuePair<int, Vector2> playerNumberAndRowHeight in playerPositionsInGrid)
                    {
                        if (playerNumberAndRowHeight.Value.Y == yPoint && xPoint == xLenght - 1)//The player row is being drawn. should draw player else he is placed in front of the blocks.
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
                pointOnGrid.X = (int)Math.Floor(xCord / (BlockWidth));
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

        public void LoadAllFieldContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            //_backgroundTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            //_backgroundTexture.SetData(new Color[] { Color.CornflowerBlue });
            //_background = new SpriteHelper(_backgroundTexture, new Rectangle(0, 0, 1, 1));

            _explosionProtoType.LoadContent(content);
            for (int i = 0; i < Gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < Gamefield.GetLength(1); j++)
                {
                    Gamefield[i, j].LoadAllItems(content);
                }

            }
        }

        public void PlaceBomb(BomberManGuy bomberManGuy)
        {
            Vector2 bombpoint = GetPosistionInGrid(bomberManGuy.GetCenterOfPositionedHitBox(_fieldScale));
            if (_gamefield[(int)bombpoint.X, (int)bombpoint.Y].GetBomb() == null) //Make sure there isn't a bomb already
            {
                Bomb newBomb = bomberManGuy.GetBomb();
                if (newBomb != null)
                {
                    _gamefield[(int)bombpoint.X, (int)bombpoint.Y].Items.Add(newBomb);
                }

            }
        }

        public void updateField(GameTime gameTime, List<BomberManGuy> players)
        {
            for (int yPoint = 0; yPoint < yLenght; yPoint++)
            {
                for (int xPoint = 0; xPoint < xLenght; xPoint++)
                {
                    _gamefield[xPoint, yPoint].UpdateAllItems(gameTime);
                }
            }
            //remove bombs
            HandleBombs();
            //handle explosions              
            HandleExplosions();

            CollisionChecker.CheckExplosionsOnPLayers(this, players);
        }

        private void HandleBombs()
        {
            for (int yPoint = 0; yPoint < yLenght; yPoint++)
            {
                for (int xPoint = 0; xPoint < xLenght; xPoint++)
                {
                    Bomb placedBomb = _gamefield[xPoint, yPoint].GetBomb();
                    if (placedBomb != null)
                    {
                        if (placedBomb.Exploded)
                        {
                            //remove it
                            _gamefield[xPoint, yPoint].Items.Remove(placedBomb);
                            CreateExplosion(placedBomb, xPoint, yPoint);
                        }
                    }
                }
            }
        }

        private void HandleExplosions()
        {
            for (int yPoint = 0; yPoint < yLenght; yPoint++)
            {
                for (int xPoint = 0; xPoint < xLenght; xPoint++)
                {
                    List<Explosion> explosions = _gamefield[xPoint, yPoint].GetExplosions();
                    foreach (Explosion explosion in explosions)
                    {
                        if (explosions != null)
                        {
                            if (explosion.CanRemove)
                            {
                                //Explosion can be removed
                                _gamefield[xPoint, yPoint].Items.Remove(explosion);
                            }
                            else
                            {
                                //set the correct explosion type

                                bool up = false, down = false, left = false, right = false;


                                if (xPoint - 1 > 0 && _gamefield[xPoint - 1, yPoint].GetExplosions().Count != 0)
                                {
                                    left = true;
                                }
                                if (xPoint + 1 < _gamefield.GetLength(0) && _gamefield[xPoint + 1, yPoint].GetExplosions().Count != 0)
                                {
                                    right = true;
                                }
                                if (yPoint - 1 > 0 && _gamefield[xPoint, yPoint - 1].GetExplosions().Count != 0)
                                {
                                    up = true;
                                }
                                if (yPoint + 1 < _gamefield.GetLength(1) && _gamefield[xPoint, yPoint + 1].GetExplosions().Count != 0)
                                {
                                    down = true;
                                }

                                GetCorrectExplosionStyle(xPoint, yPoint, right, left, up, down, explosion);
                            }
                        }
                    }
                }
            }
        }

        private void GetCorrectExplosionStyle(int xPoint, int yPoint, bool right, bool left, bool up, bool down, Explosion explosion)
        {

            //if (up && down && left && right)
            //{
            //    explosion.ExplosionType = ExplosionTypes.Cross;
            //}
            //else
            //{
            bool unset = true;
            if (up && !down)
            {
                explosion.ExplosionType = ExplosionTypes.DownEnd;
                unset = false;
            }
            if (down && !up)
            {
                explosion.ExplosionType = ExplosionTypes.UpEnd;
                unset = false;
            }
            if (up && down)
            {

                if (FindExplosionCross(true, xPoint, yPoint, explosion))
                {
                    explosion.ExplosionType = ExplosionTypes.Up;
                    unset = false;
                }
                else
                {
                    explosion.ExplosionType = ExplosionTypes.Down;
                    unset = false;
                }
            }


            if (left && !right)
            {
                explosion.ExplosionType = ExplosionTypes.RightEnd;
                unset = false;
            }
            if (right && !left)
            {
                explosion.ExplosionType = ExplosionTypes.LeftEnd;
                unset = false;
            }
            if (left && right)
            {
                if (FindExplosionCross(false, xPoint, yPoint, explosion))
                {
                    explosion.ExplosionType = ExplosionTypes.Left;
                    unset = false;
                }
                else
                {
                    explosion.ExplosionType = ExplosionTypes.Right;
                    unset = false;
                }
                //}
            }
            if (unset)
            {
                throw new Exception("OhNO strange behavior");
            }
        }

        /// <summary>
        /// Look for the explosioncross to set the correct explosion type
        /// </summary>
        /// <param name="UpDown">Search up down or left right</param>
        /// <param name="xPoint"> </param>
        /// <param name="yPoint"> </param>
        /// <param name="explosion">the explosion</param>
        /// <returns>true for cross is down or left, flase for cross is up or right </returns>
        private bool FindExplosionCross(bool UpDown, int xPoint, int yPoint, Explosion explosion)
        {
            if (UpDown)
            {
                for (int i = yPoint; i > 0; i--)
                {
                    List<Explosion> possibleCrossExplosions = _gamefield[xPoint, i].GetExplosions();
                    foreach (Explosion possibleCrossExplosion in possibleCrossExplosions)
                    {

                        if (possibleCrossExplosion != null)
                        {
                            if (possibleCrossExplosion.ExplosionType == ExplosionTypes.Cross)
                            {//we found the cross in above us,
                                return false;
                            }
                        }
                    }
                }
                //we haven't returned for values above us, the cross must be downwards.
                return true;
            }
            else
            {
                for (int i = xPoint; i > 0; i--)
                {
                    List<Explosion> possibleCrossExplosions = _gamefield[i, yPoint].GetExplosions();
                    foreach (var possibleCrossExplosion in possibleCrossExplosions)
                    {
                        if (possibleCrossExplosion != null)
                        {
                            if (possibleCrossExplosion.ExplosionType == ExplosionTypes.Cross)
                            {//we found the cross on the right from us,
                                return false;
                            }
                        }
                    }
                }
                //we haven't returned for values right us, the cross must be on our left.
                return true;
            }
        }

        private void CreateExplosion(Bomb placedBomb, int xPoint, int yPoint)
        {
            _gamefield[xPoint, yPoint].Items.Add(_explosionProtoType.Copy(ExplosionTypes.Cross));
            bool stopDown = false, stopUp = false, stopLeft = false, stopRight = false;

            for (int i = 1; i <= placedBomb.Power; i++)
            {

                SetStopsForExplosion(xPoint, yPoint, i, ref stopLeft, ref stopDown, ref stopUp, ref stopRight);

                if (!stopRight)
                {
                    _gamefield[xPoint + i, yPoint].Items.Add(_explosionProtoType.Copy());
                    ExplodeBombHere(xPoint + i, yPoint);
                }

                if (!stopLeft)
                {
                    _gamefield[xPoint - i, yPoint].Items.Add(_explosionProtoType.Copy());
                    ExplodeBombHere(xPoint - i, yPoint);

                }
                if (!stopDown)
                {
                    _gamefield[xPoint, yPoint + i].Items.Add(_explosionProtoType.Copy());
                    ExplodeBombHere(xPoint, yPoint + i);
                }
                if (!stopUp)
                {
                    _gamefield[xPoint, yPoint - i].Items.Add(_explosionProtoType.Copy());
                    ExplodeBombHere(xPoint, yPoint - i);
                }
            }

        }

        private void ExplodeBombHere(int xPoint, int yPoint)
        {            
            Bomb explodeThis = _gamefield[xPoint, yPoint].GetBomb();
            if (explodeThis != null)
            {
                explodeThis.Explode();
            }
        }

        private void SetStopsForExplosion(int xPoint, int yPoint, int i, ref bool stopLeft, ref bool stopDown, ref bool stopUp, ref bool stopRight)
        {
            if (xPoint - i < 0)
            {
                stopLeft = true;
            }
            else
            {
                //We're using this notation to keep the value of original boolean, in case our method returns false
                stopLeft = StopExplosionOnBrick(xPoint - i, yPoint) || stopLeft;
            }
            if (yPoint - i < 0)
            {
                stopUp = true;
            }
            else
            {
                stopUp = StopExplosionOnBrick(xPoint, yPoint - i) || stopUp;
            }
            if (xPoint + i > _gamefield.GetLength(0) - 1)
            {
                stopRight = true;
            }
            else
            {
                stopRight = StopExplosionOnBrick(xPoint + i, yPoint) || stopRight;
            }
            if (yPoint + i > _gamefield.GetLength(1) - 1)
            {
                stopDown = true;
            }
            else
            {
                stopDown = StopExplosionOnBrick(xPoint, yPoint + i) || stopDown;
            }
        }

        private bool StopExplosionOnBrick(int xPoint, int yPoint)
        {
            bool stop = false;
            Brick brick = _gamefield[xPoint, yPoint].GetBrick();
            if (brick != null && brick.CollisionType == CollisionTypes.Block)
            {
                stop = true;
            }
            return stop;
        }

        public bool wasPlayerOnThisPointPreviously(GameField gameField, float xPos, float yPos, MoveObject moveObject)
        {
            Vector2 topLeft = GetPosistionInGrid(new Vector2(moveObject.PreviousHitBox.Left, moveObject.PreviousHitBox.Top));
            Vector2 topRight = GetPosistionInGrid(new Vector2(moveObject.PreviousHitBox.Right, moveObject.PreviousHitBox.Top));
            Vector2 bottomLeft = GetPosistionInGrid(new Vector2(moveObject.PreviousHitBox.Left, moveObject.PreviousHitBox.Bottom));
            Vector2 bottomRight = GetPosistionInGrid(new Vector2(moveObject.PreviousHitBox.Right, moveObject.PreviousHitBox.Bottom));
            if (topLeft.X == xPos && topLeft.Y == yPos)
            {
                return true;
            }
            if (topRight.X == xPos && topRight.Y == yPos)
            {
                return true;
            }
            if (bottomLeft.X == xPos && bottomLeft.Y == yPos)
            {
                return true;
            }
            if (bottomRight.X == xPos && bottomRight.Y == yPos)
            {
                return true;
            }
            return false;
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
        public void UpdateAllItems(GameTime gameTime)
        {
            foreach (var gamefieldItem in Items)
            {
                gamefieldItem.Update(gameTime);
            }
        }

        public Bomb GetBomb()
        {
            foreach (GamefieldItem gamefieldItem in Items)
            {
                if (gamefieldItem.GetType() == typeof(Bomb))
                {
                    return (Bomb)gamefieldItem;
                }
            }
            return null;
        }

        public List<Explosion> GetExplosions()
        {
            List<Explosion> explosions = new List<Explosion>();
            foreach (GamefieldItem gamefieldItem in Items)
            {
                if (gamefieldItem.GetType() == typeof(Explosion))
                {
                    explosions.Add((Explosion)gamefieldItem);
                }
            }
            return explosions;
        }

        public Brick GetBrick()
        {
            foreach (GamefieldItem gamefieldItem in Items)
            {
                if (gamefieldItem is Brick)
                {
                    return (Brick)gamefieldItem;
                }
            }
            return null;
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
        Block, Empty, Bomb, PowerUp, Explosion
    }
}
