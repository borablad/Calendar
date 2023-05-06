using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Calendar.Models;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Calendar.ViewModels
{
    public partial class MainPageViwModel:BaseViewModel
    {
        [ObservableProperty]
        private DateTime selectedDay=DateTime.Today,minDate=DateTime.Now.AddDays(-1),maxDate=DateTime.Now.AddMonths(4),toDay = DateTime.Now;

        [ObservableProperty]
        private Event eventsInfo;



        [ObservableProperty]
        private bool needFilter, expand;

        [ObservableProperty]
        private string owner, topik;

        private bool check;
        [ObservableProperty]
        private bool inPage = true;
        private const int CountToGetEvents = 10/**60*60*/;
        public ObservableRangeCollection<Room> RoomData { get; set; }    =new ();
        public ObservableRangeCollection<Event> EventData { get; set; }    =new ();
        [ObservableProperty]
        Room actualRoom;
        public ObservableRangeCollection<AllTimeModel> AllTimeCollection { get; set; }    =new ObservableRangeCollection<AllTimeModel>();
        public CultureInfo Culture => new CultureInfo("ru-RU");

        private List<Event> events = new List<Event>();
        private List<AllTimeModel> AllTimeList = new List<AllTimeModel>();

        internal ScrollView sv;
        public MainPageViwModel() 
        {
 
           
        }


        internal async void OnAppering()
        {
            try
            {
                
                RoomData.ReplaceRange( await DataStore.GetRooms());
                if (ActualRoom == null)
                    ActualRoom = RoomData[0];
                else
                    ActualRoom = RoomData.Where(x => x.RoomCode == ActualRoom.RoomCode && x.RoomName == ActualRoom.RoomName && x.RoomLocation == ActualRoom.RoomLocation).FirstOrDefault();

                events = ActualRoom.Events;
               

            }
            catch(Exception ex) {
                ShowWarning("Ошибка","Интернет не доступен");
            }

            events.Add(new Event { StartDate = SelectedDay.AddMinutes(30), EndDate = SelectedDay.AddMinutes(60 + 60 + 60), Title = "Встреча " , CreatedName = "Боря" , OwnerName = "Миша"});

            EventData.ReplaceRange(events);
        
            FilterObservableRange();
            GetEvents();

        }

        [RelayCommand]
        public void EventInfo(AllTimeModel TimeEv)
        {
           
            Expand = false;
            if (TimeEv.Events == null)
                return;
            EventsInfo = TimeEv.Events;
            Expand = true;
        }

        private async Task GetEvents()
        {
            if (check) 
                return;
            check = true;
            int count = 0;
            while (InPage)
            {
                if (count >= CountToGetEvents)
                {
                    count = 0;
                    try
                    {

                        RoomData.ReplaceRange(await DataStore.GetRooms());
                        if (ActualRoom == null)
                            ActualRoom = RoomData[0];
                        else
                            ActualRoom = RoomData.Where(x => x.RoomCode == ActualRoom.RoomCode && x.RoomName == ActualRoom.RoomName && x.RoomLocation == ActualRoom.RoomLocation).FirstOrDefault();

                        events = ActualRoom.Events;
                       

                    }
                    catch (Exception ex)
                    {
                        ShowWarning("Ошибка", "Интернет не доступен");
                    }

                    EventData.ReplaceRange(events);
                 
                    FilterObservableRange();
                   
                }
                count++;
                await Task.Delay(1000);
            }
        }


        internal void FilterObservableRange()
        {
            AllTimeList.Clear();
            AllTimeCollection.Clear();
            if (NeedFilter)
                return;
            NeedFilter = true;
            EventData.ReplaceRange(events.Where(x => x.EndDate.Day == SelectedDay.Day && x.EndDate.DayOfWeek == SelectedDay.DayOfWeek
            && x.EndDate.Year == SelectedDay.Year && x.EndDate.Month == SelectedDay.Month));
            NeedFilter = false;

            DateTime hour = SelectedDay.AddMinutes(-60);
            
            for (int i = 0; i < 24; i++)
            {
                hour = hour.AddMinutes(60);
               
                try
                {
                    if (EventData.Where(x => x.StartDate.Hour == hour.Hour && x.StartDate.Minute == 0).First() != null)
                    {
                        AllTimeList.Add(new AllTimeModel
                        {
                            Hour = hour,
                            Events = EventData.Where(x => x.StartDate.Hour == hour.Hour && x.StartDate.Minute == 0).First()
                           ,
                            IsStart = true
                        }) ;

                        continue;
                    }
                }
                catch
                {

                }
                
                try
                {
                    if (EventData.Where(x => x.StartDate.Hour == hour.Hour && x.StartDate.Minute > 0).First() != null)
                    {
                        AllTimeList.Add(new AllTimeModel
                        {
                            Hour = hour,
                            Events = EventData.Where(x => x.StartDate.Hour == hour.Hour && x.StartDate.Minute > 0).First()
                          ,
                            IsStartMiddle = true
                        });

                        continue;
                    }
                }
                catch
                {

                }

                try
                {
                   
                    if (EventData.Where(x => (x.EndDate.Hour > hour.Hour &&x.StartDate.Hour <hour.Hour )).First() != null)
                    {
                        AllTimeList.Add(new AllTimeModel
                        {
                            Hour = hour,
                            Events = EventData.Where(x => (x.EndDate.Hour > hour.Hour && x.StartDate.Hour < hour.Hour)).First()
                          ,
                            IsMiddle = true
                        });

                        continue;
                    }
                }
                catch
                {

                }

                try
                {
                
                    if (EventData.Where(x => ( x.EndDate.Hour == hour.Hour) && (x.EndDate.Minute > 0 && x.StartDate.Hour != hour.Hour)).First() != null)
                    {
                        AllTimeList.Add(new AllTimeModel
                        {
                            Hour = hour,
                            Events = EventData.Where(x => (x.EndDate.Hour == hour.Hour) && (x.EndDate.Minute > 0 && x.StartDate.Hour != hour.Hour)).First()
                          ,
                            IsEndMiddle = true
                        });

                        continue;
                    }
                }
                catch
                {

                }
                try
                {
                    if (EventData.Where(x => x.EndDate.Hour == hour.AddHours(1).Hour && x.EndDate.Minute == 0).First() != null)
                    {
                   
                        
                        AllTimeList.Add(new AllTimeModel
                        {
                            Hour = hour,
                            Events = EventData.Where(x => x.EndDate.Hour == hour.AddHours(1).Hour && x.EndDate.Minute == 0).First()
                             ,
                            IsEnd = true
                        });
                        continue;
                        
                    }

                }
                catch { }
                AllTimeList.Add(new AllTimeModel
                {
                    Hour = hour,

                });




            }
          
              AllTimeCollection.ReplaceRange(AllTimeList);
             ScrolToDateNow();
            }


        internal  async Task ScrolToDateNow()
        {
            await Task.Delay(250);
            if (sv is not null)
            {
                var hourNow= DateTime.Now.Hour;
                var count = 0;
                foreach (var item in AllTimeList)
                {
                    if (item.Hour.Hour == hourNow)
                    {
                        break;
                    }
                    count++;
                }
                
                var sizeItem = 90;
                count -= 1;
                var positiony=sizeItem*count; 
                await sv.ScrollToAsync(0, positiony, true);

            }
        }
        [RelayCommand]
        private void SelectRoom(Room room)
        {
            ActualRoom=room;
            GetEvents();

        }



        internal void OnDessapiring()
        {
            InPage = false;
        }



        

    }
}
