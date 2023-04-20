using Ex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ex.Pages.admin.orders
{
    public class listModel : PageModel
    {
        private readonly PRN221DBContext _context;
		public listModel(PRN221DBContext context)
		{
			_context = context;
		}

		public IList<Order> Order { get; set; } = default!;
		public List<Order> orderList { get; set; }


		[BindProperty(SupportsGet = true)]
		public DateTime? dateFrom { get; set; }
		[BindProperty(SupportsGet = true)]
		public DateTime? dateTo { get; set; }

		public async Task<IActionResult> OnGet()
		{

			orderList = getAllOrders();
			return Page();
		}

		private List<Order> getAllOrders()
		{
			var list = from order in _context.Orders
					   .Include(e => e.Employee)
					   .Include(c => c.Customer)
					   orderby order.OrderDate ascending
					   select order;
			List<Models.Order> orders = list.ToList();

			return orders;
		}
	}
}
