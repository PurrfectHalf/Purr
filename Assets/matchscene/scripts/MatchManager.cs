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

        // Hafizadaki degerleri yukle
        currentCustomerIndex = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);
        currentReputation = PlayerPrefs.GetInt("SavedReputation", 10);

        UpdateReputationUI();

        if (allCats.Count > 0) ShowCat(0);

        // Liste sinir kontrolu
        if (currentCustomerIndex >= allCustomers.Count)
        {
            currentCustomerIndex = 0;
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();
        }

        ShowCustomer(currentCustomerIndex);
    }

    public void OnConfirmMatchButtonClicked()
    {
        if (activeCat == null || allCustomers.Count == 0) return;

        CustomerData customer = allCustomers[currentCustomerIndex];

        // Eslesme kontrolu (Kucuk harf ve bosluk temizleme ile)
        string c1 = customer.preferredTrait1.Trim().ToLower();
        string c2 = customer.preferredTrait2.Trim().ToLower();
        string k1 = activeCat.trait1.Trim().ToLower();
        string k2 = activeCat.trait2.Trim().ToLower();

        // Iki ozelligin de uymasi icin && (VE) olarak guncellendi
        bool trait1Matched = (c1 == k1 || c1 == k2);
        bool trait2Matched = (c2 == k1 || c2 == k2);

        if (trait1Matched && trait2Matched)
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
            feedbackText.text = "Dogru Eslestirme! Mini oyun yukleniyor...";
            feedbackText.color = Color.green;
        }

        // Un puanini artir ve kaydet
        currentReputation += 20;
        PlayerPrefs.SetInt("SavedReputation", currentReputation);

        // Bir sonraki sahne donusunde siradaki musterinin gelmesi icin indeksi artir
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
        StartCoroutine(ShowFeedbackTemporarily("Yanlis kedi! (Un -10)", Color.red, 2f));
    }

    private void UpdateReputationUI()
    {
        if (reputationText != null)
            reputationText.text = "Un: " + currentReputation;

        // Un puani negatif olursa oyunu sifirla ve giris sahnesine don
        if (currentReputation < 0)
        {
            Debug.Log("Un bitti! GirisSahnesine donuluyor...");

            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GirisSahnesi");
        }
    }

    private void ShowCustomer(int index)
    {
        if (allCustomers.Count > 0 && index < allCustomers.Count)
        {
            customerUI.DisplayCustomer(allCustomers[index]);
        }
    }

    private void ShowCat(int index)
    {
        if (allCats.Count > 0)
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