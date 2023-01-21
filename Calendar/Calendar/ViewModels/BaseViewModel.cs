
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;


using System.Threading;


namespace Calendar.ViewModels
{
   
    public partial class BaseViewModel : ObservableObject
    {
        #region Fields
       
        [ObservableProperty]
        bool isBusy = false;
        [ObservableProperty]
        string title = string.Empty;
        [ObservableProperty]
        private Size pageSize;
       

        #endregion


    }
}
