using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI reputationText;
    public CustomerUI customerUI;

    [Header("Ün Puaný Ayarlarý")]
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

        // Hafýzadaki deðerleri yükle
        currentCustomerIndex = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);
        currentReputation = PlayerPrefs.GetInt("SavedReputation", 10);

        UpdateReputationUI();

        if (allCats.Count > 0) ShowCat(0);

        // Liste sýnýr kontrolü
        if (currentCustomerIndex >= allCustomers.Count)
        {
            currentCustomerIndex = 0;
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
        }

        ShowCustomer(currentCustomerIndex);
    }

    public void OnConfirmMatchButtonClicked()
    {
        if (activeCat == null || allCustomers.Count == 0) return;

        CustomerData customer = allCustomers[currentCustomerIndex];

        // Eþleþme kontrolü (Küçük harf ve boþluk temizleme ile)
        string c1 = customer.preferredTrait1.Trim().ToLower();
        string c2 = customer.preferredTrait2.Trim().ToLower();
        string k1 = activeCat.trait1.Trim().ToLower();
        string k2 = activeCat.trait2.Trim().ToLower();

        bool trait1Matched = (c1 == k1 || c1 == k2);
        bool trait2Matched = (c2 == k1 || c2 == k2);

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
        feedbackText.text = "Doðru Eþleþtirme! Mini oyun yükleniyor...";
        feedbackText.color = Color.green;

        currentReputation += 20;
        PlayerPrefs.SetInt("SavedReputation", currentReputation);
        PlayerPrefs.SetInt("CurrentCustomerIndex", currentCustomerIndex + 1);
        PlayerPrefs.Save();

        Invoke("LoadMinigame", 1.5f);
    }

    private void MatchFail()
    {
        currentReputation -= wrongMatchPenalty;
        UpdateReputationUI(); // Sahne geçiþ kontrolü burada yapýlýyor

        StopAllCoroutines();
        StartCoroutine(ShowFeedbackTemporarily("Yanlýþ kedi! (Ün -10)", Color.red, 2f));
    }

    private void UpdateReputationUI()
    {
        if (reputationText != null)
            reputationText.text = "Ün: " + currentReputation;

        // Ün puaný negatife düþtüyse direkt ana menüye (GirisSahnesi) dön
        if (currentReputation < 0)
        {
            Debug.Log("Ün bitti! GirisSahnesine dönülüyor...");

            // Oyunu sýfýrla
            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.Save();

            // DÝKKAT: Build Settings'te 'GirisSahnesi' isminin doðru olduðundan emin ol
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

    public void NextCat() { currentCatIndex = (currentCatIndex + 1) % allCats.Count; ShowCat(currentCatIndex); }
    public void PreviousCat() { currentCatIndex = (currentCatIndex - 1 + allCats.Count) % allCats.Count; ShowCat(currentCatIndex); }
    void LoadMinigame() { SceneManager.LoadScene("MiniGame_FlappyNot"); }
}