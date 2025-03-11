using System;
using Domain.Entities;

namespace Domain;

public interface IApplicationPackageRepository
{
    Task<ApplicationPackage?> GetPackageByApplicationIdAsync(long applicationId);

    Task<List<long>> GetApplicationPackageIdsByApplicationId(long applicationId);

}
