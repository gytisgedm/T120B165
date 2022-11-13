namespace api.Domain
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Department { get; set; }
        public bool IsAdmin { get; set; } = false;
        //public bool ContractTerminated { get; set; } = false;
    }
}
