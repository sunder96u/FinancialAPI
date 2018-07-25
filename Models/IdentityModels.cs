using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace FinancialAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //==============================--Get All Functions--========================================

        /// <summary>
        /// This will use the stored procedure to get a list of all accounts associated with a bank
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public async Task<List<AllAccountData>> GetAllAccountsData(int bankId)
        {
            return await Database.SqlQuery<AllAccountData>("GetAllAccountsData @bankId",
                new SqlParameter("bankId", bankId)).ToListAsync();
        }

        /// <summary>
        /// This will use a stored procedure to create a list of all banks associated with a household
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns></returns>
        public async Task<List<Bank>> GetBanks(int householdId)
        {
            return await Database.SqlQuery<Bank>("GetBanks @householdId",
                new SqlParameter("householdId", householdId)).ToListAsync();
        }


        /// <summary>
        /// This will use a stored procedure to construct a list of all budgetitems associated with a budget
        /// </summary>
        /// <param name="budgetId"></param>
        /// <returns></returns>
        public async Task<List<AllBudgetItem>> GetAllBudgetItemsDetail(int budgetId)
        {
            return await Database.SqlQuery<AllBudgetItem>("GetAllBudgetItemsDetail @budgetId",
                new SqlParameter("budgetId", budgetId)).ToListAsync();
        }

        /// <summary>
        /// This will use a stored procedure to create a list of all transactions associated with an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<List<AllTransactions>> GetAllTransactionData(int accountId)
        {
            return await Database.SqlQuery<AllTransactions>("GetAllTransactionData @accountId",
                new SqlParameter("accountId", accountId)).ToListAsync();
        }


        //===========================-- Get Functions-- =============================================

        /// <summary>
        /// This will get specific details about a specific account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<AccountData> GetAccountDetails(int accountId)
        {
            return await Database.SqlQuery<AccountData>("GetAccountDetails @accountId",
                new SqlParameter("accountId", accountId)).FirstOrDefaultAsync();

        }

        /// <summary>
        /// This will grab the specific details about a specifit budgetitem
        /// </summary>
        /// <param name="budgetItemId"></param>
        /// <returns></returns>
        public async Task<BudgetItem> GetBudgetItemsDetails(int budgetItemId)
        {
            return await Database.SqlQuery<BudgetItem>("GetBudgetItemsDetails @budgetItemId",
                new SqlParameter("budgetItemId", budgetItemId)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// this will produce the specific details about a specific budget
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns></returns>
        public async Task<Budget> GetBudgetsData(int householdId)
        {
            return await Database.SqlQuery<Budget>("GetBudgetsData @householdId",
                new SqlParameter("householdId", householdId)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// This will grab the specific details about a specific household
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns></returns>
        public async Task<Household> GetHouseholdData(int householdId)
        {
            return await Database.SqlQuery<Household>("GetHouseholdData @householdId",
                new SqlParameter("householdId", householdId)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// This will grab the exact details about a specific transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<Transactions> GetTransactionDetails(int transactionId)
        {
            return await Database.SqlQuery<Transactions>("GetTransactionDetails @transactionId",
                new SqlParameter("transactionId", transactionId)).FirstOrDefaultAsync();
        }


        //===============================--Post Functions--======================================

        /// <summary>
        /// This will add an account using a stored procedure
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
        /// <returns></returns>
        public int AddAccount(int bankId, string name, string description, decimal startingBalance, decimal currentBalance, int typeId, string accountNumber, string routingNumber, bool isDeleted)
        {
            return Database.ExecuteSqlCommand("AddAccount @bankId, @name, @description, @startingBalance, @currentBalance, @typeId, @accountNumber, @routingNumber, @isDeleted",
                new SqlParameter("bankId", bankId),
                new SqlParameter("name", name),
                new SqlParameter("description", description),
                new SqlParameter("startingBalance", startingBalance),
                new SqlParameter("currentBalance", currentBalance),
                new SqlParameter("typeId", typeId),
                new SqlParameter("accountNumber", accountNumber),
                new SqlParameter("routingNumber", routingNumber),
                new SqlParameter("isDeleted", isDeleted));
        }

        /// <summary>
        /// This will create a bank using a stored prodedure
        /// </summary>
        /// <param name="householdId"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public int AddBank(int householdId, string name, string address, string city, string state, string zip, string phone)
        {
            return Database.ExecuteSqlCommand("AddBank @householdId, @name, @address, @city, @state, @zip, @phone",
                new SqlParameter("householdId", householdId),
                new SqlParameter("name", name),
                new SqlParameter("address", address),
                new SqlParameter("city", city),
                new SqlParameter("state", state),
                new SqlParameter("zip", zip),
                new SqlParameter("phone", phone));
        }

        /// <summary>
        /// The will create a budget using a stored procedure
        /// </summary>
        /// <param name="householdId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="spendingTarget"></param>
        /// <returns></returns>
        public int AddBudget(int householdId, string name, string description, decimal spendingTarget)
        {
            return Database.ExecuteSqlCommand("AddBudget @householdId, @name, @description, @spendingTarget",
                new SqlParameter("householdId", householdId),
                new SqlParameter("name", name),
                new SqlParameter("description", description),
                new SqlParameter("spendingTarget", spendingTarget));

        }

        /// <summary>
        /// This will create a new instance of a budgetitem using a stored procedure
        /// </summary>
        /// <param name="budgetId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="spendingTarget"></param>
        /// <param name="currentSpending"></param>
        /// <returns></returns>
        public int AddBudgetItem(int budgetId, string name, string description, decimal spendingTarget, decimal currentSpending)
        {
            return Database.ExecuteSqlCommand("AddBudgetItem @budgetId, @name, @description, @spendingTarget, @currentSpending",
                new SqlParameter("budgetId", budgetId),
                new SqlParameter("name", name),
                new SqlParameter("description", description),
                new SqlParameter("spendingTarget", spendingTarget),
                new SqlParameter("currentSpending", currentSpending));
        }


        /// <summary>
        /// This will create a new instance of an account using a stored procedure
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <param name="memo"></param>
        /// <param name="reconciled"></param>
        /// <param name="type"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        public int AddTransaction(int accountId, decimal amount, string memo, bool reconciled, string type, bool isDeleted)
        {
            return Database.ExecuteSqlCommand("AddTransaction @accountId, @amount, @memo, @reconciled, @type, @isDeleted",
                new SqlParameter("accountId", accountId),
                new SqlParameter("amount", amount),
                new SqlParameter("memo", memo),
                new SqlParameter("reconciled", reconciled),
                new SqlParameter("type", type),
                new SqlParameter("isDeleted", isDeleted));

        }
    }
}