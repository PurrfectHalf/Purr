using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        // Puanlarý ve müþteri sýrasýný oyun baþýndaki haline döndürür
        PlayerPrefs.SetInt("SavedReputation", 10);
        PlayerPrefs.SetInt("CurrentCustomerIndex", 0);

        // Giriþ sahnesini yükle
        SceneManager.LoadScene("GirisSahnesi");
    }
}