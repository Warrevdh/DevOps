using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Appointments;

public abstract class AppointmentResult
{
    public class Index
    {
        public IEnumerable<AppointmentDto.Index>? Appointments { get; set; }
        public int TotalAmount { get; set; }
    }

    public class UltraDetail
    {
        public IEnumerable<AppointmentDto.UltraDetail> Appointments { get; set; }
        public int TotalAmount { get; set; }
    }
}
