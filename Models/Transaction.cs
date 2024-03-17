using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyWiseCS.Models
{
    internal class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }

        public Transaction(
            int id,
            double amount,
            DateTime date,
            string type,
            string category)
        {
            Id = id;
            Amount = amount;
            Date = date;
            Type = type;
            Category = category;
        }
    }
}
