using PPPDDDChap18.DomainServices.Insurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.Insurance.Application
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
}
