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

        // KAZANDI: Siradaki musteriyi korumak icin verileri diske kilitliyoruz
        PlayerPrefs.Save();

        Time.timeScale = 0f;
        StartCoroutine(BesSaniyeBekleVeDon());
    }

    public void gameOver()
    {
        if ((GameCompletedScreen != null && GameCompletedScreen.activeSelf) ||
            (GameOverScreen != null && GameOverScreen.activeSelf)) return;

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }

        // KAYBETTÝ: Un puanini dusuruyoruz
        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);
        currentRep -= 10;
        PlayerPrefs.SetInt("SavedReputation", currentRep);

        // Elenirse ayni musteriyi tekrar denesin diye indeksi bir geri aliyoruz
        int currentCust = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);
        if (currentCust > 0)
        {
            PlayerPrefs.SetInt("CurrentCustomerIndex", currentCust - 1);
        }

        PlayerPrefs.Save();

        Time.timeScale = 0f;
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

        // ISTEGIN: Mini oyun bitince direkt Giris Sahnesine yonlendiriyoruz
        // Build Settings'te isminin "GirisSahnesi" oldugundan emin ol
        SceneManager.LoadScene("GirisSahnesi");
    }
}