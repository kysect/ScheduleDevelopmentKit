﻿using System;
using System.Collections.Generic;

namespace ScheduleAggregator.DataModels
{
    public class Schedule
    {
        public Guid Id { get; set; }

        private List<Lesson> Lessons { get; set; }
    }
}