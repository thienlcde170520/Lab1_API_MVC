using BusinessObjects.Models;
using Repositories.Interfaces;
using Repositories.Repository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;

        public AccountService()
        {
            iAccountRepository = new AccountRepository();
        }

        public AccountMember GetAccountById(string accountID)
        {
            return iAccountRepository.GetAccountById(accountID);
        }
    }
}
