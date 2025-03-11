
namespace Domain.Entities;
public class User : BaseEntity
{
    public string Uuid { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalCode { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? Description { get; private set; }

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    private User(){}

    public User(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        string uuid,
        string firstName,
        string lastName,
        string nationalCode,
        string email,
        string phoneNumber,
        string? description
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)
    {
        Uuid = uuid;
        FirstName = firstName;
        LastName = lastName;
        NationalCode = nationalCode;
        Email = email;
        PhoneNumber = phoneNumber;
        Description = description;
    }
}
