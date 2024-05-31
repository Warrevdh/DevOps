using Domain.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Files
{
    public interface IStorageService
    {
        Uri BasePath { get; }
        Uri GenerateImageUploadSas(Image image);
    }
}
