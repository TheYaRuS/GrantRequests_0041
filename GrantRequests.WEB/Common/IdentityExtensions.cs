using System;
using System.Security.Principal;

namespace GrantRequests.Common
{
    public static class IdentityExtensions
    {
        public static char identitySeparator = '|';
        public static string GetName(this IIdentity identity)
        {
            if (!string.IsNullOrEmpty(identity.Name))
                return identity.Name.Split(identitySeparator)[0];
            return string.Empty;
        }

        public static Role GetRole(this IIdentity identity)
        {
            if (string.IsNullOrEmpty(identity.Name))
                return Role.None;
            return (Role)Enum.Parse(typeof(Role), identity.Name.Split(identitySeparator)[1]);
        }

        public static int GetId(this IIdentity identity)
        {
            if (string.IsNullOrEmpty(identity.Name))
                return -1;
            return int.Parse(identity.Name.Split(identitySeparator)[2]);
        }
    }
}