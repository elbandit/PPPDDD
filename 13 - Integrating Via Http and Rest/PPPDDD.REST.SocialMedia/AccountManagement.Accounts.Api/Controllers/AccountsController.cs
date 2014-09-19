using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Hal;

namespace AccountManagement.Accounts.Api.Controllers
{
    public class AccountsController : ApiController
    {
        private const string EntryPointBaseUrl = "http://localhost:4100/";
        private const string AccountsBaseUrl  = "http://localhost:4101/accountmanagement/";

	    [HttpGet]
        public AccountsRepresentation Index()
        {
            return new AccountsRepresentation
            {
                Href = AccountsBaseUrl + "accounts",
                Rel = "self",
                Links = new List<Link>
                {
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts?page=1",
                        Rel = "alternative",
                    },
                    new Link
                    {
                        // automatically identified as a template 
                        Href = AccountsBaseUrl + "accounts/{accountId}",
                        Rel = "account",
                        Title = "account template"
                    },
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts/123",
                        Rel = "account",
                        Title = "account 123"
                    },
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts/456",
                        Rel = "account",
                        Title = "account 456"
                    },
                    new Link 
                    {
                        Href = AccountsBaseUrl + "acccounts?page=2",
                        Rel = "next"
                    },
                    new Link
                    {
                        Href = EntryPointBaseUrl + "accountmanagement",
                        Rel = "parent"
                    }
                },
            };
        }

        [HttpGet]
        public AccountRepresentation Account(string accountId)
        {
            // canned data.. for now
            return new AccountRepresentation
            {
                Href = AccountsBaseUrl + "accounts/" + accountId,
                Rel = "self",
                AccountId = accountId,
                Name = "Account_" + accountId,
                Links = new List<Link>
                {
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts",
                        Rel = "collection", 
                    },
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts/" + accountId + "/followers",
                        Rel = "followers", 
                    },
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts/" + accountId + "/following",
                        Rel = "following",
                    },
                    new Link
                    {
                        Href = AccountsBaseUrl + "accounts/" + accountId + "/blurbs",
                        Rel = "blurbs",
                    }
                }
            };
        }

    }

    public class AccountRepresentation : Representation
    {
        public string AccountId { get; set; }
        public string Name { get; set; }

        protected override void CreateHypermedia()
        {
        }
    }

    public class AccountsRepresentation : Representation
    {
        protected override void CreateHypermedia()
        {
        }
    }
}
