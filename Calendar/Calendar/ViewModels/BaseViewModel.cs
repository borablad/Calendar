
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
using Calendar.Servisec;
using Calendar.Widgets;
using static Xamarin.Forms.Internals.GIFBitmap;

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

        public Rest DataStore => DependencyService.Get<Rest>();
        #endregion
        public async void ShowWarning(string title, string message)
        {
            try
            {
                var popup = new WarningView();
                var popupvm = (WarningViewModel)popup.BindingContext;
                popupvm.Title = title;
                popupvm.Message = message;
                var page = await App.Current.MainPage.ShowPopupAsync(popup);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
