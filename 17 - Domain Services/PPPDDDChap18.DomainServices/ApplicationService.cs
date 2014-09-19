using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices
{
    // Application Service
    public class MultiMemberInsurancePremium
    {
        private IPolicyRepository policyRepository;
        private IMemberRepository memberRepository;

        // Domain Service
        private IMultiMemberPremiumCalculator calculator;

        public MultiMemberInsurancePremium(IPolicyRepository policyRepository, IMemberRepository memberRepository,
            IMultiMemberPremiumCalculator calculator)
        {
            this.policyRepository = policyRepository;
            this.memberRepository = memberRepository;
            this.calculator = calculator;
        }

        public Quote GetQuote(int policyId, IEnumerable<int> memberIds)
        {
            var existingMainPolicy = policyRepository.Get(policyId);
            var additionalMembers = memberRepository.Get(memberIds);
            // pass entities into Domain Service
            var multiMemberQuote = calculator.CalculatePremium(existingMainPolicy, additionalMembers);

            return multiMemberQuote;
        }
    }

    /*         Domain Objects      */
    public interface IPolicyRepository
    {
        Policy Get(int policyId);
    }

    public interface IMemberRepository
    {
        IEnumerable<Member> Get(IEnumerable<int> memberIds);
    }

    // Entities
    public class Policy
    {
        public Guid Id { get; protected set; }

        // ...
    }

    public class Member
    {
        public Guid Id { get; protected set; }

        // ...
    }

    // Domain Service interface
    public interface IMultiMemberPremiumCalculator
    {
        Quote CalculatePremium(Policy mainPolicy, IEnumerable<Member> additionalMembers);
    }

    // Value Object
    public class Quote
    {
        // ...
    }
}
