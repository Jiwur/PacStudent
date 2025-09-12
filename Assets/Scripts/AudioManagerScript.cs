using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Background Music")]
    public AudioClip BGM_Start;
    public AudioClip BGM_Intro;
    public AudioClip BGM_Normal;
    public AudioClip BGM_Scared;
    public AudioClip BGM_Return;
    
    [Header("Sound Effects")]
    public AudioClip SFX_PSDeath;
    public AudioClip SFX_PSPellet;
    public AudioClip[] SFX_PSSteps;
    public AudioClip SFX_PSWall;
    

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    private string currentMusicState = "";
    
    private int lastStepIndex = -1;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene" || scene.name == "MainMenu")
        {
            PlayStartBGM();
        }
        else if (scene.name == "GameScene" || scene.name == "Level01")
        {
            PlayIntroThenNormalBGM();
        }
    }
    
    private void Start()
    {
        PlayStartBGM();
    }
    
    public void PlayStartBGM()
    {
        if (BGM_Start != null && currentMusicState != "Start")
        {
            musicSource.clip = BGM_Start;
            musicSource.loop = true;
            musicSource.Play();
            currentMusicState = "Start";
        }
    }
    
    public void PlayIntroThenNormalBGM()
    {
        if (BGM_Intro != null)
        {
            musicSource.clip = BGM_Intro;
            musicSource.loop = false; 
            musicSource.Play();
            currentMusicState = "Intro";
            
            StartCoroutine(SwitchToNormalAfterIntro());
        }
    }
    
    private IEnumerator SwitchToNormalAfterIntro()
    {
        yield return new WaitWhile(() => musicSource.isPlaying && musicSource.clip == BGM_Intro);
        
        PlayNormalBGM();
    }
    
    public void PlayNormalBGM()
    {
        if (BGM_Normal != null && currentMusicState != "Normal")
        {
            musicSource.clip = BGM_Normal;
            musicSource.loop = true;
            musicSource.Play();
            currentMusicState = "Normal";
        }
    }
    
    public void PlayScaredBGM()
    {
        if (BGM_Scared != null && currentMusicState != "Scared")
        {
            musicSource.clip = BGM_Scared;
            musicSource.loop = true;
            musicSource.Play();
            currentMusicState = "Scared";
        }
    }
    
    public void PlayReturnBGM()
    {
        if (BGM_Return != null && currentMusicState != "Return")
        {
            musicSource.clip = BGM_Return;
            musicSource.loop = true;
            musicSource.Play();
            currentMusicState = "Return";
        }
    }
    
    public void PlayDeathSFX()
    {
        if (SFX_PSDeath != null)
        {
            sfxSource.PlayOneShot(SFX_PSDeath);
        }
    }
    
    public void PlayPelletSFX()
    {
        if (SFX_PSPellet != null)
        {
            sfxSource.PlayOneShot(SFX_PSPellet);
        }
    }
    
    public void PlayStepSFX()
    {
        if (SFX_PSSteps != null && SFX_PSSteps.Length > 0)
        {
            int newIndex;
            do
            {
                newIndex = Random.Range(0, SFX_PSSteps.Length);
            } while (SFX_PSSteps.Length > 1 && newIndex == lastStepIndex);
            
            sfxSource.PlayOneShot(SFX_PSSteps[newIndex]);
            lastStepIndex = newIndex;
        }
    }
    
    public void PlayWallSFX()
    {
        if (SFX_PSWall != null)
        {
            sfxSource.PlayOneShot(SFX_PSWall);
        }
    }
    
    public void StopAllAudio()
    {
        if (musicSource != null) musicSource.Stop();
        if (sfxSource != null) sfxSource.Stop();
    }
    
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null) musicSource.volume = volume;
    }
    
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null) sfxSource.volume = volume;
    }
}