using DAL.Auth;
using Dapper;
using RepositoryLayer.Models;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelRepositories
{
    public class ApplicationUserRepository : GenericRepo<ApplicationUser>
    {
        private readonly IJwtAuthManager _jwtAuthManager;

        public ApplicationUserRepository(IDbTransaction dbTransaction,IJwtAuthManager jwtAuthManager) : base(dbTransaction)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        private readonly string StoreProcedure = "usp_ApplicationUser";

        public string Register(ApplicationUser user)
        {
            var hashPass = PasswordEncode(user.Password);
            var parms = new DynamicParameters();
            parms.Add("@Name", user.Name);
            parms.Add("@Email", user.Email);
            parms.Add("@Password", hashPass);
            parms.Add("@StatementType", "Insert");

            var insertedId = Insert(StoreProcedure, parms);
            return "";
        }

        public string Login(ApplicationUser user)
        {
            var parms = new DynamicParameters();
            parms.Add("@Name", user.Name);
            parms.Add("@StatementType", "SelectByName");

            var currentUser = GetByIdOrName(StoreProcedure,parms);
            var checkPassword = CheckPassword(user.Password, currentUser.Password);
            if (currentUser != null && checkPassword)
            {
                var token = _jwtAuthManager.GenerateToken(user.Name);

                return token;
            }
            return null;
        }

        public static bool CheckPassword(string enteredPass,string retrivedPass)
        {
            var result = PasswordDecode(retrivedPass) == enteredPass;

            return result;
        }

        public static string PasswordEncode(string password)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string PasswordDecode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
