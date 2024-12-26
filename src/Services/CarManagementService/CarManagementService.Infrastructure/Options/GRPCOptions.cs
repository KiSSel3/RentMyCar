namespace CarManagementService.Infrastructure.Options;

public class GRPCOptions
{
    public const string SectionName = "GRPC";
    
    public string ConnectionString { get; set; }
}