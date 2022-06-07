﻿using OriolOr.Maneko.Infrastructure;
using OriolOr.Maneko.Domain;
using OriolOr.Maneko.Domain.IdentityManagement;
using System.Collections.ObjectModel;

namespace OriolOr.Maneko.Source.Services
{
    public class AccountService
    {
        
        public AccountService() {

        }

        public double GetCurrentBalanceFromDb(UserCredentials userCredentials)
        {
           return MongoDbConfigurator.GetAccountCurrentBalance();
        }

        public Collection<YearBalance> GetYearBalanceFromDb(UserCredentials userCredentials)
        {
            return MongoDbConfigurator.GetYearBalance();
        }
   }
}