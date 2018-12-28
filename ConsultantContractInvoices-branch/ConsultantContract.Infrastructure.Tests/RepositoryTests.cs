using System;
using System.Linq;
using ConsultantContracts.Infrastructure.DAL;
using ConsultantContracts.Infrastructure.Models;
using ConsultantContracts.Interfaces.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsultantContracts.Infrastructure.Helpers;
using System.Collections.Generic;

namespace ConsultantContract.Infrastructure.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void TestContractGetByName()
        {
            using (var context = new ConsultantContractsEntities())
            {
                IRepository repository = new Repository(context);
                var keyValue = 17;

                var result = repository.GetByKey<Contract>(keyValue);

                Assert.IsTrue(result != null);

            }
        }
        [TestMethod]
        public void TestGetContractByNameAndIncludeConsultant()
        {
            using (var context = new ConsultantContractsEntities())
            {
                IRepository repository = new Repository(context);
                var keyValue = 17;

                var result = repository.GetQuery<Contract>(p => p.ContractCode.Equals(keyValue)).IncludeMultiple(p => p.Consultant).FirstOrDefault();
                Assert.IsNotNull(result.Consultant);

            }
        }
        [TestMethod]
        public void TestGetContractByNameAndIncludeInvoices()
        {
            using (var context = new ConsultantContractsEntities())
            {
                IRepository repository = new Repository(context);
                var keyValue = 17;

                var result = repository.GetQuery<Contract>(p => p.ContractCode.Equals(keyValue)).IncludeMultiple(p => p.Invoices).FirstOrDefault();
                Assert.IsNotNull(result.Invoices);

            }
        }
        [TestMethod]
        public void TestGetContractByNameAndIncludeInvoicesAndConsultant()
        {
            using (var context = new ConsultantContractsEntities())
            {
                IRepository repository = new Repository(context);
                var keyValue = 17;

                var result = repository.GetQuery<Contract>(p => p.ContractCode.Equals(keyValue)).IncludeMultiple(
                                                                                                    p => p.Invoices, 
                                                                                                    d => d.Consultant ).FirstOrDefault();
                Assert.IsTrue(result.Invoices != null && result.Consultant != null);

            }
        }
    }
}
