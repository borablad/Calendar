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
       
        public MainPageViwModel() 
        {
            //events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(60), Topic = "Совещание" });
            //events.Add(new Event { StartDate = SelectedDay.AddHours(-2), EndDate = SelectedDay.AddDays(1), Topic = "Встреча " });
            //events.Add(new Event { StartDate = SelectedDay.AddHours(-1), EndDate = SelectedDay.AddMonths(1), Topic = "Занято" });
            //events.Add(new Event { StartDate = SelectedDay.AddMinutes(60), EndDate = SelectedDay.AddMinutes(120), Topic = "MeetUp" });
            //
            //events.Add(new Event { StartDate = SelectedDay.AddMinutes(150), EndDate = SelectedDay.AddMinutes(150+60), Topic = "Совещание" });
            //events.Add(new Event { StartDate = SelectedDay.AddMinutes(150 + 60+30), EndDate = SelectedDay.AddMinutes(150 + 60 + 30+60), Topic = "Совещание" });
      
            
        //   EventData.ReplaceRange(events);

           
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

               // events = ActualRoom.Events;
                //events = await DataStore.GetEvents();

            }
            catch(Exception ex) {
                ShowWarning("Ошибка","Интернет не доступен");
            }

            events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(60), Title = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay.AddHours(-2), EndDate = SelectedDay.AddDays(1), Title = "Встреча " });
            events.Add(new Event { StartDate = SelectedDay.AddHours(-1), EndDate = SelectedDay.AddMonths(1), Title = "Занято" });
            events.Add(new Event { StartDate = SelectedDay.AddMinutes(60), EndDate = SelectedDay.AddMinutes(120), Title = "MeetUp" });
            events.Add(new Event { StartDate = SelectedDay.AddMinutes(150), EndDate = SelectedDay.AddMinutes(150 + 60), Title = "Совещание" });
            events.Add(new Event { StartDate = SelectedDay.AddMinutes(150 + 60 + 30), EndDate = SelectedDay.AddMinutes(150 + 60 + 30 + 60), Title = "Совещание" });


            EventData.ReplaceRange(events);
            //EventData.ReplaceRange(events);
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
            if (check) return;
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
                        //events = await DataStore.GetEvents();

                    }
                    catch (Exception ex)
                    {
                        ShowWarning("Ошибка", "Интернет не доступен");
                    }

                    events.Add(new Event { StartDate = SelectedDay, EndDate = SelectedDay.AddMinutes(60), Title = "Совещание" });
                    events.Add(new Event { StartDate = SelectedDay.AddHours(-2), EndDate = SelectedDay.AddDays(1), Title = "Встреча " });
                    events.Add(new Event { StartDate = SelectedDay.AddHours(-1), EndDate = SelectedDay.AddMinutes(90), Title = "Занято" });
                    events.Add(new Event { StartDate = SelectedDay.AddMinutes(60), EndDate = SelectedDay.AddMinutes(120), Title = "MeetUp" });
                    events.Add(new Event { StartDate = SelectedDay.AddMinutes(150), EndDate = SelectedDay.AddMinutes(150 + 60), Title = "Совещание" });
                    events.Add(new Event { StartDate = SelectedDay.AddMinutes(150 + 60 + 30), EndDate = SelectedDay.AddMinutes(150 + 60 + 30 + 60), Title = "Совещание" });


                    EventData.ReplaceRange(events);
                    //                    EventData.ReplaceRange(events);
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
            //bool havno = false;
            for (int i = 0; i < 24; i++)
            {
                hour = hour.AddMinutes(60);
                /* Event list;
                 try
                 {
                     list= EventData.First(x => x.StartDate.Hour <= hour.Hour && x.EndDate.Hour >= hour.Hour);
                 }

                 catch
                 {
                     list = null;
                 }
                 if(list is null) {


                     AllTimeList.Add(new AllTimeModel
                     {
                         Hour = hour,


                     });

                 }
                 else
                 {
                     AllTimeList.Add(new AllTimeModel
                     {
                         Hour = hour,
                         Events = list

                     }) ;
                */
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
                    //if (EventData.Where(x => (x.EndDate.Hour-x.StartDate.Hour == hour.Hour )).First() != null)
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
                  /*  if (hour.Hour == DateTime.Now.AddHours(2).Hour)
                    {
                        var piva = 2;
                    }*/
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
                    if (EventData.Where(x => x.EndDate.Hour == hour.Hour && x.EndDate.Minute == 0).First() != null)
                    {
                        AllTimeList.Add(new AllTimeModel
                        {
                            Hour = hour,
                            Events = EventData.Where(x => x.EndDate.Hour == hour.Hour && x.EndDate.Minute == 0).First()
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
            }


        /*  foreach(var item in AllTimeList) 
          {

              var pivas = EventData.First(x => x.StartDate.Hour <= item.Hour.Hour && x.EndDate.Hour >= item.Hour.Hour);

              if (pivas == null) continue;
              else
              {
                  if (pivas.EndDate.Hour >= item.Hour.Hour)
                  {
                      List<int> helppivas = new List<int>();
                      foreach(var i in AllTimeList)
                      {
                          if (i.Events.EndDate.Hour >= item.Hour.Hour)
                              helppivas.Add(AllTimeList.IndexOf(i));
                      }

                      for (int i = 0; i < helppivas.Count; i++)
                      {
                          if (helppivas[i] + 1 == helppivas[i + 1])
                          {
                              item.IsStart = true;
                              var helppivas2 = AllTimeList.IndexOf(item) + 1;

                          }
                      }    


                  }
                  else
                  {
                      item.Events = pivas;
                      item.IsMiddle = true;
                  }
              }

          }*/






        internal void OnDessapiring()
        {
            InPage = false;
        }



        [RelayCommand]
        private void Filter()
        {
           /* EventData.ReplaceRange(events.Where(x => x.EndDate.Day == SelectedDay.Day && x.EndDate.DayOfWeek == SelectedDay.DayOfWeek
            && x.EndDate.Year == SelectedDay.Year && x.EndDate.Month == SelectedDay.Month));*/
        }

    }
}
