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
    public static class TransactionMapping
    {
        public static Transaction ToEntity(this CreateTransactionRequestDto request)
        {
            var persianCalendar = new PersianCalendar();

            var parts = request.TransactionDate.Split('/');

            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);

            return new Transaction
            {
                Amount = request.Amount,
                Type = request.Type,
                Status = request.Status,
                Description = request.Description,
                PartyId = request.PartyId,

                TransactionDate = persianCalendar.ToDateTime(
                    year,
                    month,
                    day,
                    0,
                    0,
                    0,
                    0)
            };
        }
    }
}
