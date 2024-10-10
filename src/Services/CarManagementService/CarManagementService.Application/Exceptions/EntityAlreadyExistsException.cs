namespace CarManagementService.Application.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException() : base() { }
    public EntityAlreadyExistsException(string message) : base(message) { }
    public EntityAlreadyExistsException(string entityName, Guid id)
        : base($"Entity of type {entityName} already exists with ID {id}.") { }
    public EntityAlreadyExistsException(string entityName, string name)
        : base($"Entity of type {entityName} already exists with Name {name}.") { }
}