using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için bu þart!

public class MenuManager : MonoBehaviour
{
    public void AnaGiriseGit()
    {
        // "GirisSahnesi" yazan yere kendi ana giriþ sahninin TAM adýný yazmalýsýn
        SceneManager.LoadScene("GirisSahnesi");
    }
}