using Endava.Internship2020.WebApiExamples.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Endava.Internship2020.WebApiExamples.Services.Data
{
    public interface IRepository<T> where T : class
    {
        IReadOnlyCollection<T> Entities { get; }

        T Add(T entity);

        T Update(T entity);

        void Delete(int id);

        void SaveChanges();
    }
}
