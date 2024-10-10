namespace CarManagementService.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base() { }
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string entityName, Guid id)
        : base($"Can't find entity of type {entityName} with ID {id}.") { }
    public EntityNotFoundException(string entityName, string name)
        : base($"Can't find entity of type {entityName} with Name {name}.") { }
}