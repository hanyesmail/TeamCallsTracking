using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCallsTracking.Data;
using TeamCallsTracking.Data.Dtos.Client;
using TeamCallsTracking.Data.Models;
using TeamCallsTracking.Data.Models.General;

namespace TeamCallsTracking.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController(AppDbContext context) : ControllerBase
{
    [HttpGet("/getAllClients")]
    public async Task<ActionResult<GenericResponse>> GetAllClients()
    {
        var clients = await context.Clients.Select(c => new ClientDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.SecondName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
        }).ToListAsync();
        
        return Ok(new GenericResponse
        {
            Success =  true,
            Message = "Successfully retrieved all clients",
            Data = clients
        });
    }

    [HttpGet("/getClientById")]
    public async Task<ActionResult<GenericResponse>> GetClientById(int id)
    {
        var client = await context.Clients.FirstOrDefaultAsync(c => c.Id  == id);

        if (client == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "Client not found",
            });
        }

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Successfully retrieved client",
                Data = client
            }
        );
    }

    [HttpPost("/addClient")]
    public async Task<ActionResult<GenericResponse>> AddClient([FromBody] CreateClientDto client)
    {
        var newClient = new Client
        {
            FirstName = client.FirstName,
            SecondName = client.LastName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
        };

        await context.Clients.AddAsync(newClient);
        await context.SaveChangesAsync();

        var addedClient = context.Clients.FirstOrDefault(c => c.Id == newClient.Id);
        if (addedClient == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Failed to add the client, Please try again later",
            });
        }

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Successfully added the client",
                Data = addedClient
            }
        );
    }

    [HttpPut("/updateClient")]
    public async Task<ActionResult<GenericResponse>> UpdateClient([FromBody] ClientDto client)
    {
        var updatedClient = await context.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

        if (updatedClient == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Client not existing",
            });
        }

        updatedClient.FirstName = client.FirstName;
        updatedClient.SecondName = client.LastName;
        updatedClient.Email = client.Email;
        updatedClient.PhoneNumber = client.PhoneNumber;

        await context.SaveChangesAsync();
        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Successfully updated the client",
            }
        );
    }

    [HttpDelete("/deleteClient")]
    public async Task<ActionResult<GenericResponse>> DeleteClient(int id)
    {
        var deletedClient = await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

        if (deletedClient == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Client not existing",
            });
        }

        context.Clients.Remove(deletedClient);
        await context.SaveChangesAsync();

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Successfully deleted the client",
            }
        );
    }
}