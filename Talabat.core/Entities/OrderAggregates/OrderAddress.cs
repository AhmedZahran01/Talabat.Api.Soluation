using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.OrderAggregates
{
    public class OrderAddress
    {
        #region Constractor Region

        public OrderAddress()
        {

        }
        public OrderAddress(string fName, string lName, string street, string city, string country)
        {
            FirstName = fName;
            LastName = lName;
            Street = street;
            City = city;
            Country = country;
        }
        #endregion
      
        #region Properties Region

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        #endregion
    
    }
}
