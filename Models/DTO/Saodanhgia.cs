namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Saodanhgia")]
    public partial class Saodanhgia
    {
        public int ID { get; set; }

        public int? ID_Sach { get; set; }

        public int? Sosao { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
