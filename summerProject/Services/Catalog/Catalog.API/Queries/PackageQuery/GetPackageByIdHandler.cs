using BuildingBlocks.CQRS;
using Catalog.API.Dtos.Package;
using Catalog.API.Exceptions;
using Catalog.API.Services;
using Catalog.API.Services.impl;

namespace Catalog.API.Queries.PackageQuery
{
    public record GetPackagewithDetailByIdQuery(string PackageId) : IQuery<GetPackageWithDetailByIdResult>;

    public record GetPackageWithDetailByIdResult(PackageDetailDto package);

    public class GetPackageByIdHandler(IPackageService packageService) : IQueryHandler<GetPackagewithDetailByIdQuery, GetPackageWithDetailByIdResult>
    {
        public async Task<GetPackageWithDetailByIdResult> Handle(GetPackagewithDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var packageDetail = await packageService.GetPackageDetailAsync(request.PackageId);

            if (packageDetail == null)
                throw new ProductNotFoundException("Package not found");

            return new GetPackageWithDetailByIdResult(packageDetail);
        }
    }
}
