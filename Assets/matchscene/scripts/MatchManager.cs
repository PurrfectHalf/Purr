using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public CustomerData currentCustomer;
    public List<CatData> allCats;

    public CustomerUI customerUI;
    public CatUI catUI;

    private int catIndex = 0;

    void Start()
    {
        if (currentCustomer != null) customerUI.DisplayCustomer(currentCustomer);
        UpdateCatDisplay();
    }

    public void NextCat()
    {
        if (catIndex < allCats.Count - 1)
        {
            catIndex++;
            UpdateCatDisplay();
        }
    }

    public void PreviousCat()
    {
        if (catIndex > 0)
        {
            catIndex--;
            UpdateCatDisplay();
        }
    }

    void UpdateCatDisplay()
    {
        if (catUI != null && allCats.Count > 0)
        {
            catUI.DisplayCat(allCats[catIndex]);
        }
    }

    public void ConfirmMatch()
    {
        CheckMatch(allCats[catIndex]);
    }

    void CheckMatch(CatData cat)
    {
        bool isMatch = (cat.trait1 == currentCustomer.preferredTrait1 ||
                        cat.trait1 == currentCustomer.preferredTrait2 ||
                        cat.trait2 == currentCustomer.preferredTrait1 ||
                        cat.trait2 == currentCustomer.preferredTrait2);

        if (isMatch) Debug.Log("EÞLEÞTÝ!");
        else Debug.Log("EÞLEÞMEDÝ!");

        // Eþleþme durumuna göre sahneye geçmek istersen buraya ekleyebilirsin
    }
}
