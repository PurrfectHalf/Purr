using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class GameOverButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(GirisSahnesineDon);
    }

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