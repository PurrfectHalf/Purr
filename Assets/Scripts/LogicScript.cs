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

        // KAZANDI: Ün puanýna +20 ekle ve diske yaz
        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);
        currentRep += 20;
        PlayerPrefs.SetInt("SavedReputation", currentRep);
        PlayerPrefs.Save();

        Time.timeScale = 0f;
        StartCoroutine(BesSaniyeBekleVeDon());
    }

    public void gameOver()
    {
        if (GameOverScreen != null && GameOverScreen.activeSelf) return;

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }

        // KAYBETTÝ: Ün puanýndan 10 düþür ve diske yaz
        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);
        currentRep -= 10;
        PlayerPrefs.SetInt("SavedReputation", currentRep);
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

        // Karar aný tam olarak burada devreye giriyor:
        int currentRep = PlayerPrefs.GetInt("SavedReputation", 10);

        if (currentRep < 0)
        {
            // EÐER EKSÝYE DÜÞTÜYSE: Her þeyi sýfýrla ve direkt Giriþ Sahnesine git!
            Debug.Log("Un eksiye dustu! Doðrudan GirisSahnesine gidiliyor...");
            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GirisSahnesi");
        }
        else
        {
            // EÐER EKSIYE DÜÞMEDÝYSE: Doðrudan Barýnak Sahnesine dön (Yeni müþteri zaten hazýr)
            Debug.Log("Un yeterli. BarinakSahnesine donuluyor...");
            SceneManager.LoadScene("BarinakSahnesi");
        }
    }
}