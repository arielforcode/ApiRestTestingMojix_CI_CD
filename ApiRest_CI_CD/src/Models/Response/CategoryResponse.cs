﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest_CI_CD.src.Models.Response
{
    public class CategoryResponse
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string sorting { get; set; }
    }
}
