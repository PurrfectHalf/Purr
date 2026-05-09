using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Ziyaretci : MonoBehaviour
{
    [Header("Diyalog Ayarlari")]
    public string[] diyaloglar = {
        "Selam! Tatli bir kedi ariyorum.",
        "Merhaba! Bir dost edinmek istiyorum.",
        "Ruh kedimi burada bulacagima eminim!",
        "Selam! Evime nese katacak bir pati lazim.",
        "Merhaba! Hayatima biraz tüy ve pati ariyorum.",
        "Iyi gunler! Kalbimi calacak o kediyi ariyorum.",
        "Selam! Minik bir kaplan sahiplenmeye geldim.",
        "Merhaba! Birlikte uyuyabilecegim bir dost ariyorum.",
        "Selam! Evimin yeni patronuyla tanismaya geldim.",
        "Merhaba! En yaramaz kedinizle tanismak istiyorum.",
        "Selam! Evdeki saksilari devirecek bir suç ortagi ariyorum.",
        "Merhaba! Bilgisayarimin klavyesine oturacak o özel kediyi ariyorum.",
        "Iyi gunler! Geceleri evde kosturup hayalet avlayacak bir dost lazim.",
        "Selam! Koltuklarimi tirmalamasi icin en tatli adayiniza talibim.",
        "Merhaba! Hayatima biraz tatli bir kaos ve bolca mirlama ariyorum.",
        "Selam... Sabah 5'te beni miyavlayarak uyandiracak o yüzsüzü ariyorum.",
        "Merhaba! Bardaklari masadan asagi itecek cesur bir pati lazim."
    };

    [Header("UI Objeleri (Hierarchy'den Surukle)")]
    public GameObject unlemObjesi;
    public GameObject baloncukObjesi;
    public TextMeshProUGUI diyalogText;
    public GameObject matchButonu;

    [Header("Yuruyus Ayarlari")]
    public Transform hedefResepsiyon;
    public string yeniSahneAdi = "EslestirmeEkrani";

    private bool ulastiMi = false;
    private Animator anim;
    private ZiyaretciYurume yurusScripti;

    void Start()
    {
        // ÖNEMLÝ: Kodun Animator'ý bulmasý için en garanti yol
        anim = GetComponent<Animator>();
        yurusScripti = GetComponent<ZiyaretciYurume>();

        // Oyun baþý temizlik: Her þeyi kapat
        if (unlemObjesi) unlemObjesi.SetActive(false);
        if (baloncukObjesi) baloncukObjesi.SetActive(false);
        if (matchButonu) matchButonu.SetActive(false);
        if (diyalogText) diyalogText.text = "";

        // Yürüyüþü Baþlat
        if (yurusScripti != null && hedefResepsiyon != null)
        {
            yurusScripti.HedefeGit(hedefResepsiyon.position);

            // Eðer Animator bulunduysa yürüme animasyonunu aç
            if (anim != null)
            {
                anim.SetBool("isWalking", true);
                Debug.Log("Animator bulundu, yürüme animasyonu tetiklendi.");
            }
            else
            {
                Debug.LogError("HATA: 'Adam' üzerinde Animator bileþeni bulunamadi!");
            }
        }
    }

    void Update()
    {
        // Hedefe ulaþýp ulaþmadýðýný kontrol et
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

        // Durunca yürüme animasyonunu kapat (isWalking = false)
        if (anim != null) anim.SetBool("isWalking", false);

        // Kafasýnda ünlemi yak
        if (unlemObjesi) unlemObjesi.SetActive(true);
    }

    // KARAKTERÝN GÖVDESÝNE TIKLANINCA (OnMouseDown çalýþmasý için BoxCollider2D þart)
    private void OnMouseDown()
    {
        // Sadece hedefe vardýysa ve ünlem yanýyorken týklanabilsin
        if (ulastiMi && unlemObjesi != null && unlemObjesi.activeSelf)
        {
            KonusmayiAc();
        }
    }

    void KonusmayiAc()
    {
        unlemObjesi.SetActive(false); // Ünlem gitsin
        baloncukObjesi.SetActive(true); // Boþ baloncuk image'ý gelsin
        matchButonu.SetActive(true); // Sahne geçiþ butonu gelsin

        // Rastgele metin seçimi
        string secilenMetin = diyaloglar[Random.Range(0, diyaloglar.Length)];
        diyalogText.text = secilenMetin;

        Debug.Log("Konuþma açýldý, metin: " + secilenMetin);
    }

    public void SahneyeGec()
    {
        SceneManager.LoadScene("MatchScene");
    }
}