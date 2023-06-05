namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            shippers = new HashSet<shipper>();
        }

        public int ID { get; set; }

        public int? ID_KhachHang { get; set; }

        public int? ID_Sach { get; set; }

        public int? ID_ThanhToan { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(20)]
        public string Ma_GG { get; set; }

        public string Note { get; set; }

        public int? Tong_gia { get; set; }

        public int? Trang_thai { get; set; }

        public DateTime? Ngay0 { get; set; }

        [StringLength(250)]
        public string Hoten { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        public string Diachi { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual ThanhToan ThanhToan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shipper> shippers { get; set; }
    }
}
