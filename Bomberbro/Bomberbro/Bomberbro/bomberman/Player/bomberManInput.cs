using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;

namespace Bomberbro.bomberman
{
    public class BomberManInput
    {
        private List<BomberManGuy> _players;
        private GameField _gameField;

        public BomberManInput(List<BomberManGuy> players, GameField gameField)
        {
            _players = players;
            _gameField = gameField;
        }

        public void Update(GameTime gameTime)
        {
            #region player one
            int playerOne = 0;

            _players[playerOne].SetAnimation(playerAnimations.normal);
            if (_players[playerOne].Dead)
            {
                _players[playerOne].SetAnimation(playerAnimations.dead);

                if (Input.KeyboardSpaceJustPressed)
                {
                    _players[playerOne].Dead=false;
                }
            }
            else
            {
                if (Input.KeyboardDownPressed)
                {
                    int movementY = Convert.ToInt32(0.2f * gameTime.ElapsedGameTime.Milliseconds);
                    var movement = CreateMoveObject(playerOne, 0, movementY);
                    _players[playerOne].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerOne, movement);
                    _players[playerOne].SetAnimation(playerAnimations.down);
                    //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X, _players[playerOne].Position.Y + 0.2f * gameTime.ElapsedGameTime.Milliseconds);
                }
                if (Input.KeyboardUpPressed)
                {
                    int movementY = Convert.ToInt32((0.2f * gameTime.ElapsedGameTime.Milliseconds) * -1);
                    var movement = CreateMoveObject(playerOne, 0, movementY);
                    _players[playerOne].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerOne, movement);
                    _players[playerOne].SetAnimation(playerAnimations.up);
                    //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X, _players[playerOne].Position.Y - 0.2f * gameTime.ElapsedGameTime.Milliseconds);
                }
                if (Input.KeyboardLeftPressed)
                {

                    int movementX = Convert.ToInt32((0.2f * gameTime.ElapsedGameTime.Milliseconds) * -1);
                    var movement = CreateMoveObject(playerOne, movementX, 0);
                    _players[playerOne].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerOne, movement);
                    _players[playerOne].SetAnimation(playerAnimations.left);
                    //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X - 0.2f * gameTime.ElapsedGameTime.Milliseconds, _players[playerOne].Position.Y);
                    //position.X = position.X - 0.2f * gameTime.ElapsedGameTime.Milliseconds;
                }
                if (Input.KeyboardRightPressed)
                {
                    int movementX = Convert.ToInt32((0.2f * gameTime.ElapsedGameTime.Milliseconds));
                    var movement = CreateMoveObject(playerOne, movementX, 0);
                    _players[playerOne].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerOne, movement);
                    _players[playerOne].SetAnimation(playerAnimations.right);
                    //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X + 0.2f * gameTime.ElapsedGameTime.Milliseconds, _players[playerOne].Position.Y);
                    //position.X = position.X + 0.2f * gameTime.ElapsedGameTime.Milliseconds;
                }

                if (Input.KeyboardSpaceJustPressed)
                {
                    _gameField.PlaceBomb(_players[playerOne]);
                }
            }

            #endregion
        }

        private MoveObject CreateMoveObject(int playerNumber, int movementX, int movementY)
        {
            MoveObject movement = new MoveObject();
            movement.OrigionalScreenPos = _players[playerNumber].Position;
            movement.OrigionalHitBox = _players[playerNumber].GetBombermanGuyPositionedHitBox(_gameField.FieldScale);
            movement.OriginalGamefieldPosition = _gameField.getPosistionOfRectInGrid(movement.OrigionalHitBox);
            movement.PreviousHitBox = _players[playerNumber].BombermanGuyPositionedHitBoxPreviousUpdate;

            movement.DestinationScreenPos = new Vector2(movement.OrigionalScreenPos.X + movementX, movement.OrigionalScreenPos.Y + movementY);
            movement.DestinationHitBox = new Rectangle(movement.OrigionalHitBox.X + movementX, movement.OrigionalHitBox.Y + movementY, movement.OrigionalHitBox.Width, movement.OrigionalHitBox.Height);
            movement.DestinationGameFieldPosition = _gameField.getPosistionOfRectInGrid(movement.DestinationHitBox);

            return movement;
        }
    }
}
