
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using yaSingleton;

namespace AudioManagerNS
{
    [CreateAssetMenu(fileName = "Audio Manager", menuName = "Singletons/AudioManager")]
    public class AudioManager : Singleton<AudioManager>
    {
        #region Public Fields

        // public static AudioManager Instance;

        public AudioMixerGroup masterGroup;
        public AudioMixer masterMixer;
        public AudioMixerGroup idleGroup;
        public AudioMixerGroup introGroup;
        public AudioMixerGroup fireGroup;
        public AudioMixerGroup hopeGroup;
        public AudioMixerGroup jungleGroup;
        public AudioMixerGroup narrationGroup;

        public int lowestDeciblesBeforeMute = -20;

        #endregion Public Fields

        #region Public Enums

        public enum AudioChannel { Master = 0, Narration, Fire, Intro, Idle, Hope, Jungle }

        #endregion Public Enums

        #region Public Methods

        /// <summary>
        /// Plays a sound at the given point in space by creating an empty game object with an
        /// AudioSource in that place and destroys it after it finished playing.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="emitter"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public AudioSource CreatePlaySource(AudioClip clip, Transform emitter, float volume, float pitch, AudioChannel channel = 0)
        {
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = emitter.position;
            go.transform.parent = emitter;

            //Create the source
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            // Output sound through the correct mixer group.
            switch(channel)
            {
                case AudioChannel.Idle:
                    source.outputAudioMixerGroup = idleGroup;
                    break;
                case AudioChannel.Intro:
                    source.outputAudioMixerGroup = introGroup;
                    break;
                case AudioChannel.Fire:
                    source.outputAudioMixerGroup = fireGroup;
                    break;
                case AudioChannel.Hope:
                    source.outputAudioMixerGroup = hopeGroup;
                    break;
                case AudioChannel.Jungle:
                    source.outputAudioMixerGroup = jungleGroup;
                    break;
                case AudioChannel.Narration:
                    source.outputAudioMixerGroup = narrationGroup;
                    break;
                default:
                    source.outputAudioMixerGroup = masterGroup;
                    break;
            }

            source.Play();
            return source;
        }

        public AudioSource Play(AudioClip clip, Transform emitter)
        {
            return Play(clip, emitter, 1f, 1f);
        }

        public AudioSource Play(AudioClip clip, Transform emitter, float volume)
        {
            return Play(clip, emitter, volume, 1f);
        }

        /// <summary>
        /// Plays a sound by creating an empty game object with an AudioSource and attaching it to
        /// the given transform (so it moves with the transform). Destroys it after it finished playing.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="emitter"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch, AudioChannel channel = 0)
        {
            //Create an empty game object
            AudioSource source = CreatePlaySource(clip, emitter, volume, pitch, channel);
            Destroy(source.gameObject, clip.length);
            return source;
        }

