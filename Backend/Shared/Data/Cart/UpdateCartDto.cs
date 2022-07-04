using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Cart;

public class UpdateCartDto : CartDto
{
    public new ICollection<Guid>? ProductIds { get; set; }
}
