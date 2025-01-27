﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class LLegadaTardeDTO
    {
        public required DateTime Fecha { get; set; }

        public required int MinutosTarde{ get; set; }

        public required int IdEmpleado { get; set; }

    }

    //Agrego idempleado que es la fk?

    public class LLegadaTardeDTOConId : LLegadaTardeDTO
    {
        public int Id { get; set; }
    }
}

