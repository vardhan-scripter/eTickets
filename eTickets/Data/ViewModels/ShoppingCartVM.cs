using eTickets.Data.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCart shoppingCart { get; set; }
        public double ShoopingCartTotal { get; set; }
    }
}
