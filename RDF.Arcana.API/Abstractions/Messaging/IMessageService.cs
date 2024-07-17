namespace RDF.Arcana.API.Abstractions.Messaging
{
    public interface IMessageService
    {
        Task<bool> SendMessageAsync(MessageRequest message);
    }
}