        public AudioSource Play(AudioClip clip, Vector3 point)
        {
            return Play(clip, point, 1f, 1f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume)
        {
            return Play(clip, point, volume, 1f);
        }

        /// <summary>
        /// Plays a sound at the given point in space by creating an empty game object with an
        /// AudioSource in that place and destroys it after it finished playing.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="point"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch, AudioChannel channel = 0)
        {
            AudioSource source = CreatePlaySource(clip, point, volume, pitch, channel);
            Destroy(source.gameObject, clip.length);
            return source;
        }

        /// <summary>
        /// Plays the sound effect in a loop. Should destroy the audio source in your script when it
        /// is ready to end.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="point"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public AudioSource PlayLoop(AudioClip clip, Transform emitter, float volume = 1f, float pitch = 1f, AudioChannel channel = 0)
        {
            AudioSource source = CreatePlaySource(clip, emitter, volume, pitch, channel);
            source.loop = true;
            return source;
        }

        /// <summary>
        /// Plays the sound effect in a loop. Should destroy the audio source in your script when it
        /// is ready to end.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="point"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public AudioSource PlayLoop(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, AudioChannel channel = 0)
        {
            AudioSource source = CreatePlaySource(clip, point, volume, pitch, channel);
            source.loop = true;
            return source;
        }

        /// <summary>
        /// Adjusts the volume on the audio channel in the unity audio mixer
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="volume">From 0 (mute) to 100 (full volume - 0 DB)</param>
        public void SetVolume(AudioChannel channel, float volume)
        {
            // Converts the 0 - 100 input into decibles | volume of 0 will mute, 1 should be ~the lowestDecibles set,
            // and 100 should be 0 DB offset from the base volume on the channel
            float adjustedVolume = lowestDeciblesBeforeMute + (-lowestDeciblesBeforeMute / 5 * volume / 20);

            // Effectively completed muted if volume if 0
            if (volume == 0)
            {
                adjustedVolume = -100;
            }

            switch (channel)
            {
                case AudioChannel.Master:
                    masterMixer.SetFloat("MasterVolume", adjustedVolume);
                    break;
                case AudioChannel.Narration:
                    masterMixer.SetFloat("NarrationVolume", adjustedVolume);
                    break;
                case AudioChannel.Idle:
                    masterMixer.SetFloat("IdleVolume", adjustedVolume);
                    break;
                case AudioChannel.Intro:
                    masterMixer.SetFloat("IntroVolume", adjustedVolume);
                    break;
                case AudioChannel.Fire:
                    masterMixer.SetFloat("FireVolume", adjustedVolume);
                    break;
                case AudioChannel.Hope:
                    masterMixer.SetFloat("HopeVolume", adjustedVolume);
                    break;
                case AudioChannel.Jungle:
                    masterMixer.SetFloat("JungleVolume", adjustedVolume);
                    break;
            }
        }

        /// <summary>
        /// Fade a sound with an array of sounds to delete after fade is complete.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="fadeTime"></param>
        /// <param name="fadeIn"></param>
        public void FadeMixer(AudioChannel channel, float fadeTime, bool fadeIn, AudioSource audioSource = null)
        {
            string mixerName = "";

            switch (channel)
            {
                case AudioChannel.Master:
                    mixerName = "MasterVolume";
                    break;
                case AudioChannel.Narration:
                    mixerName = "NarrationVolume";
                    break;
                case AudioChannel.Idle:
                    mixerName = "IdleVolume";
                    break;
                case AudioChannel.Intro:
                    mixerName = "IntroVolume";
                    break;
                case AudioChannel.Fire:
                    mixerName = "FireVolume";
                    break;
                case AudioChannel.Hope:
                    mixerName = "HopeVolume";
                    break;
                case AudioChannel.Jungle:
                    mixerName = "JungleVolume";
                    break;
            }

            if(fadeIn)
            {
                StartCoroutine(FadeInMixer(mixerName, fadeTime));
            }
            else
            {
                if(audioSource == null)
                {
                    StartCoroutine(FadeOutMixer(mixerName, fadeTime));
                }
                else
                {
                    StartCoroutine(FadeOutMixer(mixerName, fadeTime, audioSource));
                }
            }
        }

        /// <summary>
        /// Fade a mixer with an array of sounds to delete after fade is complete.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="fadeTime"></param>
        /// <param name="fadeIn"></param>
        /// <param name="audioSource"></param>
        public void FadeMixer(AudioChannel channel, float fadeTime, bool fadeIn, AudioSource[] audioSource = null)
        {
            string mixerName = "";

            switch (channel)
            {
                case AudioChannel.Master:
                    mixerName = "MasterVolume";
                    break;
                case AudioChannel.Narration:
                    mixerName = "NarrationVolume";
                    break;
                case AudioChannel.Idle:
                    mixerName = "IdleVolume";
                    break;
                case AudioChannel.Intro:
                    mixerName = "IntroVolume";
                    break;
                case AudioChannel.Fire:
                    mixerName = "FireVolume";
                    break;
                case AudioChannel.Hope:
                    mixerName = "HopeVolume";
                    break;
                case AudioChannel.Jungle:
                    mixerName = "JungleVolume";
                    break;
            }

            if (fadeIn)
            {
                StartCoroutine(FadeInMixer(mixerName, fadeTime));
            }
            else
            {
                if (audioSource == null)
                {
                    StartCoroutine(FadeOutMixer(mixerName, fadeTime));
                }
                else
                {
                    StartCoroutine(FadeOutMixer(mixerName, fadeTime, audioSource));
                }
            }
        }

        /// <summary>
        /// Fade mixer in and all sounds associated with it.
        /// </summary>
        /// <param name="volParam"></param>
        /// <param name="FadeTime"></param>
        /// <returns></returns>
        private IEnumerator FadeInMixer(string volParam, float FadeTime)
        {
            masterMixer.SetFloat(volParam, -80);
            float vol = 0;
            masterMixer.GetFloat(volParam, out vol);
            while (vol < 0)
            {
                 vol += (Time.deltaTime / FadeTime) * 100;
                masterMixer.SetFloat(volParam, vol);
                yield return null;
            }
        }

        /// <summary>
        /// Just fade Out a mixer and all sounds associated with it.
        /// </summary>
        /// <param name="volParam"></param>
        /// <param name="FadeTime"></param>
        /// <returns></returns>
        private IEnumerator FadeOutMixer(string volParam, float FadeTime)
        {
            float vol = 0;
            masterMixer.GetFloat(volParam, out vol);
            while (vol > -80)
            {
                vol -= (Time.deltaTime / FadeTime) * 100;
                masterMixer.SetFloat(volParam, vol);
                yield return null;
            }
        }

        /// <summary>
        /// FadeOut mixer and destroy referenced audio source
        /// </summary>
        /// <param name="volParam"></param>
        /// <param name="FadeTime"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private IEnumerator FadeOutMixer(string volParam, float FadeTime, AudioSource audioSource)
        {
            float vol = 0;
            masterMixer.GetFloat(volParam, out vol);
            while (vol > -80)
            {
                vol -= (Time.deltaTime / FadeTime) * 100;
                masterMixer.SetFloat(volParam, vol);
                yield return null;
            }
            Destroy(audioSource);
        }

        /// <summary>
        /// FadeOut mixer and destroy references audio source array.
        /// </summary>
        /// <param name="volParam"></param>
        /// <param name="FadeTime"></param>
        /// <param name="audioSource"></param>
        /// <returns></returns>
        private IEnumerator FadeOutMixer(string volParam, float FadeTime, AudioSource[] audioSource)
        {
            float vol = 0;
            masterMixer.GetFloat(volParam, out vol);
            while (vol > -80)
            {
                vol -= (Time.deltaTime / FadeTime) * 100;
                masterMixer.SetFloat(volParam, vol);
                yield return null;
            }
            foreach (var asource in audioSource)
            {
                Destroy(asource);
            }
        }


        #endregion Public Methods

        #region Private Methods

        private AudioSource CreatePlaySource(AudioClip clip, Vector3 point, float volume, float pitch, AudioChannel channel = 0)
        {
            //Create an empty game object
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = point;

            //Create the source
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            // Output sound through the correct mixer group.
            switch (channel)
            {
                case AudioChannel.Idle:
                    source.outputAudioMixerGroup = idleGroup;
                    break;
                case AudioChannel.Intro:
                    source.outputAudioMixerGroup = introGroup;
                    break;
                case AudioChannel.Fire:
                    source.outputAudioMixerGroup = fireGroup;
                    break;
                case AudioChannel.Hope:
                    source.outputAudioMixerGroup = hopeGroup;
                    break;
                case AudioChannel.Jungle:
                    source.outputAudioMixerGroup = jungleGroup;
                    break;
                case AudioChannel.Narration:
                    source.outputAudioMixerGroup = narrationGroup;
                    break;
                default:
                    source.outputAudioMixerGroup = masterGroup;
                    break;
            }
            source.Play();
            return source;
        }

        /// <summary>
        /// Set up audio levels
        /// </summary>
        private void Start()
        {
            // Set the audio levels from player preferences
            int masterVolume = PlayerPrefs.GetInt("MasterVolume", 100);
            int narrationVolume = PlayerPrefs.GetInt("NarrationVolume", 100);
            int idleVolume = PlayerPrefs.GetInt("IdleVolume", 100);
            int introVolume = PlayerPrefs.GetInt("IntroVolume", 100);
            int fireVolume = PlayerPrefs.GetInt("FireVolume", 100);
            int hopeVolume = PlayerPrefs.GetInt("HopeVolume", 100);
            int jungleVolume = PlayerPrefs.GetInt("JungleVolume", 100);

            // Update the audio mixer
            SetVolume(AudioChannel.Master, masterVolume);
            SetVolume(AudioChannel.Narration, narrationVolume);
            SetVolume(AudioChannel.Idle, idleVolume);
            SetVolume(AudioChannel.Intro, introVolume);
            SetVolume(AudioChannel.Fire, fireVolume);
            SetVolume(AudioChannel.Hope, hopeVolume);
            SetVolume(AudioChannel.Jungle, jungleVolume);
        }
        #endregion Private Methods
    }
}