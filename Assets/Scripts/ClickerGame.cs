using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickerGame : MonoBehaviour
{
    [Header("Gorsel Referanslar")]
    public RectTransform fleaButton;
    public RectTransform hayaletTarak;
    public TextMeshProUGUI statusText;

    [Header("Oyun Ayarlari")]
    public float totalGameTime = 15f;
    public int targetScore = 5;

    private int currentScore = 0;
    private float timer;
    private bool isGameActive = false;
    private bool miniGameEnded = false;

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (hayaletTarak != null)
        {
            hayaletTarak.position = Input.mousePosition;
        }

        if (isGameActive)
        {
            timer -= Time.deltaTime;
            UpdateUI();

            if (timer <= 0)
            {
                EndGame(false, "SURE BITTI!");
            }
        }
    }

    public void OnFleaClick()
    {
        if (!isGameActive || miniGameEnded)
        {
            return;
        }

        currentScore++;

        if (currentScore >= targetScore)
        {
            EndGame(true, "BASARDIN!");
        }
        else
        {
            MoveFlea();
        }
    }

    public void OnMissClick()
    {
        if (!isGameActive || miniGameEnded)
        {
            return;
        }

        if (EventSystem.current != null &&
            EventSystem.current.currentSelectedGameObject != null &&
            fleaButton != null &&
            EventSystem.current.currentSelectedGameObject == fleaButton.gameObject)
        {
            return;
        }

        EndGame(false, "PIREYI KACIRDIN!");
    }

    public void ResetGame()
    {
        currentScore = 0;
        timer = totalGameTime;
        isGameActive = true;
        miniGameEnded = false;

        if (fleaButton != null)
        {
            fleaButton.gameObject.SetActive(true);
        }

        Cursor.visible = false;

        if (hayaletTarak != null)
        {
            hayaletTarak.gameObject.SetActive(true);
        }

        MoveFlea();
        UpdateUI();
    }

    void MoveFlea()
    {
        if (fleaButton == null)
        {
            return;
        }

        float limitX = 250f;
        float limitY = 400f;

        float randomX = Random.Range(-limitX, limitX);
        float randomY = Random.Range(-limitY, limitY);

        fleaButton.anchoredPosition = new Vector2(randomX, randomY);
    }

    void EndGame(bool success, string message)
    {
        if (miniGameEnded)
        {
            return;
        }

        miniGameEnded = true;
        isGameActive = false;

        if (fleaButton != null)
        {
            fleaButton.gameObject.SetActive(false);
        }

        Cursor.visible = true;

        if (hayaletTarak != null)
        {
            hayaletTarak.gameObject.SetActive(false);
        }

        if (statusText != null)
        {
            statusText.text = success
                ? $"<color=green>{message}</color>"
                : $"<color=red>{message}</color>";
        }

        StartCoroutine(FinishMiniGame(success));
    }

    IEnumerator FinishMiniGame(bool success)
    {
        yield return new WaitForSecondsRealtime(1.5f);

        if (success)
        {
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
        else
        {
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
    }

    void UpdateUI()
    {
        if (statusText != null)
        {
            statusText.text = $"Sure: {timer:F1}s | Pireler: {currentScore}/{targetScore}";
        }
    }
}