using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        // Oyuna her baþladýðýnda itibarýn 10'dan baþlamasýný istiyorsan burayý aktif et:
        // PlayerPrefs.SetInt("SavedReputation", 10);
        // PlayerPrefs.SetInt("CurrentCustomerIndex", 0);

        // Sahne isminin tam olarak "purrfect" olduðundan emin ol
        SceneManager.LoadScene("purrfect");
    }
}