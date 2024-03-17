using PennyWiseCS.Models;
using PennyWiseCS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyWiseCS.Controllers
{
    internal static class InputController
    {
        private static TransactionManager transactionManager = new TransactionManager();

        public static void AcceptUserInput()
        {
            while (true)
            {
                string statement = "Choose an option:\n";
                statement += "[A]dd Transaction\n";
                statement += "[V]iew Transactions\n";
                statement += "[D]elete Transaction\n";
                statement += "[F]ilter Transactions\n";
                statement += "[E]xit\n";

                Console.WriteLine(statement);
                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "A":
                        HandleAddTransaction();
                        break;

                    case "V":
                        HandleViewTransactions();
                        break;

                    case "D":
                        HandleDeleteTransaction();
                        break;

                    case "F":
                        HandleFilterTransactions();
                        break;

                    case "E":
                        HandleExitApplication();
                        break;

                    default:
                        break;
                }
            }
        }

        public static void HandleAddTransaction()
        {
            Console.WriteLine("Enter transaction amount: ");
            string amountString = Console.ReadLine();
            double amount = Math.Round(Convert.ToDouble(amountString), 2);

            Console.WriteLine("Enter transaction type (Income/Expense):");
            string type = Console.ReadLine();

            Console.WriteLine("Enter transaction category:");
            string category = Console.ReadLine();

            DateTime date = DateTime.Now;

            Transaction transaction = new Transaction(0, amount, date, type, category);
            transactionManager.AddTransaction(transaction);
        }

        public static void HandleViewTransactions()
        {
            List<Transaction> transactions = transactionManager.GetTransactions();
            DisplayTransactions(transactions);
        }

        public static void HandleDeleteTransaction()
        {
            HandleViewTransactions();
            Console.WriteLine("Which transation would you like to delete?");

            string idString = Console.ReadLine();
            int id = Convert.ToInt32(idString);

            bool is_removed = transactionManager.DeleteTransactionById(id);

            if (is_removed)
            {
                Console.WriteLine($"Transaction {id} has been removed");
            }
            else
            {
                Console.WriteLine($"No transaction with id {id} can be found");
            }

            Console.WriteLine("\n");
        }

        public static void HandleFilterTransactions()
        {
            string statement = "Filter transactions by:\n";
            statement += "[A]mount\n";
            statement += "[D]ate\n";
            statement += "[T]ype\n";
            Console.WriteLine(statement);

            string choice = Console.ReadLine();

            switch (choice.ToUpper())
            {
                case "A":
                    HandleFilterTransactionsByAmount();
                    break;

                case "D":
                    HandleFilterTransactionsByDate();
                    break;

                case "T":
                    HandleFilterTransactionsByType();
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }

        private static void HandleFilterTransactionsByAmount()
        {
            Console.WriteLine("Please provide a minimum amount:");
            string minAmountStr = Console.ReadLine();
            double minAmount = Convert.ToDouble(minAmountStr);

            Console.WriteLine("Please provide a maximum amount:");
            string maxAmountStr = Console.ReadLine();
            double maxAmount = Convert.ToDouble(maxAmountStr);

            List<Transaction> transactions = transactionManager.GetTransactionsByAmount(
                minAmount,
                maxAmount);

            DisplayTransactions(transactions);
        }

        private static void HandleFilterTransactionsByDate()
        {
            Console.WriteLine("Please provide the earliest date to filter by (YYYY-MM-DD):");
            string startDateStr = Console.ReadLine();
            DateTime startDate = DateTime.Parse(startDateStr);

            Console.WriteLine("Please provide the latest date to filter by (YYYY-MM-DD):");
            string endDateStr = Console.ReadLine();
            DateTime endDate = DateTime.Parse(endDateStr);

            List<Transaction> transactions = transactionManager.GetTransactionsByDate(
                startDate,
                endDate);
            
            DisplayTransactions(transactions);
        }

        private static void HandleFilterTransactionsByType()
        {
            Console.WriteLine("Please provide a type to filter by:");
            string type = Console.ReadLine();

            List<Transaction> transactions = transactionManager.GetTransactionsByType(type);
            DisplayTransactions(transactions);
        }

        private static void DisplayTransactions(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("\nNo transactions found");
            }
            else
            {
                transactions.ForEach(transaction =>
                {
                    string amountFormatted = transaction.Amount.ToString("F2");
                    string statement = $"\nID: {transaction.Id} | ";
                    statement += $"Date: {transaction.Date} | ";
                    statement += $"Type: {transaction.Type} | ";
                    statement += $"Category: {transaction.Category} | ";
                    statement += $"Amount: ${amountFormatted}";

                    Console.WriteLine(statement);
                });
            }

            Console.WriteLine("\n\n");
        }

        private static void HandleExitApplication()
        {
            Environment.Exit(0);
        }
    }
}
