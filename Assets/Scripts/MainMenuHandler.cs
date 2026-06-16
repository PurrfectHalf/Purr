using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        GameStateManager.ResetGame();

        GameStateManager.GoToShelter();
    }
}