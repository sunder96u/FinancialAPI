using FinancialAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FinancialAPI.Controllers
{
    [RoutePrefix("Api")]
    public class ValuesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //====================================================================================================================
        //This section gets a list

        /// <summary>
        /// Get all the Accounts associated with a bank
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns>List of Accounts</returns>
        [Route("Accounts")]
        public async Task<List<AllAccountData>> GetAllAccounts(int bankId)
        {
            return await db.GetAllAccountsData(bankId);
        }

        /// <summary>
        /// This will grab information about all the banks associated with a household
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns>a list of banks</returns>
        [Route("Banks")]
        public async Task<List<Bank>> GetBank(int householdId)
        {
            return await db.GetBanks(householdId);
        }

        /// <summary>
        /// Get all the budgetItems in a budget
        /// </summary>
        /// <param name="budgetId"></param>
        /// <returns> list of budgetItems</returns>
        [Route("BudgetItems")]
        public async Task<List<AllBudgetItem>> GetAllBudgetItems(int budgetId)
        {
            return await db.GetAllBudgetItemsDetail(budgetId);
        }

        /// <summary>
        /// This will get all the transactions associated with a bank account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>A list of transactions</returns>
        [Route("Transactions")]
        public async Task<List<AllTransactions>> GetAllTransactions(int accountId)
        {
            return await db.GetAllTransactionData(accountId);
        }


        //======================================================================================================================
        //tThis section gets a single item

        /// <summary>
        /// This will grab all the information about an account using the account Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Account information</returns>
        [Route("Account")]
        public async Task<AccountData> GetAccountData(int accountId)
        {
            return await db.GetAccountDetails(accountId);
        }

        /// <summary>
        /// This will grab the information associated with a budgetItem
        /// </summary>
        /// <param name="budgetItemId"></param>
        /// <returns>BudgetItem information</returns>
        [Route("BudgetItem")]
        public async Task<BudgetItem> GetBudgetItemDetails(int budgetItemId)
        {
            return await db.GetBudgetItemsDetails(budgetItemId);
        }

        /// <summary>
        /// This will grab information about a budget using the budgetId
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns>Budget information</returns>
        [Route("Budget")]
        public async Task<Budget> GetBudget(int householdId)
        {
            return await db.GetBudgetsData(householdId);
        }


        /// <summary>
        /// This will grab the information about a household using the household Id
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns>household information</returns>
        [Route("Household")]
        public async Task<Household> GetHousehold(int householdId)
        {
            return await db.GetHouseholdData(householdId);
        }

        /// <summary>
        /// This will get the information about a specific transaction using the transactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>Information about a transaction</returns>
        [Route("Transaction")]
        public async Task<Transactions> GetTransactionDetails(int transactionId)
        {
            return await db.GetTransactionDetails(transactionId);
        }

        //========================================================================================================================
        //This section Inserts into the database

        /// <summary>
        /// This will Add and account to a bank when given the associating bankId
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="startingBalance"></param>
        /// <param name="currentBalance"></param>
        /// <param name="typeId"></param>
        /// <param name="accountNumber"></param>
        /// <param name="routingNumber"></param>
        /// <param name="isDeleted"></param>
        /// <returns>an integer</returns>
        [Route("AddAccount")]
        [AcceptVerbs("POST")]
        public int AddAccount(int bankId, string name, string description, decimal startingBalance, decimal currentBalance, int typeId, string accountNumber, string routingNumber, bool isDeleted)
        {
            return db.AddAccount(bankId, name, description, startingBalance, currentBalance, typeId, accountNumber, routingNumber, isDeleted);
        }

        /// <summary>
        /// This will Add a bank to the associated household
        /// </summary>
        /// <param name="householdId"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("AddBank")]
        [AcceptVerbs("POST")]
        public int AddBank(int householdId, string name, string address, string city, string state, string zip, string phone)
        {
            return db.AddBank(householdId, name, address, city, state, zip, phone);
        }


        /// <summary>
        /// This will Add a budgetItem to the associated budget using the budget Id
        /// </summary>
        /// <param name="budgetId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="spendingTarget"></param>
        /// <param name="currentSpending"></param>
        /// <returns></returns>
        [Route("AddBudgetItem")]
        [AcceptVerbs("POST")]
        public int AddBudgetItem(int budgetId, string name, string description, decimal spendingTarget, decimal currentSpending)
        {
            return db.AddBudgetItem(budgetId, name, description, spendingTarget, currentSpending);
        }


        /// <summary>
        /// This will add a transaction to the account using the associated accound Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <param name="memo"></param>
        /// <param name="reconciled"></param>
        /// <param name="type"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        [Route("AddTransaction")]
        [AcceptVerbs("POST")]
        public int AddTransaction(int accountId, decimal amount, string memo, bool reconciled, string type, bool isDeleted)
        {
            return db.AddTransaction(accountId, amount, memo, reconciled, type, isDeleted);
        }

        //========================================================================================================================
        //This section will create a list using Json

        ///// <summary>
        ///// Get all the Accounts associated with a bank
        ///// </summary>
        ///// <param name="bankId"></param>
        ///// <returns>List of Accounts</returns>
        //[Route("Account/Json")]
        //public async Task<List<AllAccountData>> GetAllAccountsJson(int bankId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetAllAccountsData(bankId));
        //    return (Json);
        //}

        ///// <summary>
        ///// This will grab information about all the banks associated with a household
        ///// </summary>
        ///// <param name="householdId"></param>
        ///// <returns>a list of banks</returns>
        //[Route("Bank/Json")]
        //public async Task<List<Bank>> GetBankJson(int householdId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetBanks(householdId));
        //    return (Json);
        //}

        ///// <summary>
        ///// Get all the budgetItems in a budget
        ///// </summary>
        ///// <param name="budgetId"></param>
        ///// <returns> list of budgetItems</returns>
        //[Route("BudgetItem/Json")]
        //public async Task<List<AllBudgetItem>> GetAllBudgetItemsJson(int budgetId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetAllBudgetItemsDetail(budgetId));
        //    return (Json);
        //}

        ///// <summary>
        ///// This will get all the transactions associated with a bank account
        ///// </summary>
        ///// <param name="accountId"></param>
        ///// <returns>A list of transactions</returns>
        //[Route("Transaction/Json")]
        //public async Task<List<AllTransactions>> GetAllTransactionsJson(int accountId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetAllTransactionData(accountId));
        //    return (Json);
        //}
        ////========================================================================================================================
        ////This section will create a instance of an object returned with Json

        ///// <summary>
        ///// This will grab all the information about an account using the account Id
        ///// </summary>
        ///// <param name="accountId"></param>
        ///// <returns>Account information</returns>
        //[Route("Account/Json")]
        //public async Task<AccountData> GetAccountDataJson(int accountId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetAccountDetails(accountId));
        //    return (Json);
        //}

        ///// <summary>
        ///// This will grab the information associated with a budgetItem
        ///// </summary>
        ///// <param name="budgetItemId"></param>
        ///// <returns>BudgetItem information</returns>
        //[Route("BudgetItem/Json")]
        //public async Task<BudgetItem> GetBudgetItemDetailsJson(int budgetItemId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetBudgetItemsDetails(budgetItemId));
        //    return (Json);
        //}

        ///// <summary>
        ///// This will grab information about a budget using the budgetId
        ///// </summary>
        ///// <param name="householdId"></param>
        ///// <returns>Budget information</returns>
        //[Route("Budget/Json")]
        //public async Task<Budget> GetBudgetJson(int householdId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetBudgetsData(householdId));
        //    return (Json);
        //}


        ///// <summary>
        ///// This will grab the information about a household using the household Id
        ///// </summary>
        ///// <param name="householdId"></param>
        ///// <returns>household information</returns>
        //[Route("Household/Json")]
        //public async Task<Household> GetHouseholdJson(int householdId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetHouseholdData(householdId));
        //    return (Json);
        //}

        ///// <summary>
        ///// This will get the information about a specific transaction using the transactionId
        ///// </summary>
        ///// <param name="transactionId"></param>
        ///// <returns>Information about a transaction</returns>
        //[Route("Transaction/Json")]
        //public async Task<Transactions> GetTransactionDetailsJson(int transactionId)
        //{
        //    var Json = JsonConvert.SerializeObject(await db.GetTransactionDetails(transactionId));
        //    return (Json);
        //}

    }
}
