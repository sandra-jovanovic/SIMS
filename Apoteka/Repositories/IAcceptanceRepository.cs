using Apoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Repositories
{
    public interface IAcceptanceRepository
    {
        List<Acceptance> GetAllAcceptances();
        void CreateAcceptance(Acceptance acceptance);
        void DeleteAcceptance(Acceptance acceptance);
    }
}
