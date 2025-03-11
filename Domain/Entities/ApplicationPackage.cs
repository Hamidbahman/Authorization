using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities;
public class ApplicationPackage : BaseEntity
{
    [StringLength(100)]
    public string Title { get; private set; }

    [ForeignKey("Application")]
    public long ApplicationId { get; private set; }
    public Aplication Application { get; private set; }

    // Constructor with BaseEntity parameters passed to the base class
    private ApplicationPackage(){}
    public ApplicationPackage(
        long id,
        DateTime createDate,
        DateTime modifyDate,
        DateTime? deleteDate,
        string? deleteUser,
        string? modifyUser,
        string title,
        long applicationId
    ) : base(id, createDate, modifyDate, deleteDate, deleteUser, modifyUser)  // Calling the BaseEntity constructor
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));  // Ensuring Title is not null
        ApplicationId = applicationId;
    }
}
