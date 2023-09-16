using DataLayer.Context;
using DataLayer.Models.Store;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.StoreViewModels;

namespace ServiceLayer.Services
{
    public class StoreService : IStoreService
	{
		private readonly ApplicationDbContext _context;

		public StoreService(ApplicationDbContext context)
		{
			_context = context;
		}


		public bool SendRequestForSalePanel(ManageSellerViewModel model)
		{
			if (model != null)
			{
				var seller = new Seller
				{
					CreateDate = DateTime.Now,
					Description = model.Description,
					IsDeleted = false,
					Request = 1,
					UserId = model.UserId
				};

				_context.Sellers.Add(seller);
				int res = _context.SaveChanges();
				if (res > 0)
				{
					var user = _context.Users.FirstOrDefault(x => x.UserId == seller.UserId);
					user.SellerId = seller.Id;
					_context.Users.Update(user);
					_context.SaveChanges();
					return true;
				}

			}
			return false;
		}

		public BaseFilterViewModel<ListSellerViewModel> GetAllSellersForAdmin(int pageIndex, string search)
		{
			var sellerList = _context.Sellers.Include(x => x.User).Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreateDate).ToList();
			int take = 15;
			int howManyPageShow = 2;
			var pager = PagingHelper.Pager(pageIndex, sellerList.Count(), take, howManyPageShow);

			if (search != null)
			{
				sellerList = sellerList.Where(x => x.User.PhoneNumber.Contains(search) || x.User.DisplayName.Contains(search)).ToList();
			}

			var resault = sellerList.Select(x => new ListSellerViewModel
			{
				CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.CreateDate, false),
				DisplayName = x.User.DisplayName,
				PhoneNumber = x.User.PhoneNumber,
				Request = x.Request,
				Id = x.Id
			}).ToList();

			var outPut = PagingHelper.Pagination<ListSellerViewModel>(resault, pager);

			BaseFilterViewModel<ListSellerViewModel> res = new BaseFilterViewModel<ListSellerViewModel>
			{
				EndPage = pager.EndPage,
				Entities = outPut,
				PageCount = pager.PageCount,
				StartPage = pager.StartPage,
				PageIndex = pageIndex
			};

			return res;
		}

		public ManageSellerViewModel GetSellerForUpdate(int sellerId)
		{
			return _context.Sellers.Where(x => x.Id == sellerId).Select(x => new ManageSellerViewModel
			{
				Description = x.Description,
				Id = x.Id,
				Request = x.Request
			}).FirstOrDefault();
		}

		public bool UpdateSellerRequest(ManageSellerViewModel model)
		{
			var seller = _context.Sellers.FirstOrDefault(x => x.Id == model.Id);
			if (seller != null)
			{
				seller.Request = model.Request;
				seller.Description = model.Description;
				_context.Sellers.Update(seller);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

		public int GetSellerStatusByPhoneNumber(string phoneNumber)
		{
			var user = _context.Users.Include(x => x.Seller).FirstOrDefault(x => x.PhoneNumber == phoneNumber);
			if (user.SellerId != null)
			{
				return user.Seller.Request;
			}
			return -1;
		}

		public BaseFilterViewModel<ListProductForSellerViewModel> GetProductListForSeller(int pageIndex, string search, int userId)
		{
			var productList = _context.Products.Where(x => x.IsDeleted == false && x.SellerId == userId).OrderByDescending(x => x.CreateDate).ToList();
			int take = 15;
			int howManyPageShow = 2;
			var pager = PagingHelper.Pager(pageIndex, productList.Count(), take, howManyPageShow);

			if (search != null)
			{
				productList = productList.Where(x => x.FaTitle.Contains(search) || x.EnTitle.Contains(search)).ToList();
			}

			var resault = productList.Select(x => new ListProductForSellerViewModel
			{
				CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.CreateDate, false),
				Id = x.Id,
				DefaultImg = x.IndexImage1,
				EnTitle = x.EnTitle,
				FaTitle = x.FaTitle,
				DefaultPrice = _context.ProductPrices.Where(p => p.IsDefault == true && p.IsDeleted == false && p.ProductId == x.Id).FirstOrDefault().Price.ToString("#,0"),

			}).ToList();

			var outPut = PagingHelper.Pagination<ListProductForSellerViewModel>(resault, pager);

			BaseFilterViewModel<ListProductForSellerViewModel> res = new BaseFilterViewModel<ListProductForSellerViewModel>
			{
				EndPage = pager.EndPage,
				Entities = outPut,
				PageCount = pager.PageCount,
				StartPage = pager.StartPage,
				PageIndex = pageIndex
			};

			return res;
		}

		//ConfirmStatus = 1 => درحال بررسی
		//ConfirmStatus = 2 => تایید شذه
		//ConfirmStatus = 3 => رد شده
		public bool CreateProductBySeller(ManageProductBySellerViewModel model, int sellerId)
		{
			if (model != null)
			{
				Product product = new Product
				{
					CategoryId = model.CategoryId,
					confirmStatus = 1,
					CreateDate = DateTime.Now,
					Description = model.Description,
					EnTitle = model.EnTitle,
					FaTitle = model.FaTitle,
					IndexImage1 = model.IndexImage1,
					IsDeleted = false,
					ProductFeature = model.ProductFeature,
					SellerId = sellerId,
					ShortDescription = model.ShortDescription,
					Tags = model.Tags
				};
				_context.Products.Add(product);
				int res = _context.SaveChanges();
				if (res > 0)
				{
					ProductPrice productPrice = new ProductPrice
					{
						ConfirmStatus = 1,
						Count = model.Count,
						CreateDate = DateTime.Now,
						DiscountPercentage = 0,
						IsDeleted = false,
						IsDefault = true,
						Labale = model.Labale,
						Price = model.Price,
						ProductId = product.Id,
						Weight = model.Weight,
						PercentageSite = 0
					};
					_context.ProductPrices.Add(productPrice);
					_context.SaveChanges();
					return true;
				}
			}
			return false;

		}

		public int? GetSellerIdByUserName(string userName)
		{
			return _context.Users.FirstOrDefault(x=>x.PhoneNumber == userName).SellerId;
		}

		public List<SelectListItem> GetSubCategoriesListItems()
		{
			return _context.ProductCategories.Where(x => x.ParentId != null && x.IsDeleted == false).
				Select(x => new SelectListItem
				{
					Text = x.Title,
					Value = x.Id.ToString()

				}).ToList();
		}

		public List<ProductCategory> GetCategoriesForAdmin()
		{
			return _context.ProductCategories.Where(x => x.IsDeleted == false).Include(x => x.Parent).ToList();
		}

		public bool CreateCategoryByAdmin(ProductCategory model)
		{
			if (model != null)
			{
				_context.ProductCategories.Add(model);
				_context.SaveChanges();
				return true;
			}

			return false;

		}





		public List<SelectListItem> GetParentCategoriesListItems()
		{
			return _context.ProductCategories.Where(x => x.ParentId == null && x.IsDeleted == false).
				Select(x => new SelectListItem
				{
					Text = x.Title,Value=x.Id.ToString()

				}).ToList();
		}

	}
}
