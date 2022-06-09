using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data;

public class ShipmentDto
{
    public Guid Id { get; set; }

    public string TrackingNumber { get; set; } = string.Empty;
}
