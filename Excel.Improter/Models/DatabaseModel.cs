using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excel.Improter.Models
{
    public class DatabaseModel
    {
        public int Id { get; set; }
        public string YataginInzibatiErazisi { get; set; }
        public string YataginKodu { get; set; }
        public string YataginAdi { get; set; }
        public string SaheninKodu { get; set; }
        public string SaheninAdi { get; set; }
        public string DMAANomresi { get; set; }
        public DateTime? DMAABitmeTarix { get; set; }
        public DateTime? DMAAQeydiyyatTarixi { get; set; }
        public string EhtiyyatinKategoryasi { get; set; }
        public string IstisimarVeziyyeti { get; set; }
        public string IlkinEhtiyyat { get; set; }
        public string IlUzreHasilatCemi { get; set; }
        public string HasilatQaliqlari { get; set; }
        public string FaydaliQazintiNovu { get; set; }
        public string AyrilanSahe { get; set; }
        public string VOEN { get; set; }
        public string MilliGealojiKeshfiyyat { get; set; }
        public string Kordinat { get; set; }
        public string MedaxilVeziyyeti { get; set; }
    }
}
