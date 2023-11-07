namespace RDF.Arcana.API.Common.Messaging;

public interface ICommand : IRequest, ICommandBase
{
}

public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
{
}

public interface ICommandBase
{
}