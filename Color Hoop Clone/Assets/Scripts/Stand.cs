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
        return Cemberler[^1]; 
    }

    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket];
    }
    public void SoketDegistirmeİslemleri(GameObject silinecekObje)
    {
        Cemberler.Remove(silinecekObje);  

        if (Cemberler.Count != 0)
        {
            BosOlanSoket--;
            Cemberler[^1].GetComponent<Cember>().HareketEdebilirmi = true;
        }

        else
        {
            BosOlanSoket = 0;
        }
    }
    public void CemberleriKontrolEt()
    {
        if (Cemberler.Count == 4)
        {
            string renk = Cemberler[0].GetComponent<Cember>().color;
            foreach (var item in Cemberler)
            {
                if (renk == item.GetComponent<Cember>().color)
                    CemberTamamlamaSayisi++;
            }
            if (CemberTamamlamaSayisi == 4)
            {
                gameManager.StandTamamlandi();
                tamamlanmisStand();
            }
            else
            {
                CemberTamamlamaSayisi = 0; 
            }
        }
    }
    public void tamamlanmisStand()
    {
        foreach (var item in Cemberler)
        {
            item.GetComponent<Cember>().HareketEdebilirmi = false;
            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            color.a = 100;
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);

            gameObject.tag = "tamamlanmis";
        }
    }
}
