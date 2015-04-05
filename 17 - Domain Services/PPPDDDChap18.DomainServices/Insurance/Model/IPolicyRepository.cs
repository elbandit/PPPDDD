using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.Insurance.Model
{
    public interface IPolicyRepository
    {
        Policy Get(int policyId);
    }

}
