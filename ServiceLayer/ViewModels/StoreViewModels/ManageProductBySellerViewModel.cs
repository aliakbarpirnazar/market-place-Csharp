using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLayer.ViewModels.StoreViewModels
{
	public class ManageProductBySellerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان فارسی")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        public string FaTitle { get; set; }

		[Display(Name = "عنوان خارجی")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string EnTitle { get; set; }

		[Display(Name = "ویژگی های محصول")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string ProductFeature { get; set; }

		[Display(Name = "تصویر پیش فرض")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string IndexImage1 { get; set; }

		[Display(Name = "کلمات کلیدی")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string Tags { get; set; }

		[Display(Name = "توضیحات مختصر")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string ShortDescription { get; set; }

		[Display(Name = "توضیحات کامل")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string Description { get; set; }

		[Display(Name = "دسته بندی")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public int CategoryId { get; set; }

		#region Price

		[Display(Name = "عنوان قیمت")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public string Labale { get; set; }

		[Display(Name = "قیمت")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public int Price { get; set; }

		[Display(Name = "تعداد محصول")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public int Count { get; set; }

		[Display(Name = "وزن")]
		[Required(ErrorMessage = "نباید بدون مقدار باشد")]
		public int Weight { get; set; }

		#endregion

	}
}
