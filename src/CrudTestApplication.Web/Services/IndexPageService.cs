using CrudTestApplication.Application.Interfaces;
using CrudTestApplication.Web.Interfaces;
using CrudTestApplication.Web.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudTestApplication.Web.Services
{
    public class IndexPageService : IIndexPageService
    {
        private readonly ICustomerService _customerAppService;        
        private readonly IMapper _mapper;

        public IndexPageService(ICustomerService customerAppService, IMapper mapper)
        {
            _customerAppService = customerAppService ?? throw new ArgumentNullException(nameof(customerAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            var list = await _customerAppService.GetCustomerList();
            var mapped = _mapper.Map<IEnumerable<CustomerViewModel>>(list);
            return mapped;
        }
    }
}
