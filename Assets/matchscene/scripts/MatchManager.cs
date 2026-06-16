using UnityEngine;
using TMPro;
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

        if (allCustomers.Count > 0)
        {
            GameStateManager.SetTotalCustomerCount(allCustomers.Count);
        }

        currentCustomerIndex = GameStateManager.GetCurrentCustomerIndex();
        currentReputation = GameStateManager.GetReputation();

        if (currentReputation < 0)
        {
            GameStateManager.GoToGameOver();
            return;
        }

        UpdateReputationUI();

        if (allCustomers.Count == 0)
        {
            Debug.LogWarning("Musteri listesi bos!");
            return;
        }

        if (currentCustomerIndex >= allCustomers.Count)
        {
            GameStateManager.SaveFinalScore();
            GameStateManager.GoToFinishScene();
            return;
        }

        if (allCats.Count > 0)
        {
            currentCatIndex = 0;
            ShowCat(currentCatIndex);
        }

        ShowCustomer(currentCustomerIndex);
    }

    public void OnConfirmMatchButtonClicked()
    {
        if (activeCat == null || allCustomers.Count == 0 || currentCustomerIndex >= allCustomers.Count)
        {
            return;
        }

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
            feedbackText.text = "Dogru eslestirme! Mini oyun basliyor...";
            feedbackText.color = Color.green;
        }

        Invoke(nameof(LoadMinigame), 1.5f);
    }

    private void MatchFail()
    {
        bool gameEnded = GameStateManager.AddReputation(-GameStateManager.WrongMatchPenalty);

        currentReputation = GameStateManager.GetReputation();
        UpdateReputationUI();

        if (gameEnded)
        {
            return;
        }

        StopAllCoroutines();

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ShowWrongMatchFeedback());
        }
    }

    IEnumerator ShowWrongMatchFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "Yanlis kedi! Un -10. Tekrar dene.";
            feedbackText.color = Color.red;
        }

        yield return new WaitForSeconds(1.5f);

        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    private void UpdateReputationUI()
    {
        if (reputationText != null)
        {
            reputationText.text = "Un: " + currentReputation;
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

            if (catUI != null)
            {
                catUI.DisplayCat(activeCat);
            }
        }
    }

    public void NextCat()
    {
        if (allCats.Count == 0)
        {
            return;
        }

        currentCatIndex = (currentCatIndex + 1) % allCats.Count;
        ShowCat(currentCatIndex);
    }

    public void PreviousCat()
    {
        if (allCats.Count == 0)
        {
            return;
        }

        currentCatIndex = (currentCatIndex - 1 + allCats.Count) % allCats.Count;
        ShowCat(currentCatIndex);
    }

    void LoadMinigame()
    {
        GameStateManager.GoToMiniGame();
    }
}