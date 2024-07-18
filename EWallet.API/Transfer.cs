namespace EWallet.API
{
    public class Transfer
    {
        public string accountNumber { get; set; }
        public string pinNumber { get; set; }
        public string receiver { get; set; }
        public decimal Amount { get; set; }
    }
}
