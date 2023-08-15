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
        return Cemberler[^1]; // cemberlerin en sonuncu elamanını dönder
    }

    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket]; // cemberlerin en sonuncu elamanını dönder
    }
    public void SoketDegistirmeİslemleri(GameObject silinecekObje)
    {
        Cemberler.Remove(silinecekObje);  

        //Cemberler listesinin eleman sayısı 0'dan farklıysa (yani listede en az bir eleman varsa)
        if (Cemberler.Count != 0)
        {
            BosOlanSoket--;
            //Cemberler listesinin son elemanının Cember component'ına erişilerek HareketEdebilirmi özelliği true olarak degistir
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
            //listesinin ilk elemanından rengi alınır
            string renk = Cemberler[0].GetComponent<Cember>().color;
            //Cemberler listesindeki her bir eleman için aşağıdaki adımlar tekrarlanır
            foreach (var item in Cemberler)
            {
                //Eğer elemanın rengi, ilk elemanın rengine esitse
                if (renk == item.GetComponent<Cember>().color)
                    // Tamamlanan çember sayısı bir artırılır
                    CemberTamamlamaSayisi++;
            }
            //ğer tamamlanan çember sayısı 4 ise (yani tüm çemberler aynı renkte ise)
            if (CemberTamamlamaSayisi == 4)
            {
                //GameManager sınıfındaki StandTamamlandi() fonksiyonu cagirir bu fonksiyon oyunun devam etmesi veya bittigini kontrol eder
                gameManager.StandTamamlandi();
                //tamamlanmisStand() fonksiyonu çağrılır Bu fonksiyon, tamamlanan tribünün rengini kapatır ve tribünün üzerindeki tüm daireleri hareketsiz hale getirir
                tamamlanmisStand();
            }
            //  Eğer tüm çemberler aynı renkte değilse
            else
            {
                CemberTamamlamaSayisi = 0; // Tamamlanan çember sayısı sıfırlanır
            }
        }
    }
    public void tamamlanmisStand()
    {
        // _Cemberler listesi içindeki her bir Cember objesi için aşağıdaki işlemleri yapar
        foreach (var item in Cemberler)
        {
            //Cember'in HareketEdebilirmi değişkenini false yapar Bu oyuncunun Cemberi taşımak veya hareket ettirmek için artık kullanamayacağı anlamına gelir
            item.GetComponent<Cember>().HareketEdebilirmi = false;
            //Cember'in mevcut rengini alır
            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            //Alınan rengin alph degerini 100 olarak ayarlar. Bu, Cember'in rengini biraz daha matlaştırı
            color.a = 100;
            //Cember'in materyal rengini, değiştirilmis renk ile günceller
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            //Standın tag değerini tamamlanmis olarak değiştirir Bu diğer nesnelerin Standı tamamlanmis olarak algılamasına yardımci olur
            gameObject.tag = "tamamlanmis";
        }
    }
}
