﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HappeningsApp.OAuth;

namespace HappeningsApp.Droid.OAuth
{

    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "com.googleusercontent.apps.34982577863-i5gcivpseeomlnksdcpnglfc0g20v7v8" },
        DataPath = "/oauth2redirect")]


    public class CustomUrlSchemeInterceptorActivity: Activity
    {
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Convert Android.Net.Url to Uri
        //    var uri = new Uri(Intent.Data.ToString());

        //    // Load redirectUrl page
        //    AuthenticationState.Authenticator.OnPageLoading(uri);

        //    Finish();
        //}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Android.Net.Uri uri_android = Intent.Data;

            Uri uri_netfx = new Uri(uri_android.ToString());

            // load redirect_url Page
            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            this.Finish();

            return;
        }

    }
}