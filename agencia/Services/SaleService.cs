using agencia.Models.Dto;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using AutoMapper;

namespace agencia.Services
{
    public class SaleService : ISaleService
    {
        public readonly ISaleRepository _saleRepository;
        public readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDTO> CreateAsync(SaleDTO saleDTO)
        {
            if (saleDTO.serviceId != 0 && saleDTO.packageId != 0)
            {
                throw new InvalidOperationException("Sale cannot be associated with both Service and Package");
            }
            if (saleDTO.serviceId == 0 && saleDTO.packageId == 0)
            {
                throw new InvalidOperationException("Sale must be associated with either Service or Package");
            }

            var sale = _mapper.Map<Sale>(saleDTO);

            // Cargar relaciones a través del repositorio
            if (saleDTO.serviceId != 0)
            {
                var service = await _saleRepository.GetServiceByIdAsync(saleDTO.serviceId)
                    ?? throw new KeyNotFoundException($"Service with id {saleDTO.serviceId} not found");
                sale.service = service;
                sale.serviceId = service.Id;
                sale.packageId = null;
            }
            else if (saleDTO.packageId != 0)
            {
                var package = await _saleRepository.GetPackageByIdAsync(saleDTO.packageId)
                    ?? throw new KeyNotFoundException($"Package with id {saleDTO.packageId} not found");
                sale.package = package;
                sale.packageId = package.Id;
                sale.serviceId = null;
            }

            var createdSale = await _saleRepository.InsertAsync(sale);
            return _mapper.Map<SaleDTO>(createdSale);
        }

        public async Task DeleteAsync(long Id)
        {
            await _saleRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<SaleDTO>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<SaleDTO> GetByIdAsync(long Id)
        {
            var sale = await _saleRepository.GetByIdAsync(Id);
            return _mapper.Map<SaleDTO>( sale);
        }

        public async Task UpdateAsync(long Id, SaleDTO saleDTO)
        {
            if (saleDTO.serviceId != 0 && saleDTO.packageId != 0)
            {
                throw new InvalidOperationException("Sale cannot be associated with both Service and Package");
            }
            if (saleDTO.serviceId == 0 && saleDTO.packageId == 0)
            {
                throw new InvalidOperationException("Sale must be associated with either Service or Package");
            }

            var saleEntity = await _saleRepository.GetByIdAsync(Id);
            _mapper.Map(saleDTO, saleEntity);

            if (saleDTO.serviceId != 0)
            {
                saleEntity.package = null;
                saleEntity.packageId = null;

                saleEntity.service = await _saleRepository.GetServiceByIdAsync(saleDTO.serviceId)
                    ?? throw new KeyNotFoundException($"Service with Id {saleDTO.serviceId} not found.");
                saleEntity.serviceId = saleEntity.service.Id;
            }
            else if(saleDTO.packageId != 0)
            {
                saleEntity.service = null;
                saleEntity.serviceId = null;

                saleEntity.package = await _saleRepository.GetPackageByIdAsync(saleDTO.packageId)
                    ?? throw new KeyNotFoundException($"Package with Id {saleDTO.packageId} not found.");
                saleEntity.packageId = saleEntity.package.Id;
            }
            await _saleRepository.UpdateAsync(saleEntity);
        }
    }
}
