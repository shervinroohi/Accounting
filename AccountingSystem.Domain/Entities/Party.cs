namespace AccountingSystem.Domain.Entities
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDelete { get; set; }


        // Foreign key for linking to users table
        public int UserId { get; set; }
        public User User { get; set; }


        // One Party can have multiple transactions
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
