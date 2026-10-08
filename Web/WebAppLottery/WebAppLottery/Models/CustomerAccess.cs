using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;

namespace WebAppLottery.Models
{
    public sealed class CustomerAccess
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsValid { get; set; }
        public DateTime? DeletedDate { get; set; }

        public static List<CustomerAccess> Load()
        {
            var settings = ConfigurationManager.ConnectionStrings["LotteryEntities"];
            var connectionString = new EntityConnectionStringBuilder(settings.ConnectionString).ProviderConnectionString;
            var customers = new List<CustomerAccess>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT Username, [Password], FromDate, ToDate, IsValid, DeletedDate FROM dbo.Customer", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        customers.Add(new CustomerAccess
                        {
                            Username = reader.GetString(0), Password = reader.GetString(1),
                            FromDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            ToDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            IsValid = reader.GetBoolean(4),
                            DeletedDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        });
                }
            }
            return customers;
        }

        public static string Validate(IEnumerable<CustomerAccess> customers, string username, string password, DateTime now)
        {
            if (string.IsNullOrWhiteSpace(username)) return "Vui lòng nhập Username.";
            if (string.IsNullOrEmpty(password)) return "Vui lòng nhập Password.";
            var matches = customers.Where(c => string.Equals(c.Username, username.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            if (matches.Count == 0) return "Username không tồn tại.";
            if (matches.Count != 1) return "Username bị trùng. Vui lòng liên hệ quản trị viên.";
            var customer = matches[0];
            if (!string.Equals(customer.Password, password, StringComparison.Ordinal)) return "Password không đúng.";
            if (customer.DeletedDate.HasValue) return "Tài khoản đã bị xóa.";
            if (!customer.IsValid) return "Tài khoản không được phép sử dụng (IsValid = 0).";
            if (customer.FromDate.HasValue && customer.ToDate.HasValue && customer.FromDate > customer.ToDate)
                return "Thời hạn tài khoản không hợp lệ. Vui lòng liên hệ quản trị viên.";
            if (customer.FromDate.HasValue && now < customer.FromDate.Value) return "Tài khoản chưa đến thời gian sử dụng.";
            if (customer.ToDate.HasValue && now > customer.ToDate.Value) return "Tài khoản đã hết hạn sử dụng.";
            return null;
        }
    }
}
