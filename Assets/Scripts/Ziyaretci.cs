using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Ziyaretci : MonoBehaviour
{
    [Header("Diyalog Ayarlari")]
    public string[] diyaloglar = {
        "Selam! Tatlý bir kedi arýyorum.",
        "Merhaba! Bir dost edinmek istiyorum.",
        "Ruh kedimi burada bulacaðýma eminim!",
        "Selam! Evime neþe katacak bir pati lazým.",
        "Merhaba! Hayatýma biraz tüy ve pati arýyorum.",
        "Ýyi günler! Kalbimi çalacak o kediyi arýyorum.",
        "Selam! Minik bir kaplan sahiplenmeye geldim.",
        "Merhaba! Birlikte uyuyabileceðim bir dost arýyorum.",
        "Selam! Evimin yeni patronuyla tanýþmaya geldim.",
        "Merhaba! En yaramaz kedinizle tanýþmak istiyorum.",
        "Selam! Evdeki saksýlarý devirecek bir suç ortaðý arýyorum.",
        "Merhaba! Bilgisayarýmýn klavyesine oturacak o özel kediyi arýyorum.",
        "Ýyi günler! Geceleri evde koþturup hayalet avlayacak bir dost lazým.",
        "Selam! Koltuklarýmý týrmalamasý için en tatlý adayýnýza talibim.",
        "Merhaba! Hayatýma biraz tatlý bir kaos ve bolca mýrlama arýyorum.",
        "Selam... Sabah 5'te beni miyavlayarak uyandýracak o yüzsüzü arýyorum.",
        "Merhaba! Bardaklarý masadan aþaðý itecek cesur bir pati lazým."
        };

    [Header("UI Objeleri (Hierarchy'den Surukle)")]
    public GameObject unlemObjesi;
    public GameObject baloncukObjesi;
    public TextMeshProUGUI diyalogText;
    public GameObject matchButonu;

    [Header("Yuruyus Ayarlari")]
    public Transform hedefResepsiyon;

    private bool ulastiMi = false;
    private Animator anim;
    private ZiyaretciYurume yurusScripti;

    void Start()
    {
        anim = GetComponent<Animator>();
        yurusScripti = GetComponent<ZiyaretciYurume>();

        if (unlemObjesi) unlemObjesi.SetActive(false);
        if (baloncukObjesi) baloncukObjesi.SetActive(false);
        if (matchButonu) matchButonu.SetActive(false);
        if (diyalogText) diyalogText.text = "";

        int savedCustomerIndex = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);

        if (savedCustomerIndex >= 7)
        {
            Debug.Log("Tüm müsteriler sahiplendirildi! Oyun Basariyla Tamamlandi.");
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GirisSahnesi");
            return;
        }

        if (yurusScripti != null && hedefResepsiyon != null)
        {
            yurusScripti.HedefeGit(hedefResepsiyon.position);

            if (anim != null)
            {
                anim.SetBool("isWalking", true);
            }
        }
    }

    void Update()
    {
        if (!ulastiMi && hedefResepsiyon != null)
        {
            if (Vector3.Distance(transform.position, hedefResepsiyon.position) < 0.2f)
            {
                DurVeUnlemGoster();
            }
        }
    }

    void DurVeUnlemGoster()
    {
        ulastiMi = true;
        if (anim != null) anim.SetBool("isWalking", false);
        if (unlemObjesi) unlemObjesi.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (ulastiMi && unlemObjesi != null && unlemObjesi.activeSelf)
        {
            KonusmayiAc();
        }
    }

    void KonusmayiAc()
    {
        if (unlemObjesi) unlemObjesi.SetActive(false);
        if (baloncukObjesi) baloncukObjesi.SetActive(true);
        if (matchButonu) matchButonu.SetActive(true);

        string secilenMetin = diyaloglar[Random.Range(0, diyaloglar.Length)];
        if (diyalogText != null) diyalogText.text = secilenMetin;
    }

    // "Match" butonuna basýnca tetiklenecek fonksiyon
    public void SahneyeGec()
    {
        // --- SES EKLEMESÝ: BUTONA TIKLANDIÐI AN SES ÇALACAK ---
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.okButonuSesi); // Abstract2 çalacak
        }

        SceneManager.LoadScene("MatchScene");
    }
}