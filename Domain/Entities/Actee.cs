using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Actee : BaseEntity
{
    public string Uuid {get; private set;}
    public ActeeTypes ActeeType {get; private set;}
    public string Title {get; private set;}
    public string Description {get; private set;}
    public StatusTypes StatusType {get; private set;}
    [ForeignKey("ApplicationPackageId")]
    public ApplicationPackage ApplicationPackage {get; private set;}
    public long ApplicationPackageId {get; private set;}
    public Actee (){}

    public Actee(
        string uuid,
        ActeeTypes acteeType,
        string title,
        string description,
        StatusTypes status,
        long applicationPackageId
    )
    {
        Uuid = uuid;
        ActeeType = acteeType;
        Title = title;
        Description = description;
        StatusType = status;
        ApplicationPackageId = applicationPackageId;
    }
}
