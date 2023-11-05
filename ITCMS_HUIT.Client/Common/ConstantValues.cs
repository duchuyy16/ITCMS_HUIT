namespace ITCMS_HUIT.Client.Common
{
    public class ConstantValues
    {
        public class Authenticate
        {
            public const string Login = "api/Authenticate/login";
            public const string Register = "api/Authenticate/register";
            public const string RegisterAdmin = "api/Authenticate/register-admin";
            public const string Logout = "api/Authenticate/logout";
            public const string ResetPasswordToken = "api/Authenticate/reset-password-token/{0}";
            public const string ResetPassword = "api/Authenticate/reset-password";
            public const string ChangePassword = "api/Authenticate/change-password";
        }

        public class Contact
        {
            public const string GetAllContacts = "api/Contacts/GetAllContacts";
            public const string AddContact = "api/Contacts/AddContact";
            public const string GetContactById = "api/Contacts/GetContactById/{0}";
            public const string UpdateContact = "api/Contacts/UpdateContact";
            public const string DeleteContact = "api/Contacts/DeleteContact";
            public const string FindContactById = "api/Contacts/FindContactById/{0}";
            public const string ExistsById = "api/Contacts/ExistsById/{0}";
        }
    }
}
