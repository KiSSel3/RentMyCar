namespace IdentityService.BLL.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base() { }
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string entityName, string id)
        : base($"Can't find entity of type {entityName} with ID {id}.") { }
}