﻿using BCrypt.Net;
using InterviewsApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passHash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, passHash);
            }
            catch(SaltParseException ex)
            {
                return false;
            }
        }
    }
}
