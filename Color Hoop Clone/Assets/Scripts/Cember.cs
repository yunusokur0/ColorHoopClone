using UnityEngine;

public class Cember : MonoBehaviour
{
    public GameObject AitOlduguStand;
    public GameObject AitOlduguCemberSkoti;
    public bool HareketEdebilirmi;
    public string color;
    public GameManager gameManager;
    GameObject HareketPozisyonu;
    GameObject aitOlduguStand;
    GameObject GidecegiStand;
    bool Secildi, Posdegistir, Soketeotut, SoketeGeriGit;

    public void HareketEt(string islem, GameObject Stand = null, GameObject Soket = null, GameObject GidilecekObje = null)
    {
        switch (islem)
        {
            case "secim":
                HareketPozisyonu = GidilecekObje;
                Secildi = true;
                break;
            case "pozisyonDegistir":
                GidecegiStand = Stand;
                AitOlduguCemberSkoti = Soket;
                HareketPozisyonu = GidilecekObje;
                Posdegistir = true;
                break;

            case "soketeGeriGit":
                SoketeGeriGit = true;
                break;
        }
    }

    void Update()
    {
        if (Secildi)
        {
            //objenin mevcut pozisyonunu, hedef pozisyona yavaþça yaklaþtýrarak geçiþ hareketi yapar ve objenin hareket etmesini saðlar
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .1f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)
            {
                Secildi = false;
            }
        }

        if (Posdegistir)
        {
            //tikladigim standa gider
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .1f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)
            {
                Posdegistir = false;
                Soketeotut = true;
            }
        }

        if (Soketeotut)
        {
            transform.position = Vector3.Lerp(transform.position, AitOlduguCemberSkoti.transform.position, .1f);
            if (Vector3.Distance(transform.position, AitOlduguCemberSkoti.transform.position) < .10)
            {
                transform.position = AitOlduguCemberSkoti.transform.position;
                Soketeotut = false;
                AitOlduguStand = GidecegiStand;

                if (AitOlduguStand.GetComponent<Stand>().Cemberler.Count > 1)
                {
                    AitOlduguStand.GetComponent<Stand>().Cemberler[^2].GetComponent<Cember>().HareketEdebilirmi = false;
                }
                gameManager.HareketVar = false;
            }
        }

        if (SoketeGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, AitOlduguCemberSkoti.transform.position, .1f);
            if (Vector3.Distance(transform.position, AitOlduguCemberSkoti.transform.position) < .10)
            {
                transform.position = AitOlduguCemberSkoti.transform.position;
                SoketeGeriGit = false;
                gameManager.HareketVar = false;
            }
        }
    }
}
