using BaSalesManagementApp.Dtos.CityDTOs;
using BaSalesManagementApp.Entites.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
      
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        //public async Task<IDataResult<List<CityListDTO>>> GetAllCitiesAsync(string sortOrder)
        //{
        //    try
        //    {
        //        var cities = await _cityRepository.GetAllAsync();
        //        var cityList = cities.Adapt<List<CityListDTO>>();

        //        if (cityList == null || cityList.Count == 0)
        //        {
        //            return new ErrorDataResult<List<CityListDTO>>(cityList, _localizer[Messages.CITY_LISTED_EMPTY]);
        //        }

        //        switch (sortOrder.ToLower())
        //        {
        //            case "date":
        //                cityList = cityList.OrderByDescending(c => c.CreatedDate).ToList();
        //                break;
        //            case "datedesc":
        //                cityList = cityList.OrderBy(c => c.CreatedDate).ToList();
        //                break;
        //            case "alphabetical":
        //                cityList = cityList.OrderBy(c => c.Name).ToList();
        //                break;
        //            case "alphabeticaldesc":
        //                cityList = cityList.OrderByDescending(c => c.Name).ToList();
        //                break;
        //            default:
        //                cityList = cityList.OrderBy(c => c.Name).ToList();
        //                break;
        //        }


        //        return new SuccessDataResult<List<CityListDTO>>(cityList, _localizer[Messages.CITY_LISTED_SUCCESS]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorDataResult<List<CityListDTO>>(_localizer[Messages.CITY_LISTED_ERROR] + ex.Message);
        //    }
        //}
        public async Task<IDataResult<CityDTO>> AddAsync(CityCreateDTO cityCreateDTO)
        {
            try
            {
                var data = await _cityRepository.GetAsync(c => c.Name == cityCreateDTO.Name);
                if (data != null)   return new ErrorDataResult<CityDTO>(Messages.CITY_CREATED_ERROR);
                var newCity = cityCreateDTO.Adapt<City>();

                await _cityRepository.AddAsync(newCity);
                await _cityRepository.SaveChangeAsync();

                return new SuccessDataResult<CityDTO>(newCity.Adapt<CityDTO>(),Messages.CITY_CREATED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CityDTO>(Messages.CITY_CREATED_ERROR);
            }
        }

        public async Task<IResult> DeleteAsync(Guid cityId)
        {
            try
            {
                var deletingCity = await _cityRepository.GetByIdAsync(cityId);

                await _cityRepository.DeleteAsync(deletingCity);
                await _cityRepository.SaveChangeAsync();

                return new SuccessResult(Messages.CITY_DELETED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.CITY_DELETED_ERROR);
            }
        }

        public async Task<IDataResult<List<CityListDTO>>> GetAllAsync()
        {
            try
            {
                IEnumerable<City> cities;
                cities = await _cityRepository.GetAllAsync();

                if (cities.Count() == 0)
                {
                    return new ErrorDataResult<List<CityListDTO>>(new List<CityListDTO>(), Messages.CITY_LISTED_EMPTY);
                }

                return new SuccessDataResult<List<CityListDTO>>(cities.Adapt<List<CityListDTO>>(),Messages.CITY_LISTED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<CityListDTO>>(new List<CityListDTO>(), Messages.CITY_LISTED_ERROR);
            }
        }

        public async Task<IDataResult<CityDTO>> GetByIdAsync(Guid cityId)
        {
            try
            {
                var city = await _cityRepository.GetByIdAsync(cityId);

                if (city == null)
                {
                    return new ErrorDataResult<CityDTO>(Messages.CITY_NOT_FOUND);
                }

                return new SuccessDataResult<CityDTO>(city.Adapt<CityDTO>(),Messages.CITY_GET_SUCCESS);
            }
            catch
            {
                return new ErrorDataResult<CityDTO>(Messages.CITY_NOT_FOUND);
            }
        }

        public async Task<IDataResult<CityDTO>> UpdateAsync(CityUpdateDTO cityUpdateDTO)
        {
            try
            {
                var updatingCity = await _cityRepository.GetByIdAsync(cityUpdateDTO.Id);

                if (updatingCity == null)
                {
                    return new ErrorDataResult<CityDTO>(Messages.CITY_NOT_FOUND);
                }

                await _cityRepository.UpdateAsync(cityUpdateDTO.Adapt(updatingCity));
                await _cityRepository.SaveChangeAsync();

                return new SuccessDataResult<CityDTO>(cityUpdateDTO.Adapt<CityDTO>(), Messages.CITY_UPDATED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CityDTO>(Messages.CITY_UPDATED_ERROR);
            }
        }


        public bool CityExist(string cityName)
        {

            return _cityRepository.AnyAsync(c => c.Name == cityName).Result;
        }

        public async Task<IDataResult<List<CityListDTO>>> GetAllAsync(string orderOrder)
        {

            try
            {
                var cities = await _cityRepository.GetAllAsync();
                var cityList = cities.Adapt<List<CityListDTO>>();

                if (cityList == null || cityList.Count == 0)
                {
                    return new ErrorDataResult<List<CityListDTO>>(cityList,Messages.CITY_LISTED_EMPTY);
                }

                switch(orderOrder.ToLower())
                {
                    case "date":
                        cityList = cityList.OrderByDescending(c => c.CreatedDate).ToList();
                        break;
                    case "datedesc":
                        cityList = cityList.OrderBy(c => c.CreatedDate).ToList();
                        break;
                    case "alphabetical":
                        cityList = cityList.OrderBy(c => c.Name).ToList();
                        break;
                    case "alphabeticaldesc":
                        cityList = cityList.OrderByDescending(c => c.Name).ToList();
                        break;
                    default:
                        cityList = cityList.OrderBy(c => c.Name).ToList();
                        break;
                }


                return new SuccessDataResult<List<CityListDTO>>(cityList, Messages.CITY_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CityListDTO>>(Messages.CITY_LISTED_ERROR + ex.Message);
            }
        }
        
    }
}
