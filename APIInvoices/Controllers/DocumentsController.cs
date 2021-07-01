using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIInvoices.Data;
using Models;
using Unity.Policy;

namespace APIInvoices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly APIInvoicesContext _context;

        public DocumentsController(APIInvoicesContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public IEnumerable<DocumentsViewModel> GetDocuments()
        {
            List<DocumentsViewModel> documents = _context.Invoice.ToList().ConvertAll(new Converter<Invoice, DocumentsViewModel>(ConvertToDocument));
            documents.AddRange(_context.CreditNote.ToList().ConvertAll(new Converter<CreditNote, DocumentsViewModel>(ConvertToDocument)));


            return documents.OrderBy(x => x.CreatedAt).ThenBy(x => x.DocumentNumber);
        }

        private DocumentsViewModel ConvertToDocument(Invoice input)
        {

            DocumentsViewModel document = new DocumentsViewModel() {
                Id = input.Id,
                DocumentNumber = input.InvoiceNumber,
                Value = input.Value, 
                CreatedAt = input.CreatedAt,
                DocumentType = "Invoice"
            };

            return document;
        }

        private DocumentsViewModel ConvertToDocument(CreditNote input)
        {

            DocumentsViewModel document = new DocumentsViewModel()
            {
                Id = input.Id,
                DocumentNumber = input.CreditNumber,
                Value = input.Value,
                CreatedAt = input.CreatedAt,
                DocumentType = "Credit Note"
            };

            return document;
        }


    }
}
