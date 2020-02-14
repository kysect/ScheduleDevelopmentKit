using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;
using ItmoScheduleApiWrapper.Types;

namespace ScheduleAggregator.Ui.CustomElements
{
    /// <summary>
    /// Interaction logic for DaySchedule.xaml
    /// </summary>
    public partial class DaySchedule : UserControl
    {
        private List<ScheduleItemModel> _items;
        public DaySchedule(List<ScheduleItemModel> items, DataWeekType dataWeekType, DataDayType dataDayType)
        {
            _items = items
                .Where(i => i.DataWeek.Compare(dataWeekType))
                .Where(i => i.DataDay == dataDayType)
                .ToList();
            InitializeComponent();
            ItemsList.ItemsSource = _items;
        }
    }
}
