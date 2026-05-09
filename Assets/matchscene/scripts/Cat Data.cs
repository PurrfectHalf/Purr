using UnityEngine;

[CreateAssetMenu(fileName = "NewCat", menuName = "CatShelter/Cat Data")]
public class CatData : ScriptableObject
{
    public string catName;
    public Sprite catThumbnail; // Gridi (kataloðu) dizerken görünecek küçük resim

    [Header("Kedi Özellikleri")]
    public string trait1; // Örn: "Uysal"
    public string trait2; // Örn: "Tüylü"

    // Eðer kedinin de detaylý bir kartý varsa:
    public Sprite fullCatCardSprite;
}