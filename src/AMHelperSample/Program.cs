﻿using System;
using System.Collections.Generic;
using System.Text;
using AMHelper.WS;


namespace AMHelperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpOrders();

        }


        static void ExpOrders()
        {

            try
            {
                // Chiavi
                string AuthKeyLM = "LMKEY2";
                string AuthKeyAM = "AMKEYk";
                string ProxyUser = "teo";
                string ProxyPassword = "gigi";
                string ProxyHost = "192.168.10.134";
                int ProxyPort = 8080;
                int LastStoredID = 1;

                bool ProxyEnable = true;


                // http://am.apexnet.it/api_fwco/v1/progetti/wt.fwco/exportPaginazione/codaOrdini?authKey=43450611-EA2D-4CE8-B1FF-B2EB7C42114A&offset=0&limit=10&count=0&lastID=1490
                // Dove è situato il mio AM ?
                GetDataLM lmdata = new GetDataLM(AuthKeyLM, false);

                if (ProxyEnable)
                {
                    lmdata.HttpProxyAutentication(ProxyUser, ProxyPassword, ProxyHost, ProxyPort);
                }
                    


                // Quali dati contiene il mio AM ?
                ws_rec_lmparam AMData = null;
                bool lmRetVal = lmdata.get_am_par(ref AMData);


                string wsURL = "";
                if (lmRetVal && AMData != null)
                {
                    wsURL = AMData.url_am_api + "/" + AMData.cod_prog;
                }
                else
                {
                    Console.WriteLine("Qualcosa è andato male");
                    return;
                }



                // Leggo l'ID dell'ultimo ordine recuperato dal WS. Se è la prima volta tornerà 0 (zero)

               
                //int LastStoredID = 12484;
                // Istanzio l'oggetto Export dell'AMHelper
                GetDataAM ed = new GetDataAM(AuthKeyAM, wsURL);


                if (ProxyEnable)
                {
                    ed.HttpProxyAutentication(ProxyUser, ProxyPassword, ProxyHost, ProxyPort);
                }
                    


                // Chiamo il WS per farmi dare la lista dei leads
                ws_rec_orders OrdersData = null;
                bool RetVal;

                try
                {

                    RetVal = ed.exp_orders(LastStoredID, ref OrdersData);

                    if (RetVal && OrdersData != null)
                    {
                        Console.WriteLine(String.Format("... found {0} record ", OrdersData.testate.Count.ToString()));

                        System.Globalization.CultureInfo itIT = System.Globalization.CultureInfo.CreateSpecificCulture("it-IT");

                        foreach (var t in OrdersData.testate)
                        {
                            Console.WriteLine("Codclifor:" + t.cod_clifor);
                            Console.WriteLine("Data consegna:" + t.data_consegna);

                            foreach (var r in t.righe)
                            {
                                Console.WriteLine(">>> Riga: " + r.id);
                                Console.WriteLine(">>> Cod art: " + r.codice_articolo);
                                // .... etc..
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("... booh! Sembra che non ci siano dati");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("..error:" + ex.Message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("..error:" + ex.Message);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

        }

    }
}