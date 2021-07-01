using APIInvoices;
using APIInvoices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUnits
{
    [TestClass]
    public class InMemoryCreditNotesControllerTest : CreditNotesControllerTest
    {
        public InMemoryCreditNotesControllerTest()
            : base(
                new DbContextOptionsBuilder<APIInvoicesContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options)
        {
        }
    }
}
