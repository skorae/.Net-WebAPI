using System.Collections.Generic;
using System.Linq;
using Endava.Internship2020.WebApiExamples.Services;
using Endava.Internship2020.WebApiExamples.Services.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Endava.Internship2020.WebApiExamples.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TicketsController : Controller
    {
        private readonly TicketsService ticketsService;

        public TicketsController(TicketsService ticketsService)
        {
            this.ticketsService = ticketsService;
        }

        [HttpGet]
        public IActionResult GetAllTickets()
        {
            var tickets = ticketsService.GetAll();

            if (tickets.Count == 0)
            {
                return NotFound("Unable to find any tickets");
            }

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (ticketsService.Contains(id))
            {
                var ticket = ticketsService.Get(id);
                return Ok(ticket);
            }
            else
                return NotFound($"Unable to find ticket with id {id}");
        }

        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            var createdTicket = ticketsService.Create(ticket);

            return Ok(createdTicket);
        }

        [HttpPut]
        public IActionResult Update(Ticket ticket)
        {
            if (ticketsService.Contains(ticket.Id))
            {
                var updatedTicket = ticketsService.Update(ticket);
                return Ok(updatedTicket);
            }
            else
                return NotFound($"Unable to find ticket with id {ticket.Id}");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, JsonPatchDocument<Ticket> patch)
        {
            if (ticketsService.Contains(id))
            {
                var updatedTicket = ticketsService.Update(id, patch);
                return Ok(updatedTicket);
            }
            else
                return NotFound($"Unable to find ticket with id {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ticketsService.Contains(id))
            {
                ticketsService.Delete(id);
                return NoContent();
            }
            else
                return NotFound($"Unable to find ticket with id {id}");
        }
    }
}