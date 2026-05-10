using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // BU SATIR EKSÝKTÝ, EKLEDÝK!

public class ClickerGame : MonoBehaviour
{
    [Header("Görsel Referanslar")]
    public RectTransform fleaButton;
    public RectTransform hayaletTarak;
    public TextMeshProUGUI statusText;

    [Header("Oyun Ayarlarý")]
    public float totalGameTime = 15f;
    public int targetScore = 5;

    private int currentScore = 0;
    private float timer;
    private bool isGameActive = false;

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (hayaletTarak != null)
            hayaletTarak.position = Input.mousePosition;

        if (isGameActive)
        {
            timer -= Time.deltaTime;
            UpdateUI();

            if (timer <= 0)
                EndGame(false, "SÜRE BÝTTÝ!");
        }
    }

    public void OnFleaClick()
    {
        if (!isGameActive) return;

        currentScore++;
        if (currentScore >= targetScore)
            EndGame(true, "BAÞARDIN!");
        else
            MoveFlea();
    }

    public void OnMissClick()
    {
        if (!isGameActive) return;

        // Eðer fare þu an pirenin üzerindeyse yanma iþlemini iptal et
        // Bu sayede çift týklama veya çakýþma hatalarýný önleriz
        if (EventSystem.current.currentSelectedGameObject == fleaButton.gameObject) return;

        Debug.Log("Pireyi kaçýrdýn, oyun bitti!");
        EndGame(false, "PÝREYÝ KAÇIRDIN!");
    }

    public void ResetGame()
    {
        currentScore = 0;
        timer = totalGameTime;
        isGameActive = true;

        fleaButton.gameObject.SetActive(true);
        Cursor.visible = false;
        if (hayaletTarak != null) hayaletTarak.gameObject.SetActive(true);

        MoveFlea();
        UpdateUI();
    }

    void MoveFlea()
    {
        // Senin SMOKÝN çerçevesine göre bu sýnýrlarý ayarlayabilirsin
        float limitX = 250f;
        float limitY = 400f;
        float randomX = Random.Range(-limitX, limitX);
        float randomY = Random.Range(-limitY, limitY);
        fleaButton.anchoredPosition = new Vector2(randomX, randomY);
    }

    void EndGame(bool success, string message)
    {
        isGameActive = false;
        fleaButton.gameObject.SetActive(false);
        Cursor.visible = true;
        if (hayaletTarak != null) hayaletTarak.gameObject.SetActive(false);

        if (statusText != null)
            statusText.text = success ? $"<color=green>{message}</color>" : $"<color=red>{message}</color>";
    }

    void UpdateUI()
    {
        if (statusText != null)
            statusText.text = $"Süre: {timer:F1}s | Pireler: {currentScore}/{targetScore}";
    }
}