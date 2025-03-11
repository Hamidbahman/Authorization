using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;
public class Menu : BaseEntity
{
    public string MenuKey { get; private set; }
    public int Priority { get; private set; }
    public string Icon { get; private set; }

    [ForeignKey("ActeeId")]
    public long ActeeId { get; private set; }
    public Actee Actee { get; private set; }

    private Menu(){}
    public Menu(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        string menuKey,
        int priority,
        string icon,
        long acteeId
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)
    {
        MenuKey = menuKey;
        Priority = priority;
        Icon = icon;
        ActeeId = acteeId;
    }
}
