using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIInvoices;
using APIInvoices.Controllers;
using APIInvoices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;


namespace TestUnits
{

    public abstract class DocumentsControllerTest
    {
        #region Seeding
        protected DocumentsControllerTest(DbContextOptions<APIInvoicesContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<APIInvoicesContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new APIInvoicesContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new CreditNote();
                one.CreditNumber = "CRNC001";
                one.Value = 100;
                context.Add(one);
                context.SaveChanges();


                var two = new CreditNote();
                two.CreditNumber = "CRNC002";
                two.Value = 121;
                context.Add(two);
                context.SaveChanges();

                var three = new CreditNote();
                three.CreditNumber = "CRNC003";
                three.Value = 100;
                context.Add(three);
                context.SaveChanges();

                var oneI = new Invoice();
                oneI.InvoiceNumber = "INVC001";
                oneI.Value = 100;
                context.Add(oneI);
                context.SaveChanges();

                var twoI = new Invoice();
                twoI.InvoiceNumber = "INVC002";
                twoI.Value = 121;
                context.Add(twoI);
                context.SaveChanges();

                var threeI = new Invoice();
                threeI.InvoiceNumber = "INVC003";
                threeI.Value = 100;
                context.Add(threeI);
                context.SaveChanges();

            }
        }
        #endregion

        #region CanGetCreditNotes
        [TestMethod]
        public async Task TASK3_Can_get_AllDocumentsAsync()
        {
            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new DocumentsController(context);

                var rs = controller.GetDocuments();

                List<DocumentsViewModel> items = rs.ToList();

                Assert.AreEqual(6, items.Count);
                Assert.AreEqual("CRNC001", items[0].DocumentNumber);
                Assert.AreEqual("CRNC002", items[1].DocumentNumber);
                Assert.AreEqual("CRNC003", items[2].DocumentNumber);
                Assert.AreEqual("INVC001", items[3].DocumentNumber);
                Assert.AreEqual("INVC002", items[4].DocumentNumber);
                Assert.AreEqual("INVC003", items[5].DocumentNumber);

            }
        }
        #endregion


    }
}
