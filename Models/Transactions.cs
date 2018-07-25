using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialAPI.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }
    }
}