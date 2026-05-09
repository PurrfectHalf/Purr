using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomer", menuName = "CatShelter/Customer Data")]
public class CustomerData : ScriptableObject
{
    [Header("Görsel Tasarým")]
    public Sprite fullCardSprite; // Üzerinde her þey yazan hazýr kartýn

    [Header("Eþleþtirme Kriterleri (Arka Plan)")]
    // Kedinin CatData'sýndaki özelliklerle birebir ayný kelimeleri kullanmalýsýn
    public string preferredTrait1; // Örn: "Uysal"
    public string preferredTrait2; // Örn: "Oyuncu"

    // Opsiyonel: Kodla bir þeyi kontrol etmek istersen isim yine de dursun
    public string customerName;
}