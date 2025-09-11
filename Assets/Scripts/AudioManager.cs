using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    
    // 必须在Unity Inspector中分配以下音频资源
    public AudioClip introMusic;        
    public AudioClip normalStateMusic;  
    public AudioClip startSceneMusic;   

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "StartScene")
        {
            PlayStartSceneMusic();
        }
        else 
        {
            PlayLevelIntroMusic();
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
        audioSource.Play();
        
        float delayBeforeNormalMusic = Mathf.Max(introMusic.length, 3.0f);
        
        Invoke("SwitchToNormalStateMusic", delayBeforeNormalMusic);
    }
    
    void SwitchToNormalStateMusic()
    {
        audioSource.clip = normalStateMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}