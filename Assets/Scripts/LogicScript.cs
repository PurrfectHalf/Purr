using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    [Header("Skor Ayarlari")]
    public int kalanKediSayisi = 10;
    public Text scoreText;

    [Header("Ekran Ayarlari")]
    public GameObject GameOverScreen;
    public GameObject GameCompletedScreen;

    private bool miniGameEnded = false;

    void Start()
    {
        Time.timeScale = 1f;
        miniGameEnded = false;

        if (scoreText != null)
        {
            scoreText.text = kalanKediSayisi.ToString();
        }
    }

    public void addScore(int scoreToAdd)
    {
        if (miniGameEnded)
        {
            return;
        }

        if ((GameCompletedScreen != null && GameCompletedScreen.activeSelf) ||
            (GameOverScreen != null && GameOverScreen.activeSelf))
        {
            return;
        }

        kalanKediSayisi -= scoreToAdd;

        if (kalanKediSayisi < 0)
        {
            kalanKediSayisi = 0;
        }

        if (scoreText != null)
        {
            scoreText.text = kalanKediSayisi.ToString();
        }

        if (kalanKediSayisi <= 0)
        {
            KazanmaSureciniBaslat();
        }
    }

    void KazanmaSureciniBaslat()
    {
        if (miniGameEnded)
        {
            return;
        }

        miniGameEnded = true;

        if (GameCompletedScreen != null)
        {
            GameCompletedScreen.SetActive(true);
        }

        Time.timeScale = 0f;
        StartCoroutine(BasariliMiniGameSonrasi());
    }

    public void gameOver()
    {
        if (miniGameEnded)
        {
            return;
        }

        miniGameEnded = true;

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }

        Time.timeScale = 0f;
        StartCoroutine(BasarisizMiniGameSonrasi());
    }

    IEnumerator BasariliMiniGameSonrasi()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;

        bool gameEndedByScore = GameStateManager.AddReputation(GameStateManager.SuccessReward);

        if (gameEndedByScore)
        {
            yield break;
        }

        bool finishedCustomers = GameStateManager.AdvanceCustomerAndCheckFinished(GameStateManager.GetTotalCustomerCount());

        if (finishedCustomers)
        {
            yield break;
        }

        GameStateManager.GoToShelter();
    }

    IEnumerator BasarisizMiniGameSonrasi()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;

        bool gameEndedByReputation = GameStateManager.AddReputation(-GameStateManager.MiniGameFailPenalty);

        if (gameEndedByReputation)
        {
            yield break;
        }

        bool finishedCustomers = GameStateManager.AdvanceCustomerAndCheckFinished(GameStateManager.GetTotalCustomerCount());

        if (finishedCustomers)
        {
            yield break;
        }

        GameStateManager.GoToShelter();
    }

    public void barinagaDon()
    {
        Time.timeScale = 1f;

        if (GameStateManager.GetReputation() < 0)
        {
            GameStateManager.GoToGameOver();
        }
        else
        {
            GameStateManager.GoToShelter();
        }
    }
}