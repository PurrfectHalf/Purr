using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    // Sahnendeki o tek hazýr kart Image objesi
    public Image fullCardImage;

    public void DisplayCustomer(CustomerData data)
    {
        if (data == null) return;

        // Sadece görseli deðiþtiriyoruz, çünkü her þey üstünde yazýyor!
        fullCardImage.sprite = data.fullCardSprite;
    }
}