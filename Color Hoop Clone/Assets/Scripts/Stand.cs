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
        return Cemberler[^1]; // cemberlerin en sonuncu elaman�n� d�nder
    }

    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket]; // cemberlerin en sonuncu elaman�n� d�nder
    }
    public void SoketDegistirme�slemleri(GameObject silinecekObje)
    {
        Cemberler.Remove(silinecekObje);  // silinecek obje gameManagerde se�ilenobjedir

        //Cemberler listesinin eleman say�s� 0'dan farkl�ysa (yani listede en az bir eleman varsa)
        if (Cemberler.Count != 0)
        {
            BosOlanSoket--;
            //Cemberler listesinin son eleman�n�n Cember component'�na eri�ilerek HareketEdebilirmi �zelli�i true olarak degistir
            Cemberler[^1].GetComponent<Cember>().HareketEdebilirmi = true;
        }

        else
        {
            BosOlanSoket = 0;
        }
    }
    public void CemberleriKontrolEt()
    {
        //listesi 4 elemana sahipse (yani her bir trib�nde bir daire varsa)
        if (Cemberler.Count == 4)
        {
            //listesinin ilk eleman�ndan rengi al�n�r
            string renk = Cemberler[0].GetComponent<Cember>().color;
            //Cemberler listesindeki her bir eleman i�in a�a��daki ad�mlar tekrarlan�r
            foreach (var item in Cemberler)
            {
                //E�er eleman�n rengi, ilk eleman�n rengine esitse
                if (renk == item.GetComponent<Cember>().color)
                    // Tamamlanan �ember say�s� bir art�r�l�r
                    CemberTamamlamaSayisi++;
            }
            //�er tamamlanan �ember say�s� 4 ise (yani t�m �emberler ayn� renkte ise)
            if (CemberTamamlamaSayisi == 4)
            {
                //GameManager s�n�f�ndaki StandTamamlandi() fonksiyonu cagirir bu fonksiyon oyunun devam etmesi veya bittigini kontrol eder
                gameManager.StandTamamlandi();
                //tamamlanmisStand() fonksiyonu �a�r�l�r Bu fonksiyon, tamamlanan trib�n�n rengini kapat�r ve trib�n�n �zerindeki t�m daireleri hareketsiz hale getirir
                tamamlanmisStand();
            }
            //  E�er t�m �emberler ayn� renkte de�ilse
            else
            {
                CemberTamamlamaSayisi = 0; // Tamamlanan �ember say�s� s�f�rlan�r
            }
        }
    }
    public void tamamlanmisStand()
    {
        // _Cemberler listesi i�indeki her bir Cember objesi i�in a�a��daki i�lemleri yapar
        foreach (var item in Cemberler)
        {
            //Cember'in HareketEdebilirmi de�i�kenini false yapar Bu oyuncunun Cemberi ta��mak veya hareket ettirmek i�in art�k kullanamayaca�� anlam�na gelir
            item.GetComponent<Cember>().HareketEdebilirmi = false;
            //Cember'in mevcut rengini al�r
            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            //Al�nan rengin alph degerini 100 olarak ayarlar. Bu, Cember'in rengini biraz daha matla�t�r�
            color.a = 100;
            //Cember'in materyal rengini, de�i�tirilmis renk ile g�nceller
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            //Stand�n tag de�erini tamamlanmis olarak de�i�tirir Bu di�er nesnelerin Stand� tamamlanmis olarak alg�lamas�na yard�mci olur
            gameObject.tag = "tamamlanmis";
        }
    }
}
