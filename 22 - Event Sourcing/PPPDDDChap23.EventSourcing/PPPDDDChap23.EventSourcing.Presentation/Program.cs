using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application;
using StructureMap;
using PPPDDDChap23.EventSourcing.Application.Application.BusinessUseCases;

namespace PPPDDDChap23.EventSourcing.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.Startup();

            var id = Guid.NewGuid();

            var createAccount = ObjectFactory.GetInstance<CreateAccount>();

            Console.WriteLine("Create account");
            Console.WriteLine();
            createAccount.Execute(id);

            Console.WriteLine("Record call");
            Console.WriteLine();

            var recordPhoneCall = ObjectFactory.GetInstance<RecordPhonecall>();

            recordPhoneCall.Execute(id, "07789923557", DateTime.Now, 8); 

            Console.WriteLine("Top up credit");
            Console.WriteLine();

            var topUpCredit = ObjectFactory.GetInstance<TopUpCredit>();

            topUpCredit.Execute(id, 20m);

            Console.WriteLine("Hit any key to continue");
            Console.ReadLine();

            Console.WriteLine("Record call");
            Console.WriteLine();
            
            recordPhoneCall.Execute(id, "07789923557", DateTime.Now, 100); 

        }
    }
}
