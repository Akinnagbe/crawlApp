﻿using System;
using System.Collections.Generic;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class DealsList : ContentPage
    {
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Deals());
        }

        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DetailPage());

        }

        IntroPageViewModel introPageViewMod;

        public DealsList()
        {
            InitializeComponent();
            //introPageViewMod = new IntroPageViewModel();
            //nearBy = new NearByViewModel();
            //BindingContext = introPageViewMod;

        }
    }
}
