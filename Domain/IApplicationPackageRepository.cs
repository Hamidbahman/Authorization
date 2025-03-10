using System;

namespace Domain;

public interface IApplicationPackageRepository
{
    Task<List<long>> GetApplicationPackageIdsByApplicationId(long applicationId);

}
