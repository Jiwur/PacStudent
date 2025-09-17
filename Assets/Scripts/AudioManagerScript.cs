using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource sfxSource; 
    
    public AudioClip introMusic;     
    public AudioClip normalStateMusic;
    public AudioClip scaredStateMusic;
    public AudioClip startSceneMusic; 

    public AudioClip[] moveSounds; 
    public AudioClip eatPelletSound;
    public AudioClip wallCollisionSound;
    public AudioClip deathSound;

    private string currentSceneName;
    private bool hasIntroFinished = false;
    private float introStartTime;

    void Start()
    {
        // 如果 audioSource 未绑定，则自动创建
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "StartScene")
        {
            PlayMusic(startSceneMusic, true);
        }
        else 
        {
            PlayMusic(introMusic, false);
            introStartTime = Time.time;
        }
    }

    void Update()
    {
        if (currentSceneName != "StartScene" && !hasIntroFinished)
        {
            if (!audioSource.isPlaying || (Time.time - introStartTime) >= 3.0f)
            {
                hasIntroFinished = true;
                PlayMusic(normalStateMusic, true);
            }
        }
    }
    
    void PlayMusic(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
    
    public void SwitchToScaredStateMusic()
    {
        PlayMusic(scaredStateMusic, true);
    }
    
    public void PlayMoveSound()
    {
        if (moveSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, moveSounds.Length);
            sfxSource.PlayOneShot(moveSounds[randomIndex]);
        }
    }
    
    public void PlayEatPelletSound()
    {
        sfxSource.PlayOneShot(eatPelletSound);
    }
    
    public void PlayWallCollisionSound()
    {
        sfxSource.PlayOneShot(wallCollisionSound);
    }
    
    public void PlayDeathSound()
    {
        sfxSource.PlayOneShot(deathSound);
    }
    
    public void ResumeNormalStateMusic()
    {
        PlayMusic(normalStateMusic, true);
    }
}
