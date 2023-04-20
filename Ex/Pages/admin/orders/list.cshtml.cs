using Ex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Ex.Pages.admin.orders
{
    public class listModel : PageModel
    {
        private readonly PRN221DBContext _context;
		public listModel(PRN221DBContext context)
		{
			_context = context;
		}
		public List<Order> orderList { get; set; }

		[BindProperty]
		public List<Customer> Customers { get; set; }
		[BindProperty(SupportsGet = true)]
		public DateTime? dateFrom { get; set; }
		[BindProperty(SupportsGet = true)]
		public DateTime? dateTo { get; set; }

		[FromQuery(Name = "id")]
		public string CusId { get; set; }

		public void OnGet()
		{
			Customers = _context.Customers.ToList();

			if (dateFrom == null || dateTo == null 
				|| (dateFrom == null && dateTo == null))
			{
				orderList = _context.Orders.ToList();
			}
			else
			{
				orderList = _context.Orders.Where(o => DateTime.Compare(o.OrderDate.Value, dateFrom.Value) >= 0
					&& DateTime.Compare(o.OrderDate.Value, dateTo.Value) <= 0).ToList();
			}


			if (CusId != null)
			{
				orderList = orderList.Where(o => o.CustomerId.Equals(CusId)).ToList();
			}

		}

	}
}
