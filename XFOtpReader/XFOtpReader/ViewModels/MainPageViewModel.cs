using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XFOtpReader.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            
            MessagingCenter.Subscribe<string,string>("OtpMessage", "NotifyMsg" ,OtpRecieved);
        }

        private void OtpRecieved(string arg1,string otpValue)
        {
             Title = otpValue; 
        }
    }
}
