using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberbro
{
   public class Animation
   {
       private int _currentFrame;
       private int _elapsedTime;
       private int _cycleSpeed;
       private List<SpriteHelper> _animationFrames; 
       
       /// <summary>
       /// Create a new animation
       /// </summary>
       /// <param name="cycleSpeed">in milliseconds</param>
       /// <param name="animationFrames">the frames</param>
       public Animation(int cycleSpeed, List<SpriteHelper> animationFrames)
       {
           _cycleSpeed = cycleSpeed;
           _animationFrames = animationFrames;
       }


       public int CycleSpeed
       {
           get { return _cycleSpeed; }
           set { _cycleSpeed = value; }
       }

       public List<SpriteHelper> AnimationFrames
       {
           get { return _animationFrames; }
           set { _animationFrames = value; }
       }

        public void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (_elapsedTime > _cycleSpeed) //Reset it, and set the nexst frame
            {
                _elapsedTime -= _cycleSpeed;

                _currentFrame++; //nexst frame
                if (_currentFrame >= _animationFrames.Count) //it's the last animation
                {
                    _currentFrame = 0;
                }
            }
        }


       public void Draw(Rectangle rectangle)
       {
           _animationFrames[_currentFrame].Render(rectangle);
       }
   }
}
