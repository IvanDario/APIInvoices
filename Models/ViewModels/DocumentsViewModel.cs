using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DocumentsViewModel : Document
    {
        public virtual string DocumentNumber { get; set; }

        public string DocumentType { get; set; }
    }
}
