using BaSalesManagementApp.Dtos.WarehouseDTOs;


namespace BaSalesManagementApp.Business.Services
{
    /// <summary>
    /// Warehose sınıfı , depolarla ilgili CRUD işlemlerini gerçekleştirir.
    /// </summary>
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IBranchRepository _branchRepository;

        /// <summary>
        /// Warehouse kurucusu, IWarehouseRepository bağımlılını alır.
        /// </summary>
        /// <param name="warehouseRepository"></param>
        public WarehouseService(IWarehouseRepository warehouseRepository, IBranchRepository branchRepository)
        {
            _warehouseRepository = warehouseRepository;
            _branchRepository = branchRepository;
        }

        /// <summary>
        /// Yeni bir depo oluşturur.
        /// </summary>
        /// <param name="warehouseCreateDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IDataResult<WarehouseDTO>> AddAsync(WarehouseCreateDTO warehouseCreateDTO)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(warehouseCreateDTO.BranchId);
                if (branch == null)
                {
                    return new ErrorDataResult<WarehouseDTO>(Messages.BRANCH_LISTED_ERROR);
                }
                var warehouse = warehouseCreateDTO.Adapt<Warehouse>();
                await _warehouseRepository.AddAsync(warehouse);
                await _warehouseRepository.SaveChangeAsync();
                return new SuccessDataResult<WarehouseDTO>(warehouse.Adapt<WarehouseDTO>(), Messages.Warehouse_CREATED_SUCCESS);                
            }
            catch (Exception)
            {

                return new ErrorDataResult<WarehouseDTO>(Messages.Warehouse_CREATE_FAILED);
            }
        }

        /// <summary>
        /// Belirtilen Id'ye ait depoyu siler
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IResult> DeleteAsync(Guid warehouseId)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (warehouse == null)
                {
                    return new ErrorDataResult<WarehouseDTO>(Messages.Warehouse_NOT_FOUND);                    
                }
                await _warehouseRepository.DeleteAsync(warehouse);
                await _warehouseRepository.SaveChangeAsync();
                return new SuccessDataResult<WarehouseDTO>(warehouse.Adapt<WarehouseDTO>(), Messages.Warehouse_DELETED_SUCCESS);                
            }
            catch (Exception)
            {
                return new ErrorDataResult<WarehouseDTO>(Messages.Warehouse_DELETE_FAILED);                

            }
        }

        /// <summary>
        /// Tüm depoları listeler
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IDataResult<List<WarehouseListDTO>>> GetAllAsync()
        {
            try
            {
                var warehouses = await _warehouseRepository.GetAllAsync();
                var branches = await _branchRepository.GetAllAsync();
                if (warehouses == null )
                {
                    return new ErrorDataResult<List<WarehouseListDTO>>(new List<WarehouseListDTO>(), Messages.Warehouse_LIST_FAILED);
                }
                else if(warehouses.Count() ==0)
                {
                    return new ErrorDataResult<List<WarehouseListDTO>>(new List<WarehouseListDTO>(), Messages.Warehouse_LIST_EMPTY);
                }

                var warehouseListDTOs = warehouses.Adapt<List<WarehouseListDTO>>();

                foreach (var warehouseDTO in warehouseListDTOs)
                {
                    var branch = branches.FirstOrDefault(b => b.Id == warehouseDTO.BranchId);
                    warehouseDTO.BranchName = branch?.Name;
                }

                return new SuccessDataResult<List<WarehouseListDTO>>(warehouseListDTOs, Messages.Warehouse_LISTED_SUCCESS);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<WarehouseListDTO>>(new List<WarehouseListDTO>(), Messages.Warehouse_LIST_FAILED);
            }
        }
        /// <summary>
        /// Tüm depoları asenkron olarak getirir ve sağlanan sıralama türüne göre listeyi sıralar.
        /// </summary>
        /// <param name="sortOrder">
        /// Depo listesinin sıralama düzeni. Geçerli değerler:
        /// "date" - Oluşturulma tarihine göre azalan sırayla sıralar (en yeni ilk).
        /// "datedesc" - Oluşturulma tarihine göre artan sırayla sıralar (en eski ilk).
        /// "alphabetical" - Depo adına göre artan sırayla sıralar (A-Z).
        /// "alphabeticaldesc" - Depo adına göre azalan sırayla sıralar (Z-A).
        /// </param>
        /// <returns>
        /// Sıralanmış depo listesini içeren List<WarehouseListDTO> döner.
        /// Başarılı olursa, sonuç sıralanmış depo listesini ve bir başarı mesajını içerir.
        /// Eğer depo bulunamazsa veya bir hata oluşursa, sonuç bir hata mesajını içerir.
        /// </returns>

        public async Task<IDataResult<List<WarehouseListDTO>>> GetAllAsync(string sortOrder)
        {
            try
            {
                var warehouses = await _warehouseRepository.GetAllAsync();
                var warehouseLists = warehouses.Adapt<List<WarehouseListDTO>>();

                if (warehouseLists == null || warehouseLists.Count == 0)
                {
                    return new ErrorDataResult<List<WarehouseListDTO>>(warehouseLists, Messages.Warehouse_LIST_EMPTY);
                }

                switch (sortOrder.ToLower())
                {
                    case "date":
                        warehouseLists = warehouseLists.OrderByDescending(c => c.CreatedDate).ToList();
                        break;
                    case "datedesc":
                        warehouseLists = warehouseLists.OrderBy(c => c.CreatedDate).ToList();
                        break;
                    case "alphabetical":
                        warehouseLists = warehouseLists.OrderBy(c => c.Name).ToList();
                        break;
                    case "alphabeticaldesc":
                        warehouseLists = warehouseLists.OrderByDescending(c => c.Name).ToList();
                        break;
                }

                return new SuccessDataResult<List<WarehouseListDTO>>(warehouseLists,Messages.Warehouse_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<WarehouseListDTO>>(Messages.Warehouse_LIST_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen Id'ye göre ilgili depoyu getirir
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IDataResult<WarehouseDTO>> GetByIdAsync(Guid warehouseId)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (warehouse == null)
                {
                    return new ErrorDataResult<WarehouseDTO>(Messages.Warehouse_NOT_FOUND);
                }
                // WarehouseDTO'yu dönüştür ve BranchName'i ekle
                var warehouseDTO = warehouse.Adapt<WarehouseDTO>();
                var branch = await _branchRepository.GetByIdAsync(warehouse.BranchId);
                if (branch != null)
                {
                    warehouseDTO.BranchName = branch.Name;
                }
                
                return new SuccessDataResult<WarehouseDTO>(warehouse.Adapt<WarehouseDTO>(), Messages.Warehouse_FOUND_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<WarehouseDTO>(Messages.Warehouse_GET_FAILED);
            }
        }

        /// <summary>
        /// Belirtilen depoyu günceller
        /// </summary>
        /// <param name="warehouseUpdateDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IResult> UpdateAsync(WarehouseUpdateDTO warehouseUpdateDTO)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(warehouseUpdateDTO.Id);
                if (warehouse == null)
                {
                    return new ErrorResult(Messages.Warehouse_NOT_FOUND);
                }

                var branch = await _branchRepository.GetByIdAsync(warehouseUpdateDTO.BranchId);
                if (branch == null)
                {
                    return new ErrorResult(Messages.BRANCH_LISTED_ERROR);
                }

                warehouse.Name = warehouseUpdateDTO.Name;
                warehouse.Address = warehouseUpdateDTO.Address;
                warehouse.BranchId = warehouseUpdateDTO.BranchId;

                _warehouseRepository.UpdateAsync(warehouse);
                await _warehouseRepository.SaveChangeAsync();

                return new SuccessResult(Messages.Warehouse_UPDATED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.Warehouse_UPDATE_FAILED);
            }
        }
    }
}
