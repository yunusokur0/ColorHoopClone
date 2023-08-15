using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject HareketPozisyonu;
    public GameObject[] Soketler;
    public int BosOlanSoket;
    public List<GameObject> Cemberler = new();
    [SerializeField] private GameManager gameManager;
    int CemberTamamlamaSayisi;

    public GameObject EnUstekiCemberiVer()
    {
        return Cemberler[^1]; // cemberlerin en sonuncu elamanýný dönder
    }

    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket]; // cemberlerin en sonuncu elamanýný dönder
    }
    public void SoketDegistirmeÝslemleri(GameObject silinecekObje)
    {
        Cemberler.Remove(silinecekObje);  // silinecek obje gameManagerde seçilenobjedir

        //Cemberler listesinin eleman sayýsý 0'dan farklýysa (yani listede en az bir eleman varsa)
        if (Cemberler.Count != 0)
        {
            BosOlanSoket--;
            //Cemberler listesinin son elemanýnýn Cember component'ýna eriþilerek HareketEdebilirmi özelliði true olarak degistir
            Cemberler[^1].GetComponent<Cember>().HareketEdebilirmi = true;
        }

        else
        {
            BosOlanSoket = 0;
        }
    }
    public void CemberleriKontrolEt()
    {
        //listesi 4 elemana sahipse (yani her bir tribünde bir daire varsa)
        if (Cemberler.Count == 4)
        {
            //listesinin ilk elemanýndan rengi alýnýr
            string renk = Cemberler[0].GetComponent<Cember>().color;
            //Cemberler listesindeki her bir eleman için aþaðýdaki adýmlar tekrarlanýr
            foreach (var item in Cemberler)
            {
                //Eðer elemanýn rengi, ilk elemanýn rengine esitse
                if (renk == item.GetComponent<Cember>().color)
                    // Tamamlanan çember sayýsý bir artýrýlýr
                    CemberTamamlamaSayisi++;
            }
            //ðer tamamlanan çember sayýsý 4 ise (yani tüm çemberler ayný renkte ise)
            if (CemberTamamlamaSayisi == 4)
            {
                //GameManager sýnýfýndaki StandTamamlandi() fonksiyonu cagirir bu fonksiyon oyunun devam etmesi veya bittigini kontrol eder
                gameManager.StandTamamlandi();
                //tamamlanmisStand() fonksiyonu çaðrýlýr Bu fonksiyon, tamamlanan tribünün rengini kapatýr ve tribünün üzerindeki tüm daireleri hareketsiz hale getirir
                tamamlanmisStand();
            }
            //  Eðer tüm çemberler ayný renkte deðilse
            else
            {
                CemberTamamlamaSayisi = 0; // Tamamlanan çember sayýsý sýfýrlanýr
            }
        }
    }
    public void tamamlanmisStand()
    {
        // _Cemberler listesi içindeki her bir Cember objesi için aþaðýdaki iþlemleri yapar
        foreach (var item in Cemberler)
        {
            //Cember'in HareketEdebilirmi deðiþkenini false yapar Bu oyuncunun Cemberi taþýmak veya hareket ettirmek için artýk kullanamayacaðý anlamýna gelir
            item.GetComponent<Cember>().HareketEdebilirmi = false;
            //Cember'in mevcut rengini alýr
            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            //Alýnan rengin alph degerini 100 olarak ayarlar. Bu, Cember'in rengini biraz daha matlaþtýrý
            color.a = 100;
            //Cember'in materyal rengini, deðiþtirilmis renk ile günceller
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            //Standýn tag deðerini tamamlanmis olarak deðiþtirir Bu diðer nesnelerin Standý tamamlanmis olarak algýlamasýna yardýmci olur
            gameObject.tag = "tamamlanmis";
        }
    }
}
