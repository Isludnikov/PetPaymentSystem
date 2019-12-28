using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Processing
    {
        public Processing()
        {
            Operation = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public string ProcessingName { get; set; }

        public virtual ICollection<Operation> Operation { get; set; }
    }
}
