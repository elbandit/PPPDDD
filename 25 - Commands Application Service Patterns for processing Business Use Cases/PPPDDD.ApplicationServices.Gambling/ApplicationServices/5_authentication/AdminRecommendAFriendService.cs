using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices._5_authentication
{
    public class AdminRecommendAFriendService
    {
        private IAuthenticationService authentication;
        private IAuthorizationService authorization;
        
        public AdminRecommendAFriendService(IAuthenticationService authentication,
                                        IAuthorizationService authorization)
        {
            this.authentication = authentication;
            this.authorization = authorization;
        }
    
        public void ReferAFriend(int referrerId, int friendId)
        {
            if (!authentication.IsLoggedInUser())
                throw new AuthenticationError();

            if (!authorization.IsCurrentUserAdmin())
                throw new AuthorizationError();

            // look up customers

            // apply referral policy
        }
    }

    public interface IAuthenticationService
    {
        bool IsLoggedInUser();
    }

    public interface IAuthorizationService
    {
        bool IsCurrentUserAdmin();
    }

    public class AuthenticationError : Exception { }

    public class AuthorizationError : Exception { }
}