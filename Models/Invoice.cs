using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Invoice : Document
    {
        [StringLength(255)]
        public string InvoiceNumber { get; set; }


    }
}
