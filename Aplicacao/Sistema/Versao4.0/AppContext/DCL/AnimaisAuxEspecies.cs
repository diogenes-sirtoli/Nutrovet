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
    
    public partial class AnimaisAuxEspecies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnimaisAuxEspecies()
        {
            this.AnimaisAuxRacas = new HashSet<AnimaisAuxRacas>();
            this.Dietas = new HashSet<Dietas>();
            this.ExigenciasNutricionais = new HashSet<ExigenciasNutricionais>();
            this.Nutraceuticos = new HashSet<Nutraceuticos>();
        }
    
        public int IdEspecie { get; set; }
        public string Especie { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public Nullable<int> IdOperador { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> DataCadastro { get; set; }
        public byte[] Versao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimaisAuxRacas> AnimaisAuxRacas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dietas> Dietas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExigenciasNutricionais> ExigenciasNutricionais { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nutraceuticos> Nutraceuticos { get; set; }
    }
}
