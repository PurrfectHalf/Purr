using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Ses Kaynaklari (Hoparlörler)")]
    public AudioSource bgmSource;  // Müziði çalan hoparlör
    public AudioSource sfxSource;  // Efektleri çalan hoparlör

    [Header("Merkezi Arka Plan Müziði")]
    public AudioClip arkaPlanMuzigi; // Cute Bossa Nova buraya gelecek

    [Header("Flappy Bird Mini Oyun Sesleri")]
    public AudioClip ziplamaSesi;
    public AudioClip skorSesi;
    public AudioClip basariSesi;
    public AudioClip yanmaSesi;

    [Header("Eþleþtirme Ekraný Sesleri")]
    public AudioClip okButonuSesi;
    public AudioClip dogruEslesmeSesi;
    public AudioClip yanlisEslesmeSesi;

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

    // --- YENÝ: MÜZÝK KAPATMA / AÇMA FONKSÝYONU ---
    // Toggle durumuna göre (true/false) müziði susturur
    public void ToggleBGM(bool isOn)
    {
        if (bgmSource != null)
        {
            bgmSource.mute = !isOn; // Eðer Toggle açýksa (true), mute kapansýn (false) yani ses gelsin.
        }
    }

    // --- YENÝ: SFX KAPATMA / AÇMA FONKSÝYONU ---
    // Toggle durumuna göre (true/false) efektleri susturur
    public void ToggleSFX(bool isOn)
    {
        if (sfxSource != null)
        {
            sfxSource.mute = !isOn;
        }
    }
}