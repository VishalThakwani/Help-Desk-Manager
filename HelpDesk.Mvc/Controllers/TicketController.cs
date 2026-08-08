using HelpDesk.Mvc.Models;
using HelpDesk.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Mvc.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (!ModelState.IsValid)
                return View(ticket);

            await _ticketService.CreateTicketAsync(ticket);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(ticket);

            await _ticketService.UpdateTicketAsync(id, ticket);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicketAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return RedirectToAction(nameof(Index));

            var tickets = await _ticketService.GetTicketsByStatusAsync(status);

            return View("Index", tickets);
        }
    }
}