using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliSdand;
    Cember Cember;
    public bool HareketVar;
    public int hedefStandSayisi;
    public int TamamlananStandSay�s�;
    void Update()
    {
        // sol kilik
        if (Input.GetMouseButtonDown(0))
        {
            //cameradan tikladigin yere isin g�nderilir , carpisma bilgilerini saglar , isinin uzunlugu
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200))
            {
                // fare t�klamas� ile sahnede bir nesneye �arp�l�p �arp�lmad���n� kontrol eder ve e�er �arp���lan nesne "Stand" etiketine sahipse kosulu sa�lar
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    // SeciliObje bos de�il ise ve  se�ili stand isinin temas ettigi stand olmadigini kontrol eder
                    if (SeciliObje != null && SeciliSdand != hit.collider.gameObject)
                    {
                        //isinin gitigi objedeki stand scriptinin bir �rne�ini _Stand degiskenine atar
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        //stand scriptindeki listenin eleman sayisi 4 veya 0 de�il ise
                        if (_Stand.Cemberler.Count != 4 && _Stand.Cemberler.Count != 0)
                        {
                            //���n�n isabet etti�i daire == sahnede bulunan t�m dairelerin listesindeki son eleman�n rengi
                            if (Cember.color == _Stand.Cemberler[^1].GetComponent<Cember>().color)
                            {
                                //SeciliObje listeden silinir
                                SeciliSdand.GetComponent<Stand>().SoketDegistirme�slemleri(SeciliObje);
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
                            SeciliSdand.GetComponent<Stand>().SoketDegistirme�slemleri(SeciliObje);
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

                    //se�ilen stand isinin gittigi standa esit ise
                    else if (SeciliSdand == hit.collider.gameObject)
                    {
                        Cember.HareketEt("soketeGeriGit");
                        SeciliSdand = null;
                        SeciliObje = null;
                    }
                    else
                    {
                        //isinin gitigi objedeki stand scriptinin bir �rne�ini _Stand degiskenine atar
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        //SeciliObje = cemberlerin en sonuncu elaman�n�
                        SeciliObje = _Stand.EnUstekiCemberiVer();
                        // SeciliObjenin Cember scriptini _Cember de�i�kenine atar
                        Cember = SeciliObje.GetComponent<Cember>();
                        HareketVar = true;

                        if (Cember.HareketEdebilirmi)
                        {
                            //                                   ,  �emberin hareket edece�i hedef konumu belirler
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
        TamamlananStandSay�s�+=1;
        if (TamamlananStandSay�s� == hedefStandSayisi)
            Debug.Log("okey");
    }
}
