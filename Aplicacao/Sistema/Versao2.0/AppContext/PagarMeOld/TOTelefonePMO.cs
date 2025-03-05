using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMeOld
{
    public class TOTelefonePMO
    {
        public int Id { get; set; }
        public string Ddi { get; set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
