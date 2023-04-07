namespace Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History_Search
    {
        public int ID { get; set; }

        public int? ID_KhachHang { get; set; }

        public string NoiDung { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
