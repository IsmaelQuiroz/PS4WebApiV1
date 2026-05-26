using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    public class MonographController : BaseApiController
    {
        private readonly IGenericRepository<Monograph> _monographRepository;
        private readonly IMapper _mapper;

        public MonographController(IGenericRepository<Monograph> monographRepository, IMapper mapper)
        {
            _monographRepository = monographRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonographDto>> getMonograph(int id)
        {
            Monograph monograph = await _monographRepository.GetByIdAsync(id);
            if(monograph == null)
            {
                throw new Exception("The monograph could not be found.");
            }
            MonographDto monographDto = _mapper.Map<Monograph, MonographDto>(monograph);
            return Ok(monographDto);
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
            //IReadOnlyList<Monograph> monographs = await _monographRepository.GetAllWithSpec(spec);
            var monographs = await _monographRepository.GetAllWithSpec(spec);

            var specCount = new MonographForCountingSpecification(monographParams);
            int totalMonographs = await _monographRepository.CountAsync(specCount);

            var totalPagesRounded = Math.Ceiling(Convert.ToDecimal(totalMonographs / monographParams.PageSize));
            int totalPages = Convert.ToInt32(totalPagesRounded);

            //IReadOnlyList<MonographDto> data = _mapper.Map<IReadOnlyList<Monograph>, IReadOnlyList<MonographDto>>(monographs);
            var data = _mapper.Map<IReadOnlyList<Monograph>, IReadOnlyList<MonographDto>>(monographs);

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

        [HttpPut("{id}")]
        public async Task<ActionResult<Monograph>> Put(int id, Monograph monograph)
        {
            monograph.Id = id;
            var result = await _monographRepository.Update(monograph);
            if(result == 0)
            {
                throw new Exception("Update Monograph was not successful");
            }
            return Ok(monograph);
        }


        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            Monograph entity = await _monographRepository.GetByIdAsync(id);
            if(entity ==null)
            {
                throw new Exception("Delete Monograph was not successful");
            }
            return await _monographRepository.Delete(entity);
        }
    }
}
