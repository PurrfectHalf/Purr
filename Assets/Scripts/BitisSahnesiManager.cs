using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BitisSahnesiManager : MonoBehaviour
{
    [Header("TextMeshPro kullanýyorsan buraya sürükle")]
    public TextMeshProUGUI sonucTMPText;

    [Header("Normal UI Text kullanýyorsan buraya sürükle")]
    public Text sonucNormalText;

    void Start()
    {
        Time.timeScale = 1f;

        int finalScore = GameStateManager.GetFinalScore();

        string sonucMesaji = finalScore + " puan kazandiniz!";

        if (sonucTMPText != null)
        {
            sonucTMPText.text = sonucMesaji;
        }

        if (sonucNormalText != null)
        {
            sonucNormalText.text = sonucMesaji;
        }

        Debug.Log("BitisSahnesi puan yazisi: " + sonucMesaji);
    }

    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f;

        GameStateManager.ResetGame();

        GameStateManager.GoToStartMenu();
    }
}