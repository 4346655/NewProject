namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shipper")]
    public partial class shipper
    {
        public int ID { get; set; }

        public int? ID_DonHang { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        public int? ID_TaiKhoan { get; set; }

        public bool? TrangThai { get; set; }

        [StringLength(500)]
        public string Hoten { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
