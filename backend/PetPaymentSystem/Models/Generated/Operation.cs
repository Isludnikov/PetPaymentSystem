using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Operation
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int SessionId { get; set; }
        public int ProcessingId { get; set; }
        public long Amount { get; set; }
        public long InvolvedAmount { get; set; }
        public int? OperationType { get; set; }

        public virtual Processing Processing { get; set; }
        public virtual Session Session { get; set; }
    }
}
