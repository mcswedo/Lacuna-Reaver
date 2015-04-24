using Microsoft.Xna.Framework.Audio;

namespace MyQuest
{
    static class SoundSystem
    {
        #region Cue Names


        #endregion

        #region Fields


        static AudioEngine engine;
        static WaveBank wavebank;
        static SoundBank soundbank;

        #endregion

        #region Initialization

        const string soundFolder = "LacunaSounds/";

        /// <summary>
        /// Starts up the sound code
        /// </summary>
        internal static void Initialize()
        {
            engine = new AudioEngine(soundFolder + "LacunaSounds.xgs");
            wavebank = new WaveBank(engine, soundFolder + "SoundWaveBank.xwb");
            soundbank = new SoundBank(engine, soundFolder + "SoundSoundBank.xsb");
            engine.Update();
            engine.GetCategory("Default").SetVolume(GlobalSettings.soundEffectsVolume);
        }


        #endregion

        #region Operations


        internal static Cue Play(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            Cue returnValue = soundbank.GetCue(name);
            returnValue.Play();
            return returnValue;
        }

        internal static void Stop(string name)
        {
            Cue cue = soundbank.GetCue(name);
            cue.Stop(AudioStopOptions.Immediate);
        }

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
        }

        #endregion
    }
}
