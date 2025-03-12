namespace csrf_example_net.Requests
{
    public class NewTransactionRq
    {
        public int Amount { get; set; }
        public string? DestinationAccount { get; set; }
    }
}
