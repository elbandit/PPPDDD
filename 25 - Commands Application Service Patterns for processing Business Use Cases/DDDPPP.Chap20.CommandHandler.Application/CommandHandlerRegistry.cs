using System;
using Agathas.Storefront.Infrastructure;
using StructureMap;

namespace Agathas.Storefront.Application
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        public Action<TCommand> find_handler_for<TCommand>(TCommand command) where TCommand : IBusinessRequest
        {
            var handler = ObjectFactory.TryGetInstance<ICommandHandler<TCommand>>();

            var transactional_handler = ObjectFactory.GetInstance<TransactionHandler>();

            Action<TCommand> method_to_handle_command = cmd => transactional_handler.action(cmd, handler);

            return method_to_handle_command;
        }
    }
}