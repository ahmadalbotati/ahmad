using System;

public class BankAccount
{
    // 1. الحقول (Fields)
    const string BankCode = "BNK001";
    readonly DateTime CreatedDate;
    private string _fullName;
    private string _nationalID;
    private string _phoneNumber;
    public decimal Balance; // جعلناه بسيطا هنا للتبسيط

    // 2. الخصائص مع التحقق (Properties)
    public string FullName
    {
        get { return _fullName; }
        set { if (!string.IsNullOrEmpty(value)) _fullName = value; }
    }

    public string NationalID
    {
        get { return _nationalID; }
        set { if (value.Length == 14) _nationalID = value; }
    }

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set { if (value.StartsWith("01") && value.Length == 11) _phoneNumber = value; }
    }

    // 3. المنشئات (Constructors)
    public BankAccount() // الافتراضي
    {
        CreatedDate = DateTime.Now;
    }

    public BankAccount(string name, string nID, string phone, string addr, decimal bal)
    {
        CreatedDate = DateTime.Now;
        FullName = name;
        NationalID = nID;
        PhoneNumber = phone;
        Balance = bal;
    }

    public BankAccount(string name, string nID, string phone, string addr)
    {
        CreatedDate = DateTime.Now;
        FullName = name;
        NationalID = nID;
        PhoneNumber = phone;
        Balance = 0; // القيمة الافتراضية
    }

    // 4. العمليات (Methods)
    public void ShowAccountDetails()
    {
        Console.WriteLine("Name: " + FullName);
        Console.WriteLine("Phone: " + PhoneNumber);
        Console.WriteLine("Balance: " + Balance);
        Console.WriteLine("Date Created: " + CreatedDate);
        Console.WriteLine("-------------------------");
    }
}




تجربة الكود في ال static main(args) {
    
}class Program
{
    static void Main()
    {
        // إنشاء الحساب الأول باستخدام المنشئ الكامل
        BankAccount account1 = new BankAccount("Ahmed Ali", "12345678901234", "01011223344", "Cairo", 1000);

        // إنشاء الحساب الثاني باستخدام المنشئ الذي لا يحتوي على رصيد
        BankAccount account2 = new BankAccount("Sara Mohamed", "22334455667788", "01255667788", "Alex");

        // عرض التفاصيل
        Console.WriteLine("Displaying Account 1:");
        account1.ShowAccountDetails();

        Console.WriteLine("Displaying Account 2:");
        account2.ShowAccountDetails();
        
        Console.ReadKey(); // لتثبيت الشاشة
    }
}