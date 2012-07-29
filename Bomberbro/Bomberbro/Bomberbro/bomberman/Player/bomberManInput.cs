using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bomberbro.bomberman
{
    public class BomberManInput
    {
        private List<BomberManGuy> _players;
        private GameField _gameField;
        List<PlayerControls> _playerControlses;

        public BomberManInput(List<BomberManGuy> players, GameField gameField)
        {
            _players = players;
            _gameField = gameField;
            _playerControlses = new List<PlayerControls>();
            PlayerControls defaultPlayerControls = new PlayerControls(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space);
            PlayerControls defaultPlayer2Controls = new PlayerControls(Keys.W, Keys.S, Keys.A, Keys.D, Keys.G);
            _playerControlses.Add(defaultPlayerControls);
            _playerControlses.Add(defaultPlayer2Controls);

        }

        public void Update(GameTime gameTime)
        {
            #region player one

            for (int i = 0; i < _players.Count; i++)
            {

                int playerNumber = i;
                PlayerControls playerControls = _playerControlses[playerNumber];
                _players[playerNumber].SetAnimation(playerAnimations.normal);
                if (_players[playerNumber].Dead)
                {
                    _players[playerNumber].SetAnimation(playerAnimations.dead);

                    if (playerControls.BombKeyJustPressed())
                    {
                        _players[playerNumber].Dead = false;
                    }
                }
                else
                {
                    if (playerControls.DownKeyPressed())
                    {
                        int movementY = Convert.ToInt32(0.2f * gameTime.ElapsedGameTime.Milliseconds);
                        var movement = CreateMoveObject(playerNumber, 0, movementY);
                        _players[playerNumber].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerNumber, movement);
                        _players[playerNumber].SetAnimation(playerAnimations.down);
                        //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X, _players[playerOne].Position.Y + 0.2f * gameTime.ElapsedGameTime.Milliseconds);
                    }
                    if (playerControls.UpKeyPressed())
                    {
                        int movementY = Convert.ToInt32((0.2f * gameTime.ElapsedGameTime.Milliseconds) * -1);
                        var movement = CreateMoveObject(playerNumber, 0, movementY);
                        _players[playerNumber].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerNumber, movement);
                        _players[playerNumber].SetAnimation(playerAnimations.up);
                        //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X, _players[playerOne].Position.Y - 0.2f * gameTime.ElapsedGameTime.Milliseconds);
                    }
                    if (playerControls.LeftKeyPressed())
                    {

                        int movementX = Convert.ToInt32((0.2f * gameTime.ElapsedGameTime.Milliseconds) * -1);
                        var movement = CreateMoveObject(playerNumber, movementX, 0);
                        _players[playerNumber].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerNumber, movement);
                        _players[playerNumber].SetAnimation(playerAnimations.left);
                        //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X - 0.2f * gameTime.ElapsedGameTime.Milliseconds, _players[playerOne].Position.Y);
                        //position.X = position.X - 0.2f * gameTime.ElapsedGameTime.Milliseconds;
                    }
                    if (playerControls.RightKeyPressed())
                    {
                        int movementX = Convert.ToInt32((0.2f * gameTime.ElapsedGameTime.Milliseconds));
                        var movement = CreateMoveObject(playerNumber, movementX, 0);
                        _players[playerNumber].Position = CollisionChecker.CheckPlayerMovement(_gameField, _players, playerNumber, movement);
                        _players[playerNumber].SetAnimation(playerAnimations.right);
                        //_players[playerOne].Position = new Vector2(_players[playerOne].Position.X + 0.2f * gameTime.ElapsedGameTime.Milliseconds, _players[playerOne].Position.Y);
                        //position.X = position.X + 0.2f * gameTime.ElapsedGameTime.Milliseconds;
                    }

                    if (playerControls.BombKeyJustPressed())
                    {
                        _gameField.PlaceBomb(_players[playerNumber]);
                    }
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

    public class PlayerControls
    {
        public Keys UpKey;
        public Keys DownKey;
        public Keys LeftKey;
        public Keys RightKey;
        public Keys BombKey;

        public PlayerControls(Keys upkey, Keys downKey, Keys leftKey, Keys rightKey, Keys bombKey)
        {
            UpKey = upkey;
            DownKey = downKey;
            LeftKey = leftKey;
            RightKey = rightKey;
            BombKey = bombKey;
        }

        public bool UpKeyPressed()
        {
            return Input.KeyboardKeyPressed(UpKey);
        }

        public bool DownKeyPressed()
        {
            return Input.KeyboardKeyPressed(DownKey);
        }
        public bool LeftKeyPressed()
        {
            return Input.KeyboardKeyPressed(LeftKey);
        }

        public bool RightKeyPressed()
        {
            return Input.KeyboardKeyPressed(RightKey);
        }

        public bool BombKeyJustPressed()
        {
            return Input.KeyboardKeyJustPressed(BombKey);
        }
    }
}
