using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Bomberbro.bomberman
{
    public class BombermanSound
    {
        static AudioEngine _audioEngine;
        static WaveBank _waveBankTheme;
        static SoundBank _soundBankTheme;
        static WaveBank _waveBankSoundEffect;
        static SoundBank _soundBankSoundEffect;

        private static Cue _currentSong;
        private static Cue _explosion2;
        private static Cue _cancel1;
        private static Cue _cancel2;
        private static Cue _collision1;
        private static Cue _collision2;
        private static Cue _completeDead;
        private static Cue _explosion1;
        private static Cue _hit;
        private static Cue _marioCoin;
        private static Cue _randomMelody1;
        private static Cue _randomMelody2;
        private static Cue _sfx1;
        private static Cue _thumbMove1;

        private static float _musicVolume;
        private static float _musicPitch;
        /// <summary>
        /// Default = 0.5f;
        /// </summary>
        public static float MusicVolume
        {

            get { return _musicVolume / 2; }
            set
            {
                _musicVolume = value * 2;
                if (_musicVolume > 2.0f)
                {
                    _musicVolume = 2.0f;
                }
                if (_musicVolume < 0)
                {
                    _musicVolume = 0;
                }
                _audioEngine.GetCategory("Music").SetVolume(_musicVolume);
            }
        }

        /// <summary>
        /// Default =  0.5f;
        /// </summary>
        public static float MusicPitch
        {
            get { return _musicPitch / 100; }
            set
            {
                _musicPitch = value * 100;
                if (_musicPitch < 0)
                {
                    _musicPitch = 0;
                }
                if (_musicPitch > 100)
                {
                    _musicPitch = 100;
                }
                SetMusicPitch(_musicPitch);
            }
        }

        static void SetMusicPitch(float pitchValue)
        {
            _currentSong.SetVariable("Pitch", pitchValue);
        }
        public static void LoadSounds(ContentManager content)
        {
            _audioEngine = new AudioEngine(content.RootDirectory + "//BomberbroAudio.xgs");
            _waveBankTheme = new WaveBank(_audioEngine, content.RootDirectory + "//Theme.xwb");
            _soundBankTheme = new SoundBank(_audioEngine, content.RootDirectory + "//Theme.xsb");
            _waveBankSoundEffect = new WaveBank(_audioEngine, content.RootDirectory + "//SoundEffects.xwb");
            _soundBankSoundEffect = new SoundBank(_audioEngine, content.RootDirectory + "//SoundEffect.xsb");

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


            MusicVolume = 0.5f;
            MusicPitch = 0.5f;
            _audioEngine.GetCategory("Music").SetVolume(_musicVolume);
            _currentSong.SetVariable("Pitch", _musicPitch);
        }

        public static void PlayTheme()
        {
            _currentSong.Play();

        }

        public static void PlayExplosionShort()
        {
            _explosion1.Play();
            _explosion1 = _soundBankSoundEffect.GetCue("Explosion 1");
        }

        public static void PlayExplosionLong()
        {
            _explosion2.Play();
            _explosion2 = _soundBankSoundEffect.GetCue("Explosion 2");
        }

        public static void PlayCancelBleep()
        {
            _cancel1.Play();
            _cancel1 = _soundBankSoundEffect.GetCue("Cancel 1");
        }

        public static void PlayCancelBonk()
        {
            _cancel2.Play();
            _cancel2 = _soundBankSoundEffect.GetCue("Cancel 2");
        }

        public static void PlayCollisionTick()
        {
            _collision1.Play();
            _collision1 = _soundBankSoundEffect.GetCue("Collision 1");
        }

        public static void PlayCollisionBump()
        {
            _collision2.Play();
            _collision2 = _soundBankSoundEffect.GetCue("Collision 2");
        }

        public static void PlayDeathSound()
        {
            _completeDead.Play();
            _completeDead = _soundBankSoundEffect.GetCue("Complete-Dead");
        }

        public static void PlayHit()
        {
            _hit.Play();
            _hit = _soundBankSoundEffect.GetCue("Hit");
        }

        public static void PlayMarioCoinSonicRingItsAllTheSame()
        {
            _marioCoin.Play();
            _marioCoin = _soundBankSoundEffect.GetCue("Mario Coin");
        }

        public static void PlayMelodySlow()
        {
            _randomMelody1.Play();
            _randomMelody1 = _soundBankSoundEffect.GetCue("Random Melody 1");
        }

        public static void PlayMelodyFast()
        {
            _randomMelody2.Play();
            _randomMelody2 = _soundBankSoundEffect.GetCue("Random Melody 2");
        }

        public static void PlayPleep()
        {
            _sfx1.Play();
            _sfx1 = _soundBankSoundEffect.GetCue("SFX1");
        }

        public static void PlayDomp()
        {
            _thumbMove1.Play();
            _thumbMove1 = _soundBankSoundEffect.GetCue("ThumbMove 1");
        }
    }
}
