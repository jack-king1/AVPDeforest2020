using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yaSingleton;

[System.Serializable]
public struct audioClips
{
    public string name;
    public AudioClip clip;
}

[CreateAssetMenu(fileName = "SFX", menuName = "Singletons/SFX")]
public class SFX : Singleton<SFX>
{
    private int audioSeed;
    public float timer;
    private AudioSource source;
    NextScene scene;
    public AudioClip NoAudioWithNameFound;

    public audioClips[] clips;

    public AudioClip GetSFX(string _name)
    {
        AudioClip sound = NoAudioWithNameFound;
        foreach(var clip in clips)
        {
            if(clip.name == _name)
            {
                sound = clip.clip;
                return sound;
            }
        }
        return sound;
    }

    #region OldCode
    //private void Update()
    //{
    //    //if(scene.startSFX)
    //    //{
    //    //    timer += Time.deltaTime;
    //    //}

    //    //if (timer >= 60 && timer <= 61 )   StopForestSounds(5);

    //    //if (timer >= 110 && timer <= 111) StartWindSounds(1);
    //}



    //#region IDLE
    //public void StartIdleSounds(float timeToFade)
    //{
    //    AudioManagerOld.instance.FadeInSound("Idle", timeToFade);
    //}

    //public void StopIdleSounds(float timeToFade)
    //{
    //    AudioManagerOld.instance.FadeOutSound("Idle", timeToFade);
    //}
    //#endregion

    //#region INTRO
    //public void StartIntroSounds(float timeToFade)
    //{
    //   // AudioManager.instance.FadeInSound("Intro", timeToFade);

    //}

    //public void StopIntroSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeOutSound("Intro", timeToFade);
    //}
    //#endregion

    //#region JUNGLE
    //public void StartWindSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeInSound("Whoosh", timeToFade);
    //}

    //public void StopWindSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeOutSound("Whoosh", timeToFade);
    //}

    //public void StartForestSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeInSound("Alt", timeToFade);
    //    //AudioManager.instance.FadeInSound("Jungle", timeToFade);
    //    //AudioManager.instance.FadeInSound("Cicada", timeToFade);
    //}

    //public void StopForestSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeOutSound("Alt", timeToFade);
    //    //AudioManager.instance.FadeOutSound("Jungle", timeToFade);
    //    //AudioManager.instance.FadeOutSound("Cicada", timeToFade);
    //}
    //#endregion

    //#region OUTRO
    //public void StartOutroSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeInSound("Outro", timeToFade);
    //}

    //public void StopOutroSounds(float timeToFade)
    //{
    //    //AudioManager.instance.FadeOutSound("Outro", timeToFade);
    //}
    //#endregion

    //void ResetValues()
    //{
    //    //timer = 0;
    //}
    #endregion
}
