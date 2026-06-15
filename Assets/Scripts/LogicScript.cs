using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    [Header("Skor Ayarlari")]
    public int kalanKediSayisi = 10;
    public Text scoreText;

    [Header("Ekran Ayarlari")]
    public GameObject GameOverScreen;
    public GameObject GameCompletedScreen;

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = kalanKediSayisi.ToString();
        }
        Time.timeScale = 1f;
    }

    public void addScore(int scoreToAdd)
    {
        if ((GameCompletedScreen != null && GameCompletedScreen.activeSelf) ||
            (GameOverScreen != null && GameOverScreen.activeSelf)) return;

        kalanKediSayisi -= scoreToAdd;

        if (scoreText != null)
        {
            scoreText.text = kalanKediSayisi.ToString();
        }

        if (kalanKediSayisi <= 0)
        {
            kalanKediSayisi = 0;
            if (scoreText != null) scoreText.text = "0";
            KazanmaSureciniBaslat();
        }
    }

    void KazanmaSureciniBaslat()
    {
        if (GameCompletedScreen != null)
        {
            GameCompletedScreen.SetActive(true);
        }

        // KAZANMA: Önce puaný ekle ve diske hemen kaydet
        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);
        currentRep += 10;
        PlayerPrefs.SetInt("SavedReputation", currentRep);
        PlayerPrefs.Save(); // Zaman durmadan önce kesin kaydet

        Time.timeScale = 0f;
        StartCoroutine(BesSaniyeBekleVeDon());
    }

    public void gameOver()
    {
        // GÜVENLÝK KONTROLÜ: Eðer zaten kaybetme ekraný açýldýysa ikinci kez bu kod çalýþmasýn
        if (GameOverScreen != null && GameOverScreen.activeSelf) return;

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }

        // --- PUAN DÜÞMEME SORUNUNUN ÇÖZÜMÜ ---
        // Zamaný durdurmadan önce puaný düþüyoruz ve hemen diske yazýyoruz.
        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);
        currentRep -= 10;
        PlayerPrefs.SetInt("SavedReputation", currentRep);

        // Müþteri indeksini 1 geri alýyoruz ki ayný müþteri tekrar gelsin
        int currentCust = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);
        if (currentCust > 0)
        {
            PlayerPrefs.SetInt("CurrentCustomerIndex", currentCust - 1);
        }

        PlayerPrefs.Save(); // Veriyi diske kilitle

        Time.timeScale = 0f; // Zamaný þimdi dondurabiliriz
        StartCoroutine(BesSaniyeBekleVeDon());
    }

    IEnumerator BesSaniyeBekleVeDon()
    {
        yield return new WaitForSecondsRealtime(5f);
        barinagaDon();
    }

    public void barinagaDon()
    {
        Time.timeScale = 1f;

        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);

        if (currentRep < 0)
        {
            Debug.Log("Un negatif! Oyun bitti, GirisSahnesine gidiliyor...");
            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GirisSahnesi");
        }
        else
        {
            Debug.Log("Un gecerli, BarinakSahnesine donuluyor... Guncel Un: " + currentRep);
            SceneManager.LoadScene("BarinakSahnesi");
        }
    }
}