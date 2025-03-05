using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TOCartaoCreditoPMOBll
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCVV { get; set; }
        public string CardHash { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string ErrorMessage { get; set; }
        public bool ErrorHash { get; set; }
    }
}
