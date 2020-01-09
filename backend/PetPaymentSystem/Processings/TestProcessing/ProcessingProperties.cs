using PetPaymentSystem.Library;

namespace TestProcessing
{
    class ProcessingProperties : IProcessingProperties
    {
        public bool AllowDebit { get; }
        public bool AllowCredit { get; }
        public bool AllowRefund { get; }
        public bool AllowHold { get; }
        public bool AllowPartialCharge { get; }
        public bool AllowPartialRetrieve { get; }

        internal ProcessingProperties(bool allowDebit , bool allowCredit, bool allowRefund, bool allowHold, bool allowPartialCharge, bool allowPartialRetrieve)
        {
            AllowCredit = allowCredit;
            AllowDebit = allowDebit;
            AllowRefund = allowRefund;
            AllowHold = allowHold;
            AllowRefund = allowRefund;
            AllowPartialCharge = allowPartialCharge;
            AllowPartialRetrieve = allowPartialRetrieve;
        }
    }
}
