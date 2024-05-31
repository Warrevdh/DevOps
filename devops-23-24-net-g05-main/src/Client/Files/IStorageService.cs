using Microsoft.AspNetCore.Components.Forms;

namespace Client.Files
{
    public interface IStorageService
    {
        Task UploadImageAsync(string sas, IBrowserFile file);
    }
}
