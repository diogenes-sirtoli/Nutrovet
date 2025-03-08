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
    
    public partial class Nutrientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nutrientes()
        {
            this.AlimentoNutrientes = new HashSet<AlimentoNutrientes>();
            this.ExigenciasNutricionais = new HashSet<ExigenciasNutricionais>();
            this.ExigenciasNutricionais1 = new HashSet<ExigenciasNutricionais>();
            this.Nutraceuticos = new HashSet<Nutraceuticos>();
            this.ReceituarioNutrientes = new HashSet<ReceituarioNutrientes>();
        }
    
        public int IdGrupo { get; set; }
        public int IdNutr { get; set; }
        public string Nutriente { get; set; }
        public int IdUnidade { get; set; }
        public string Referencia { get; set; }
        public Nullable<decimal> ValMin { get; set; }
        public Nullable<decimal> ValMax { get; set; }
        public Nullable<bool> ListarCardapio { get; set; }
        public Nullable<bool> ListarEmAlim { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public Nullable<int> IdOperador { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> DataCadastro { get; set; }
        public byte[] Versao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlimentoNutrientes> AlimentoNutrientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExigenciasNutricionais> ExigenciasNutricionais { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExigenciasNutricionais> ExigenciasNutricionais1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nutraceuticos> Nutraceuticos { get; set; }
        public virtual NutrientesAuxGrupos NutrientesAuxGrupos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceituarioNutrientes> ReceituarioNutrientes { get; set; }
    }
}
