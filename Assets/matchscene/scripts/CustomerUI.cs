using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public Image fullCardImage; // Sahnendeki o tek hazýr kart Image objesi

    [Header("Veri Ayarlarý")]
    public CustomerData defaultCustomer; // Inspector'dan Aslý'nýn verisini buraya sürükle

    private void Start()
    {
        // Oyun baþladýðýnda eðer bir müþteri verisi atanmýþsa onu göster
        if (defaultCustomer != null)
        {
            DisplayCustomer(defaultCustomer);
        }
    }

    public void DisplayCustomer(CustomerData data)
    {
        if (data == null) return;

        // Kartýn görselini deðiþtiriyoruz
        fullCardImage.sprite = data.fullCardSprite;
    }
}