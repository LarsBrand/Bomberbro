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
            checkCollisionWithBombs();
            checkCollisionWithPlayers();
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
                            newPosition = movement.DestinationScreenPos;
                            //stop against that wall.
                            Rectangle targetBlock = gameField.GetBlocksRectange(gamefieldPos);
                            if (movementX > 0 && movement.DestinationHitBox.Right >= targetBlock.Left)
                            {   //Set the player next the left side of the block,
                                //Align the hitbox with the wall
                                int actualMovement = movement.DestinationHitBox.Right - targetBlock.Left;
                                newPosition.X = newPosition.X - actualMovement - 1;

                            }
                            else if (movementX < 0 && movement.DestinationHitBox.Left <= targetBlock.Right)
                            {   //set the player to the right side of the block
                                int actualMovement = movement.DestinationHitBox.Left - targetBlock.Right;
                                newPosition.X = newPosition.X - actualMovement + 1;

                            }
                            if (movementY > 0 && movement.DestinationHitBox.Bottom >= targetBlock.Top)
                            {   //set the player to the bottom side of the block
                                int actualMovement = movement.DestinationHitBox.Bottom - targetBlock.Top;
                                newPosition.Y = newPosition.Y - actualMovement - 1;

                            }
                            else if (movementY < 0 && movement.DestinationHitBox.Top <= targetBlock.Bottom)
                            {   //set the player to the top of the block.
                                int actualMovement = movement.DestinationHitBox.Top - targetBlock.Bottom;
                                newPosition.Y = newPosition.Y - actualMovement + 1;

                            }

                            break;
                        case CollisionTypes.Moveable:
                            //something else
                            break;
                        case CollisionTypes.PowerUp:
                            //something ellie
                            break;
                        case CollisionTypes.Empty:
                        default:
                            newPosition = movement.DestinationScreenPos;

                            break;
                    }

                }
            }
            return newPosition;

        }

        private static void checkCollisionWithPlayers()
        {
            //throw new NotImplementedException();
        }

        private static void checkCollisionWithBombs()
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
        public List<Vector2> OriginalGamefieldPosition;
        public List<Vector2> DestinationGameFieldPosition;
    }
}
