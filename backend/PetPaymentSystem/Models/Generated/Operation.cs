using System;

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
        public string ProcessingOrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public string MaskedPan { get; set; }
        public int? ExpireMonth { get; set; }
        public int? ExpireYear { get; set; }
        public virtual Session Session { get; set; }
        public virtual Terminal Terminal { get; set; }
        public virtual Operation3ds Operation3ds { get; set; }
    }
}
