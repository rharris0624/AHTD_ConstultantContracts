﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantContracts.Interfaces.DAL
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
