using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;
using Raven.Client;
using Raven.Abstractions.Exceptions;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Application.BusinessUseCases
{
    public class CreateAccount
    {
        private IPayAsYouGoAccountRepository _payAsYouGoAccountRepository;
        private IDocumentSession _unitOfWork;

        public CreateAccount(IPayAsYouGoAccountRepository payAsYouGoAccountRepository,
                           IDocumentSession unitOfWork)
        {
            _payAsYouGoAccountRepository = payAsYouGoAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public void Execute(Guid id)
        {
            var payAsYouGoAccount = new PayAsYouGoAccount(id, new Money(10m));

            _payAsYouGoAccountRepository.Add(payAsYouGoAccount);

            _unitOfWork.SaveChanges();            
        }
    }
}
