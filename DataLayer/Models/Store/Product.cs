using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Store
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string ProductFeature { get; set; }

        public string IndexImage1 { get; set; }
        public string IndexImage2 { get; set; }


        public string Tags { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public int confirmStatus { get; set; }



        #region rel

        public int SellerId { get; set; }
        [ForeignKey(nameof(SellerId))]
        public Seller Seller { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public ProductCategory ProductCategory { get; set; }

        public IEnumerable<ProductPrice> ProductPrices { get; set; }

        #endregion

    }
}
