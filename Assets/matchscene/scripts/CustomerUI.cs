using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public Image fullCardImage; // Inspector'dan MusteriPaneli (Image) baðlý olmalý

    // Start fonksiyonunu ve defaultCustomer deðiþkenini sildik!
    // Artýk patron MatchManager.

    public void DisplayCustomer(CustomerData data)
    {
        if (data == null) return;

        // Kartýn görselini MatchManager'dan gelen veriye göre günceller
        if (fullCardImage != null)
        {
            fullCardImage.sprite = data.fullCardSprite;
        }
    }
}