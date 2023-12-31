﻿using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.StoreViewModels
{
	public class ManageSellerViewModel
	{
		public int Id { get; set; }

		[Display(Name = "درخواست")]
		public int Request { get; set; }

		[Display(Name = "توضیحات")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
		public string Description { get; set; }

		public int UserId { get; set; }
	}
}
