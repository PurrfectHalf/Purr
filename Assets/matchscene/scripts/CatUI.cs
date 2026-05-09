using UnityEngine;            // MonoBehaviour ve Header için
using UnityEngine.UI;         // Image ve Text bileþenleri için
using System.Collections.Generic; // List<> yapýsý için

public class CatUI : MonoBehaviour
{
    public Image catImageDisplay; // Hazýr kartýn görüneceði Image bileþeni

    public void DisplayCat(CatData data)
    {
        if (data == null) return;

        // Kedinin tüm bilgilerinin üzerinde olduðu görseli buraya basýyoruz
        catImageDisplay.sprite = data.fullCatCardSprite;
    }
}