﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.DTOs
{
    public class CreateDonationDto
    {
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
    }
}
