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
    public class RecordPhonecall
    {
        private IPayAsYouGoAccountRepository _payAsYouGoAccountRepository;
        private IDocumentSession _unitOfWork;
        private IClock _clock;

        public RecordPhonecall(IPayAsYouGoAccountRepository payAsYouGoAccountRepository,
                           IDocumentSession unitOfWork, IClock clock)
        {
            _payAsYouGoAccountRepository = payAsYouGoAccountRepository;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public void Execute(Guid id, string phoneNumber, DateTime callStart, int callLengthInMinutes)
        {
            try{
                var payAsYouGoAccount = _payAsYouGoAccountRepository.FindBy(id);

                var numberDialled = new PhoneNumber(phoneNumber);
                var phoneCall = new PhoneCall(numberDialled, callStart, new Minutes(callLengthInMinutes));

                payAsYouGoAccount.Record(phoneCall, new PhoneCallCosting(), _clock);

                _payAsYouGoAccountRepository.Save(payAsYouGoAccount);

                _unitOfWork.SaveChanges();
            }
            catch (ConcurrencyException ex)
            {
                _unitOfWork.Advanced.Clear();

                // TODO: Add logic to retry X times then move to an error queue
                // Execute(id, phoneNumber, callStart, callLengthInMinutes);

                throw ex;
            }
        }
    }
}
