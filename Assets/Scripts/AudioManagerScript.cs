using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;   // 背景音乐
    public AudioSource sfxSource;     // 脚步声

    public AudioClip introMusic;      
    public AudioClip normalStateMusic;
    public AudioClip stepClip;        // 脚步声 Step1

    private bool hasIntroFinished = false;
    private float introStartTime;

    void Start()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        // 播放 Intro 音乐（不循环）
        PlayMusic(introMusic, false);
        introStartTime = Time.time;
    }

    void Update()
    {
        // Intro 播放完成或超时 3 秒 → 切换到 Normal
        if (!hasIntroFinished)
        {
            if (!musicSource.isPlaying || (Time.time - introStartTime) >= 3.0f)
            {
                hasIntroFinished = true;
                PlayMusic(normalStateMusic, true);
            }
        }
    }

    private void PlayMusic(AudioClip clip, bool loop)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    /// <summary>
    /// 开始播放脚步声（循环）
    /// </summary>
    public void StartStepSound()
    {
        if (stepClip != null && !sfxSource.isPlaying)
        {
            sfxSource.clip = stepClip;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    /// <summary>
    /// 停止脚步声
    /// </summary>
    public void StopStepSound()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
}