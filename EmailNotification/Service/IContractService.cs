namespace EmailNotification.Service
{
    public interface IContractService
    {
        Task CheckAndSendContractNotifications();
    }
}
