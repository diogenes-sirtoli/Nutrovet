//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DCL
{
    using System;
    using System.Collections.Generic;
    
    public partial class NutraceuticosDietas
    {
        public int IdNutrac { get; set; }
        public int IdNutDie { get; set; }
        public int IdDieta { get; set; }
        public string Observacao { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public Nullable<int> IdOperador { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> DataCadastro { get; set; }
        public byte[] Versao { get; set; }
    
        public virtual Dietas Dietas { get; set; }
        public virtual Nutraceuticos Nutraceuticos { get; set; }
    }
}
