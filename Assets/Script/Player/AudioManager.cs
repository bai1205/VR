using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public enum AudioType
{
    bg,
    OnClick,
    Loop
}
public class AudioManager : MonoSingleton<AudioManager>
{
    public float bgAudio=1;
    public float hitAudio=1;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> bgAudioSources = new List<AudioSource>();
    public void PlayAudio(string path,AudioType audioType=AudioType.OnClick)
    {
        AudioClip audioClip;
        AudioSource audioSource;
        if (audioType==AudioType.bg)
        {
            foreach (var item in bgAudioSources)
            {
                item.Stop();
            }
        }
        for (int i = 0; i < audioSources.Count; i++) 
        {
            audioSource = audioSources[i];
            if (!audioSource.isPlaying)
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioSource.clip = audioClip;
                audioSource.Play();
                switch (audioType)
                {
                    case AudioType.bg:
                        audioSource.loop = true;
                        audioSource.volume = bgAudio;
                        bool isadd=false ;
                        foreach (var item in bgAudioSources)
                        {
                            if (item== audioSource)
                            {
                                isadd = true;
                                break;
                            }
                        }
                        if (isadd == false )
                        {
                            bgAudioSources.Add(audioSource);
                        }
                        return;
                    case AudioType.OnClick:
                        audioSource.loop = false ;
                        audioSource.volume = hitAudio;
                        return;
                    case AudioType.Loop:
                        audioSource.loop = true;
                        return;
                }
                return;
            }
        }
        audioSource=gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
        audioClip = Resources.Load<AudioClip>(path);
        audioSource.clip = audioClip;
        audioSource.Play();
        switch (audioType)
        {
            case AudioType.bg:
                audioSource.loop = true;
                audioSource.volume = bgAudio;
                bool isadd = false;
                foreach (var item in bgAudioSources)
                {
                    if (item == audioSource)
                    {
                        isadd = true;
                        break;
                    }
                }
                if (isadd == false)
                {
                    bgAudioSources.Add(audioSource);
                }
                break;
            case AudioType.OnClick:
                audioSource.loop = false;
                audioSource.volume = hitAudio;
                break;
            case AudioType.Loop:
                audioSource.loop = true;
                break;
        }
    }

    public void StopAllAudio()
    {
        foreach (var item in bgAudioSources)
        {
            item.Stop();
        }
    }
    public void SetAudio(float v)
    {
        bgAudio = v;
        foreach (var item in audioSources)
        {
            if (item.isPlaying)
            {
                item.volume = bgAudio;
            }
        }
    }
}
