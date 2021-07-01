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

    public abstract class CreditNotesControllerTest
    {
        #region Seeding
        protected CreditNotesControllerTest(DbContextOptions<APIInvoicesContext> contextOptions)
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

                var two = new CreditNote();
                two.CreditNumber = "CRNC002";
                two.Value = 121;


                var three = new CreditNote();
                three.CreditNumber = "CRNC003";
                three.Value = 100;


                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }
        #endregion

        #region CanGetCreditNotes
        [TestMethod]
        public async Task Can_get_creditNotesAsync()
        {
            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new CreditNotesController(context);

                var rs = await controller.GetCreditNotes();

                List<CreditNote> items = rs.Value.OrderBy(x => x.CreatedAt).ToList();

                Assert.AreEqual(3, items.Count);
                Assert.AreEqual("CRNC001", items[0].CreditNumber);
                Assert.AreEqual("CRNC002", items[1].CreditNumber);
                Assert.AreEqual("CRNC003", items[2].CreditNumber);
            }
        }
        #endregion

        #region Rejections
        [TestMethod]
        public async Task Reject_CreditNote_NoCreditNumber_Async()
        {
            // Arrange:

            List<CreditNote> creditNotes = new List<CreditNote>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                CreditNumber = String.Empty,
                Value = 100.34M
            };

            creditNotes.Add(creditNote);

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new CreditNotesController(context);
                //async Task act() => creditNoteResult = await dc.PostCreditNote(creditNote);
                var rs = await controller.PostCreditNotes(creditNotes);
                var creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.AreEqual("Credit Note Number is Required", creditNotesResult);
            }
        }

        [TestMethod]
        public async Task Reject_RepeatedCreditNumberInBatch_Async()
        {
            // Arrange:

            List<CreditNote> creditNotes = new List<CreditNote>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                CreditNumber = "A1",
                Value = 100.34M
            };

            creditNotes.Add(creditNote);
            creditNotes.Add(creditNote);

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new CreditNotesController(context);
                //async Task act() => creditNoteResult = await dc.PostCreditNote(creditNote);
                var rs = await controller.PostCreditNotes(creditNotes);
                var creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(creditNotesResult.ToString().Contains("Duplicated CreditNotes"));
            }
        }


        [TestMethod]
        public async Task Reject_RepeatedCreditNumberInDB_Async()
        {
            // Arrange:

            List<CreditNote> creditNotes = new List<CreditNote>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                CreditNumber = "A1",
                Value = 100.34M
            };

            creditNotes.Add(creditNote);


            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new CreditNotesController(context);
                var rs = await controller.PostCreditNotes(creditNotes);
                var creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                rs = await controller.PostCreditNotes(creditNotes);
                creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(creditNotesResult.ToString().Contains("Duplicated CreditNotes"));
            }
        }


        [TestMethod]
        public async Task Reject_ZeroValueCreditNote_Async()
        {
            // Arrange:

            List<CreditNote> creditNotes = new List<CreditNote>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                CreditNumber = "CRN-A00010",
                Value = 0M
            };

            creditNotes.Add(creditNote);


            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new CreditNotesController(context);
                var rs = await controller.PostCreditNotes(creditNotes);
                var creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(creditNotesResult.ToString().Contains("value needs to be greater than zero"));
            }
        }

        [TestMethod]
        public async Task Reject_NegativeValueCreditNote_Async()
        {
            // Arrange:

            List<CreditNote> creditNotes = new List<CreditNote>();

            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                CreditNumber = "CRN-N00010",
                Value = -10M
            };
            creditNotes.Add(creditNote);

            // Act:

            using (var context = new APIInvoicesContext(ContextOptions))
            {
                var controller = new CreditNotesController(context);
                var rs = await controller.PostCreditNotes(creditNotes);
                var creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert
                Assert.IsTrue(creditNotesResult.ToString().Contains("value needs to be greater than zero"));
            }
        }



        #endregion

        #region CanAddCreditNotes
        [TestMethod]
        public async Task TASK2_Can_add_creditNotesAsync()
        {
            // Arrange:
            List<CreditNote> creditNotes = new List<CreditNote>();


            for (int i = 1; i < 20; i++)
            {
                string twoCharacterCounter = ("00" + i.ToString()).PadRight(2);
                var creditNote = new CreditNote
                {
                    CreditNumber = $"CRN-000{twoCharacterCounter}",
                    Value = i * 100.34M
                };

                creditNotes.Add(creditNote);
            }


            using (var context = new APIInvoicesContext(ContextOptions))
            {

                // Act:
                var controller = new CreditNotesController(context);

                var rs = await controller.PostCreditNotes(creditNotes);

                var creditNotesResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)rs.Result).Value;

                // Assert:
                Assert.AreEqual(19, ((List<CreditNote>)creditNotesResult).Count);
            }

        }
        #endregion


    }
}
