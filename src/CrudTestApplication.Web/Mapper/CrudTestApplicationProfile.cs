using CrudTestApplication.Application.Models;
using CrudTestApplication.Web.ViewModels;
using AutoMapper;

namespace CrudTestApplication.Web.Mapper
{
    public class CrudTestApplicationProfile : Profile
    {
        public CrudTestApplicationProfile()
        {
            CreateMap<CustomerModel, CustomerViewModel>().ReverseMap();
        }
    }
}
