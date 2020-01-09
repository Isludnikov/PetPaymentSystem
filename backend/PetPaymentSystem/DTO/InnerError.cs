namespace PetPaymentSystem.DTO
{
    public enum InnerError
    {
        CommonError = 0,
        SessionAlreadyExists = 1,
        ValidationError = 2,
        SessionNotFound = 3,
        SessionExpired = 4,
        TerminalNotConfigured = 5,
        TerminalBlocked = 6
    }
}
