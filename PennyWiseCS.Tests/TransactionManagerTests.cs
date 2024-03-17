using PennyWiseCS.Models;
using PennyWiseCS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyWiseCS.Tests
{
    public class TransactionManagerTests
    {
        [Fact]
        public void AddTransactionTest()
        {
            TransactionManager transactionManager = new TransactionManager();
            Transaction transaction1 = new Transaction(
                0,
                10.00,
                DateTime.Now,
                "Income",
                "Groceries");

            Assert.Empty(transactionManager.GetTransactions());

            transactionManager.AddTransaction(transaction1);
            Assert.Single(transactionManager.GetTransactions());

            Transaction transaction2 = new Transaction(
                0,
                10.00,
                DateTime.Now,
                "Income",
                "Groceries");

            transactionManager.AddTransaction(transaction2);
            Assert.Equal(2, transactionManager.GetTransactions().Count);
        }

        [Fact]
        public void GetTransactionsByAmountTest()
        {
            TransactionManager transactionManager = new TransactionManager();
            Transaction transaction1 = new Transaction(
                0,
                10.00,
                DateTime.Now,
                "Income",
                "Groceries");

            Transaction transaction2 = new Transaction(
                0,
                20.00,
                DateTime.Now,
                "Income",
                "Groceries");

            transactionManager.AddTransaction(transaction1);
            transactionManager.AddTransaction(transaction2);

            Assert.Empty(transactionManager.GetTransactionsByAmount(0, 9));
            Assert.Single(transactionManager.GetTransactionsByAmount(0, 10));
            Assert.Equal(2, transactionManager.GetTransactionsByAmount(0, 20.00).Count);
            Assert.Empty(transactionManager.GetTransactionsByAmount(21, 100));
        }

        [Fact]
        public void GetTransactionsByDateTest()
        {
            TransactionManager transactionManager = new TransactionManager();
            Transaction transaction1 = new Transaction(
                0,
                10.00,
                DateTime.Parse("2024-03-14"),
                "Income",
                "Groceries");

            Transaction transaction2 = new Transaction(
                0,
                20.00,
                DateTime.Parse("1970-01-01"),
                "Income",
                "Groceries");

            transactionManager.AddTransaction(transaction1);
            transactionManager.AddTransaction(transaction2);

            Assert.Empty(transactionManager.GetTransactionsByDate(
                DateTime.Parse("1900-01-01"),
                DateTime.Parse("1968-01-01")));

            Assert.Empty(transactionManager.GetTransactionsByDate(
                DateTime.Parse("3000-01-01"),
                DateTime.Parse("4000-01-01")));

            Assert.Single(transactionManager.GetTransactionsByDate(
                DateTime.Parse("1970-01-01"),
                DateTime.Parse("1989-01-01")));

            Assert.Single(transactionManager.GetTransactionsByDate(
                DateTime.Parse("2024-01-01"),
                DateTime.Parse("2025-01-01")));

            Assert.Equal(2, transactionManager.GetTransactionsByDate(
                DateTime.Parse("1970-01-01"),
                DateTime.Parse("2025-01-01"))
                    .Count);
        }

        [Fact]
        public void GetTransactionsByTypeTest()
        {
            TransactionManager transactionManager = new TransactionManager();
            Transaction transaction1 = new Transaction(
                0,
                10.00,
                DateTime.Parse("2024-03-14"),
                "Income",
                "Groceries");

            Transaction transaction2 = new Transaction(
                0,
                20.00,
                DateTime.Parse("1970-01-01"),
                "SomethingElse",
                "Groceries");

            transactionManager.AddTransaction(transaction1);
            transactionManager.AddTransaction(transaction2);

            Assert.Empty(transactionManager.GetTransactionsByType("Expense"));
            Assert.Single(transactionManager.GetTransactionsByType("Income"));
        }

        public void DeleteTransactionByIdTest()
        {
            TransactionManager transactionManager = new TransactionManager();
            Transaction transaction1 = new Transaction(
                0,
                10.00,
                DateTime.Parse("2024-03-14"),
                "Income",
                "Groceries");

            Transaction transaction2 = new Transaction(
                1,
                20.00,
                DateTime.Parse("1970-01-01"),
                "SomethingElse",
                "Groceries");

            transactionManager.AddTransaction(transaction1);
            transactionManager.AddTransaction(transaction2);

            Assert.False(transactionManager.DeleteTransactionById(3));
            Assert.Equal(2, transactionManager.GetTransactions().Count);
            Assert.True(transactionManager.DeleteTransactionById(1));
            Assert.Single(transactionManager.GetTransactions());
        }
    }
}
