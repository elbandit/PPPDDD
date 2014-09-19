using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.CommandProcessorChained
{
    public interface ICommandProcessor<T>
    {
        void Process(T command);
    }

    public class RecommendAFriend
    {
        public int ReferrerId { get; set; }
    }

    public class RecommendAFriendProcessor : ICommandProcessor<RecommendAFriend>
    {
        public void Process(RecommendAFriend command)
        {
            Console.WriteLine("Processing ReferAFriend command");
        }
    }

    public class LoggingProcessor<T> : ICommandProcessor<T>
    {
        private ICommandProcessor<T> nextLinkInChain;

        public LoggingProcessor(ICommandProcessor<T> processor)
        {
            this.nextLinkInChain = processor;
        }

        public void Process(T command)
        {
            // log something before
            nextLinkInChain.Process(command);
            // log something after
        }
    }

    public class TransactionProcessor<T> : ICommandProcessor<T>
    {
        private ICommandProcessor<T> nextLinkInChain;

        public TransactionProcessor(ICommandProcessor<T> processor)
        {
            this.nextLinkInChain = processor;
        }

        public void Process(T command)
        {
            // start transaction
            try
            {
                nextLinkInChain.Process(command);
                // commit transaction
            }
            catch
            {
                // rollback transaction
            }
        }
    }

    public static class Bootstrap
    {
        public static ICommandProcessor<RecommendAFriend> ReferAFriendProcessor { get; set; }

        public static void ConfigureApplication()
        {
            // create inner processor
            var referAFriendProcessor = new RecommendAFriendProcessor();
            // wrap inner processor with logging
            var loggingProcessor = new LoggingProcessor<RecommendAFriend>(referAFriendProcessor);
            // wrap logging processor (that wraps inner) with a transaction
            var transactionProcessor = new TransactionProcessor<RecommendAFriend>(loggingProcessor);

            ReferAFriendProcessor = transactionProcessor;

            // alternatively you can use dependency injection
        }
    }
}