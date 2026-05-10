using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuManager : MonoBehaviour
{
    public void AnaGiriseGit()
    {
        // "GirisSahnesi" yazan yere kendi ana giriþ sahninin TAM adýný yazmalýsýn
        SceneManager.LoadScene("GirisSahnesi");
    }
}