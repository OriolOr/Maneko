﻿using Microsoft.IdentityModel.Tokens;
using OriolOr.Maneko.API.Domain.IdentityManagement;
using OriolOr.Maneko.API.Infrastructure;
using OriolOr.Maneko.API.Infrastructure.Interfaces;
using OriolOr.Maneko.API.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OriolOr.Maneko.API.Service
{
    public class UserCredentialsService : IUserCredentialsService
    {
        public IUserDataRepository UserDataRepository; 
        public IAccountDataRepository AccountDataRepository;

        public UserCredentialsService(IUserDataRepository userDataRepository, IAccountDataRepository accountDataRepository)
        {
            this.UserDataRepository = userDataRepository;
            this.AccountDataRepository = accountDataRepository;
        }

        public bool Authenticate(UserCredentials userCredentials)
        {
            bool loginSucced;

            //TO BE REMOVED 
            if (userCredentials.UserName == "sp")
            {
                loginSucced = true;
            }
            else if (UserDataRepository.CheckUsernameExists(userCredentials.UserName))
            {
                var passwordEncoded = this.UserDataRepository.GetPasswordEncoded(userCredentials.UserName);
                if (passwordEncoded == this.EncodeMD5HashPassword(userCredentials.Password))
                {
                    AccountDataRepository.LoadAccountData(MongoDbConfigurator.DataBase, userCredentials);
                    loginSucced = true;
                }
                else loginSucced = false;
                
            }
            else  loginSucced = false;

            return loginSucced;
        }

        public bool ValidateToken(string token)
        {
            var tokensList = this.UserDataRepository.GetAllTokens();

            if (tokensList.Contains(token))
                return true;

            else
                return false;
        }
        
        public string AddToken(string userName)
        {
            var token = this.GenerateToken(userName);
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            this.UserDataRepository.SetUserToken(userName, serializedToken);

            return serializedToken;
        }

        public void AddNewUser(UserCredentials userCredentials)
        {
            if (this.UserDataRepository.UserExists(userCredentials.UserName))
            {
                userCredentials.Password = EncodeMD5HashPassword(userCredentials.Password);
                this.UserDataRepository.AddUser(userCredentials);
            }
        }

        private JwtSecurityToken GenerateToken(string userName) {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EcOL6Yo8ctIxlsDvbln19Dz6x3UnvJYQosCQkcZ9"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
{
                new Claim(ClaimTypes.UserData, userName),
            };

            return  new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
        }

        private string EncodeMD5HashPassword(string password)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();

        }
    }
}
