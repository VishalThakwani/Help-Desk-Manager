using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Sockets;

namespace HelpDesk.Tests
{
    public class UnitTest1
    {
        private readonly Mock<ITicketRepository> _repositoryMock;
        private readonly TicketController _controller;

        public UnitTest1()
        {
            _repositoryMock = new Mock<ITicketRepository>();
            _controller = new TicketController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllTickets_ReturnsOkResult_WhenTicketsExist()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Title = "Laptop Issue",
                    Description = "Laptop is not working",
                    Priority = "High",
                    Status = "Open",
                    RaisedBy = "Vishal",
                    CreatedDate = DateTime.Now
                },
                new Ticket
                {
                    Id = 2,
                    Title = "Printer Issue",
                    Description = "Printer is not responding",
                    Priority = "Medium",
                    Status = "Open",
                    RaisedBy = "Rahul",
                    CreatedDate = DateTime.Now
                }
            };

            _repositoryMock
                .Setup(repo => repo.GetAllTicketsAsync())
                .ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetAllTickets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTickets = Assert.IsType<List<Ticket>>(okResult.Value);

            Assert.Equal(2, returnedTickets.Count);

            _repositoryMock.Verify(
                repo => repo.GetAllTicketsAsync(),
                Times.Once);
        }

        [Fact]
        public async Task GetTicketById_ReturnsOkResult_WhenTicketExists()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 1,
                Title = "Laptop Issue",
                Description = "Laptop is not working",
                Priority = "High",
                Status = "Open",
                RaisedBy = "Vishal",
                CreatedDate = DateTime.Now
            };

            _repositoryMock
                .Setup(repo => repo.GetTicketByIdAsync(1))
                .ReturnsAsync(ticket);

            // Act
            var result = await _controller.GetTicketById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTicket = Assert.IsType<Ticket>(okResult.Value);

            Assert.Equal(1, returnedTicket.Id);
            Assert.Equal("Laptop Issue", returnedTicket.Title);

            _repositoryMock.Verify(
                repo => repo.GetTicketByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(repo => repo.GetTicketByIdAsync(99))
                .ReturnsAsync((Ticket?)null);

            // Act
            var result = await _controller.GetTicketById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _repositoryMock.Verify(
                repo => repo.GetTicketByIdAsync(99),
                Times.Once);
        }

        [Fact]
        public async Task CreateTicket_ReturnsOkResult_WhenTicketsCreatedSuccessfully()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 0,
                Title = "VPN Issue",
                Description = "VPN connection failed",
                Priority = "High",
                Status = "Open",
                RaisedBy = "Vishal",
                CreatedDate = DateTime.Now
            };

            _repositoryMock
                .Setup(repo => repo.CreateTicketAsync(ticket))
                .ReturnsAsync(10);

            // Act
            var result = await _controller.CreateTicket(ticket);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(10, okResult.Value);

            _repositoryMock.Verify(
                repo => repo.CreateTicketAsync(ticket),
                Times.Once);
        }

        [Fact]
        public async Task CreateTicket_ReturnsBadRequest_WhenTicketsNull()
        {
            // Arrange
            Ticket ticket = null!;

            // Act
            var result = await _controller.CreateTicket(ticket);

            // Assert
            Assert.IsType<BadRequestResult>(result);

            _repositoryMock.Verify(
                repo => repo.CreateTicketAsync(It.IsAny<Ticket>()),
                Times.Never);
        }

        [Fact]
        public async Task GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Title = "Laptop Issue",
                    Description = "Laptop is not working",
                    Priority = "High",
                    Status = "Open",
                    RaisedBy = "Vishal",
                    CreatedDate = DateTime.Now
                },
                new Ticket
                {
                    Id = 2,
                    Title = "VPN Issue",
                    Description = "VPN connection failed",
                    Priority = "Medium",
                    Status = "Open",
                    RaisedBy = "Rahul",
                    CreatedDate = DateTime.Now
                }
            };

            _repositoryMock
                .Setup(repo => repo.GetTicketsByStatusAsync("Open"))
                .ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetTicketsByStatus("Open");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTickets = Assert.IsType<List<Ticket>>(okResult.Value);

            Assert.Equal(2, returnedTickets.Count);
            Assert.All(returnedTickets, ticket => Assert.Equal("Open", ticket.Status));

            _repositoryMock.Verify(
                repo => repo.GetTicketsByStatusAsync("Open"),
                Times.Once);
        }
    }
}