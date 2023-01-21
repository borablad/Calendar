using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Calendar.Models;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Calendar.ViewModels
{
    public partial class MainPageViwModel:BaseViewModel
    {
        [ObservableProperty]
        private DateTime selectedDay=DateTime.Now;
        

        ///public IList<Event> EventData { get => eventData; set => eventData = value; }
        public ObservableRangeCollection<Event> EventData { get; set; }    =new ObservableRangeCollection<Event>();

        private List<Event> events = new List<Event>(); 
        public MainPageViwModel() 
        {
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Кемас" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "MeetUp" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Бухич" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(61), Topic = "Совещание" });
            EventData.ReplaceRange(events);

            var ev =new Event();
        }

        
    }
}
