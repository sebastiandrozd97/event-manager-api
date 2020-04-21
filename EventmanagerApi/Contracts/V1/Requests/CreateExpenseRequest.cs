namespace EventmanagerApi.Contracts.V1.Requests
{
    public class CreateExpenseRequest
    {
        public string Name { get; set; }

        public double Cost { get; set; }
    }
}