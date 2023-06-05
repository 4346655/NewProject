namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        [StringLength(50)]
        public string EmailUser { get; set; }
    }
}
