using System.Linq.Expressions;
using AutoMapper;
using Catalog.API.Dtos.Ingredient;
using Catalog.API.Dtos.Package;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Repositories;
using Catalog.API.Repositories.impl;

namespace Catalog.API.Services.impl
{
    public class PackageService : IPackageService
    {

        private readonly IPackageRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIngredientRepository _ingredientRepository;

        public PackageService(IPackageRepository repository, IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
            
        }



        public async Task AddAsync(Package entity)
        {
            await _repository.AddAsync(entity);
        }

        public Task<IEnumerable<Package>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Package?> GetByIdAsync(string id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task<(IEnumerable<Package> Items, long TotalCount)> GetPagedAsync(string? searchTerm, int page, int pageSize)
        {
            Expression<Func<Package, bool>> filter = p =>
                string.IsNullOrWhiteSpace(searchTerm) ||
                p.Name.ToLower().Contains(searchTerm.ToLower());

            int skip = (page - 1) * pageSize;
            return await _repository.GetPagedAsync(filter,skip,pageSize);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return _repository.RemoveAsync(id);
        }

        public Task<bool> UpdateAsync(string id, Package entity)
        {
            return _repository.UpdateAsync(id, entity);
        }

        public async Task<PackageDetailDto> GetPackageDetailAsync(string packageId)
        {
            var package = await _repository.GetByIdAsync(packageId);
            if (package == null)
                throw new ProductNotFoundException(packageId);

            var ingredientIds = package.Ingredients.Select(i => i.IngredientId).ToList();

            var ingredientTasks = ingredientIds.Select(id => _ingredientRepository.GetByIdAsync(id));
            var ingredients = (await Task.WhenAll(ingredientTasks))
                              .Where(i => i != null)
                              .ToList()!;

            var ingredientDtos = package.Ingredients.Select(pi =>
            {
                var ing = ingredients.FirstOrDefault(i => i.Id == pi.IngredientId);
                if (ing == null)
                    throw new ProductNotFoundException($"Ingredient not found: {pi.IngredientId}");

                return _mapper.Map<IngredientInPackageDto>((pi, ing));
            }).ToList();

            return new PackageDetailDto
            {
                Id = package.Id,
                Name = package.Name,
                Ingredients = ingredientDtos,
                TotalPrice = ingredientDtos.Sum(i => i.UnitPrice * (decimal)i.Quantity)
            };
        }

    }
}
