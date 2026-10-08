using System;
using WebAppLottery.Models;

class CustomerAccessTests
{
    static int checks;
    static void Check(string name, bool passed)
    {
        if (!passed) throw new Exception("Failed: " + name);
        checks++;
    }
    static void Main()
    {
        var now = new DateTime(2026, 10, 8, 16, 0, 0);
        var customer = new CustomerAccess { Username = "demo", Password = "Secret", IsValid = true };
        var customers = new[] { customer };
        Func<string, string, string> validate = (u, p) => CustomerAccess.Validate(customers, u, p, now);
        Check("missing username", validate("", "Secret").Contains("Username"));
        Check("missing password", validate("demo", "").Contains("Password"));
        Check("unknown username", validate("unknown", "Secret").Contains("không tồn tại"));
        Check("case sensitive password", validate("demo", "secret").Contains("không đúng"));
        Check("no date bounds", validate("demo", "Secret") == null);
        Check("trim and case insensitive username", validate(" DEMO ", "Secret") == null);
        customer.IsValid = false;
        Check("disabled", validate("demo", "Secret").Contains("IsValid"));
        customer.IsValid = true;
        customer.DeletedDate = now;
        Check("deleted", validate("demo", "Secret").Contains("bị xóa"));
        customer.DeletedDate = null;
        customer.FromDate = now.AddSeconds(1);
        Check("not yet active", validate("demo", "Secret").Contains("chưa đến"));
        customer.FromDate = now;
        customer.ToDate = now;
        Check("inclusive boundaries", validate("demo", "Secret") == null);
        customer.FromDate = null;
        customer.ToDate = now.AddSeconds(-1);
        Check("expired", validate("demo", "Secret").Contains("hết hạn"));
        customer.FromDate = now;
        Check("inverted period", validate("demo", "Secret").Contains("không hợp lệ"));
        Check("duplicate username", CustomerAccess.Validate(new[] { customer, customer }, "demo", "Secret", now).Contains("bị trùng"));
        Console.WriteLine("Passed " + checks + " customer validation checks.");
    }
}
