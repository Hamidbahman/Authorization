namespace Domain.Entities;
public class Mask : BaseEntity
{
    public long PermissionId { get; private set; }
    public Permission Permission { get; private set; }


    private Mask(){}
    public Mask(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        long permissionId
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)
    {
        PermissionId = permissionId;
    }
}
