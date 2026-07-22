using AccountingSystem.Domain.Enums;

namespace AccountingSystem.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public TransactionType Type {  get; set; }
        public TransactionStatus Status { get; set; }

        public DateTime TransactionDate { get; set; }

    
        // Foreign key to Parties table
        public int PartyId { get; set; }
        public Party Party { get; set; }


    }
}
