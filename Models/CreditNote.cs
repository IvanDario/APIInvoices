using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class CreditNote : Document
    {
        [StringLength(255)]
        public string CreditNumber { get; set; }


    }
}
