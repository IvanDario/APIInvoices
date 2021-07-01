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
   
    public abstract class InvoicesControllerTest
    {
        #region Seeding
        protected InvoicesControllerTest(DbContextOptions<APIInvoicesContext> contextOptions)
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

                var one = new Invoice();
                one.InvoiceNumber = "INVC001";
                one.Value = 100;

                var two = new Invoice();
                two.InvoiceNumber = "INVC002";
                two.Value = 121;


                var three = new Invoice();
                three.InvoiceNumber = "INVC003";
                three.Value = 100;


                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }
        #endregion

        #region CanGetInvoices
        [TestMethod]
        public async Task Can_get_invoicesAsync()
        {
            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new InvoicesController(context);

                var rs = await controller.GetInvoices();

                List<Invoice> items = rs.Value.OrderBy(x => x.CreatedAt).ToList();

                Assert.AreEqual(3, items.Count);
                Assert.AreEqual("INVC001", items[0].InvoiceNumber);
                Assert.AreEqual("INVC002", items[1].InvoiceNumber);
                Assert.AreEqual("INVC003", items[2].InvoiceNumber);
            }
        }
        #endregion

        #region Rejections
        [TestMethod]
        public async Task Reject_Invoice_NoInvoiceNumber_Async()
        {
            // Arrange:

            List<Invoice> invoices = new List<Invoice>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                InvoiceNumber = String.Empty,
                Value = 100.34M
            };

            invoices.Add(invoice);

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new InvoicesController(context);
                //async Task act() => invoiceResult = await dc.PostInvoice(invoice);
                var rs = await controller.PostInvoices(invoices);
                var invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.AreEqual("Invoice Number is Required", invoicesResult);
            }
        }

        [TestMethod]
        public async Task Reject_RepeatedInvoiceNumberInBatch_Async()
        {
            // Arrange:

            List<Invoice> invoices = new List<Invoice>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                InvoiceNumber = "A1",
                Value = 100.34M
            };

            invoices.Add(invoice);
            invoices.Add(invoice);

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new InvoicesController(context);
                //async Task act() => invoiceResult = await dc.PostInvoice(invoice);
                var rs = await controller.PostInvoices(invoices);
                var invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(invoicesResult.ToString().Contains("Duplicated Invoices"));
            }
        }


        [TestMethod]
        public async Task Reject_RepeatedInvoiceNumberInDB_Async()
        {
            // Arrange:

            List<Invoice> invoices = new List<Invoice>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                InvoiceNumber = "A1",
                Value = 100.34M
            };

            invoices.Add(invoice);
            

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new InvoicesController(context);
                var rs = await controller.PostInvoices(invoices);
                var invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                rs = await controller.PostInvoices(invoices);
                invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(invoicesResult.ToString().Contains("Duplicated Invoices"));
            }
        }


        [TestMethod]
        public async Task Reject_ZeroValueInvoice_Async()
        {
            // Arrange:

            List<Invoice> invoices = new List<Invoice>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                InvoiceNumber = "INV-A00010",
                Value = 0M
            };

            invoices.Add(invoice);


            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new InvoicesController(context);
                var rs = await controller.PostInvoices(invoices);
                var invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(invoicesResult.ToString().Contains("value needs to be greater than zero"));
            }
        }

        [TestMethod]
        public async Task Reject_NegativeValueInvoice_Async()
        {
            // Arrange:

            List<Invoice> invoices = new List<Invoice>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                InvoiceNumber = "INV-N00010",
                Value = -10M
            };
            invoices.Add(invoice);

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new InvoicesController(context);
                var rs = await controller.PostInvoices(invoices);
                var invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(invoicesResult.ToString().Contains("value needs to be greater than zero"));
            }
        }



        #endregion

        #region CanAddInvoices
        [TestMethod]
        public async Task TASK1_Can_add_invoicesAsync()
        {
            // Arrange:
            List<Invoice> invoices = new List<Invoice>();


            for (int i = 1; i < 20; i++)
            {
                string twoCharacterCounter = ("00" + i.ToString()).PadRight(2);
                var invoice = new Invoice
                {
                    InvoiceNumber = $"INV-000{twoCharacterCounter}",
                    Value = i * 100.34M
                };

                invoices.Add(invoice);
            }

            
            using (var context = new APIInvoicesContext(ContextOptions))
            {

                // Act:
                var controller = new InvoicesController(context);
                
                var rs = await controller.PostInvoices(invoices);

                var invoicesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert:
                Assert.AreEqual(19, ((List<Invoice>)invoicesResult).Count);
            }

        }
        #endregion

    
    }
}
