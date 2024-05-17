using DocumentFormat.OpenXml.Presentation;
using LLMEducation.Data.Entity;
using LLMEducation.Data.Migrations;
using LLMEducation.Data.UIModels;
using LLMEducation.Enums;
using LLMEducation.Repos;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LLMEducation.Service
{
    public class UserService
    {

        public readonly UserRepo userRepo;
        public readonly AuthService authService;

        public UserService(UserRepo _userRepo, AuthService _authService)
        {
            userRepo = _userRepo;
            authService = _authService;
        }

        public async Task Register(string firstName, string lastName, string email, string password, int accountType)
        {
            UserRole role = (UserRole)accountType;
            User user = new User() { Email = email, Password = EncodePasswordToBase64(password), Name = firstName, LastName = lastName, Role = role };
            await userRepo.Save(user);
        }

        public async Task<User?> Login(string email, string password)
        {
            password = EncodePasswordToBase64(password);
            var user = await userRepo.GetUser(email, password);
            if (user != null)
            {
                authService.SetUser(user);
            }
            return user;
        }

        public static string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

    }

}
