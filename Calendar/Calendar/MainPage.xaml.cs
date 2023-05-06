using Calendar.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calendar
{
    public partial class MainPage : ContentPage
    {
        MainPageViwModel vm;
        public MainPage()
        {
            InitializeComponent();
            vm = (MainPageViwModel)BindingContext;
        }

        private void Calendar_LayoutChanged(object sender, EventArgs e)
        {
            if (vm is null) return;

            vm.FilterObservableRange();

        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppering();
            vm.sv = scrollList;
            vm.ScrolToDateNow();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnDessapiring();
        }
    }
}
