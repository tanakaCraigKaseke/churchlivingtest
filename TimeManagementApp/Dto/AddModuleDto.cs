﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Dto
{
    internal class AddModuleDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }
    }
}
