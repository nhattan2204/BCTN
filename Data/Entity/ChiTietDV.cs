//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Entity
{
    using System;
    using System.Collections.Generic;
	using System.Web;

	public partial class ChiTietDV
    {
        public int MaSP { get; set; }
        public Nullable<int> MaDV { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public string Hinh { get; set; }
        public Nullable<int> DonGia { get; set; }
        public string TrangThai { get; set; }
        public string MoTaNgan { get; set; }
		public HttpPostedFileWrapper ImageFile { get; set; }
	}
}