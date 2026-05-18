using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    public class MonographController : BaseApiController
    {
        private readonly IGenericRepository<Monograph> _monographRepository;
        private readonly IMapper _mapper;

        public MonographController(IGenericRepository<Monograph> monographRepository)
        {
            _monographRepository = monographRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Monograph>> Post(Monograph monograph)
        {
            var res = await _monographRepository.Add(monograph);
            if(res == 0)
            {
                throw new Exception("No se insertó el producto");
            }
            return Ok(monograph);
        }

        [HttpGet]
        public async Task<ActionResult<MyPagination<MonographDto>>> GetMonographs([FromQuery] MonographSpecificationParams monographParams)
        {
            //  (Sort, CategoryId, Keyword, )
            var spec = new MonographWithCategorySpecification(monographParams);
            IReadOnlyList<Monograph> monographs = await _monographRepository.GetAllWithSpec(spec);

            var specCount = new MonographForCountingSpecification(monographParams);
            int totalMonographs = await _monographRepository.CountAsync(specCount);

            var totalPagesRounded = Math.Ceiling(Convert.ToDecimal(totalMonographs / monographParams.PageSize));
            int totalPages = Convert.ToInt32(totalPagesRounded);

            IReadOnlyList<MonographDto> data = _mapper.Map<IReadOnlyList<Monograph>, IReadOnlyList<MonographDto>>(monographs);

            return Ok(
                new MyPagination<MonographDto>
                {
                    Count = totalMonographs,
                    PageIndex = monographParams.PageIndex,
                    PageSize = monographParams.PageSize,
                    Data = data,
                    PageCount = totalPages
                }
             );
        }
    }
}
