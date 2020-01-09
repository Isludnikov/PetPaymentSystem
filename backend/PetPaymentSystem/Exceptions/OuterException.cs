using PetPaymentSystem.DTO;
using System;

namespace PetPaymentSystem.Exceptions
{
    public class OuterException : Exception
    {
        public InnerError InnerError { get; }

        public OuterException(InnerError error, string message = "") : base(message)
        {
            InnerError = error;
        }
    }
}
