namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            BinhLuans = new HashSet<BinhLuan>();
            DonHangs = new HashSet<DonHang>();
            GioHangs = new HashSet<GioHang>();
            Saodanhgias = new HashSet<Saodanhgia>();
        }

        public int ID { get; set; }

        public int? ID_LoaiSach { get; set; }

        public string TenSach { get; set; }

        public string Anh { get; set; }

        public int? SoLuong { get; set; }

        public int? SoLuong_DaBan { get; set; }

        public string Mota { get; set; }

        public int? Gia { get; set; }

        public int? Gia_giam { get; set; }

        public bool? Trangthai { get; set; }

        [StringLength(500)]
        public string NhaXuatBan { get; set; }

        [StringLength(500)]
        public string TacGia { get; set; }

        public bool? DeleteStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        public virtual Loai_Sach Loai_Sach { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Saodanhgia> Saodanhgias { get; set; }
    }
}
