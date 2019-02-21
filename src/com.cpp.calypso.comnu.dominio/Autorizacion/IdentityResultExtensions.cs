using Abp.Collections.Extensions;
using Microsoft.AspNet.Identity;
using System;

namespace com.cpp.calypso.comun.dominio
{
    public static class IdentityResultExtensions
    {
       
        /// <summary>
        /// Checks errors of given <see cref="IdentityResult"/> and throws <see cref="UserFriendlyException"/> if it's not succeeded.
        /// </summary>
        /// <param name="identityResult">Identity result to check</param>
        public static void CheckErrors(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return;
            }

            var msg = identityResult.Errors.JoinAsString(", ");

            throw new GenericException(msg, msg);
        }

         
    }
}
