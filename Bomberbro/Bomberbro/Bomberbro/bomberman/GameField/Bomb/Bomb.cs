using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberbro.bomberman
{
    public class Bomb : GamefieldItem
    {
        private List<SpriteHelper> _bombAnimationFrames;
        private Animation _bombAnimation;
        private int _origionalTime;
        private int _timeToLive;
        private bool _exploded;
        private int _power;





        public bool Exploded
        {
            get { return _exploded; }
            set { _exploded = value; }
        }

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }


        public override void Draw(Rectangle rect, GameTime gameTime)
        {
            _bombAnimation.Draw(rect);
        }

        public override void Update(GameTime gameTime)
        {
            _timeToLive -= gameTime.ElapsedGameTime.Milliseconds;
            if (_timeToLive < 0)
            {
                Explode();
            }
            else
            {
                _bombAnimation.CycleSpeed =Convert.ToInt32( Convert.ToDouble(_timeToLive)/Convert.ToDouble(_origionalTime)*300);
            }
            _bombAnimation.Update(gameTime);
        }



        public override void LoadContent(ContentManager content)
        {
            List<SpriteHelper> bombAnimationFrames = new List<SpriteHelper>();
            Texture2D bombTexture = content.Load<Texture2D>("blocks");
            Rectangle bombRectangle1 = new Rectangle(3, 65, 64 - 3, 126 - 65);
            SpriteHelper bombAnimation1 = new SpriteHelper(bombTexture, bombRectangle1);
            Rectangle bombRectangle2 = new Rectangle(64, 65, 125 - 64, 126 - 65);
            SpriteHelper bombAnimation2 = new SpriteHelper(bombTexture, bombRectangle2);
            Rectangle bombRectangle3 = new Rectangle(125, 65, 186-125 , 126 - 65);
            SpriteHelper bombAnimation3 = new SpriteHelper(bombTexture, bombRectangle3);
            Rectangle bombRectangle4 = new Rectangle(186 , 65, 247-186, 126 - 65);
            SpriteHelper bombAnimation4 = new SpriteHelper(bombTexture, bombRectangle4);
            Rectangle bombRectangle5 = new Rectangle(247, 65, 308-247, 126 - 65);
            SpriteHelper bombAnimation5 = new SpriteHelper(bombTexture, bombRectangle5);
            bombAnimationFrames.Add(bombAnimation1);
            bombAnimationFrames.Add(bombAnimation2);
            bombAnimationFrames.Add(bombAnimation3);
            bombAnimationFrames.Add(bombAnimation4);
            bombAnimationFrames.Add(bombAnimation5);
            _bombAnimationFrames = bombAnimationFrames;
            _timeToLive = 5000;
            _origionalTime = _timeToLive;
            _power = 2;
            CollisionType= CollisionTypes.Bomb;
            _exploded = false;
        }


        public Bomb copy()
        {
            Bomb outputBomb= new Bomb();
            outputBomb._bombAnimation = new Animation(1000,_bombAnimationFrames);
            outputBomb._timeToLive = _timeToLive;
            outputBomb._origionalTime = _origionalTime;
            outputBomb.CollisionType= CollisionType;
            outputBomb._power = _power;
            return outputBomb;
        }


        public void Explode()
        {
            _exploded = true;
        }
    }
}
