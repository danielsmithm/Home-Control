﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Domain
{
    public interface IPersistable<PK>
    {
       PK Id { get; set; }
    }
}
