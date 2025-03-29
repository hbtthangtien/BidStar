using Application.DTOs.Category;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<List<CategoryDTO>> GetAllCategoryAsync()
        {
            var data =await _unitOfWork.Categories.GetAllAsync();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(data);
            return categoriesDTO;
        }
    }
}
