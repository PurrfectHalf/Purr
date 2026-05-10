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

    [Header("Ün Puaný Ayarlarý")]
    public int currentReputation = 10;
    private const int wrongMatchPenalty = 10;

    [Header("Veri Listeleri")]
    public List<CatData> allCats;
    public CustomerData currentCustomer;

    private int currentCatIndex = 0;
    private CatData activeCat;
    private CatUI catUI;

    void Start()
    {
        catUI = Object.FindFirstObjectByType<CatUI>();
        UpdateReputationUI();
        if (feedbackText != null) feedbackText.text = "";

        if (allCats != null && allCats.Count > 0)
        {
            ShowCat(0);
        }
    }

    public void NextCat() { currentCatIndex = (currentCatIndex + 1) % allCats.Count; ShowCat(currentCatIndex); }
    public void PreviousCat() { currentCatIndex = (currentCatIndex - 1 + allCats.Count) % allCats.Count; ShowCat(currentCatIndex); }

    private void ShowCat(int index)
    {
        activeCat = allCats[index];
        if (catUI != null) catUI.DisplayCat(activeCat);
    }

    public void OnConfirmMatchButtonClicked()
    {
        if (activeCat == null || currentCustomer == null) return;

        // Birebir eþleþme için Trim() (boþluk temizleme) ve ToLower() (küçük harf yapma) kullanýyoruz
        string c1 = currentCustomer.preferredTrait1.Trim().ToLower();
        string c2 = currentCustomer.preferredTrait2.Trim().ToLower();
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
        StopAllCoroutines(); // Eski mesajlarý temizle
        feedbackText.text = "Doðru Eþleþme!";
        feedbackText.color = Color.green;
        PlayerPrefs.SetInt("SavedReputation", currentReputation);
        Invoke("LoadMinigame", 1.0f);
    }

    private void MatchFail()
    {
        currentReputation -= wrongMatchPenalty;
        UpdateReputationUI();

        // Yanlýþ mesajýný gösterip silecek olan Coroutine'i baþlatýyoruz
        StopAllCoroutines();
        StartCoroutine(ShowFeedbackTemporarily("Yanlýþ kedi! (Ün -10)", Color.red, 2f));
    }

    // Yazýyý gösterip belirli bir süre sonra silen fonksiyon
    IEnumerator ShowFeedbackTemporarily(string message, Color color, float delay)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        yield return new WaitForSeconds(delay);
        feedbackText.text = "";
    }

    private void UpdateReputationUI()
    {
        if (reputationText != null)
            reputationText.text = "Ün: " + currentReputation;
    }

    void LoadMinigame()
    {
        SceneManager.LoadScene("MiniGame_FlappyNot");
    }
}