namespace BookingService.BLL.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException() : base() { }
    public EntityAlreadyExistsException(string message) : base(message) { }
    public EntityAlreadyExistsException(string entityName, Guid id)
        : base($"Entity of type {entityName} already exists with ID {id}.") { }
}