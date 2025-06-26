using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokEkstresi.DataAccess.Abstracts
{
    public interface ISTIRepository
    {
        Task<IEnumerable<STI?>> GetAllSTIS();
    }
}
