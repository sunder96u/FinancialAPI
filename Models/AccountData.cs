using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialAPI.Models
{
    public class AccountData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public decimal CurrentBalance { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public int AccountTypeId { get; set; }
    }
}