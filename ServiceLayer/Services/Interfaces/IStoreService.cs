using DataLayer.Models.Store;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.StoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
	public interface IStoreService
	{
		bool SendRequestForSalePanel(ManageSellerViewModel model);
		BaseFilterViewModel<ListSellerViewModel> GetAllSellersForAdmin(int pageIndex, string search);
		public ManageSellerViewModel GetSellerForUpdate(int sellerId);
		public bool UpdateSellerRequest(ManageSellerViewModel model);
		public int GetSellerStatusByPhoneNumber(string phoneNumber);
		BaseFilterViewModel<ListProductForSellerViewModel> GetProductListForSeller(int pageIndex, string search, int userId);
		List<ProductCategory> GetCategoriesForAdmin();
		bool CreateCategoryByAdmin(ProductCategory model);
		List<SelectListItem> GetParentCategoriesListItems();
		bool CreateProductBySeller(ManageProductBySellerViewModel model, int sellerId);
		int? GetSellerIdByUserName(string userName);
		List<SelectListItem> GetSubCategoriesListItems();

	}
}
