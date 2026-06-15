using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    [Header("UI Elemanlari")]
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI reputationText;
    public CustomerUI customerUI;

    [Header("Un Puani Ayarlari")]
    public int currentReputation = 10;
    private const int wrongMatchPenalty = 10;

    [Header("Veri Listeleri")]
    public List<CatData> allCats;
    public List<CustomerData> allCustomers;

    private int currentCatIndex = 0;
    private int currentCustomerIndex = 0;
    private CatData activeCat;
    private CatUI catUI;

    void Start()
    {
        catUI = Object.FindFirstObjectByType<CatUI>();

        // Hafizadaki guncel degerleri yukle
        currentCustomerIndex = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);
        currentReputation = PlayerPrefs.GetInt("SavedReputation", 10);

        // --- UN PUANI KONTROLÜ ---
        // Eger un puani 0'dan kucukse alt kodlar hic tetiklenmeden aninda sutlansin
        if (currentReputation < 0)
        {
            Debug.Log("Un bitti! GirisSahnesine firlatiliyorsunuz...");

            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GirisSahnesi");
            return;
        }

        UpdateReputationUI();

        // Liste sinir kontrolu (Musteriler bittiyse basa sar)
        if (allCustomers.Count == 0 || currentCustomerIndex >= allCustomers.Count)
        {
            currentCustomerIndex = 0;
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();
        }

        // Ilk kedi ve guncel musteriyi ekranda goster
        if (allCats.Count > 0)
        {
            currentCatIndex = 0;
            ShowCat(currentCatIndex);
        }

        ShowCustomer(currentCustomerIndex);
    }

    public void OnConfirmMatchButtonClicked()
    {
        if (activeCat == null || allCustomers.Count == 0 || currentCustomerIndex >= allCustomers.Count) return;

        CustomerData customer = allCustomers[currentCustomerIndex];

        string c1 = customer.preferredTrait1 != null ? customer.preferredTrait1.Trim().ToLower() : "";
        string c2 = customer.preferredTrait2 != null ? customer.preferredTrait2.Trim().ToLower() : "";
        string k1 = activeCat.trait1 != null ? activeCat.trait1.Trim().ToLower() : "";
        string k2 = activeCat.trait2 != null ? activeCat.trait2.Trim().ToLower() : "";

        bool hasC1 = !string.IsNullOrEmpty(c1);
        bool hasC2 = !string.IsNullOrEmpty(c2);

        bool trait1Matched = hasC1 && (c1 == k1 || c1 == k2);
        bool trait2Matched = hasC2 && (c2 == k1 || c2 == k2);

        if (trait1Matched || trait2Matched)
        {
            MatchSuccess();
        }
        else
        {
            MatchFail();
        }
    }

    private void MatchSuccess()
    {
        StopAllCoroutines();
        if (feedbackText != null)
        {
            feedbackText.text = "Dogru Eslestirme! Gece Devriyesi Basliyor...";
            feedbackText.color = Color.green;
        }

        currentCustomerIndex += 1;
        PlayerPrefs.SetInt("CurrentCustomerIndex", currentCustomerIndex);
        PlayerPrefs.Save();

        Invoke("LoadMinigame", 1.5f);
    }

    private void MatchFail()
    {
        currentReputation -= wrongMatchPenalty;
        UpdateReputationUI();

        StopAllCoroutines();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ShowFeedbackTemporarily("Yanlis kedi! (Un -10)", Color.red, 2f));
        }
    }

    private void UpdateReputationUI()
    {
        if (reputationText != null)
            reputationText.text = "Un: " + currentReputation;

        if (currentReputation < 0)
        {
            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GirisSahnesi");
        }
    }

    private void ShowCustomer(int index)
    {
        if (allCustomers.Count > 0 && index < allCustomers.Count && customerUI != null)
        {
            customerUI.DisplayCustomer(allCustomers[index]);
        }
    }

    private void ShowCat(int index)
    {
        if (allCats.Count > 0 && index < allCats.Count)
        {
            activeCat = allCats[index];
            if (catUI != null) catUI.DisplayCat(activeCat);
        }
    }

    IEnumerator ShowFeedbackTemporarily(string message, Color color, float delay)
    {
        if (feedbackText == null) yield break;
        feedbackText.text = message;
        feedbackText.color = color;
        yield return new WaitForSeconds(delay);
        feedbackText.text = "";
    }

    public void NextCat()
    {
        if (allCats.Count == 0) return;
        currentCatIndex = (currentCatIndex + 1) % allCats.Count;
        ShowCat(currentCatIndex);
    }

    public void PreviousCat()
    {
        if (allCats.Count == 0) return;
        currentCatIndex = (currentCatIndex - 1 + allCats.Count) % allCats.Count;
        ShowCat(currentCatIndex);
    }

    void LoadMinigame()
    {
        SceneManager.LoadScene("MiniGame_FlappyNot");
    }
}