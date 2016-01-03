﻿using System;
namespace Asp_Mvc_2015_2016.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Asp_Mvc_2015_2016.Models.Departement> DepartementRepository { get; }
        void Save();
        void Dispose();
    }
}
