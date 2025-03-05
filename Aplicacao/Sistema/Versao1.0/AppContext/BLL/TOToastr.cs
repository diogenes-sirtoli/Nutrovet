using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOToastr
    {
        public bool Mostrar { get; set; }
        public char Tipo { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
