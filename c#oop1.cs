public class BankAccount
{
    public const string BankCode = "BNK001";
    public readonly DateTime CreatedDate = DateTime.Now;

    private string _fullName, _nationalID, _phoneNumber, _address;
    private decimal _balance;

    // Properties with Validation
    public string FullName { get => _fullName; set => _fullName = !string.IsNullOrEmpty(value) ? value : "Unknown"; }
    public string NationalID { get => _nationalID; set => _nationalID = value?.Length == 14 ? value : "Invalid"; }
    public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = (value?.Length == 11 && value.StartsWith("01")) ? value : "Invalid"; }
    public decimal Balance { get => _balance; set => _balance = value >= 0 ? value : 0; }
    public string Address { get; set; }

    // Constructors
    public BankAccount() { } // Default

    public BankAccount(string name, string id, string phone, string addr, decimal bal) // Parameterized
    {
        FullName = name; NationalID = id; PhoneNumber = phone; Address = addr; Balance = bal;
    }

    public BankAccount(string name, string id, string phone, string addr) // Overloaded
        : this(name, id, phone, addr, 0) { }

    // Methods
    public void ShowAccountDetails() => 
        Console.WriteLine($"Name: {FullName}, Phone: {PhoneNumber}, ID: {NationalID}, Balance: {Balance}, Date: {CreatedDate}");

    public bool IsValidNationalID() => NationalID.Length == 14;
    public bool IsValidPhoneNumber() => PhoneNumber.Length == 11 && PhoneNumber.StartsWith("01");
}




static void Main()
{
    // Object 1: Full data
    BankAccount acc1 = new BankAccount("Karim", "12345678901234", "01012345678", "Cairo", 5000);
    
    // Object 2: No balance (default 0)
    BankAccount acc2 = new BankAccount("Ahmed", "98765432109876", "01187654321", "Giza");

    acc1.ShowAccountDetails();
    acc2.ShowAccountDetails();
}