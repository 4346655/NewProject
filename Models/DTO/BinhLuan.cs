namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        public int ID { get; set; }

        public int? ID_Sach { get; set; }

        public int? ID_KhachHang { get; set; }

        public string NoiDung { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
