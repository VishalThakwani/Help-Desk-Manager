using HelpDesk.Mvc.Services;
using HelpDesk.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicketService _ticketService;

        public HomeController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync() ?? new();

            var dashboard = new DashboardViewModel
            {
                TotalTickets = tickets.Count,

                OpenTickets = tickets.Count(t =>
                    t.Status.Equals("Open",
                    StringComparison.OrdinalIgnoreCase)),

                ClosedTickets = tickets.Count(t =>
                    t.Status.Equals("Closed",
                    StringComparison.OrdinalIgnoreCase)),

                RecentTickets = tickets
                    .OrderByDescending(t => t.CreatedDate)
                    .Take(5)
                    .ToList()
            };

            return View(dashboard);
        }
    }
}