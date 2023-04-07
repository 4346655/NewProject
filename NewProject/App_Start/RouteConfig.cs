﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewProject
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Mua lai",
				url: "mua-lai-{idorder}",
				defaults: new { controller = "Order", action = "Rebuy", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Huy don",
				url: "huy-don-{idorder}",
				defaults: new { controller = "Order", action = "HuyDon", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Dang xuat",
				url: "dang-xuat",
				defaults: new { controller = "Login", action = "Logout", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Them moi don hang",
				url: "them-moi-don-hang",
				defaults: new { controller = "Order", action = "AddtoOrder" }
			);
			routes.MapRoute(
				name: "Xac minh don hang",
				url: "xac-minh-don-hang-{idsach}-{soluong}",
				defaults: new { controller = "Order", action = "TempPage", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Gio Don hang",
				url: "gio-don-hang",
				defaults: new { controller = "Home", action = "CartOrder", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Them binh luan",
				url: "binh-luan",
				defaults: new { controller = "ProductDetails", action = "Addcomment" }
			);
			routes.MapRoute(
				name: "Doi mat khau",
				url: "doi-mat-khau",
				defaults: new { controller = "Profile", action = "Doimatkhau" }
			);
			routes.MapRoute(
				name: "Sua ho so",
				url: "sua-ho-so",
				defaults: new { controller = "Profile", action = "Suathongtin"}
			);
			routes.MapRoute(
				name: "Ho so",
				url: "ho-so",
				defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Quen mat khau",
				url: "quen-mat-khau",
				defaults: new { controller = "Login", action = "Forgetpassword", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Dang ki",
				url: "dang-ki",
				defaults: new { controller = "Register", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "xoa gio hang",
				url: "xoa-gio-hang-{id}",
				defaults: new { controller = "Cart", action = "DeleteCart" }
			);
			routes.MapRoute(
				name: "them gio hang",
				url: "them-vao-gio-hang-{idsach}-{soluong}",
				defaults: new { controller = "Cart", action = "AddtoCart" }
			);
			routes.MapRoute(
				name: "gio hang",
				url: "gio-hang",
				defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: " chi tiet sach",
				url: "chi-tiet-sach-{id}",
				defaults: new { controller = "ProductDetails", action = "Index" }
			);
			routes.MapRoute(
				name: " lay danh sach sach",
				url: "lay-danh-sach-sach",
				defaults: new { controller = "Home", action = "Listpr", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Danh sach sach",
				url: "danh-sach-sach-{id}",
				defaults: new { controller = "Home", action = "Listproduct" }
			);
			routes.MapRoute(
				name: "Box",
				url: "box",
				defaults: new { controller = "Home", action = "Box", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Dang nhap",
				url: "dang-nhap",
				defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Ho so khach hang",
				url: "ho-so",
				defaults: new { controller = "Profiles", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Chi tiet gio hang",
				url: "chi-tiet-gio-hang-{id}",
				defaults: new { controller = "Carts", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Chi tiet ",
				url: "chi-tiet-san-pham-{id}",
				defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Trang chu",
				url: "trang-chu",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
