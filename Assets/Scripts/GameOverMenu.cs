using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void GirisSahnesineDon()
    {
        Debug.Log("GameOver butonuna basildi. GirisSahnesi yukleniyor.");

        Time.timeScale = 1f;

        PlayerPrefs.SetInt("SavedReputation", 10);
        PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
        PlayerPrefs.SetInt("WrongMatchThisRound", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GirisSahnesi");
    }
}