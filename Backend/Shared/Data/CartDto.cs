﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data;

public class CartDto
{
    public Guid Id { get; set; }

    public ICollection<Guid> ProductIds { get; set; } = Array.Empty<Guid>();

    public bool Active { get; set; }
}
