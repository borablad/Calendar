using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calendar.ViewModels
{
    public partial class MainPageViwModel:BaseViewModel
    {
        [ObservableProperty]
        private DateTime selectedDay = DateTime.Today;

        public MainPageViwModel() 
        {
            
        }   


    }
}
