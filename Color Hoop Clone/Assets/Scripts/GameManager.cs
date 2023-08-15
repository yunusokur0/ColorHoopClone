using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliSdand;
    Cember Cember;
    public bool HareketVar;
    public int hedefStandSayisi;
    public int TamamlananStandSayýsý;
    void Update()
    {
        // sol kilik
        if (Input.GetMouseButtonDown(0))
        {
            //cameradan tikladigin yere isin gönderilir , carpisma bilgilerini saglar , isinin uzunlugu
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200))
            {
                // fare týklamasý ile sahnede bir nesneye çarpýlýp çarpýlmadýðýný kontrol eder ve eðer çarpýþýlan nesne "Stand" etiketine sahipse kosulu saðlar
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    // SeciliObje bos deðil ise ve  seçili stand isinin temas ettigi stand olmadigini kontrol eder
                    if (SeciliObje != null && SeciliSdand != hit.collider.gameObject)
                    {
                        //isinin gitigi objedeki stand scriptinin bir örneðini _Stand degiskenine atar
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        //stand scriptindeki listenin eleman sayisi 4 veya 0 deðil ise
                        if (_Stand.Cemberler.Count != 4 && _Stand.Cemberler.Count != 0)
                        {
                            //ýþýnýn isabet ettiði daire == sahnede bulunan tüm dairelerin listesindeki son elemanýn rengi
                            if (Cember.color == _Stand.Cemberler[^1].GetComponent<Cember>().color)
                            {
                                //SeciliObje listeden silinir
                                SeciliSdand.GetComponent<Stand>().SoketDegistirmeÝslemleri(SeciliObje);
                                Cember.HareketEt("pozisyonDegistir", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);

                                _Stand.BosOlanSoket++;
                                _Stand.Cemberler.Add(SeciliObje);
                                _Stand.CemberleriKontrolEt();
                                SeciliSdand = null;
                                SeciliObje = null;
                            }
                            else
                            {
                                Cember.HareketEt("soketeGeriGit");
                                SeciliSdand = null;
                                SeciliObje = null;
                            }


                        }
                        //liste  eleman sayisinin sifir ise
                        else if (_Stand.Cemberler.Count == 0)
                        {
                            SeciliSdand.GetComponent<Stand>().SoketDegistirmeÝslemleri(SeciliObje);
                            Cember.HareketEt("pozisyonDegistir", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);

                            _Stand.BosOlanSoket++;
                            _Stand.Cemberler.Add(SeciliObje);
                            _Stand.CemberleriKontrolEt();
                            SeciliSdand = null;
                            SeciliObje = null;
                        }
                        else
                        {
                            Cember.HareketEt("soketeGeriGit");
                            SeciliSdand = null;
                            SeciliObje = null;
                        }
                    }

                    //seçilen stand isinin gittigi standa esit ise
                    else if (SeciliSdand == hit.collider.gameObject)
                    {
                        Cember.HareketEt("soketeGeriGit");
                        SeciliSdand = null;
                        SeciliObje = null;
                    }
                    else
                    {
                        //isinin gitigi objedeki stand scriptinin bir örneðini _Stand degiskenine atar
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        //SeciliObje = cemberlerin en sonuncu elamanýný
                        SeciliObje = _Stand.EnUstekiCemberiVer();
                        // SeciliObjenin Cember scriptini _Cember deðiþkenine atar
                        Cember = SeciliObje.GetComponent<Cember>();
                        HareketVar = true;

                        if (Cember.HareketEdebilirmi)
                        {
                            //                                   ,  çemberin hareket edeceði hedef konumu belirler
                            Cember.HareketEt("secim", null, null, Cember.AitOlduguStand.GetComponent<Stand>().HareketPozisyonu);
                            SeciliSdand = Cember.AitOlduguStand;
                        }
                    }
                }
            }
        }
    }

    public void StandTamamlandi()
    {
        TamamlananStandSayýsý+=1;
        if (TamamlananStandSayýsý == hedefStandSayisi)
            Debug.Log("okey");
    }
}
