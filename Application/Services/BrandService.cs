using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.Application.Services.Helpers;

namespace Fiap.Agnello.CLI.Application.Services
{
    internal class BrandService(ICrudRepository<Brand, int> repository)
    {
        private readonly ICrudRepository<Brand, int> _repository = repository;

        public Result<Brand> Create(CreateBrandDTO dto)
        {
            try
            {
                return Result<Brand>.Ok(_repository.Create(Brand.FromDTO(dto)));
            }
            catch (Exception ex)
            {
                return Result<Brand>.Fail(new($"Erro criando marca: [{ex.Message}]"));
            }
        }

        public Result<List<Brand>> GetAll()
        {
            try
            {
                return Result<List<Brand>>.Ok(_repository.GetAll());
            }
            catch (Exception ex)
            {
                return Result<List<Brand>>.Fail(new($"Erro buscando marcas: [{ex.Message}]"));
            }
        }

        public Result<Brand> FindById(int id)
        {
            try
            {
                Brand? brand = _repository.GetById(id);

                if (brand is null)
                {
                    return Result<Brand>.Fail(new($"Marca com ID [{id}] não encontrada"));
                }

                return Result<Brand>.Ok(brand);

            } catch (Exception ex)
            {
                return Result<Brand>.Fail(new($"Erro buscando marca com id [{id}]: [{ex.Message}]"));
            }
        }

        public Result<Brand> Update(int id, UpdateBrandDTO dto)
        {
            var res = FindById(id);
            if (!res.Success) return res;

            return Update(res.Value!, dto);
        }

        public Result<Brand> Update(Brand brand, UpdateBrandDTO dto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dto.Name)) brand.Name = dto.Name;
                if (!string.IsNullOrWhiteSpace(dto.Country)) brand.Country = dto.Country;

                _repository.Update(brand);

                return Result<Brand>.Ok(brand);
            } catch (Exception ex)
            {
                return Result<Brand>.Fail(new($"Erro atualizando marca de id [{brand.Id}]: [{ex.Message}]"));
            }
        } 

        public Result Delete (int id)
        {
            try
            {
                bool success = _repository.Delete(id);
                if (success)
                {
                    return Result.Ok();
                }

                return Result.Fail(new($"Erro deletando marca de id [{id}]"));
            } catch (Exception ex)
            {
                return Result.Fail(new($"Erro deletando marca de id [{id}]: [{ex.Message}]"));
            }
        }
    }
}
