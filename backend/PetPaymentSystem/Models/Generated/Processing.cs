using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Processing
    {
        public Processing()
        {
            Terminal = new HashSet<Terminal>();
        }

        public int Id { get; set; }
        public string ProcessingName { get; set; }
        public string LibraryName { get; set; }
        public string Namespace { get; set; }

        public virtual ICollection<Terminal> Terminal { get; set; }
    }
}
