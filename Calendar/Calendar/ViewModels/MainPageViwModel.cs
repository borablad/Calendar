using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using Calendar.Models;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Calendar.ViewModels
{
    public partial class MainPageViwModel:BaseViewModel
    {
        [ObservableProperty]
        private DateTime selectedDay=DateTime.Now,minDate=DateTime.Now.AddDays(-1),maxDate=DateTime.Now.AddMonths(4);

        [ObservableProperty]
        private bool needFilter;

        public ObservableRangeCollection<Event> EventData { get; set; }    =new ObservableRangeCollection<Event>();
        public CultureInfo Culture => new CultureInfo("ru-RU");

        private List<Event> events = new List<Event>();
       
        public MainPageViwModel() 
        {
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay.AddHours(-2), EndDate = SelectedDay.AddDays(1), Topic = "Встреча " });
            events.Add(new Event { StartDate = SelectedDay.AddHours(-1), EndDate = SelectedDay.AddMonths(1), Topic = "Занято" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "MeetUp" });

            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            EventData.ReplaceRange(events);

           
        }

        internal void FilterObservableRange()
        {
            if (NeedFilter)
                return;
            NeedFilter = true;
            EventData.ReplaceRange(events.Where(x => x.EndDate.Day == SelectedDay.Day&&x.EndDate.DayOfWeek==SelectedDay.DayOfWeek
            && x.EndDate.Year==SelectedDay.Year &&x.EndDate.Month==SelectedDay.Month));
            NeedFilter = false;
        }

        [RelayCommand]
        private void Filter()
        {
           /* EventData.ReplaceRange(events.Where(x => x.EndDate.Day == SelectedDay.Day && x.EndDate.DayOfWeek == SelectedDay.DayOfWeek
            && x.EndDate.Year == SelectedDay.Year && x.EndDate.Month == SelectedDay.Month));*/
        }

    }
}
