namespace AccountingSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }


        // One User can have multiple Parties
        public ICollection<Party> Parties { get; set; } = new List<Party>();
    }
}
