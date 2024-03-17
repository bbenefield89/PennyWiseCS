using PennyWiseCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyWiseCS.Services
{
    internal class TransactionManager
    {
        private List<Transaction> transactions;

        public TransactionManager(List<Transaction> transactions = null)
        {
            this.transactions = transactions ?? new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public List<Transaction> GetTransactions()
        {
            return this.transactions;
        }

        public List<Transaction> GetTransactionsByAmount(
            double minAmount,
            double maxAmount)
        {
            return this.transactions.FindAll(transaction =>
            {
                return transaction.Amount >= minAmount
                    && transaction.Amount <= maxAmount;
            });
        }

        public List<Transaction> GetTransactionsByDate(
            DateTime startDate,
            DateTime endDate)
        {
            return this.transactions.FindAll(transaction =>
            {
                return transaction.Date.Date >= startDate
                    && transaction.Date.Date <= endDate;
            });
        }

        public List<Transaction> GetTransactionsByType(string type)
        {
            return this.transactions.FindAll(transaction =>
            {
                return type.Equals(transaction.Type);
            });
        }

        public bool DeleteTransactionById(int id)
        {
            int idx = transactions.FindIndex(0, transactions.Count, transaction =>
            {
                return transaction.Id == id;
            });

            if (idx == -1)
            {
                return false;
            }

            transactions.RemoveAt(idx);
            return true;
        }
    }
}
