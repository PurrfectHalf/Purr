using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Ses Kaynaklari")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Ses Klipleri")]
    public AudioClip arkaPlanMuzigi;
    public AudioClip ziplamaSesi;
    public AudioClip skorSesi;
    public AudioClip basariSesi;
    public AudioClip yanmaSesi;

    void Awake()
    {
        if (instance == null)
        {
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (bgmSource != null && arkaPlanMuzigi != null)
        {
            bgmSource.clip = arkaPlanMuzigi;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}