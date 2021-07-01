using APIInvoices;
using APIInvoices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUnits
{
    [TestClass]
    public class InMemoryInvoicesControllerTest : InvoicesControllerTest
    {
        public InMemoryInvoicesControllerTest()
            : base(
                new DbContextOptionsBuilder<APIInvoicesContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options)
        {
        }
    }
}
