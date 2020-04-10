using Endava.Internship2020.WebApiExamples.Services.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Endava.Internship2020.WebApiExamples.Services.Data
{
    public class TicketRepository : IRepository<Ticket>
    {
        private const string FileName = @"\tickets.json";
        private readonly string path;

        private readonly List<Ticket> tickets = new List<Ticket>
        {
            new Ticket { Id = 1, EventName = "Metallica Plovdiv 2020", Owner = "Georgi Petrov" },
            new Ticket { Id = 2, EventName = "Sofia Rock 2021", Owner = "Ivan Ivanov" }
        };

        public TicketRepository()
        {
            this.path = Directory.GetCurrentDirectory() + FileName;
            this.Initiate();
        }

        public IReadOnlyCollection<Ticket> Entities
            => this.tickets.AsReadOnly();

        public Ticket Add(Ticket ticket)
        {
            this.tickets.Add(ticket);
            return ticket;
        }

        public Ticket Update(Ticket ticket)
        {
            this.tickets[this.tickets.IndexOf(ticket)] = ticket;
            return ticket;
        }

        public void Delete(int id)
        {
            this.tickets.RemoveAll(x => x.Id == id);
        }

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(this.tickets.OrderBy(x => x.Id).ToList(), Formatting.Indented);
            File.WriteAllText(this.path, json);
        }

        private void Initiate()
        {
            if (!File.Exists(this.path))
            {
                File.WriteAllText(this.path, JsonConvert.SerializeObject(this.tickets, Formatting.Indented));
            }
            else
            {
                this.tickets.Clear();
                this.tickets.AddRange(this.Deserialize());
            }

        }

        private List<Ticket> Deserialize()
        {
            var json = File.ReadAllText(this.path);
            return JsonConvert.DeserializeObject<List<Ticket>>(json);
        }
    }
}
