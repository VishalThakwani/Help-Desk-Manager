using HelpDesk.Api.Data;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HelpDeskDbContext _context;

        public TicketRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<int> CreateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            return ticket.Id;
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
        {
            return await _context.Tickets
                         .Where(t => t.Status == status)
                         .ToListAsync();
        }
    }
}