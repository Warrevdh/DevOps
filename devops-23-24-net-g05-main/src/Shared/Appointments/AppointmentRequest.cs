using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Appointments;

public abstract class AppointmentRequest
{
	public class Index
	{
		public string? Date { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
