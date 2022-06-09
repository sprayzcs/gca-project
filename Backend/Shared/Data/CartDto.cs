using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data;

public class CartDto
{
    public Guid Id { get; set; }

    public ICollection<ProductDto> Products { get; set; } = Array.Empty<ProductDto>();

    public bool Active { get; set; }
}
