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
    
    public partial class ReceituarioNutrientes
    {
        public int IdReceita { get; set; }
        public int IdNutrRec { get; set; }
        public Nullable<bool> EmReceita { get; set; }
        public int IdNutr { get; set; }
        public Nullable<decimal> Consta { get; set; }
        public Nullable<decimal> Falta { get; set; }
        public Nullable<decimal> DoseMin { get; set; }
        public Nullable<int> IdUnidMin { get; set; }
        public Nullable<int> IdPrescrMin { get; set; }
        public Nullable<decimal> DoseMax { get; set; }
        public Nullable<int> IdUnidMax { get; set; }
        public Nullable<int> IdPrescrMax { get; set; }
        public Nullable<decimal> Adequado { get; set; }
        public Nullable<decimal> Recomendado { get; set; }
        public Nullable<decimal> Sobra { get; set; }
        public Nullable<decimal> Dose { get; set; }
        public Nullable<int> IdUnid { get; set; }
        public Nullable<int> IdPrescr { get; set; }
        public Nullable<decimal> PesoAtual { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public Nullable<int> IdOperador { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> DataCadastro { get; set; }
        public byte[] Versao { get; set; }
    
        public virtual Nutrientes Nutrientes { get; set; }
        public virtual PrescricaoAuxTipos PrescricaoAuxTipos { get; set; }
        public virtual PrescricaoAuxTipos PrescricaoAuxTipos1 { get; set; }
        public virtual PrescricaoAuxTipos PrescricaoAuxTipos2 { get; set; }
        public virtual Receituario Receituario { get; set; }
    }
}
