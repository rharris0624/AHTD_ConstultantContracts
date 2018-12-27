using System;
using ConsultantContracts.Infrastructure.DAL;
using ConsultantContracts.Infrastructure.Models;
using ConsultantContracts.Interfaces.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

                var result = repository.GetByKey<Contract>(keyValue);
                result.Consultant = repository.GetByKey<Consultant>(result.ConsultantId);

                Assert.IsNotNull(result.Consultant);
            }
        }
    }
}
