﻿using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.LoginSignUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
        LoginViewModel lvm;
		public SignUp ()
		{
			InitializeComponent ();
            lvm = new LoginViewModel();
            
            this.BindingContext = lvm;
           
		}
        async void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                var url = e.Url;
                using (UserDialogs.Instance.Loading("Please wait..."))
                {
                    lvm.ExtractAccessToken(url);
                    if (!string.IsNullOrEmpty(lvm.accessToken))
                    {
                        await lvm.CallFaceBookGraphAPI(lvm.accessToken);
                        if (lvm.IsSuccess)
                        {
                            using (UserDialogs.Instance.Loading("Facebook Authenticated.Now logging on.."))
                            {
                                lvm.SetFacebookUserProfileAsync();
                                var lg =await lvm.GetTokenFromAPI();
                                if (lg)
                                {
                                    Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
                                }

                                else
                                {
                                    var res = await lvm.Register();
                                    if (res)
                                    {
                                        var tkResponse = await lvm.GetTokenFromAPI();

                                        //navigate to sign in user
                                        if (tkResponse)
                                        {
                                           // Navigation.PopModalAsync(true);
                                            Application.Current.MainPage.Navigation.PushAsync(new AppLanding());



                                        }
                                        else
                                        {
                                            await DisplayAlert("Sorry", "Sign in not successful at this time", "OK");
                                        }

                                    }
                                }
                               

                                //await lvm.CallProviderLoginAPI();
                            }
                        

                        }
                    }
                }
              
              
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());

            }
        }

 
        private  void facebook_clicked(object sender, EventArgs e)
        {
            try
            {
                using (UserDialogs.Instance.Loading("Connecting to FaceBook.."))
                {

                    var apiRequest = $"{Constants.FacebookOAuthURL}?client_id={Constants.fbClientID}&display=popup&response_type=token&redirect_uri={Constants.redirectURI}&scope=email";
                    var wbView = new WebView()

                    {
                        Source = apiRequest,
                        HeightRequest = 1

                    };

                    wbView.Navigated += WbView_Navigated;
                    Content = wbView;

                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
            }
        
        }

        private async void signUp_Clicked(object sender, EventArgs e)
        {
            //LogService.LogErrors("Testing logs");
            try
            {
                if (!lvm.IsRegisterationDetailsValid())
                {
                    await DisplayAlert("Error", lvm.RegisterationError, "OK");
                    return;
                }
                GlobalStaticFields.Username = lvm.User.Username;
                var res = await lvm.Register();
                if (res)
                {
                    var tkResponse = await lvm.GetTokenFromAPI();

                    //navigate to sign in user
                    if (tkResponse)
                    {
                        lvm.PersistUserDetails();
                       // await Task.Run(() => (GlobalStaticFields.IntroModel = new IntroPageViewModel()));

                        await Application.Current.MainPage.Navigation.PushAsync(new AppLanding());



                    }
                    else
                    {
                        await DisplayAlert("Sorry", "Sign in not successful at this time", "OK");
                    }

                }
                else
                {
                    await DisplayAlert("Sorry", lvm.RegisterationError, "OK");

                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
           //     var logger = new LogModel()
           //     {
           //         User = GlobalStaticFields.Username,
           //         Error = log.ToString()
           //     };
           //await APIService.PostNew<LogModel>(logger, "Error");
            }
          
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }
        void Password_Completed(object sender, System.EventArgs e)
        {
            txtConfirmPassword.Focus();
        }


        void ConfirmPassword_Completed(object sender, System.EventArgs e)
        {
            signUp_Clicked(this, new EventArgs());
        }

     

        private  void goBack_Tapped(object sender, EventArgs e)
        {
             Navigation.PopAsync(true);

        }

        private void googleBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (UserDialogs.Instance.Loading(""))
                {
                    lvm.OnGoogleButtonClick();
                    lvm.GetTokenFromAPI();
                    if (lvm.IsSuccess)
                    {
                        Navigation.PushAsync(new AppLanding(), true);
                    }
                    else
                    {
                        lvm.Register();
                        if (lvm.IsSuccess)
                        {
                            Navigation.PushAsync(new AppLanding(), true);

                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
            }

        }
    }
}