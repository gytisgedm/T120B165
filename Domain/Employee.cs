namespace api.Domain
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public string Department { get; set; }
        public bool IsAdmin { get; set; } = false;
        //public bool ContractTerminated { get; set; } = false;
    }
}
