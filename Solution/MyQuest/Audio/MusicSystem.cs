using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyQuest
{
    static class MusicSystem
    {
        #region Fields


        const short packetSize = 16;  // Minimum size is 2; 16 is optimal for DVD.
        static AudioEngine engine;
        static WaveBank wavebank;
        static SoundBank soundbank;
        static Cue musicCue;

        static List<Cue> interruptingSFX = new List<Cue>();


        #endregion

        #region Initialize 

        const string musicFolder = "LacunaMusic/";

        internal static void Initialize()
        {
            int offset = 0;
            engine = new AudioEngine(musicFolder + "LacunaMusic.xgs");
            wavebank = new WaveBank(engine, musicFolder + "MusicWaveBank.xwb", offset, packetSize);
            soundbank = new SoundBank(engine, musicFolder + "MusicSoundBank.xsb");
            engine.Update();
            engine.GetCategory("Music").SetVolume(GlobalSettings.musicVolume);
            musicCue = null;
        }


        #endregion

        #region Operations


        //internal static Cue PlayIfNothingIsPlaying(String musicCueName)
        //{
        //    if (musicCue != null && musicCue.IsPlaying)
        //    {
        //        return musicCue;
        //    }
        //    musicCue = soundbank.GetCue(musicCueName);
        //    musicCue.Play();
        //    return musicCue;
        //}

        internal static Cue Play(String musicCueName)
        {
            if (String.IsNullOrEmpty(musicCueName))
                throw new Exception("musicCueName cannot be null or empty");

            Cue nextCue = soundbank.GetCue(musicCueName);

            if (musicCue == null || !nextCue.Name.Equals(musicCue.Name))
            {
                Stop();
                nextCue.Play();
                musicCue = nextCue;
            }

            return musicCue;
        }

        internal static void Pause()
        {
            if (musicCue != null && musicCue.IsPlaying)
            {
                musicCue.Pause();
            }
        }

        internal static void Resume()
        {
            if (musicCue != null && musicCue.IsPaused)
            {
                musicCue.Resume();
            }
        }

        internal static void InterruptMusic(string cueName)
        {
            Cue cue = SoundSystem.Play(cueName);
            interruptingSFX.Add(cue);
            Pause();
        }

        internal static void Stop()
        {
            if (musicCue != null && !musicCue.IsStopped)
            {
                musicCue.Stop(AudioStopOptions.AsAuthored);
            }
            musicCue = null;
        }

        // Shuts down the sound code tidily
        internal static void Shutdown()
        {
            soundbank.Dispose();
            wavebank.Dispose();
            engine.Dispose();
        }


        #endregion

        #region Update


        internal static void Update()
        {
            engine.Update();

            bool interruptingCueWasRemoved = false;

            for (int i = interruptingSFX.Count - 1; i >= 0; --i)
            {
                Cue cue = interruptingSFX[i];
                if (cue.IsStopped)
                {
                    interruptingSFX.RemoveAt(i);
                    interruptingCueWasRemoved = true;                    
                }
            }

            if (interruptingSFX.Count > 0)
            {
                if (musicCue != null && musicCue.IsPlaying)
                {
                    Pause();
                }
            }
            else if (interruptingCueWasRemoved)
            {
                if (musicCue != null && musicCue.IsPaused)
                {
                    Resume();
                }
            }

        }


        #endregion
    }
}
