using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Consts
{
    public static class Messages
    {
        public static string EmptyNameMessage = "Name cannot be left blank.";
        public static string MaximumSymbolMessage = "You have reached the maximum character limit.";
        public static string MaximumNameSymbolMessage = "Name must be fewer than 25 characters.";
        public static string MaximumLastNameSymbolMessage = "Last name must be fewer than 35 characters.";
        public static string InvalidEmailMessage = "The provided email address is not valid.";
        public static string EmptyEmailMessage = "Email cannot be empty.";
        public static string UsedEmailMessage = "The email provided is already in use.";
        public static string MaximumUsernameSymbolMessage = "Username must be fewer than 16 characters.";
        public static string NoUserFoundMessage = "No user found.";
        public static string NoJobPostsFoundMessage = "No posts found.";
        public static string EmptyPhoneNumberMessage = "Phone number is required.";
    }
}
