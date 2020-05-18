namespace EventmanagerApi.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        
        public static class OrganizedEvents
        {
            public const string GetAll = Base + "/events";
            public const string Get = Base + "/events/{eventId}";
            public const string Create = Base + "/events";
            public const string Update = Base + "/events/{eventId}";
            public const string Delete = Base + "/events/{eventId}";
        }
        
        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
            public const string LoggedIn = Base + "/identity/loggedin";
        }

        public static class ApplicationUser
        {
            public const string Get = Base + "/user";
            public const string Update = Base + "/user";
            public const string ChangePassword = Base + "/user";
        }
    }
}