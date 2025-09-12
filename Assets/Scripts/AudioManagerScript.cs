using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    
    public AudioClip introMusic;     
    public AudioClip normalStateMusic; 
    public AudioClip startSceneMusic; 

    private string currentSceneName;
    private bool hasIntroFinished = false;
    
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "StartScene")
        {
            PlayStartSceneMusic();
        }
        else 
        {
            PlayLevelIntroMusic();
        }
    }
    
    void Update()
    {
        if (currentSceneName != "StartScene" && !hasIntroFinished)
        {
            if (!audioSource.isPlaying)
            {
                hasIntroFinished = true;
                SwitchToNormalStateMusic();
            }
        }
    }
    
    void PlayStartSceneMusic()
    {
        audioSource.clip = startSceneMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    void PlayLevelIntroMusic()
    {
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();

    }
    
    void SwitchToNormalStateMusic()
    {
        audioSource.clip = normalStateMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}