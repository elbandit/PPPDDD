using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Hal;

namespace AccountManagement.EntryPoint.Api.Controllers
{
    public class EntryPointController : ApiController
    {
        private const string EntryPointBaseUrl = "http://localhost:4100/";
        private const string AccountsBaseUrl = "http://localhost:4101/";

        [HttpGet]
        public EntryPointRepresentation Get()
        {
            return new EntryPointRepresentation
            {
                Href = EntryPointBaseUrl + "accountmanagement",
                Rel = "self",
                Links = new List<Link>
                {
                    new Link
                    {
                        Href = AccountsBaseUrl + "accountmanagement/accounts",
                        Rel = "accounts"
                    },
                    new Link
                    {
                        Href = "http://localhost:4102/accountmanagement/beganfollowing",
                        Rel = "beganfollowing"
                    }
                }
            };
        }
    }

    public class EntryPointRepresentation : Representation
    {
        protected override void CreateHypermedia() { }
    }
}