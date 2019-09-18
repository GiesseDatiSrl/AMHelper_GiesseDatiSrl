AMHelper
===
AMHelper è una libreria distribuita attraverso nuget che consente di interfacciarsi
in maniera estremamente semplice con il servizio di import ed export dei dati verso
l'AppManager.

All'interno di AMHelper sono già definite tutte le strutture dati dei tracciati di import
e i metodi per l'esportazione dei dati (es: ordini) attraverso chiamate a webservice


Usare AMHelper
---
In questo breve tutorial vedremo un esempio su come recuperare i dati degli ordini da AppManager usando AMHelper

1. Creare una nuova solution in Visual studio

2. Installare con nuget la libreria
  ```
    PM> Install-Package AMHelper
  ```

3. Aggiungere lo using in cima progetto
  ```c#
    using AMHelper.WS;
  ```

4. Imposta i token di autorizzazione per leggere da License Manager e dall'AppManager (contattare Giesse Dati Srl per le chiavi)
  ```c#
    // Chiavi
    string AuthKeyLM = "3405D863-C49C-4D4B-B1FF-35D6231C61D9";
    string AuthKeyAM = "AAB993AE-92B7-4E88-BC59-B231F0CDAD7C";
  ```

5. Istanzia l'oggetto per la connessione al License Manager
  ```c#
    // Dove è situato il mio AM ?
    GetDataLM lmdata = new GetDataLM(AuthKeyLM);
  ```

6. Recupera gli url dei web service dell' AppManager.
  ```c#
    // Quali dati contiene il mio AM ?
    ws_rec_lmparam AMData = null;
    bool lmRetVal = lmdata.get_am_par(ref AMData);
  ```
  
7. Recupera l'url dell'appmanager
  ```c#
    wsURL = AMData.url_am_api;
  ```

8. Invia la versione della'applicazione al License Manager
  ```c#
  bool retRelease = lmdata.send_release("1.0");
  ```
  
9. Recupera da una tua tabella di configurazione l'ultimi ID Ordine ottenuto
  GetMyLastOrdersID è la tua funzione che dovrai sviluppare per recuperare il dato
  ```c#
    // Leggo l'ID dell'ultimo ordine recuperato dal WS. Se è la prima volta tornerà 0 (zero)
    int LastStoredID = GetMyLastOrdersID();
  ```

10. Recupero i dati degli ordini
  ```c#
    // Istanzio l'oggetto Export dell'AMHelper
    GetDataAM ed = new GetDataAM(AuthKeyAM, wsURL);
  
    // Chiamo il WS per farmi dare la lista dei leads
    ws_rec_orders OrdersData = null;
    bool RetVal = ed.exp_orders(LastStoredID, ref OrdersData);
  ```

11. Leggo i dati recuperati
  ```c#
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
            Console.WriteLine("... booh! ");
        }
  }
  ```

Inviare notifiche push
---
La libreria contiene anche 2 metodi per inviare le notifiche push agli utenti degli iPad.
La notifica può essere mandata specificando il codice agente del gestionale o il codice utente configurato nell'appmanager.

Esempio di invio con codice agente:

```c#
  static void SendNotificationByAgent_Sample()
    {
        // notifica_push_send_by_username
        // Impostare le chiavi di autorizzazione
        string AuthKeyLM = "41AF11C0-36C9-4C16-8BAF-83B033B959CA";
        // string AuthKeyAM = "FDFC769C-E950-4067-B4FF-0F3A134FB94B";
  
        // Impostare true per l'utilizzo dei server di produzione (default)
        // false consente l'utilizzo di server di test (solo ad uso interno)
        bool Produzione =  true;
  
        GetDataLM lmdata = new GetDataLM(AuthKeyLM, Produzione);
  
        ws_rec_lmparam AMData = null;
        bool lmRetVal = lmdata.get_am_par(ref AMData);
  
        bool retRelease = lmdata.send_push_notification_by_agent("8", "Testo del messaggio per l'agente");
    }
```
Esempio di invio con utente appmanager

```c#
  static void SendNotificationByUserName_Sample()
  {
      // notifica_push_send_by_username
      // Impostare le chiavi di autorizzazione
      string AuthKeyLM = "41AF11C0-36C9-4C16-8BAF-83B033B959CA";
      // string AuthKeyAM = "FDFC769C-E950-4067-B4FF-0F3A134FB94B";
  
      // Impostare true per l'utilizzo dei server di produzione (default)
      // false consente l'utilizzo di server di test (solo ad uso interno)
      bool Produzione = true;
  
      GetDataLM lmdata = new GetDataLM(AuthKeyLM, Produzione);
  
      ws_rec_lmparam AMData = null;
      bool lmRetVal = lmdata.get_am_par(ref AMData);
  
      bool retRelease = lmdata.send_push_notification_by_username("admin", "Testo del messaggio per l'utente");
  }
```


Dipendenze
---
Nella versione compilata per il framework 2.0, vengono utilizzate 2 librerie:

* RestSharpNet2.0 - https://github.com/mcintyre321/RestSharp-.NET-2.0-Fork
* Newtonsoft 4.5

Nella versione compilata per il framework 4.0, viene usata la libreria:

* RestSharp - http://restsharp.org


