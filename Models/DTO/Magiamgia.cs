namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Magiamgia")]
    public partial class Magiamgia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Magiamgia()
        {
            Temps = new HashSet<Temp>();
        }

        public int ID { get; set; }

        [StringLength(20)]
        public string Ma { get; set; }

        public int? Soluong { get; set; }

        public bool? Trangthai { get; set; }

        public int? Giatri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Temp> Temps { get; set; }
    }
}
