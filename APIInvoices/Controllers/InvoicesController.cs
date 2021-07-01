using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIInvoices.Data;
using Models;


namespace APIInvoices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly APIInvoicesContext _context;

        public InvoicesController(APIInvoicesContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoice.ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _context.Invoice.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        //{
        //    string message="";
        //    if (CompleteAndEvaluate(invoice, out message))
        //    {
        //        _context.Invoice.Add(invoice);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        //    }
        //    else
        //        return BadRequest(message);
        //}


        [HttpPost]
        public async Task<ActionResult<List<Invoice>>> PostInvoices(List<Invoice> invoices)
        {
            List<Invoice> invoicesResult = new List<Invoice>();
            string message = "";

            List<string> duplicatedInvoices = await _context.Invoice.Where(x => invoices.Select(y => y.InvoiceNumber).Contains(x.InvoiceNumber)).Select(x => x.InvoiceNumber).ToListAsync();


            var duplicates = invoices.GroupBy(s => s.InvoiceNumber)
                     .Where(g => g.Count() > 1)
                     .Select(g => g.Key); // or .SelectMany(g => g)

            duplicatedInvoices.AddRange(duplicates.ToList());
            if (duplicatedInvoices.Count >0 )
            {
                message = "Duplicated Invoices:" + String.Join(",", duplicatedInvoices);
                return BadRequest(message);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach (Invoice invoice1 in invoices)
                {

                    if (CompleteAndEvaluate(invoice1, out message))
                    {
                        try
                        {
                            _context.Invoice.Add(invoice1);
                            await _context.SaveChangesAsync();
                            invoicesResult.Add(invoice1);
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            return BadRequest(e.Message);

                        }
                    }
                    else
                    {
                        transaction.Rollback();
                        return BadRequest(message);
                    }
                }

                await transaction.CommitAsync();
            }

            return CreatedAtAction("GetInvoices", invoicesResult );
        }


        private bool CompleteAndEvaluate(Invoice invoice, out string message ) {

            if (String.IsNullOrWhiteSpace(invoice.InvoiceNumber)) {
                message = "Invoice Number is Required";
                return false;
            }

            if (String.IsNullOrWhiteSpace(invoice.InvoiceNumber))
            {
                message = "Invoice Number is Required";
                return false;
            }

            if (!(invoice.Value > 0))
            {
                message = "The value needs to be greater than zero, Invoice:" + invoice.InvoiceNumber;
                return false;
            }


            invoice.CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
            invoice.Id = Guid.NewGuid();


            message = "OK";
            return true;

        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(Guid id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }
    }
}
