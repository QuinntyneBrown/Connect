namespace Connect.Core.Common
{
    public enum OrderStatuses
    {
        Started = 1,
        AwaitingValidation,
        AwaitingPayment,
        Completed,
        Cancelled
    }
}
