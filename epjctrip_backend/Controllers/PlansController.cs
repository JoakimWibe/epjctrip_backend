using Microsoft.AspNetCore.Mvc;
using epjctrip_backend.Models;
using epjctrip_backend.Repositories;

namespace epjctrip_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanRepository _planRepository;
        
        public PlansController(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        // GET: api/Plans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlan()
        {
            if (_planRepository != null)
            {
                return await _planRepository.GetAll();
            }

            return NotFound();
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
            var planFromDb = await _planRepository.GetById(id);
            if (planFromDb != null)
            {
                return planFromDb;
            }

            return NotFound();
        }
        
        // POST: api/Plans
        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(CreatePlanRequest plan)
        {
            var savedPlan = await _planRepository.Create(new Plan
            {
                Id = 0,
                Name = plan.Name,
                StartDate = plan.StartDate,
                EndDate = plan.EndDate,
                Destination = plan.Destination,
                Departure = plan.Departure,
                Activities = null,
                Participants = plan.Participants,
                Budget = plan.Budget,
                UserId = plan.UserId
            });
            
            var actionName = nameof(GetPlan);
            var routeValue = new { id = savedPlan.Id };
            return CreatedAtAction(actionName, routeValue, savedPlan);
        }

        // PUT: api/Plans/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Plan>> PutPlan(int id, UpdatePlanRequest plan)
        {
            var planFromDb = await _planRepository.GetById(id);
            if (planFromDb == null)
            {
                return NotFound();
            }

            planFromDb.Name = plan.Name;
            planFromDb.StartDate = plan.StartDate;
            planFromDb.EndDate = plan.EndDate;
            planFromDb.Destination = plan.Destination;
            planFromDb.Departure = plan.Departure;
            planFromDb.Participants = plan.Participants;
            planFromDb.Budget = plan.Budget;
            planFromDb.TotalCo2E = plan.TotalCo2E;
            planFromDb.TransportType = plan.TransportType;
            planFromDb.TransportCo2E = plan.TransportCo2E;
            planFromDb.AccommodationType = plan.AccommodationType;
            planFromDb.AccommodationCo2E = plan.AccommodationCo2E;

            await _planRepository.UpdatePlan(planFromDb);
            return planFromDb;
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public IActionResult DeletePlan(int id)
        {
            _planRepository.Delete(id);
            return NoContent();
        }
    }
}