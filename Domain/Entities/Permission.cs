using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
namespace Domain.Entities;
public class Permission : BaseEntity
{
    [ForeignKey("ActeeId")]
    public long ActeeId { get; private set; }
    public Actee Actee { get; private set; }

    [ForeignKey("RoleId")]
    public long RoleId { get; private set; }
    public Role Role { get; private set; }

    public StatusTypes Status { get; private set; }
    public int Granting { get; private set; }

    private Permission(){}
    public Permission(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        long acteeId,
        long roleId,
        StatusTypes status,
        int granting
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)
    {
        ActeeId = acteeId;
        RoleId = roleId;
        Status = status;
        Granting = granting;
    }
}
