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
    public class AuthService
    {

        private User currentUser;

        public AuthService()
        {
            Console.WriteLine("new");
        }

        public void SetUser(User user)
        {
            currentUser = user;
        }

        public bool isAdmin()
        {
            if(currentUser != null && currentUser.Role == UserRole.Admin) {return true;} 
            return false;
        }

    }

}
