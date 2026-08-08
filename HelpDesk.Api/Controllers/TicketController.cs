using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            int ticketId = await _ticketRepository.CreateTicketAsync(ticket);

            return Ok(ticketId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            await _ticketRepository.UpdateTicketAsync(ticket);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _ticketRepository.DeleteTicketAsync(id);

            return Ok();
        }

        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetTicketsByStatus(string status)
        {
            var tickets = await _ticketRepository.GetTicketsByStatusAsync(status);

            return Ok(tickets);
        }
    }


}