﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class OrderMessageAttachment : BaseAttachment<OrderMessageAttachmentData>, IBeautyCareRepository
    {
        public virtual OrderMessage OrderMessage { get; set; }
        public int OrderMessageId { get; set; }
    }
}