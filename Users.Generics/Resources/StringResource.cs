﻿using System;
namespace Users.Generics.Resources
{
    public class StringResource
    {
        public const string RecoverMessageSentSuccess = "Please check your email inbox to reset password.";
        public const string RecoverMessageSentFail = "This email are not registered.";
        public const string RecoverMessageSuccess = "Password updated!";
        public const string RecoverMessageFail = "A inesperated error occurred. Please try again or contact the system admin.";
        public const string RecoverMessageUserDontExists = "This user doesn't exists";
        public const string ValidationMessageInvalidUser = "This user is invalid";
        public const string ValidationMessageUserDontExists = "This user doesn't exists";
        public const string ValidationMessageUserAlreadyExist = "This email are in use for another user";
    }
}
