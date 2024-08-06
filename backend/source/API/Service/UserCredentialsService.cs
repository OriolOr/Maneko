using Microsoft.IdentityModel.Tokens;
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

        public UserCredentialsService(IUserDataRepository userDataRepository)
        {
            this.UserDataRepository = userDataRepository;
        }

        public bool Authenticate(UserCredentials userCredentials)
        {
            bool loginSucced;

            if (UserDataRepository.UserExists(userCredentials.UserName))
            {
                var passwordEncoded = this.UserDataRepository.GetPasswordEncoded(userCredentials.UserName);
                if (passwordEncoded == this.EncodeMD5HashPassword(userCredentials.Password))
                {
                    loginSucced = true;
                }
                else loginSucced = false;
                
            }
            else  loginSucced = false;

            return loginSucced;
        }

        public bool ValidateToken(string token)
            => this.UserDataRepository.GetAllTokens().Contains(token);
       
        
        public string AddToken(string userName)
        {
            var token = this.GenerateToken(userName);
            var expirationDate = Convert.ToUInt32(token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value);
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            this.UserDataRepository.SetUserToken(userName, serializedToken, expirationDate);

            return serializedToken;
        }

        public bool AddNewUser(UserCredentials userCredentials)
        {
            if (this.UserDataRepository.UserExists(userCredentials.UserName))
            {
                return false;
            }

            userCredentials.Password = EncodeMD5HashPassword(userCredentials.Password);
            this.UserDataRepository.AddUser(userCredentials);
            return true;
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
