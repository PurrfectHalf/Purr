using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public CustomerData currentCustomer;
    public List<CatData> allCats;
    public CustomerUI customerUI;
    public CatUI catUI;

    [Header("Arkaplan Puan Ayarlarý")]
    public TextMeshProUGUI mesajYazisi; // "Yanlýþ Eþleþtirme" uyarýsý için
    public int unPuani = 10;             // Ün puaný 10 ile baþlýyor

    private int catIndex = 0;
    private bool isGameOver = false;

    void Start()
    {
        if (currentCustomer != null) customerUI.DisplayCustomer(currentCustomer);
        UpdateCatDisplay();

        if (mesajYazisi != null) mesajYazisi.text = "";
    }

    // Ok butonlarý için fonksiyonlar
    public void NextCat() { if (!isGameOver && catIndex < allCats.Count - 1) { catIndex++; UpdateCatDisplay(); } }
    public void PreviousCat() { if (!isGameOver && catIndex > 0) { catIndex--; UpdateCatDisplay(); } }

    void UpdateCatDisplay()
    {
        if (catUI != null && allCats.Count > 0) catUI.DisplayCat(allCats[catIndex]);
    }

    public void ConfirmMatch()
    {
        if (isGameOver) return; // Oyun bittiyse týklamayý engelle
        CheckMatch(allCats[catIndex]);
    }

    void CheckMatch(CatData cat)
    {
        // Eþleþme mantýðý kontrolü
        bool isMatch = (cat.trait1 == currentCustomer.preferredTrait1 ||
                        cat.trait1 == currentCustomer.preferredTrait2 ||
                        cat.trait2 == currentCustomer.preferredTrait1 ||
                        cat.trait2 == currentCustomer.preferredTrait2);

        if (isMatch)
        {
            ShowMessage("Doðru! Mini oyuna geçiliyor...");
            // Buraya arkadaþýnýn mini oyun sahne geçiþi eklenecek
        }
        else
        {
            HandleWrongMatch();
        }
    }

    void HandleWrongMatch()
    {
        unPuani -= 10;

        if (unPuani < 0)
        {
            isGameOver = true;
            ShowMessage("Ün Tükendi! Oyun Bitti.");
            Invoke("RestartGame", 3.0f); // Oyun bittiðinde yazý 3 saniye kalsýn sonra resetlesin
        }
        else
        {
            ShowMessage("Yanlýþ Eþleþtirme! Tekrar Dene."); // Normal hata 2 saniye görünür
        }
    }

    void ShowMessage(string mesaj)
    {
        if (mesajYazisi != null)
        {
            mesajYazisi.text = mesaj;
            CancelInvoke("ClearMessage"); // Eðer oyuncu çok hýzlý basarsa önceki sayacý iptal et
            Invoke("ClearMessage", 2.0f); // 2 saniye sonra sil
        }
    }

    void ClearMessage()
    {
        if (mesajYazisi != null) mesajYazisi.text = "";
    }
}