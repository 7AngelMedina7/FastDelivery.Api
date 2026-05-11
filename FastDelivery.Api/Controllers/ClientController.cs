using FastDelivery.Api.DTOs.Auth;
using FastDelivery.Api.DTOs.Client;
using FastDelivery.Api.Models;
using FastDelivery.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            try
            {
                var response = await _clientService.CreateClientAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> Clients()
        {
            var response = await _clientService.GetAllClientsAsync();

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> ClientById(int id)
        {
            var response = await _clientService.GetClientByIdAsync(id);

            return Ok(response);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(UpdateClientDto dto, int id)
        {
            try
            {
                var response = await _clientService.UpdateClientAsync(dto, id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var response = await _clientService.DeleteClientAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
