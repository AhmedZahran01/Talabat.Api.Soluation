using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.OrderAggregates
{
    public class ProductItemOrderd
    {
        #region Constractor Region

        public ProductItemOrderd()
        {

        }
        public ProductItemOrderd(int productId, string productName, string pictureUrl)
        {
            this.productId = productId;
            this.productName = productName;
            PictureUrl = pictureUrl;
        }

        #endregion
      
        public int productId { get; set; }
        public string productName { get; set; }
        public string PictureUrl { get; set; }
    }
}
