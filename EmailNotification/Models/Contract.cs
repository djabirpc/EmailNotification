namespace EmailNotification.Models
{
    public class Contract
    {
        //ContractId = 1, ClientEmail = "client1@email.com", EndDate = currentDate.AddDays(10)
        public int ContractId { get; set; }
        public string ClientEmail { get; set; }
        public DateTime EndDate { get; set; }
    }
}
