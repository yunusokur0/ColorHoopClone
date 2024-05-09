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
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (SeciliObje != null && SeciliSdand != hit.collider.gameObject)
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>();

                        if (_Stand.Cemberler.Count != 4 && _Stand.Cemberler.Count != 0)
                        {
                            if (Cember.color == _Stand.Cemberler[^1].GetComponent<Cember>().color)
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


                    else if (SeciliSdand == hit.collider.gameObject)
                    {
                        Cember.HareketEt("soketeGeriGit");
                        SeciliSdand = null;
                        SeciliObje = null;
                    }
                    else
                    {
            
                        Stand _Stand = hit.collider.GetComponent<Stand>();
      
                        SeciliObje = _Stand.EnUstekiCemberiVer();
  
                        Cember = SeciliObje.GetComponent<Cember>();
                        HareketVar = true;

                        if (Cember.HareketEdebilirmi)
                        {
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
