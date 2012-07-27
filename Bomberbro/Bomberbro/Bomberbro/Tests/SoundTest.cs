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
        AudioEngine _audioEngine;
        WaveBank _waveBankTheme;
        SoundBank _soundBankTheme;

        WaveBank _waveBankSoundEffect;
        SoundBank _soundBankSoundEffect;

        Cue _currentSong;
        private Cue _explosion2;
        private Cue _cancel1;
        private Cue _cancel2;
        private Cue _collision1;
        private Cue _collision2;
        private Cue _completeDead;
        private Cue _explosion1;
        private Cue _hit;
        private Cue _marioCoin;
        private Cue _randomMelody1;
        private Cue _randomMelody2;
        private Cue _sfx1;
        private Cue _thumbMove1;
        private Cue _random;

        SpriteFont _font;

        KeyboardState _current, _previous;


        string _info;
        private float _musicVolume;
        private float _pitch;
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

            _audioEngine = new AudioEngine(_content.RootDirectory + "//BomberbroAudio.xgs");
            _waveBankTheme = new WaveBank(_audioEngine, _content.RootDirectory + "//Theme.xwb");
            _soundBankTheme = new SoundBank(_audioEngine, _content.RootDirectory + "//Theme.xsb");

            _waveBankSoundEffect = new WaveBank(_audioEngine, _content.RootDirectory + "//SoundEffects.xwb");
            _soundBankSoundEffect = new SoundBank(_audioEngine, _content.RootDirectory + "//SoundEffect.xsb");

            _currentSong = _soundBankTheme.GetCue("Theme");
            _explosion2 = _soundBankSoundEffect.GetCue("Explosion 2");
            _cancel1 = _soundBankSoundEffect.GetCue("Cancel 1");
            _cancel2 = _soundBankSoundEffect.GetCue("Cancel 2");
            _collision1 = _soundBankSoundEffect.GetCue("Collision 1");
            _collision2 = _soundBankSoundEffect.GetCue("Collision 2");
            _completeDead = _soundBankSoundEffect.GetCue("Complete-Dead");
            _explosion1 = _soundBankSoundEffect.GetCue("Explosion 1");
            _hit = _soundBankSoundEffect.GetCue("Hit");
            _marioCoin = _soundBankSoundEffect.GetCue("Mario Coin");
            _randomMelody1 = _soundBankSoundEffect.GetCue("Random Melody 1");
            _randomMelody2 = _soundBankSoundEffect.GetCue("Random Melody 2");
            _sfx1 = _soundBankSoundEffect.GetCue("SFX1");
            _thumbMove1 = _soundBankSoundEffect.GetCue("ThumbMove 1");
            _random = _soundBankSoundEffect.GetCue("Random");

            _currentSong.Play();
            _musicVolume = 1.0f;
            _pitch = 50.0f;


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
                _musicVolume += 0.01f;
                if (_musicVolume > 1.0f)
                {
                    _musicVolume = 1.0f;
                }
                _audioEngine.GetCategory("Music").SetVolume(_musicVolume);
            }

            if (_current.IsKeyDown(Keys.Down) && _previous.IsKeyDown(Keys.Down))
            {
                _musicVolume -= 0.01f;
                if (_musicVolume < 0)
                {
                    _musicVolume = 0;
                }
                _audioEngine.GetCategory("Music").SetVolume(_musicVolume);
            }
            if (_current.IsKeyDown(Keys.Left) && _previous.IsKeyDown(Keys.Left))
            {
                _pitch -= 1f;
                if (_pitch < 0)
                {
                    _pitch = 0;
                }
                _currentSong.SetVariable("Pitch", _pitch);
            }
            if (_current.IsKeyDown(Keys.Right) && _previous.IsKeyDown(Keys.Right))
            {
                _pitch += 1f;
                if (_pitch > 100)
                {
                    _pitch = 100;
                }
                _currentSong.SetVariable("Pitch", _pitch);
            }

            if (_current.IsKeyDown(Keys.Q)
            && !_previous.IsKeyDown(Keys.Q))
            {
                _explosion1.Play();
                _explosion1 = _soundBankSoundEffect.GetCue("Explosion 1");
            }

            if (_current.IsKeyDown(Keys.W)
            && !_previous.IsKeyDown(Keys.W))
            {
                _explosion2.Play();
                _explosion2 = _soundBankSoundEffect.GetCue("Explosion 2");
            }

            if (_current.IsKeyDown(Keys.D)
            && !_previous.IsKeyDown(Keys.D))
            {
                _cancel1.Play();
                _cancel1 = _soundBankSoundEffect.GetCue("Cancel 1");
            }

            if (_current.IsKeyDown(Keys.F)
            && !_previous.IsKeyDown(Keys.F))
            {
                _cancel2.Play();
                _cancel2 = _soundBankSoundEffect.GetCue("Cancel 2");
            }
            if (_current.IsKeyDown(Keys.T)
            && !_previous.IsKeyDown(Keys.T))
            {
                _collision1.Play();
                _collision1 = _soundBankSoundEffect.GetCue("Collision 1");
            }
            if (_current.IsKeyDown(Keys.Y)
            && !_previous.IsKeyDown(Keys.Y))
            {
                _collision2.Play();
                _collision2 = _soundBankSoundEffect.GetCue("Collision 2");
            }
            if (_current.IsKeyDown(Keys.U)
            && !_previous.IsKeyDown(Keys.U))
            {
                _completeDead.Play();
                _completeDead = _soundBankSoundEffect.GetCue("Complete-Dead");
            }

            if (_current.IsKeyDown(Keys.I)
            && !_previous.IsKeyDown(Keys.I))
            {
                _hit.Play();
                _hit = _soundBankSoundEffect.GetCue("Hit");
            }
            if (_current.IsKeyDown(Keys.O)
            && !_previous.IsKeyDown(Keys.O))
            {
                _marioCoin.Play();
                _marioCoin = _soundBankSoundEffect.GetCue("Mario Coin");
            }

            if (_current.IsKeyDown(Keys.P)
            && !_previous.IsKeyDown(Keys.P))
            {
                _randomMelody1.Play();
                _randomMelody1 = _soundBankSoundEffect.GetCue("Random Melody 1");
            }
            if (_current.IsKeyDown(Keys.A)
            && !_previous.IsKeyDown(Keys.A))
            {
                _randomMelody2.Play();
                _randomMelody2 = _soundBankSoundEffect.GetCue("Random Melody 2");
            }
            if (_current.IsKeyDown(Keys.S)
            && !_previous.IsKeyDown(Keys.S))
            {
                _sfx1.Play();
                _sfx1 = _soundBankSoundEffect.GetCue("SFX1");
            }
            if (_current.IsKeyDown(Keys.E)
            && !_previous.IsKeyDown(Keys.E))
            {
                _thumbMove1.Play();
                _thumbMove1 = _soundBankSoundEffect.GetCue("ThumbMove 1");
            }
            if (_current.IsKeyDown(Keys.R)
            && !_previous.IsKeyDown(Keys.R))
            {
                _random.Play();
                _random = _soundBankSoundEffect.GetCue("Random");
            }
            StringBuilder builder = new StringBuilder();            
            builder.AppendLine("The current song playing is " + _currentSong.Name);
            builder.AppendLine("Press the Up/Down key to modify the volume");
            builder.AppendLine("Press the Right/Left key to modify the pitch");
            builder.AppendLine("Music Volume is set at " + _musicVolume * 100 + "%");
            builder.AppendLine("Music Volume is set at " + _pitch * 2 + "%");
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
            builder.AppendLine("R - Pick a random sound from the song que");
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
