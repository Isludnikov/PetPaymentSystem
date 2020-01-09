namespace PetPaymentSystem.Library
{
    public interface IProcessingProperties
    {
        bool AllowDebit { get; }
        bool AllowCredit { get; }
        bool AllowRefund { get; }
        bool AllowHold { get; }
        bool AllowPartialCharge { get; }
        bool AllowPartialRetrieve { get; }

    }
}
