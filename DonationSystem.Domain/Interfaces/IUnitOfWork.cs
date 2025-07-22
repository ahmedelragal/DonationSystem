using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<User> Users { get; }
        //IRepository<Donation> Donations { get; }
        IRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
    }
}
