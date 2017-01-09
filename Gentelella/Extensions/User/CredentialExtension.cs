using Gentelella.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gentelella.Extensions
{
    public static class CredentialExtension
    {
        public static PosCredential ToPosCredential(this CredentialModel credential)
        {
            var posCredential = new PosCredential()
            {
                EmailAddress = credential.EmailAddress,
                UserName = credential.UserName,
                Password = credential.Password
            };

            return posCredential;
        }
    }
}