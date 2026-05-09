using UnityEngine;
using System.Collections.Generic;

public class SiraYonetici : MonoBehaviour
{
    public List<Transform> noktalar; // Sahnede belirlediðin noktalar

    public void SiraBirAdimKaydir()
    {
        // Buraya daha sonra sýradaki diðer kiþileri öne kaydýran kod gelecek
        Debug.Log("Sýra bir adým kaydý!");
    }
}