using Ex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ex.Pages.admin.orders
{
    public class detailsModel : PageModel
    {

        private readonly PRN221DBContext _context;
        public detailsModel(PRN221DBContext context)
        {
            _context = context;
        }

        public List<OrderDetail> orderDetails { get; set; }
        public decimal Sum { get; set; } = 0;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            orderDetails = _context.OrderDetails.Include(x => x.Product).Where(x => x.OrderId == id).ToList();
            foreach (var item in orderDetails)
            {
                Sum += item.UnitPrice * item.Quantity;
            }

            return Page();

        }
    }
}
