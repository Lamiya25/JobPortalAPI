using JobPortalAPI.Application.Abstractions.IServices.Persistance;
using JobPortalAPI.Application.DTOs;
using JobPortalAPI.Application.DTOs.CompanyDTOs;
using JobPortalAPI.Application.DTOs.LocationDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllLocations()
        {
            var data = await _locationService.GetLocations();
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLocationTrends()
        {
            var data = await _locationService.GetLocationTrends();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetNearbyJobs(string userId)
        {
            var data = await _locationService.GetNearbyJobs(userId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchJobsByLocation(string city, string country)
        {
            var data = await _locationService.SearchJobsByLocation(city, country);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> AddPreferredLocation(string userId, string city, string country)
        {
            var data = await _locationService.AddPreferredLocation(userId, city,country);
            return StatusCode(data.StatusCode, data);
        }
    }
}
