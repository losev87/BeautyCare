﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class BaseRatedMessage<TAttachment> : BaseMessage<TAttachment> 
        where TAttachment : EntityBase
    {
        public int Rating { get; set; }
    }
}