using Shared.Users;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Client.Admin.Components.Team
{
    public class TeamService
    {
        private readonly HttpClient authorizedClient;
        private const string endpoint = "api/user";
        public TeamService(HttpClient authorizedClient)
        {
            this.authorizedClient = authorizedClient;
        }

        public async Task<IEnumerable<UserDto.Index>> GetUsers()
        {
            return await authorizedClient.GetFromJsonAsync<IEnumerable<UserDto.Index>>(endpoint);
        }

        public async Task CreateUser(UserDto.Mutate model)
        {
            await authorizedClient.PostAsJsonAsync(endpoint, model);
        }

        public async Task ChangeRole(string userId)
        {
            await authorizedClient.PutAsJsonAsync($"{endpoint}/{userId}", userId);
        }

        public async Task DeleteUser(string userId)
        {
            await authorizedClient.DeleteAsync($"{endpoint}/{userId}");
        }

        public async Task UpdateUser(string oldEmail, string newEmail)
        {
            string apiUrl = $"{endpoint}/emailchange/{oldEmail}";
            await authorizedClient.PutAsJsonAsync(apiUrl, newEmail);
        }
    }
}
