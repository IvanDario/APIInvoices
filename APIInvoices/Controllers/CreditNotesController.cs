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
    public class CreditNotesController : ControllerBase
    {
        private readonly APIInvoicesContext _context;

        public CreditNotesController(APIInvoicesContext context)
        {
            _context = context;
        }

        // GET: api/CreditNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditNote>>> GetCreditNotes()
        {
            return await _context.CreditNote.ToListAsync();
        }

        // GET: api/CreditNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditNote>> GetCreditNote(Guid id)
        {
            var CreditNote = await _context.CreditNote.FindAsync(id);

            if (CreditNote == null)
            {
                return NotFound();
            }

            return CreditNote;
        }

        // PUT: api/CreditNotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditNote(Guid id, CreditNote creditNote)
        {
            if (id != creditNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(creditNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditNoteExists(id))
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

        // POST: api/CreditNotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<CreditNote>> PostCreditNote(CreditNote creditNote)
        //{
        //    string message="";
        //    if (CompleteAndEvaluate(CreditNote, out message))
        //    {
        //        _context.creditNote.Add(CreditNote);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetCreditNote", new { id = creditNote.Id }, CreditNote);
        //    }
        //    else
        //        return BadRequest(message);
        //}


        [HttpPost]
        public async Task<ActionResult<List<CreditNote>>> PostCreditNotes(List<CreditNote> creditNotes)
        {
            List<CreditNote> CreditNotesResult = new List<CreditNote>();
            string message = "";

            List<string> duplicatedCreditNotes = await _context.CreditNote.Where(x => creditNotes.Select(y => y.CreditNumber).Contains(x.CreditNumber)).Select(x => x.CreditNumber).ToListAsync();

            var duplicates = creditNotes.GroupBy(s => s.CreditNumber)
                     .Where(g => g.Count() > 1)
                     .Select(g => g.Key); // or .SelectMany(g => g)

            duplicatedCreditNotes.AddRange(duplicates.ToList());


            if (duplicatedCreditNotes.Count > 0)
            {
                message = "Duplicated CreditNotes:" + String.Join(",", duplicatedCreditNotes);
                return BadRequest(message);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach (CreditNote creditNote1 in creditNotes)
                {

                    if (CompleteAndEvaluate(creditNote1, out message))
                    {
                        try
                        {
                            _context.CreditNote.Add(creditNote1);
                            await _context.SaveChangesAsync();
                            CreditNotesResult.Add(creditNote1);
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

            return CreatedAtAction("GetCreditNotes", CreditNotesResult);
        }


        private bool CompleteAndEvaluate(CreditNote creditNote, out string message)
        {

            if (String.IsNullOrWhiteSpace(creditNote.CreditNumber))
            {
                message = "Credit Note Number is Required";
                return false;
            }

            if (String.IsNullOrWhiteSpace(creditNote.CreditNumber))
            {
                message = "Credit Note Number is Required";
                return false;
            }

            if (!(creditNote.Value > 0))
            {
                message = "The value needs to be greater than zero, Credit Note:" + creditNote.CreditNumber;
                return false;
            }


            creditNote.CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
            creditNote.Id = Guid.NewGuid();


            message = "OK";
            return true;

        }

        // DELETE: api/CreditNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditNote(Guid id)
        {
            var CreditNote = await _context.CreditNote.FindAsync(id);
            if (CreditNote == null)
            {
                return NotFound();
            }

            _context.CreditNote.Remove(CreditNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditNoteExists(Guid id)
        {
            return _context.CreditNote.Any(e => e.Id == id);
        }
    }
}

