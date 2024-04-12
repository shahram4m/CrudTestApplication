using CrudTestApplication.Application.Models.Base;

namespace CrudTestApplication.Application.Models
{
    public class CategoryModel : BaseModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
