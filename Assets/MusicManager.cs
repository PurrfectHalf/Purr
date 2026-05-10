using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Eðer zaten bir MusicManager varsa, yenisini yok et (Singleton)
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        // Sahne deðiþtiðinde bu objenin yok olmasýný engelle
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            // Müziði kýsýk sesle baþlat (0.0 ile 1.0 arasý)
            audioSource.volume = 0.2f;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}