namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Temp")]
    public partial class Temp
    {
        public int ID { get; set; }

        public int? ID_TK { get; set; }

        public int? ID_MGG { get; set; }

        public virtual Magiamgia Magiamgia { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
