using System;
using System.Collections.Generic;
using System.Text;

namespace AMHelper.WS
{
    public class ws_rec_leads
    {
        public ws_meta meta  { get; set; }
        public string cod_progetto { get; set; }
        public DateTime? last_data_import { get; set; }
        public decimal? last_id { get; set; }
        public List<TestataLeadsExport> leads { get; set; }
    }


    public class TestataLeadsExport
    {
        public decimal id { get; set; }
        public string unique_id { get; set; }

       
        public string descrizione1 { get; set; } // ragione sociale ?
        public string partita_iva { get; set; }
        public string codice_fiscale { get; set; }

        // Dati generali
        public string telefono { get; set; }
        public string cellulare { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string note { get; set; }

        // Localit�
        public string indirizzo { get; set; }

        public string cod_citta { get; set; }
        public string citta { get; set; }
        public string cap { get; set; }
        public string provincia { get; set; }

        public string cod_nazione { get; set; }
        public string nazione { get; set; }

        // Campagne
        public string cod_campagna { get; set; }
        public string campagna { get; set; } // aggiungo io
        public string cod_agente { get; set; }

        public string cod_ditta { get; set; } //ws ipad CodiceDitta
        public string cod_lead { get; set; }  //ws ipad CodiceLead
        public string cod_operatore { get; set; }
        public string cod_prog { get; set; }
        public string descrizione2 { get; set; }

        public string internet { get; set; }  // Non mostrato nell'ipad

        public string utente { get; set; }

        public string nome { get; set; }
        public string cognome { get; set; }
        public string fatturato { get; set; }

        public string cod_categoria { get; set; }
        public string categoria { get; set; }

        public string cod_agente_lead { get; set; }

        public string cod_canale_vendita { get; set; }
        public string canale_vendita { get; set; }

        public string cod_concorrente { get; set; }
        public string concorrente { get; set; }

        public string cod_modalita_acquisizione { get; set; }
        public string modalita_acquisizione { get; set; }

        public string cod_fonte { get; set; }
        public string fonte { get; set; }

        public string cod_interesse { get; set; }
        public string interesse { get; set; }

        public string cod_settore { get; set; }
        public string settore { get; set; }

        public string cod_mansione { get; set; }
        public string mansione { get; set; }

        public string cod_valuta { get; set; }
 
        public DateTime? data_import { get; set; }
    }
}