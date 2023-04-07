namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            BinhLuans = new HashSet<BinhLuan>();
            DonHangs = new HashSet<DonHang>();
            GioHangs = new HashSet<GioHang>();
            History_Search = new HashSet<History_Search>();
        }

        public int ID { get; set; }

        public int? ID_TaiKhoan { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        public string Diachi { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        public string AnhDaiDien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History_Search> History_Search { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
