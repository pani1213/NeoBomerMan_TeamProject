using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] BgmClips;
    public float BgmVolume;
    AudioSource BgmPlayer;

    public AudioClip[] SfxClips;
    public float SfxVolume;
    public int channels;
    AudioSource[] SfxPlayer;
    int channelIndex;

    public enum Bgm { town, underwater }
    public enum Sfx { start, explosion, setbomb }

    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        BgmPlayer = bgmObject.AddComponent<AudioSource>();
        BgmPlayer.playOnAwake = false;
        BgmPlayer.loop = true;
        BgmPlayer.volume = BgmVolume;

        GameObject SfxObject = new GameObject("SfxPlayer");
        SfxObject.transform.parent = transform;
        SfxPlayer = new AudioSource[channels];

        for (int i = 0; i < SfxPlayer.Length; i++)
        {
            SfxPlayer[i] = SfxObject.AddComponent<AudioSource>();
            SfxPlayer[i].playOnAwake = false;
            SfxPlayer[i].volume = SfxVolume;
        }
    }
    public void PlayBgm(Bgm bgm)
    {
        BgmPlayer.clip = BgmClips[(int)bgm];
        BgmPlayer.Play();
    }

    public void PlaySfx (Sfx sfx)
    {
        for (int i = 0; i < SfxPlayer.Length; i++)
        {
            int loopIndex = (i + channelIndex) % SfxPlayer.Length;

            if (SfxPlayer[loopIndex].isPlaying)
            {
                continue;
            }
            channelIndex = loopIndex;
            SfxPlayer[loopIndex].clip = SfxClips[(int)sfx];
            SfxPlayer[loopIndex].Play();
            break;
        }   

    }  
}
