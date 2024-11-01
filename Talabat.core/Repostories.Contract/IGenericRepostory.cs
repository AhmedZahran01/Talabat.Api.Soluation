using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Repostories.Contract
{
    public interface IGenericRepostory<T> where T : BaseEntity
    {
        Task<T> GetAsync(int Id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
