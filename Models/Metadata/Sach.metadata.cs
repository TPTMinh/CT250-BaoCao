using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Metadata
{
	[MetadataTypeAttribute(typeof(SachMetadata))]

	public partial class Sach
	{
		internal sealed class SachMetadata
		{
            public int MaSach { get; set; }
            [Display(Name = "Tên sách")]
            public string TenSach { get; set; }

            [Display(Name = "Giá bán")]
            public Nullable<decimal> GiaBan { get; set; }

            [Display(Name = "Mô tả")]
            public string MoTa { get; set; }

            [Display(Name = "Ngày cập nhật")]
            //[DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            [Display(Name = "Ảnh bìa")]
            public string AnhBia { get; set; }

            [Display(Name = "Số lượng tồn")]
            public Nullable<int> SoLuongTon { get; set; }

            [Display(Name = "Chủ đề")]
            public Nullable<int> MaChuDe { get; set; }

            [Display(Name = "Nhà Xuất Bản")]
            public Nullable<int> MaNXB { get; set; }

        }
	}
}