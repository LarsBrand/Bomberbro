using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bomberbro.bomberman
{
    public static class CollisionChecker
    {
        public static Vector2 CheckPlayerMovement(GameField gameField, List<BomberManGuy> players, int playerID, MoveObject movement)
        {
            Vector2 newpoint = checkCollisionWithGameField(gameField, players[playerID], movement);

            CheckCollisionWithBombs();
            CheckCollisionWithPlayers();
            return newpoint;
        }

        private static Vector2 checkCollisionWithGameField(GameField gameField, BomberManGuy bomberManGuy, MoveObject movement)
        {
            Vector2 newPosition = movement.DestinationScreenPos;
            float movementX = movement.DestinationScreenPos.X - movement.OrigionalScreenPos.X;
            float movementY = movement.DestinationScreenPos.Y - movement.OrigionalScreenPos.Y;

            foreach (Vector2 gamefieldPos in movement.DestinationGameFieldPosition)
            {
                foreach (GamefieldItem gamefieldItem in gameField.Gamefield[(int)gamefieldPos.X, (int)gamefieldPos.Y].Items)
                {
                    switch (gamefieldItem.CollisionType)
                    {
                        case CollisionTypes.Block:
                            newPosition = BlockMovement(gameField, movement, movementY, movementX, gamefieldPos);

                            break;
                        case CollisionTypes.Bomb:
                            if (WasPlayerOnThisPointPreviously(gameField, gamefieldPos.X, gamefieldPos.Y, movement))
                            {
                                newPosition = UnRestrictedWalk(newPosition,movement);
                            }
                            else
                            {
                                newPosition = BlockMovement(gameField, movement, movementY, movementX, gamefieldPos);
                            }
                            break;
                        case CollisionTypes.PowerUp:
                            //something ellie
                            break;
                        case CollisionTypes.Explosion:
                            bomberManGuy.Dead = true;
                            break;

                        case CollisionTypes.Empty:
                        default:
                            newPosition = UnRestrictedWalk(movement.DestinationScreenPos,movement);

                            break;
                    }

                }
            }
            return newPosition;

        }

        /// <summary>
        /// Check if the player was at that point previus update, for when walking on and off a bomb
        /// </summary>
        /// <param name="gameField"></param>
        /// <param name="xPos"> </param>
        /// <param name="yPos"> </param>
        /// <param name="moveObject"> </param>
        /// <returns>true if the player was on that point previously. thus ignore the bomb</returns>
        private static bool WasPlayerOnThisPointPreviously(GameField gameField, float xPos, float yPos, MoveObject moveObject)
        {
            return gameField.wasPlayerOnThisPointPreviously(gameField, xPos, yPos, moveObject);

        }

        private static Vector2 UnRestrictedWalk(Vector2 newPosition, MoveObject movement)
        {

            if (newPosition.X == movement.DestinationScreenPos.X && newPosition.Y == movement.DestinationScreenPos.Y)
                newPosition = movement.DestinationScreenPos;
            return newPosition;
        }

        private static Vector2 BlockMovement(GameField gameField, MoveObject movement, float movementY, float movementX, Vector2 gamefieldPos)
        {
            Vector2 newPosition;
            newPosition = movement.DestinationScreenPos;
            //stop against that wall.
            Rectangle targetBlock = gameField.GetBlocksRectange(gamefieldPos);
            if (movementX > 0 && movement.DestinationHitBox.Right >= targetBlock.Left)
            {
                //Set the player next the left side of the block,
                //Align the hitbox with the wall
                int actualMovement = movement.DestinationHitBox.Right - targetBlock.Left;
                newPosition.X = newPosition.X - actualMovement - 1;
            }
            else if (movementX < 0 && movement.DestinationHitBox.Left <= targetBlock.Right)
            {
                //set the player to the right side of the block
                int actualMovement = movement.DestinationHitBox.Left - targetBlock.Right;
                newPosition.X = newPosition.X - actualMovement + 1;
            }
            if (movementY > 0 && movement.DestinationHitBox.Bottom >= targetBlock.Top)
            {
                //set the player to the bottom side of the block
                int actualMovement = movement.DestinationHitBox.Bottom - targetBlock.Top;
                newPosition.Y = newPosition.Y - actualMovement - 1;
            }
            else if (movementY < 0 && movement.DestinationHitBox.Top <= targetBlock.Bottom)
            {
                //set the player to the top of the block.
                int actualMovement = movement.DestinationHitBox.Top - targetBlock.Bottom;
                newPosition.Y = newPosition.Y - actualMovement + 1;
            }
            return newPosition;
        }

        private static void CheckCollisionWithPlayers()
        {
            //throw new NotImplementedException();
        }

        private static void CheckCollisionWithBombs()
        {
            //throw new NotImplementedException();
        }

    }

    public class MoveObject
    {
        public Vector2 OrigionalScreenPos;
        public Vector2 DestinationScreenPos;
        public Rectangle OrigionalHitBox;
        public Rectangle DestinationHitBox;
        public Rectangle PreviousHitBox;
        public List<Vector2> OriginalGamefieldPosition;
        public List<Vector2> DestinationGameFieldPosition;
    }
}
