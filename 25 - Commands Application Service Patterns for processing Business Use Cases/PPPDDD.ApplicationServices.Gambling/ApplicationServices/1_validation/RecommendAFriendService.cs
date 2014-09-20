using System;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.ApplicationValidation
{
    public class RecommendAFriendService
    {
        public void RecommendAFriend(int referrerId, NewAccount friendsAccountDetails)
        {
            Validate(friendsAccountDetails);

            // ...
        }

        // technical validation carried out at the application level
        private void Validate(NewAccount account)
        {
            if (!account.Email.Contains("@"))
                throw new ValidationFailure("Not a valid email address");

            if (account.Email.Length >= 50)
                throw new ValidationFailure("Email address must be less than 50 characters");

            if (account.Nickname.Length >= 25)
                throw new ValidationFailure("Nickname must be less than 25 characters");

            if (String.IsNullOrWhiteSpace(account.Email))
                throw new ValidationFailure("You must supply an email");

            if (String.IsNullOrWhiteSpace(account.Nickname))
                throw new ValidationFailure("You must supply a Nickname");
        }
    }

    public class NewAccount
    {
        public string Email { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }
    }

    public class ValidationFailure : Exception 
    {
        public ValidationFailure(string message) : base(message) { }
    }
}
