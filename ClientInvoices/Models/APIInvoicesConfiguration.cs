using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;

namespace ClientInvoices.Models
{
    public class APIInvoicesConfiguration : IAPIInvoicesConfiguration
    {
        public string BaseUrl { get; set; }
    }
}
