namespace EWallet.API
{
    public class Transaction
    {
        public string accountNumber { get; set; }
        public string pinNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
