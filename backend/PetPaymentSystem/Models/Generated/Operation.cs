using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Operation
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int SessionId { get; set; }
        public int TerminalId { get; set; }
        public long Amount { get; set; }
        public long InvolvedAmount { get; set; }
        public int OperationType { get; set; }
        public int Status { get; set; }
        public string ProcessingOrderId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Session Session { get; set; }
        public virtual Terminal Terminal { get; set; }
    }
}
