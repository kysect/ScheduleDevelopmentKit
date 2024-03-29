﻿using Kysect.ItmoScheduleSdk.Models;

namespace ScheduleDevelopmentKit.ConsolePolygon.ViewItem
{
    public class SimpleViewItem : IScheduleIViewItem
    {
        public ScheduleItemModel Model { get; set; }

        public SimpleViewItem(ScheduleItemModel model)
        {
            Model = model;
        }

        public string ToViewString()
        {
            return (Model.StartTime, Model.ShortSubjectTitle(), Model.ShortTeacherName(), Model.Group).ToString();
        }
    }
}