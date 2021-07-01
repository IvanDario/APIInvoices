using Models.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ClientInvoices
{
    public class APIInvoicesClient
    {
        HttpClient client = null;

        public APIInvoicesClient(IAPIInvoicesConfiguration config)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(config.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public APIInvoicesClient(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<List<Invoice>> PostInvoices(List<Invoice> invoices)
        {
            var rs = await client.PostAsJsonAsync("/api/Invoices", invoices);
            if (rs.IsSuccessStatusCode)
            {
                return await rs.Content.ReadAsAsync<List<Invoice>>();
            }
            else
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new ArgumentException(msg);
            }
        }

        public async Task<Invoice> PostInvoice(Invoice invoice)
        {
            List<Invoice> invoices = new List<Invoice>(){ invoice };

            var rs = await client.PostAsJsonAsync("/api/Invoices", invoices);
            if (rs.IsSuccessStatusCode)
            {
                return (await rs.Content.ReadAsAsync<List<Invoice>>()).FirstOrDefault();
            }
            else
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new Exception(msg);
            }
        }


        public async Task DeleteInvoice(Guid id) {
            var rs = await client.DeleteAsync($"/api/Invoices/{id}");
            if (!rs.IsSuccessStatusCode)
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new Exception(msg);
            }
        }

        public async Task<List<CreditNote>> PostCreditNotes(List<CreditNote> creditNotes)
        {
            var rs = await client.PostAsJsonAsync("/api/CreditNotes", creditNotes);
            if (rs.IsSuccessStatusCode)
            {
                return await rs.Content.ReadAsAsync<List<CreditNote>>();
            }
            else
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new ArgumentException(msg);
            }
        }

        public async Task<CreditNote> PostCreditNote(CreditNote creditNote)
        {
            List<CreditNote> creditNotes = new List<CreditNote>() { creditNote };

            var rs = await client.PostAsJsonAsync("/api/CreditNotes", creditNotes);
            if (rs.IsSuccessStatusCode)
            {
                return (await rs.Content.ReadAsAsync<List<CreditNote>>()).FirstOrDefault();
            }
            else
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new Exception(msg);
            }
        }


        public async Task DeleteCreditNote(Guid id)
        {
            var rs = await client.DeleteAsync($"/api/CreditNotes/{id}");
            if (!rs.IsSuccessStatusCode)
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new Exception(msg);
            }
        }

        public async Task<List<DocumentsViewModel>> GetAllDocuments() {
            var rs = await client.GetAsync("/api/Documents");

            if (rs.IsSuccessStatusCode)
            {
                return (await rs.Content.ReadAsAsync<List<DocumentsViewModel>>());
            }
            else
            {
                var body = await rs.Content.ReadAsStringAsync();
                var msg = rs.StatusCode + ":" + body;
                throw new Exception(msg);
            }

        }

    }
}
