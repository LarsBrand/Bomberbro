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
       private bool _playOnce;
       private List<SpriteHelper> _animationFrames; 
       
       /// <summary>
       /// Create a new animation
       /// </summary>
       /// <param name="cycleSpeed">in milliseconds</param>
       /// <param name="animationFrames">the frames</param>
       public Animation(int cycleSpeed, List<SpriteHelper> animationFrames, bool playonce=false)
       {
           _cycleSpeed = cycleSpeed;
           _animationFrames = animationFrames;
           _playOnce = playonce;
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
            if (_playOnce)
            {
                PlayOnceAnimationUpdate(gameTime);
            }
            else
            {
            ReapeatingAnimationUpdate(gameTime);                
            }
        }

       private void ReapeatingAnimationUpdate(GameTime gameTime)
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
      
       private void PlayOnceAnimationUpdate(GameTime gameTime)
       {
           _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

           if (_elapsedTime > _cycleSpeed) //Reset it, and set the nexst frame
           {
               _elapsedTime -= _cycleSpeed;

               _currentFrame++; //nexst frame
               if (_currentFrame >= _animationFrames.Count) //it's the last animation
               {
                   _currentFrame = AnimationFrames.Count - 1;
               }
           }
       }

       public void Draw(Rectangle rectangle)
       {
           Draw(rectangle,Color.White);
       }

       public void ResetAnimation()
       {
           _currentFrame = 0;
       }

       public void Draw(Rectangle rectangle, Color DrawColor)
       {
           _animationFrames[_currentFrame].Render(rectangle,DrawColor);
       }
   }
}
