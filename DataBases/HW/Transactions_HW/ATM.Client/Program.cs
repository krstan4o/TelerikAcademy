using System;
using System.Linq;
using Transactions;

class Program
{
    static void Main()
    {
        //2.Using transactions write a method which retrieves some money (for example $200) from certain account.
        //The retrieval is successful when the following sequence of sub-operations is completed successfully:
        //A query checks if the given CardPIN and CardNumber are valid.
        //The amount on the account (CardCash) is evaluated to see whether it is bigger than the requested sum (more than $200).
        //The ATM machine pays the required sum (e.g. $200) and stores in the table CardAccounts the new amount (CardCash = CardCash - 200).

        //3.Extend the project from the previous exercise and add a new table
        //TransactionsHistory with fields (Id, CardNumber, TransactionDate, Ammount)
        //containing information about all money retrievals on all accounts.
        //Modify the program logic so that it saves historical information (logs)
        //in the new table after each successful money withdrawal.

        string cardPin = "4321";
        string cardNumber = "0987654321";

        GetMoney(cardNumber, cardPin, 1000m);
    }

    public static void GetMoney(string cardNumber, string cardPin, decimal moneyValue)
    {
        if (CardValidation(cardNumber, cardPin))
        {
            if (CardAmount(cardNumber) > moneyValue)
            {
                WithdrawTransfer(cardNumber, cardPin, moneyValue);
            }
            else
            {
                throw new Exception("Not enough money priqtelche! :D");
            }
        }
        else
        {
            throw new Exception("Invalid card.");
        }
    }
    #region CardValidation
    private static bool CardValidation(string cardNumber, string cardPin)
    {
        bool isValid;

        using (ATMEntities db = new ATMEntities())
        {
            
            bool isCardNumberValid = db.CardAccounts.Any(a => a.CardNumber == cardNumber);
            bool isCardPinValid = db.CardAccounts.Any(b => b.CardPin == cardPin);

            if (isCardNumberValid && isCardPinValid)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
        }
        return isValid;
    }
    #endregion
    #region CardAmount
    private static decimal? CardAmount(string cardNumber)
    {
        decimal? moneyAmount;

        using (ATMEntities db = new ATMEntities())
        {
            moneyAmount = db.CardAccounts.Where(x => cardNumber != null && x.CardNumber == cardNumber).Select(a => a.CardCash).First();
        }

        return moneyAmount;
    }
    #endregion
    #region WithdrawTransfer
    private static void WithdrawTransfer(string cardNumber, string cardPin, decimal moneyValue)
    {
        using (ATMEntities db = new ATMEntities())
        {
            var account = db.CardAccounts.FirstOrDefault(x => x.CardNumber == cardNumber && x.CardPin == cardPin);

            account.CardCash = account.CardCash - moneyValue;
            DateTime transactionDate = DateTime.Now;

            Console.WriteLine("Successful, remaining availability: {0}", account.CardCash);

            TransactionsHistory currentTransaction = new TransactionsHistory()
            {
                Id = account.Id,
                CardNumber = cardNumber,
                Ammount = account.CardCash,
                TransactionDate = transactionDate                
            };

            db.TransactionsHistories.Add(currentTransaction);
            db.SaveChanges();
        }
    }
    #endregion
}