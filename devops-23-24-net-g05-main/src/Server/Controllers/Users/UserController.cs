using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using Shared.Users;
using System.Net.NetworkInformation;

namespace Server.Controllers.Users;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class UserController : ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;
    public IPagedList<Role> allRoles;

    public UserController(IManagementApiClient managementApiClient)
    {
        _managementApiClient = managementApiClient;
        InitializeVar();
    }

    private async Task InitializeVar()
    {
        allRoles = await _managementApiClient.Roles.GetAllAsync(new GetRolesRequest());
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto.Index>> GetUsers()
    {
        var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
        List<UserDto.Index> usersList = new List<UserDto.Index>();

        foreach (var user in users)
        {
            var roles = await _managementApiClient.Users.GetRolesAsync(user.UserId);
            UserDto.Index userDto = new UserDto.Index()
            {
                Email = user.Email,
                UserId = user.UserId,
                Role = roles.Select(x => x.Name).First(),
            };
            usersList.Add(userDto);
        }

        return usersList;
    }

    [HttpPost]
    public async Task CreateUser(UserDto.Mutate user)
    {
        UserCreateRequest createRequest = new UserCreateRequest
        {
            Email = user.Email,
            Password = user.Password,
            Connection = "Username-Password-Authentication",
        };
        var createdUser = await _managementApiClient.Users.CreateAsync(createRequest);
     
        Role adminRole = allRoles.First(x => x.Name == "User");

        AssignRolesRequest assignRoleRequest = new AssignRolesRequest
        {
            Roles = new string[] { adminRole.Id }
        };
        await _managementApiClient.Users.AssignRolesAsync(createdUser?.UserId, assignRoleRequest);
    }


    [HttpPut("{userId}")]
    public async Task ChangeRole(string userId)
    {
        try
        {
            
            var existingRoles = await _managementApiClient.Users.GetRolesAsync(userId);
            string newRoleId = "";

            // Check the current role and assign the opposite role
            if (existingRoles.Any(role => role.Name == "Administrator"))
            {
                newRoleId = GetRoleIdByName(existingRoles, "User");
            }
            else if (existingRoles.Any(role => role.Name == "User"))
            {
                newRoleId = GetRoleIdByName(existingRoles, "Administrator");
            }

            if (!string.IsNullOrEmpty(newRoleId))
            {

                // Assign the new role
                await _managementApiClient.Users.AssignRolesAsync(userId, new AssignRolesRequest { Roles = new string[] { newRoleId } });


                // Find the previous role to remove
                var previousRole = existingRoles.FirstOrDefault(role => role.Id != newRoleId);
                if (previousRole != null)
                {
                    // Remove the previous role using Auth0 Management API
                    await _managementApiClient.Users.RemoveRolesAsync(userId, new AssignRolesRequest { Roles = new string[] { previousRole.Id } });
                }

            }
        }
        catch (Exception ex)
        {
            // Handle exceptions or errors accordingly
            Console.WriteLine(ex.Message);
        }
    }

    private string GetRoleIdByName(IPagedList<Role> existingRoles, string roleName)
    {
        var role = allRoles.FirstOrDefault(r => r.Name == roleName);
        return role?.Id;
    }

    [HttpDelete("{userId}")]
    public async Task RemoveUser(string userId)
    {
        await _managementApiClient.Users.DeleteAsync(userId);
    }

    [HttpPut("emailchange/{oldEmail}")]
    public async Task ChangeUserEmail(string oldEmail, [FromBody] string newEmail)
    {
        var user = await _managementApiClient.Users.GetUsersByEmailAsync(oldEmail);    
        UserUpdateRequest newUser = new UserUpdateRequest()
        {
            Email = newEmail,
            FullName = newEmail,
        };
        await _managementApiClient.Users.UpdateAsync(user.First().UserId,newUser);
    }
}
