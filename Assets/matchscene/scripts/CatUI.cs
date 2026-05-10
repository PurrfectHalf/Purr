using UnityEngine;
using UnityEngine.UI;

public class CatUI : MonoBehaviour
{
    [Header("Görsel Elemanlar")]
    public Image catImageDisplay;

    // MatchManager artýk kedileri kendi içinde yönettiði için 
    // burada manager referansýna veya UpdateActiveCat çaðrýsýna gerek yok.

    public void DisplayCat(CatData data)
    {
        if (data == null) return;

        // Sadece görseli güncelliyoruz
        catImageDisplay.sprite = data.fullCatCardSprite;
    }
}