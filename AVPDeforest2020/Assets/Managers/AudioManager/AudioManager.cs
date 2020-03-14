
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
        public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
        {
            //Create an empty game object
            AudioSource source = CreatePlaySource(clip, emitter, volume, pitch);
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
        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
        {
            AudioSource source = CreatePlaySource(clip, point, volume, pitch);
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
        public AudioSource PlayLoop(AudioClip clip, Transform emitter, float volume = 1f, float pitch = 1f, bool music = true)
        {
            AudioSource source = CreatePlaySource(clip, emitter, volume, pitch, true);
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
        public AudioSource PlayLoop(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, bool music = true)
        {
            AudioSource source = CreatePlaySource(clip, point, volume, pitch, true);
            source.loop = true;
            return source;
        }

        /// <summary>
        /// Adjusts the volume on the audio channel in the unity audio mixer
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="volume">From 0 (mute) to 100 (full volume - 0 DB)</param>
        public void SetVolume(AudioChannel channel, int volume)
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
            }
        }

        /// <summary>
        /// Fade in a mixer which will fade in all audio under that type of mixer. to either max vol or muted.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="fadeTime"></param>
        /// <param name="fadeIn"></param>
        public void FadeMixer(AudioChannel channel, float fadeTime, bool fadeIn)
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
            }

            if(fadeIn)
            {
                StartCoroutine(FadeInMixer(mixerName, fadeTime));
            }
            else
            {
                StartCoroutine(FadeOutMixer(mixerName, fadeTime));
            }
        }

        private IEnumerator FadeInMixer(string volParam, float FadeTime)
        {
            masterMixer.SetFloat(volParam, 0);
            float vol = 0;
            masterMixer.GetFloat(volParam, out vol);
            while (vol < 1)
            {
                 vol += Time.deltaTime / FadeTime;
                masterMixer.SetFloat(volParam, vol * 100);
                yield return null;
            }
        }
        private IEnumerator FadeOutMixer(string volParam, float FadeTime)
        {
            float vol = 0;
            masterMixer.GetFloat(volParam, out vol);
            while (vol > 0)
            {
                vol -= Time.deltaTime / FadeTime;
                masterMixer.SetFloat(volParam, vol * 100);
                yield return null;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private AudioSource CreatePlaySource(AudioClip clip, Vector3 point, float volume, float pitch, bool music = false)
        {
            //Create an empty game object
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = point;

            //Create the source
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            // Output sound through the sound group or music group
            if (music)
                source.outputAudioMixerGroup = musicGroup;
            else
                source.outputAudioMixerGroup = narrationGroup;

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

            // Update the audio mixer
            SetVolume(AudioChannel.Master, masterVolume);
            SetVolume(AudioChannel.Narration, narrationVolume);
            SetVolume(AudioChannel.Idle, idleVolume);
            SetVolume(AudioChannel.Intro, introVolume);
            SetVolume(AudioChannel.Fire, fireVolume);
            SetVolume(AudioChannel.Hope, hopeVolume);
        }
        #endregion Private Methods
    }
}