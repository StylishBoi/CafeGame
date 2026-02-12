using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Maybe a bit stupid to put all the sound effects in the same script
    [Header("Audio Sources")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;
    
    [Header("Music")]
    [SerializeField] public AudioClip mainmenuMusic;
    [SerializeField] public AudioClip cafeMusic;
    [SerializeField] public AudioClip predayMusic;

    [Header("Sounds")]
    [SerializeField] public AudioClip goodServeSFX;
    [SerializeField] public AudioClip midServeSFX;
    [SerializeField] public AudioClip badServeSFX;
    [SerializeField] public AudioClip clientLeaveSFX;
    [SerializeField] public AudioClip itemReceiveSFX;
    [SerializeField] public AudioClip dayEndSFX;
    [SerializeField] public AudioClip savedGameSFX;
    [SerializeField] public AudioClip buttonSelectSFX;
    [SerializeField] public AudioClip buttonPressSFX;
    [SerializeField] public AudioClip buttonErrorSFX;
    [SerializeField] public AudioClip dishwasherSFX;
    [SerializeField] public AudioClip filledTrashcanSFX;
    [SerializeField] public AudioClip scoreScreenSFX;
    [SerializeField] public AudioClip clientArrivalSFX;
    [Header("Minigame Sounds")]
    [SerializeField] public AudioClip milkshakeSuccessSFX;
    [SerializeField] public AudioClip milkshakeFailSFX;
    [SerializeField] public AudioClip sweepSFX;
    [SerializeField] public AudioClip coffeePourSFX;
    [SerializeField] public AudioClip teaBubbleSFX;
    [SerializeField] public AudioClip plateSFX;
    [SerializeField] public AudioClip trashcanSFX;
    
    public static AudioManager Instance;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        if(musicSource) musicSource.loop = true;
    }

    public void PlayBGM(AudioClip clip)
    {
        //Stops the previous background music
        musicSource.clip = clip;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }
    
    public void StopBGM()
    {
        //Stops the background music
        musicSource.Stop();
    }

    public void ChangeBGM(AudioClip clip)
    {
        StartCoroutine(GradualChangeEnumerator(clip));
    }

    public void PlaySfx(AudioClip clip)
    {
        //Allows for SFX to be played in other scripts
        if(sfxSource) sfxSource.PlayOneShot(clip);
    }

    public void StopSfx()
    {
        if(sfxSource) sfxSource.Stop();
    }
    
    private IEnumerator GradualChangeEnumerator(AudioClip newClip)
    {
        float t = 0f;
        float startVolume = musicSource.volume;
        
        while (t < 1f)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t);
            t += Time.deltaTime / 1;
            yield return null;
        }

        float t2 = 0f;

        musicSource.clip = newClip;
        musicSource.Play();

        while (t2 < 1f)
        {
            musicSource.volume = Mathf.Lerp(0, startVolume, t2);
            t2 += Time.deltaTime / 1;
            yield return null;
        }
    }
}