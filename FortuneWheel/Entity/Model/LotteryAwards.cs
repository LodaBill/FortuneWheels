﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Model
{
    public class LotteryAwards
    {
        public string AwardName { get; set; }
        public float Rate { get; set; }
        public int Angle { get; set; }
        public string TotalCount { get; set; }
        public string SurplusCount { get; set; }
    }
}
