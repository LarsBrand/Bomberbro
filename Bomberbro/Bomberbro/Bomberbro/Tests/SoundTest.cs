using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberbro.Helpers;
using Bomberbro.bomberman;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace Bomberbro.Tests
{
    public class SoundTest : IGameState
    {
        private static Rectangle _logoRect = new Rectangle(0, 0, 500, 298);
        private SpriteHelper _logoHelper;

        private ContentManager _content;
        private GraphicsDeviceManager _graphics;
        private int _width, _height;
        SpriteBatch _spriteBatch;
       
      //  static AudioEngine _audioEngine;
      //  static WaveBank _waveBankTheme;
      // static SoundBank _soundBankTheme;

      //  WaveBank _waveBankSoundEffect;
      //  SoundBank _soundBankSoundEffect;

      //private  static Cue _currentSong;
      //  private Cue _explosion2;
      //  private Cue _cancel1;
      //  private Cue _cancel2;
      //  private Cue _collision1;
      //  private Cue _collision2;
      //  private Cue _completeDead;
      //  private Cue _explosion1;
      //  private Cue _hit;
      //  private Cue _marioCoin;
      //  private Cue _randomMelody1;
      //  private Cue _randomMelody2;
      //  private Cue _sfx1;
      //  private Cue _thumbMove1;
      //  private Cue _random;

        SpriteFont _font;

        KeyboardState _current, _previous;

        
        string _info;
        //private float _musicVolume;
        //private static float _musicPitch;

        ///// <summary>
        ///// Default =  0.5f;
        ///// </summary>
        //public static float MusicPitch
        //{
        //    get { return _musicPitch / 100; }
        //    set
        //    {
        //        _musicPitch = value * 100;
        //        if (_musicPitch < 0)
        //        {
        //            _musicPitch = 0;
        //        }
        //        if (_musicPitch > 100)
        //        {
        //            _musicPitch = 100;
        //        }
        //        _currentSong.SetVariable("Pitch", _musicPitch);
        //    }
        //}
        public SoundTest(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;

        }
        public void Initialize()
        {
            // TODO: Add your initialization logic here
            _current = _previous = new KeyboardState();
            _width = _graphics.GraphicsDevice.Viewport.Width;
            _height = _graphics.GraphicsDevice.Viewport.Height;

        }

        public void LoadContent()
        {

            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            Texture2D logo = _content.Load<Texture2D>("logo");
            _logoHelper = new SpriteHelper(logo, _logoRect);

            //_audioEngine = new AudioEngine(_content.RootDirectory + "//BomberbroAudio.xgs");
            //_waveBankTheme = new WaveBank(_audioEngine, _content.RootDirectory + "//Theme.xwb");
            //_soundBankTheme = new SoundBank(_audioEngine, _content.RootDirectory + "//Theme.xsb");

            //_waveBankSoundEffect = new WaveBank(_audioEngine, _content.RootDirectory + "//SoundEffects.xwb");
            //_soundBankSoundEffect = new SoundBank(_audioEngine, _content.RootDirectory + "//SoundEffect.xsb");

            //_currentSong = _soundBankTheme.GetCue("Theme");
            //_explosion2 = _soundBankSoundEffect.GetCue("Explosion 2");
            //_cancel1 = _soundBankSoundEffect.GetCue("Cancel 1");
            //_cancel2 = _soundBankSoundEffect.GetCue("Cancel 2");
            //_collision1 = _soundBankSoundEffect.GetCue("Collision 1");
            //_collision2 = _soundBankSoundEffect.GetCue("Collision 2");
            //_completeDead = _soundBankSoundEffect.GetCue("Complete-Dead");
            //_explosion1 = _soundBankSoundEffect.GetCue("Explosion 1");
            //_hit = _soundBankSoundEffect.GetCue("Hit");
            //_marioCoin = _soundBankSoundEffect.GetCue("Mario Coin");
            //_randomMelody1 = _soundBankSoundEffect.GetCue("Random Melody 1");
            //_randomMelody2 = _soundBankSoundEffect.GetCue("Random Melody 2");
            //_sfx1 = _soundBankSoundEffect.GetCue("SFX1");
            //_thumbMove1 = _soundBankSoundEffect.GetCue("ThumbMove 1");
            //_random = _soundBankSoundEffect.GetCue("Random");

            //_currentSong.Play();
            //_musicVolume = 1.0f;
            //_musicPitch = 50.0f;
            
            BombermanSound.LoadSounds(_content);
            BombermanSound.PlayTheme();

            _font = _content.Load<SpriteFont>("TestFont");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            _previous = _current;
            _current = Keyboard.GetState();


            if (_current.IsKeyDown(Keys.Up) && _previous.IsKeyDown(Keys.Up))
            {
                BombermanSound.MusicVolume += 0.01f;
            }

            if (_current.IsKeyDown(Keys.Down) && _previous.IsKeyDown(Keys.Down))
            {
                BombermanSound.MusicVolume -= 0.01f;
            }
            if (_current.IsKeyDown(Keys.Left) && _previous.IsKeyDown(Keys.Left))
            {

                BombermanSound.MusicPitch -= 0.01f;
            }
            if (_current.IsKeyDown(Keys.Right) && _previous.IsKeyDown(Keys.Right))
            {
                BombermanSound.MusicPitch += 0.01f;
            }

            if (_current.IsKeyDown(Keys.Q)
            && !_previous.IsKeyDown(Keys.Q))
            {
               BombermanSound.PlayExplosionShort();
            }

            if (_current.IsKeyDown(Keys.W)
            && !_previous.IsKeyDown(Keys.W))
            {
                BombermanSound.PlayExplosionLong();}

            if (_current.IsKeyDown(Keys.D)
            && !_previous.IsKeyDown(Keys.D))
            {
                BombermanSound.PlayCancelBleep();

            }

            if (_current.IsKeyDown(Keys.F)
            && !_previous.IsKeyDown(Keys.F))
            {
                BombermanSound.PlayCancelBonk();
            }
            if (_current.IsKeyDown(Keys.T)
            && !_previous.IsKeyDown(Keys.T))
            {
                BombermanSound.PlayCollisionTick();
            }
            if (_current.IsKeyDown(Keys.Y)
            && !_previous.IsKeyDown(Keys.Y))
            {
                BombermanSound.PlayCollisionBump();
            }
            if (_current.IsKeyDown(Keys.U)
            && !_previous.IsKeyDown(Keys.U))
            {
                BombermanSound.PlayDeathSound();
            }

            if (_current.IsKeyDown(Keys.I)
            && !_previous.IsKeyDown(Keys.I))
            {
                BombermanSound.PlayHit();
            }
            if (_current.IsKeyDown(Keys.O)
            && !_previous.IsKeyDown(Keys.O))
            {
                BombermanSound.PlayMarioCoinSonicRingItsAllTheSame();
            }

            if (_current.IsKeyDown(Keys.P)
            && !_previous.IsKeyDown(Keys.P))
            {
                BombermanSound.PlayMelodySlow();
            }
            if (_current.IsKeyDown(Keys.A)
            && !_previous.IsKeyDown(Keys.A))
            {
                BombermanSound.PlayMelodyFast();
            }
            if (_current.IsKeyDown(Keys.S)
            && !_previous.IsKeyDown(Keys.S))
            {
                BombermanSound.PlayPleep();
            }
            if (_current.IsKeyDown(Keys.E)
            && !_previous.IsKeyDown(Keys.E))
            {
                BombermanSound.PlayDomp();
            }
          
            StringBuilder builder = new StringBuilder();            
            builder.AppendLine("Press the Up/Down key to modify the volume");
            builder.AppendLine("Press the Right/Left key to modify the pitch");
            builder.AppendLine("Music Volume is set at " + BombermanSound.MusicVolume * 100 + "%");
            builder.AppendLine("Music Volume is set at " + BombermanSound.MusicPitch * 200 + "%");
            builder.AppendLine("");
            builder.AppendLine("Q - Explosion 1");
            builder.AppendLine("W - Explosion 2");
            builder.AppendLine("E - ThumbMove 1");
            builder.AppendLine("T - Collision 1");
            builder.AppendLine("Y - Collision 2");
            builder.AppendLine("U - CompleteDead");
            builder.AppendLine("I - hit");
            builder.AppendLine("O - Mario Coin");
            builder.AppendLine("P - Random Melody 1");
            builder.AppendLine("A - Random Melody 2");
            builder.AppendLine("S - SFX 1");
            builder.AppendLine("D - Cancel 1");
            builder.AppendLine("F - Cancel 2");
            _info = builder.ToString();
        }

        public void Draw(GameTime gameTime)
        {

            _logoHelper.Render(new Rectangle(_width - _logoRect.Width / 2, 0, _logoRect.Width / 2, _logoRect.Height / 2));
            SpriteHelper.DrawSprites(_width, _height);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, _info, new Vector2(10, 10), Color.White);
            _spriteBatch.End();
        }
    }
}
