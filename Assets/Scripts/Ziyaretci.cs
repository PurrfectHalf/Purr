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

    private bool ulastiMi = false;
    private Animator anim;
    private ZiyaretciYurume yurusScripti;

    void Start()
    {
        anim = GetComponent<Animator>();
        yurusScripti = GetComponent<ZiyaretciYurume>();

        // Temizlik
        if (unlemObjesi) unlemObjesi.SetActive(false);
        if (baloncukObjesi) baloncukObjesi.SetActive(false);
        if (matchButonu) matchButonu.SetActive(false);
        if (diyalogText) diyalogText.text = "";

        // OYUN BÝTÝÞ KONTROLÜ:
        // Eðer oyuncu mini oyunu kazanýp buraya döndüyse ve 7 müþteriyi de geçtiyse oyunu sýfýrlayýp ana menüye atmalýyýz.
        // MatchManager'daki toplam müþteri sayýsýný elinle kontrol etmek yerine (örn: 7), buraya sýnýr koyuyoruz:
        int savedCustomerIndex = PlayerPrefs.GetInt("CurrentCustomerIndex", 0);

        // EÐER 7 MÜÞTERÝ DE BÝTTÝYSE (Ýndeks 7'ye ulaþtýysa)
        if (savedCustomerIndex >= 7)
        {
            Debug.Log("Tüm müsteriler sahiplendirildi! Oyun Basariyla Tamamlandi.");
            // Verileri tamamen sýfýrla ki bir sonraki oynayýþta baþtan baþlasýn
            PlayerPrefs.SetInt("CurrentCustomerIndex", 0);
            PlayerPrefs.SetInt("SavedReputation", 10);
            PlayerPrefs.Save();

            // Oyuncuyu ana giriþ ekranýna gönder
            SceneManager.LoadScene("GirisSahnesi");
            return; // Altýndaki yürüme kodlarýný çalýþtýrma
        }

        // Yürüyüþü Baþlat
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
        // Gitmek istediðin eþleþtirme sahnesinin adý tam olarak neyse onu yükle.
        // MatchManager o sahnede olduðu için hafýzadaki yeni indeksli müþteriyi otomatik getirecek!
        SceneManager.LoadScene("MatchScene");
    }
}