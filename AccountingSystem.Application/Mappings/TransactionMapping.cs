using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Mappings
{
    //public static class TransactionMapping
    //{
    //    public static Transaction ToEntity(this CreateTransactionRequestDto request)
    //    {


    //        return new Transaction
    //        {
    //            Amount = request.Amount,
    //            Type = request.Type,
    //            Status = request.Status,
    //            Description = request.Description,
    //            PartyId = request.PartyId,


    //            TransactionDate=request.TransactionDate,
    //        };
    //    }

    //    public static TransactionResponseDto ToDto(this Transaction transaction)
    //    {
    //        return new TransactionResponseDto
    //        {
    //            Id = transaction.Id,
    //            Amount = transaction.Amount,
    //            Type = transaction.Type,
    //            Status = transaction.Status,
    //            TransactionDate = transaction.TransactionDate,
    //            Description = transaction.Description,
    //            PartyId = transaction.PartyId,
    //            PartyName = transaction.Party.Name
    //        };
    //    }
    //}
    public static class TransactionMapper
    {
        private static readonly string[] AllowedDateFormats =
        {
            "yyyy-MM-dd",
            "yyyy-MM-ddTHH:mm:ss"
        };

        // Request DTO → Entity
        public static Transaction ToEntity(
            this CreateTransactionRequestDto dto)
        {
            return new Transaction
            {
                Amount = dto.Amount,
                Type = dto.Type,
                Status = dto.Status,
                TransactionDate = DateTime.ParseExact(
                    dto.TransactionDate!,
                    AllowedDateFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None),
                Description = dto.Description,
                PartyId = dto.PartyId
            };
        }

        // Entity → Response DTO
        public static TransactionResponseDto ToDto(
            this Transaction transaction)
        {
            return new TransactionResponseDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Status = transaction.Status,
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description,
                PartyId = transaction.PartyId
            };
        }
    }


}
