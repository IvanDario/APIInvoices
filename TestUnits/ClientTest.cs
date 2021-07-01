using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientInvoices;
using ClientInvoices.Models;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TestUnits
{
    [TestClass]
    public class ClientTest
    {

        APIInvoicesClient dc { get; set; }

        public ClientTest()
        {
            dc = new APIInvoicesClient(new APIInvoicesConfiguration()
            {
                BaseUrl = "https://localhost:5001/"
            });
        }



        [TestMethod]
        [Ignore]
        public async Task can_List_And_DELETE_AllDocuments()
        {
            // Arrange:
            List<DocumentsViewModel> documents = await dc.GetAllDocuments();

            // Act:
            foreach (DocumentsViewModel document in documents)
            {
                switch (document.DocumentType)
                {
                    case "Invoice":
                        await dc.DeleteInvoice(document.Id);
                        break;
                    case "Credit Note":
                        await dc.DeleteCreditNote(document.Id);
                        break;
                }
            }

            List<DocumentsViewModel> documents2 = await dc.GetAllDocuments();

            // Assert:
            Assert.AreEqual(0, documents2.Count);
        }



        [TestMethod]
        public async Task can_get_InvoiceAsync()
        {
            // Arrange:
            var Guid = System.Guid.NewGuid();
            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                Id = Guid,
                InvoiceNumber = $"INV-{prefix}B01",
                Value = 100
            };

            // Act:
            invoice = await dc.PostInvoice(invoice);

            // Assert:
            Assert.AreEqual(invoice.InvoiceNumber, $"INV-{prefix}B01");
            Assert.IsNotNull(invoice.Id);
            Assert.IsNotNull(invoice.CreatedAt);
            Assert.AreNotEqual(invoice.CreatedAt, 0);
            Assert.AreNotEqual(Guid, invoice.Id);
        }


        [TestMethod]
        public async Task Reject_Invoice_NoInvoiceNumer_Async() {

            // Arrange:
            var Guid = System.Guid.NewGuid();
            Invoice invoiceResult;

            var invoice = new Invoice
            {
                Id = Guid,
                InvoiceNumber = "",
                Value = 100
            };

            // Act:
            async Task act() => invoiceResult = await dc.PostInvoice(invoice); 

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(act);
        }

        [TestMethod]
        public async Task TASK1_can_Add_Invoice_BatchAsync()
        {
            // Arrange:
            List<Invoice> invoices = new List<Invoice>();
            List<Invoice> invoicesResult = new List<Invoice>();
            string prefix = DateTime.Now.ToString("MMddhhmmss");
            for (int i = 1; i < 20; i++) {
                string twoCharacterCounter = ("00" + i.ToString()).PadRight(2);
                var invoice = new Invoice
                {
                    InvoiceNumber = $"INV-{prefix}000{twoCharacterCounter}",
                    Value = i * 100.34M
                };

                invoices.Add(invoice);
            }


            // Act:
            invoicesResult = await dc.PostInvoices(invoices);

            // Assert:
            Assert.AreEqual(19, invoicesResult.Count );
        }


        [TestMethod]
        public async Task can_Add_SingleCreditNoteAsync()
        {
            // Arrange:
            var Guid = System.Guid.NewGuid();
            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                Id = Guid,
                CreditNumber = $"CRN-{prefix}A01",
                Value = 100
            };

            // Act:
            creditNote = await dc.PostCreditNote(creditNote);

            // Assert
            Assert.AreEqual(creditNote.CreditNumber, $"CRN-{prefix}A01");
            Assert.IsNotNull(creditNote.Id);
            Assert.IsNotNull(creditNote.CreatedAt);
            Assert.AreNotEqual(creditNote.CreatedAt, 0);
            Assert.AreNotEqual(Guid, creditNote.Id);
        }


        [TestMethod]
        public async Task can_Add_SingleInvoiceAsync()
        {
            // Arrange:
            var Guid = System.Guid.NewGuid();
            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var invoice = new Invoice
            {
                Id = Guid,
                InvoiceNumber = $"INV-{prefix}A01",
                Value = 100
            };

            // Act:
            invoice = await dc.PostInvoice(invoice);

            // Assert
            Assert.AreEqual(invoice.InvoiceNumber, $"INV-{prefix}A01");
            Assert.IsNotNull(invoice.Id);
            Assert.AreNotEqual(invoice.CreatedAt, 0);
            Assert.IsNotNull(invoice.CreatedAt);
            Assert.AreNotEqual(invoice.CreatedAt, 0);
            Assert.AreNotEqual(Guid, invoice.Id);
        }




        [TestMethod]
        public async Task Reject_CreditNote_No_CreditNoteNumer_ReturnExceptionAsync()
        {

            // Arrange:
            var Guid = System.Guid.NewGuid();
            CreditNote creditNoteResult;

            var creditNote = new CreditNote
            {
                Id = Guid,
                CreditNumber = "",
                Value = 100
            };

            // Act:
            async Task act() => creditNoteResult = await dc.PostCreditNote(creditNote);

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(act);
        }

        [TestMethod]
        public async Task TASK2_can_Add_CreditNote_BatchAsync()
        {
            // Arrange:
            List<CreditNote> creditNotes = new List<CreditNote>();
            List<CreditNote> creditNotesResult = new List<CreditNote>();
            string prefix = DateTime.Now.ToString("MMddhhmmss");
            for (int i = 1; i < 20; i++)
            {
                string twoCharacterCounter = ("00" + i.ToString()).PadRight(2);
                var creditNote = new CreditNote
                {
                    CreditNumber = $"CRN-{prefix}01{twoCharacterCounter}",
                    Value = i * 100.34M
                };

                creditNotes.Add(creditNote);
            }


            // Act:
            creditNotesResult = await dc.PostCreditNotes(creditNotes);

            // Assert
            Assert.AreEqual(19, creditNotesResult.Count);
        }


        [TestMethod]
        public async Task TASK3_can_List_AllDocuments()
        {
            // Arrange:
            List<DocumentsViewModel> documents = await dc.GetAllDocuments();

            // Prepare at least one Invoice and one Credit Note:
            var Guid = System.Guid.NewGuid();
            string prefix = DateTime.Now.ToString("MMddhhmmss");
            var creditNote = new CreditNote
            {
                Id = Guid,
                CreditNumber = $"CRN-{prefix}A01",
                Value = 100
            };
            creditNote = await dc.PostCreditNote(creditNote);

            var invoice = new Invoice
            {
                Id = Guid,
                InvoiceNumber = $"INV-{prefix}A01",
                Value = 100
            };
            invoice = await dc.PostInvoice(invoice);

            DocumentsViewModel documentsViewInvoice = new DocumentsViewModel()
            {
                Id = invoice.Id,
                CreatedAt = invoice.CreatedAt,
                DocumentNumber = invoice.InvoiceNumber,
                DocumentType = "Invoice", 
                Value = invoice.Value
            };

            DocumentsViewModel documentsViewCreditNote = new DocumentsViewModel()
            {
                Id = creditNote.Id,
                CreatedAt = creditNote.CreatedAt,
                DocumentNumber = creditNote.CreditNumber,
                DocumentType = "Credit Note",
                Value = invoice.Value
            };

            // Act:
            List<DocumentsViewModel> documents2 = await dc.GetAllDocuments();

            // Assert:
            Assert.AreNotEqual(0, documents2.Count);
            Assert.AreNotEqual(0, documents2.IndexOf(documentsViewInvoice));
            Assert.AreNotEqual(0, documents2.IndexOf(documentsViewCreditNote));
        }



    }
}
