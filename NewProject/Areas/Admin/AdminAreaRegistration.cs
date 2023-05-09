using System.Web.Mvc;

namespace NewProject.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
              "thay doi thoi gian voucher",
              "gia-han",
              new { Controller = "Vouchers", action = "GianHan", id = UrlParameter.Optional }
           );

            context.MapRoute(
              "bo thung rac / khoi phuc voucher",
              "bo-khoi-phuc-{idvoucher}",
              new { Controller = "Vouchers", action = "Bo_KhoiPhuc_MGG", id = UrlParameter.Optional }
           );

            context.MapRoute(
              "phan quyen",
              "phan-quyen",
              new { Controller = "Home1", action = "Error", id = UrlParameter.Optional }
           );
            //----------------------------------------------------------------------------
            context.MapRoute(
               "Doi trang thai khach hang",
               "Doi-trang-thai-khach-hang-{id}",
               new { Controller = "Customers", action = "Bat_Tat_HoatDong" }
            );
            //----------------------------------------------------------------------------
            context.MapRoute(
               "Tai khoan",
               "tai-khoan",
               new { Controller = "Accounts", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "Them tai khoan",
               "them-tai-khoan",
               new { Controller = "Accounts", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "Sua tai khoan",
               "sua-tai-khoan-{id}",
               new { Controller = "Accounts", action = "Edit" }
            );
            context.MapRoute(
               "Chi tiet tai khoan",
               "chi-tiet-tai-khoan-{id}",
               new { Controller = "Accounts", action = "Deltails" }
            );
            context.MapRoute(
               "Xoa tai khoan",
               "xoa-tai-khoan-{id}",
               new { Controller = "Accounts", action = "Delete" }
            );

            //----------------------------------------------------------------------------
            context.MapRoute(
              "Khach hang",
              "khach-hang",
              new { Controller = "Customers", action = "Index", id = UrlParameter.Optional }
             );
            context.MapRoute(
              "Them khach hang",
              "them-khach-hang",
              new { Controller = "Customers", action = "Create", id = UrlParameter.Optional }
             );
            context.MapRoute(
              "Sua khach hang",
              "sua-khach-hang-{id}",
              new { Controller = "Customers", action = "Edit" }
             );
            context.MapRoute(
              "Chi tiet khach hang",
              "chi-tiet-khach-hang-{id}",
              new { Controller = "Customers", action = "Details" }
             );
            context.MapRoute(
              "Xoa khach hang",
              "xoa-khach-hang-{id}",
              new { Controller = "Customers", action = "Delete" }
             );
            //----------------------------------------------------------------------------
            context.MapRoute(
              "San pham",
              "san-pham",
            new { Controller = "Products", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
              "Them san pham",
              "them-san-pham",
            new { Controller = "Products", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
              "Sua san pham",
              "sua-san-pham-{id}",
            new { Controller = "Products", action = "Edit" }
            );
            context.MapRoute(
              "Chi tiet san pham",
              "chi-tiet-san-pham-{id}",
            new { Controller = "Products", action = "Details" }
            );
            context.MapRoute(
              "Xoa san pham",
              "xoa-san-pham-{id}",
            new { Controller = "Products", action = "Delete" }
            );

            context.MapRoute(
              "Thung rac san pham",
              "thung-rac-san-pham",
            new { Controller = "Products", action = "ThungRacSanPham" }
            );
            context.MapRoute(
              "Xoa vv san pham",
              "delete-san-pham-{id}",
            new { Controller = "Products", action = "XoaSanPham" }
            );

            context.MapRoute(
              "Bo thung rac san pham",
              "bo-san-pham-{id}",
            new { Controller = "Products", action = "BoThungRac" }
            );
            context.MapRoute(
              "Quay lai san pham",
              "quay-lai-san-pham-{id}",
            new { Controller = "Products", action = "KhoiPhucSanPham" }
            );
            context.MapRoute(
              "Ban hoac dung ban sach",
              "ban-hoac-dung-{id}",
            new { Controller = "Products", action = "Ban_DungBan" }
            );


            //----------------------------------------------------------------------------
            context.MapRoute(
             "Danh muc san pham",
             "danh-muc-{trangthai}",
            new { Controller = "Categories", action = "Index"}
            );
            context.MapRoute(
             "Them danh muc san pham",
             "them-danh-muc",
            new { Controller = "Categories", action = "ThemDanhMuc" }
            );
            context.MapRoute(
             "Xoa danh muc san pham",
             "xoa-danh-muc-{id}",
            new { Controller = "Categories", action = "XoaDanhMuc" }
            );
            context.MapRoute(
             "Sua danh muc san pham",
             "sua-danh-muc-{id}",
            new { Controller = "Categories", action = "Edit" }
            );
            context.MapRoute(
             "Doi trang thai danh muc",
             "doi-trang-thai-danh-muc-{idls}",
            new { Controller = "Categories", action = "Bat_Tat_HoatDong" }
            );
            //----------------------------------------------------------------------------
            context.MapRoute(
                "Don hang",
                "don-hang-{trangthai}",
            new { Controller = "Orders", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Them don hang",
                "them-don-hang",
            new { Controller = "Orders", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Sua don hang",
                "sua-don-hang-{id}",
            new { Controller = "Orders", action = "Edit" }
            );
            context.MapRoute(
                "Xoa don hang",
                "xoa-don-hang-{id}",
            new { Controller = "Orders", action = "Delete" }
             );
            context.MapRoute(
                "Chi tiet don hang",
                "chi-tiet-don-hang-{id}",
            new { Controller = "Orders", action = "Details" }
            );
             context.MapRoute(
                "Chuyen qua giao hang",
                "chuyen-trang-thai-{idor}",
            new { Controller = "Orders", action = "ChuyenTiepTrangThai" }
             );
            context.MapRoute(
                "huy don admin",
                "huy-don-admin-{idor}",
            new { Controller = "Orders", action = "HuyDonAdmin" }
             );
            context.MapRoute(
                "khoi phuc don hang admin",
                "khoi-phuc-don-admin-{idor}",
            new { Controller = "Orders", action = "KhoiphucAdmin" }
             );
            //----------------------------------------------------------------------------
            context.MapRoute(
                "Ma giam gia",
                "ma-giam-gia-{trangthai}",
             new { Controller = "Vouchers", action = "Index" }
             );
            context.MapRoute(
                "Them giam gia",
                "them-ma-giam-gia",
             new { Controller = "Vouchers", action = "Create", id = UrlParameter.Optional }
             );
            context.MapRoute(
                "Sua giam gia",
                "sua-ma-giam-gia-{id}",
             new { Controller = "Vouchers", action = "Edit" }
             );
            context.MapRoute(
               "Xoa giam gia",
               "xoa-ma-giam-gia-{id}",
            new { Controller = "Vouchers", action = "Delete" }
            );
            context.MapRoute(
                "trang chu quan li",
                "trang-chu-quan-li",
             new { Controller = "Home1", action = "Index", id = UrlParameter.Optional }
             );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}