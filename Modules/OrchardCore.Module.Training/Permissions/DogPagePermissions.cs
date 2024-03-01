using OrchardCore.Security.Permissions;

namespace OrchardCore.Module.Training.Permissions;

public class DogPagePermissions : IPermissionProvider
{
    public static readonly Permission ManageDogs = new Permission(nameof(ManageDogs), "Can manage dogs");
    public static readonly Permission EditDog = new Permission("EditDog", "Can edit dogs");

    public Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        return Task.FromResult(new[] { ManageDogs, EditDog }.AsEnumerable());
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        return new[]
        {
            new PermissionStereotype
            {
                Name = "Administrator",
                Permissions = new[] { ManageDogs, EditDog }
            },
            // more roles as needed
        };
    }
}



