namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        public int ID { get; set; }

        public int? ID_Sach { get; set; }

        public int? SoLuong { get; set; }

        public int? ID_KhachHang { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
