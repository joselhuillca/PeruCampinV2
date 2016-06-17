using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Core.Repositories;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using MLearning.Core.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MLearning.Droid.Views
{
    [Activity(Label = "View for AuthViewModel")]
    public class AuthView : MvxActivity
    {
        private MobileServiceUser user;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AuthView);

            var vm = ViewModel as AuthViewModel;

            vm.PropertyChanged += new PropertyChangedEventHandler(vm_propertyChanged);
        }

        private async Task Authenticate(MobileServiceAuthenticationProvider provider)
        {
           

                try
                {
                    WAMSRepositoryService service = Mvx.Resolve<IRepositoryService>() as WAMSRepositoryService;
                    user = await service.MobileService
                        .LoginAsync(this, provider);


                }
                catch (InvalidOperationException e)
                {

                }


                var vm = ViewModel as AuthViewModel;

                if (user != null)
                {

                    vm.CreateUserCommand.Execute(user);
                }
                else
                {
                    vm.BackCommand.Execute(null);
                }
         
        }

        async void vm_propertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FacebookFlag" && (sender as AuthViewModel).FacebookFlag)
            {
                await Authenticate(MobileServiceAuthenticationProvider.Facebook);
            }

            if (e.PropertyName == "TwitterFlag" && (sender as AuthViewModel).TwitterFlag)
            {
                await Authenticate(MobileServiceAuthenticationProvider.Twitter);
            }
        }
       



    }
}