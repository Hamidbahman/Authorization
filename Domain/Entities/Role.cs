using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
namespace Domain.Entities;
public class Role : BaseEntity
{
    public string Uuid { get; private set; }
    public AuthorityType Authority { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public StatusTypes Status { get; private set; }

    [ForeignKey("ApplicationId")]
    public long ApplicationId { get; private set; }
    public Aplication Application { get; private set; }

    public bool IsAdmin { get; private set; }

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public ICollection<Permission> Permissions { get; private set; } = new List<Permission>();

    private Role(){}

    public Role(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        string uuid,
        AuthorityType authority,
        string title,
        string description,
        StatusTypes status,
        long applicationId,
        bool isAdmin
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)
    {
        Uuid = uuid;
        Authority = authority;
        Title = title;
        Description = description;
        Status = status;
        ApplicationId = applicationId;
        IsAdmin = isAdmin;
    }
}
