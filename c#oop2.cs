using System;
using System.Collections.Generic;

// Base Class
public class BankAccount
{
    public string FullName { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(string name, decimal balance)
    {
        FullName = name;
        Balance = balance;
    }

    public virtual void ShowAccountDetails()
    {
        Console.WriteLine($"Account Holder: {FullName}");
        Console.WriteLine($"Current Balance: {Balance:C}");
    }

    public virtual void CalculateInterest()
    {
        Console.WriteLine("Interest calculation is not supported for this account type.");
    }
}

// Derived Class 1: Saving Account
public class SavingAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingAccount(string name, decimal balance, decimal rate) : base(name, balance)
    {
        InterestRate = rate;
    }

    public override void ShowAccountDetails()
    {
        Console.WriteLine("--- Saving Account Details ---");
        base.ShowAccountDetails();
        Console.WriteLine($"Interest Rate: {InterestRate}%");
    }

    public override void CalculateInterest()
    {
        decimal interest = Balance * (InterestRate / 100);
        Console.WriteLine($"Calculated Annual Interest: {interest:C}");
    }
}

// Derived Class 2: Current Account
public class CurrentAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    public CurrentAccount(string name, decimal balance, decimal limit) : base(name, balance)
    {
        OverdraftLimit = limit;
    }

    public override void ShowAccountDetails()
    {
        Console.WriteLine("--- Current Account Details ---");
        base.ShowAccountDetails();
        Console.WriteLine($"Overdraft Limit: {OverdraftLimit:C}");
    }
}

// Main Execution
class Program
{
    static void Main()
    {
        // Create Objects
        SavingAccount saving = new SavingAccount("Ahmed Ali", 5000, 4.5m);
        CurrentAccount current = new CurrentAccount("Sara Smith", 2500, 1000);

        // Polymorphism: Adding different types to a single List
        List<BankAccount> accountList = new List<BankAccount>();
        accountList.Add(saving);
        accountList.Add(current);

        // Process Accounts
        foreach (BankAccount acc in accountList)
        {
            acc.ShowAccountDetails();
            acc.CalculateInterest();
            Console.WriteLine("-----------------------------------");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}