﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Twajd_Back_End.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}