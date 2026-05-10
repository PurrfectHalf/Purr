using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    [Header("Skor Ayarlarý")]
    public int kalanKediSayisi = 10; // 10'dan baþlayacak
    public Text scoreText;

    [Header("Ekran Ayarlarý")]
    public GameObject GameOverScreen;
    public GameObject GameCompletedScreen; // Senin adlandýrdýðýn isim

    void Start()
    {
        // Baþlangýçta ekrana 10 yazdýr
        if (scoreText != null)
        {
            scoreText.text = kalanKediSayisi.ToString();
        }

        // Zamanýn akýþýný sýfýrla (Önceki oyundan 0 kalmýþ olabilir)
        Time.timeScale = 1f;
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        // Bir ekran zaten açýksa skor düþmeye devam etmesin
        if ((GameCompletedScreen != null && GameCompletedScreen.activeSelf) ||
            (GameOverScreen != null && GameOverScreen.activeSelf)) return;

        // Skoru azalt
        kalanKediSayisi -= scoreToAdd;

        if (scoreText != null)
        {
            scoreText.text = kalanKediSayisi.ToString();
        }

        // KAZANMA ÞARTI: 10 boru geçtiðinde (0 olduðunda)
        if (kalanKediSayisi <= 0)
        {
            kalanKediSayisi = 0;
            if (scoreText != null) scoreText.text = "0";
            KazanmaSureciniBaslat();
        }
    }

    void KazanmaSureciniBaslat()
    {
        // GÜVENLÝK: Ekran koda baðlandýysa aç, yoksa konsola hata yaz
        if (GameCompletedScreen != null)
        {
            GameCompletedScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("HATA: GameCompletedScreen objesini Inspector'dan sürüklemeyi unuttun!");
        }

        Time.timeScale = 0f; // Arka planý dondur

        // 5 saniye bekle ve barýnaða dön
        StartCoroutine(BesSaniyeBekleVeDon());
    }

    public void gameOver()
    {
        // Kazandýysak kaybetme ekraný çýkmasýn
        if ((GameCompletedScreen != null && GameCompletedScreen.activeSelf) ||
            (GameOverScreen != null && GameOverScreen.activeSelf)) return;

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }

        // Kaybedince de 5 saniye bekle ve barýnaða dön
        StartCoroutine(BesSaniyeBekleVeDon());
    }

    IEnumerator BesSaniyeBekleVeDon()
    {
        // Zaman 0 olsa bile gerçek zamanlý 5 saniye sayar
        yield return new WaitForSecondsRealtime(5f);
        barinagaDon();
    }

    public void barinagaDon()
    {
        Time.timeScale = 1f; // Zamaný mutlaka normale döndür!

        // BarinakSahnesi'nin Build Settings'te ekli olduðundan emin ol!
        SceneManager.LoadScene("BarinakSahnesi");
    }
}