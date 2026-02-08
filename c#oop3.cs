using System;
using System.Collections.Generic;
using System.Linq;

// 1. Core Classes
public class Transaction {
    public string Info => $"{Date}: {Type} {Amount:C}";
    public string Type; public decimal Amount; public DateTime Date = DateTime.Now;
    public Transaction(string t, decimal a) { Type = t; Amount = a; }
}

public class Account {
    public string ID = "ACC" + Guid.NewGuid().ToString().Substring(0,5);
    public decimal Balance;
    public List<Transaction> History = new List<Transaction>();
    
    public void Deposit(decimal amt) { Balance += amt; History.Add(new Transaction("Deposit", amt)); }
    public virtual bool Withdraw(decimal amt) {
        if (amt > Balance) return false;
        Balance -= amt; History.Add(new Transaction("Withdraw", amt)); return true;
    }
}

public class SavingAcc : Account {
    public decimal Rate = 0.05m;
    public void AddInterest() => Deposit(Balance * (Rate / 12));
}

public class Customer {
    public string ID = "CUST" + Guid.NewGuid().ToString().Substring(0,3);
    public string Name;
    public List<Account> Accounts = new List<Account>();
    public Customer(string n) => Name = n;
}

// 2. Bank Management
public class Bank {
    public List<Customer> Customers = new List<Customer>();

    public void Transfer(Account from, Account to, decimal amt) {
        if (from.Withdraw(amt)) to.Deposit(amt);
    }

    public void Report() {
        foreach (var c in Customers) {
            Console.WriteLine($"Cust: {c.Name} (ID: {c.ID}) - Total: {c.Accounts.Sum(a => a.Balance):C}");
            foreach (var acc in c.Accounts) Console.WriteLine($"  Acc {acc.ID}: {acc.Balance:C}");
        }
    }
}

// 3. Testing the System
class Program {
    static void Main() {
        Bank myBank = new Bank();

        // Add Customer & Accounts
        Customer c1 = new Customer("John Doe");
        myBank.Customers.Add(c1);
        
        SavingAcc s1 = new SavingAcc(); s1.Deposit(1000);
        Account a1 = new Account(); a1.Deposit(500);
        c1.Accounts.Add(s1); c1.Accounts.Add(a1);

        // Operations
        myBank.Transfer(s1, a1, 200); // Transfer 200
        s1.AddInterest();             // Monthly Interest

        // Reports
        myBank.Report();
        Console.WriteLine("\nHistory for Account 1:");
        s1.History.ForEach(t => Console.WriteLine(t.Info));
    }
}