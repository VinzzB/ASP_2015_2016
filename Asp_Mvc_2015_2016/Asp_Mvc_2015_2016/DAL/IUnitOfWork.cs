using Asp_Mvc_2015_2016.Models;
using System;
namespace Asp_Mvc_2015_2016.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Asp_Mvc_2015_2016.Models.Departement> DepartementRepository { get; }
        GenericRepository<Asp_Mvc_2015_2016.Models.Klant> KlantRepository { get; }
        GenericRepository<Asp_Mvc_2015_2016.Models.Factuur> FactuurRepository { get; }
        //GenericRepository<Asp_Mvc_2015_2016.Models.FactuurDetails> FactuurDetailsRepository { get; }
        RolesRepository RolesRepository { get; }
        GebruikerRepository GebruikerRepository { get; }
        GenericRepository<DepartementGebruiker> GebruikerDepartementRepository { get; }
        GenericRepository<DepartementKlant> DepartementKlantRepository { get; }
        UurRegistratieRepository UurRegistratieRepository { get; }
        FactuurDetailsRepository FactuurDetailsRepository { get; }
        GenericRepository<TypeWerk> TypeWerkRepository { get; }
        GenericRepository<Factuur> FactuurRepository { get; }
        void Save();
        void Dispose();
        
    }
}
