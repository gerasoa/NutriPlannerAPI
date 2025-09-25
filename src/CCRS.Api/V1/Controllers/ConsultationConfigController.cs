using CCRS.Api.Controllers;
using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ConsultationConfig")]
    public class ConsultationConfigController : MainController
    {
        private readonly IConsultationConfigService _service;
        //private readonly IAvailableSlotService _slotService;
        //private readonly IOfficeLocationService _locationService;

        public ConsultationConfigController(INotifier notifier,
                                            IUser appUser, 
                                            IConsultationConfigService service) : base(notifier, appUser)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var config = await _service.GetByIdAsync(id);
            if (config == null)
                return NotFound();

            return Ok(config);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime startDate, [FromQuery] int daysAhead = 30)
        {
            var configs = await _service.GetAllAsync(startDate, daysAhead);
            return Ok(configs);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ConsultationConfig config)
        {
            await _service.AddAsync(config);
            //foreach (var slot in slots)
            //{
            //    slot.ConsultationConfigId = config.Id;
            //    //await _slotService.AddAvailableSlotAsync(config.Id, slot);
            //}
            //foreach (var location in locations)
            //{
            //    location.ConsultationConfigId = config.Id;
            //    //await _locationService.AddOfficeLocationAsync(config.Id, location);
            //}

            return CreatedAtAction(nameof(GetById), new { id = config.Id }, config);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ConsultationConfig config)
        {
            if (id != config.Id)
                return BadRequest();

            await _service.UpdateAsync(config);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
