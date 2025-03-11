using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
namespace Domain.Entities;

public class Actee : BaseEntity
{
    public string Uuid { get; private set; }
    public ActeeTypes ActeeType { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public StatusTypes StatusType { get; private set; }

    [ForeignKey("ApplicationPackageId")]
    public ApplicationPackage ApplicationPackage { get; private set; }

    public long ApplicationPackageId { get; private set; }
    public ICollection<Permission> Permissions { get; private set; } = new List<Permission>();
    public ICollection<Menu> Menus { get; private set; } = new List<Menu>();

    private Actee() {}

    public Actee(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        string uuid,
        ActeeTypes acteeType,
        string title,
        string description,
        StatusTypes status,
        long applicationPackageId
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)
    {
        Uuid = uuid;
        ActeeType = acteeType;
        Title = title;
        Description = description;
        StatusType = status;
        ApplicationPackageId = applicationPackageId;
    }
}
