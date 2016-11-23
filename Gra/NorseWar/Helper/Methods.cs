using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Helper
{
    public class Methods
    {
        public static int? userId { get; set; }
        public static string userMail { get; set; }

        public static void SaveUserSession(int? id, string mail)
        {
            userId = id;
            userMail = mail;
        }

        public static string RegisterSuccess { get; set; }  //powodzenie rejestracji
        public static string LoginFailed { get; set; }   //nie ma takiego konta
        public static string AccountActive { get; set; }   //konto zajete

    }
}