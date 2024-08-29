using BaSalesManagementApp.Dtos.MyProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaSalesManagementApp.Business.Services.Adapt
{
    public static class MapingConfig
    {
        public static void RegisterMappings()
        {
			//Mapster kütüphanesi kullanılarak iki farklı sınıf (Admin ve Employee) ile MyProfileDTO arasında haritalama yapılandırması yapmayı amaçlamaktadır. İşlem, MyProfileDTO nesnesindeki verilerin Admin ve Employee nesnelerine nasıl aktarılacağını tanımlar. 
			TypeAdapterConfig<MyProfileDTO, Admin>.NewConfig()
                .Ignore(dest => dest.Id) 
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhotoData, src => src.PhotoData);

            TypeAdapterConfig<MyProfileDTO, Employee>.NewConfig()
                .Ignore(dest => dest.Id) 
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhotoData, src => src.PhotoData);
        }
    }
}
