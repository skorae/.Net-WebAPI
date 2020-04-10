using System;
using System.Collections.Generic;
using System.Linq;
using Endava.Internship2020.WebApiExamples.Services.Data;
using Endava.Internship2020.WebApiExamples.Services.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Endava.Internship2020.WebApiExamples.Services
{
    public class TicketsService
    {
        private readonly IRepository<Ticket> ticketRepository;

        public TicketsService(IRepository<Ticket> ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public IReadOnlyCollection<Ticket> GetAll()
        {
            return this.ticketRepository.Entities;
        }

        public Ticket Get(int id)
        {
            return this.ticketRepository.Entities.FirstOrDefault(x => x.Id == id);
        }

        public bool Contains(int id)
        {
            return this.ticketRepository.Entities.Any(x => x.Id == id);
        }

        public Ticket Create(Ticket ticket)
        {
            var generatedId = this.ticketRepository.Entities.Max(t => t.Id) + 1;
            ticket.Id = generatedId;

            this.ticketRepository.Add(ticket);
            this.ticketRepository.SaveChanges();

            return ticket;
        }

        public Ticket Update(Ticket ticket)
        {
            var existingTicket = this.ticketRepository.Entities.FirstOrDefault(x => x.Id == ticket.Id);

            existingTicket.Owner = ticket.Owner;
            existingTicket.EventName = ticket.EventName;

            this.ticketRepository.Update(ticket);
            this.ticketRepository.SaveChanges();

            return ticket;
        }

        public Ticket Update(int id, JsonPatchDocument<Ticket> patch)
        {
            var existingTicket = this.ticketRepository.Entities.FirstOrDefault(x => x.Id == id);

            patch.ApplyTo(existingTicket);
                        
            this.ticketRepository.Update(existingTicket);
            this.ticketRepository.SaveChanges();

            return existingTicket;
        }

        public void Delete(int id)
        {
            this.ticketRepository.Delete(id);
            this.ticketRepository.SaveChanges();
        }
    }
}
