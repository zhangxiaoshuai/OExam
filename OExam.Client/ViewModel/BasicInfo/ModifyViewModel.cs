using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OExam.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Client.ViewModel.BasicInfo
{
    public class ModifyViewModel:ViewModelBase
    {
        public ModifyViewModel()
        {
            UriCommand = new RelayCommand<string>((pageuri) =>
            {
                Messenger.Default.Send(pageuri,MessageToken.RedirectPage);
            });
        }

        public RelayCommand<string> UriCommand { get; set; }
    }
}
