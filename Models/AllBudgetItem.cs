using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialAPI.Models
{
    public class AllBudgetItem
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SpendingTarget { get; set; }
        public decimal CurrentSpending { get; set; }
    }
}