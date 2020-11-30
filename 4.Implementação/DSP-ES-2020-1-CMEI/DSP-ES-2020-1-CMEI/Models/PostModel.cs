using System.Collections.Generic;
using _3_Domain;

namespace DSP_ES_2020_1_CMEI.Models
{
    public class PostModel : Post
    {
        public List<Cmei> cmeisList;

        public List<Cmei> FetchCmeis()
        {
            // TODO: Implementar a chamada para http://www4.goiania.go.gov.br/daber/dadosabertos/sedetec/geoespaciais/edm.json
            return new List<Cmei>()
            {
                new Cmei("061300000519", "CEI Raboni", "MATUTINO/VESPERTINO"),
                new Cmei("061300000617", "EM Jornalista Jaime Camara", "VERPERTINO/NOTURNO"),
                new Cmei("061300000324", "CMEI  Alegria de Aprender", "MATUTINO/VESPERTINO"),
            };
        }
    }

    public class Cmei
    {
        public string id { get; set; }
        public string name { get; set; }
        public string turno { get; set; }

        public Cmei(string id, string name, string turno)
        {
            this.id = id;
            this.name = name;
            this.turno = turno;
        }
    }
}