using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Interface;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMoveHistoryController : ControllerBase
    {
        private readonly IPhoductMoveHistoryService _historyService;
        private readonly IMapper _mapper;

        public ProductMoveHistoryController(IPhoductMoveHistoryService historyService,IMapper mapper)
        {
            _historyService = historyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           List<ProductMoveHistory> histories = await _historyService.GetHistory();

           List<ProductMoveHistoryDTO> productMoveHistoryDtos = _mapper.Map<List<ProductMoveHistoryDTO>>(histories);

           return Ok(productMoveHistoryDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            ProductMoveHistory histories = await _historyService.GetHistory(id);

            ProductMoveHistoryDTO productMoveHistoryDtos = _mapper.Map<ProductMoveHistoryDTO>(histories);

            return Ok(productMoveHistoryDtos);
        }
    }
}
