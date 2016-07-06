using GalaSoft.MvvmLight;
using OExam.App.ViewModels;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OExam.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

            //InitData();
        }
        
        //List<Power> powers = new List<Power>();
        //List<MenuGroup> mgroups = new List<MenuGroup>();
        //List<MenuPage> mpages = new List<MenuPage>();
        private void InitData()
        {
            /*
            MenuPage mp;
            mp = new MenuPage();
            mp.Display = "学院维护";
            mp.PageUri = "/Pages/BasicInfo/ModifyPage.xaml";
            mpages.Add(mp);

            mp = new MenuPage();
            mp.Display = "专业维护";
            mp.PageUri = "/Pages/BasicInfo/ModifyPage.xaml";
            mpages.Add(mp);

            mp = new MenuPage();
            mp.Display = "班级维护";
            mp.PageUri = "/Pages/BasicInfo/ModifyPage.xaml";
            mpages.Add(mp);

            mp = new MenuPage();
            mp.Display = "注销";
            
            mpages.Add(mp);

            MenuGroup mg;
            mg = new MenuGroup();
            mg.Display = "基本信息";
            mg.Pages = new ObservableCollection<MenuPage>();
            mg.Pages.Add(mpages[0]);
            mg.Pages.Add(mpages[1]);
            mg.Pages.Add(mpages[2]);

            mgroups.Add(mg);

            mg = new MenuGroup();
            mg.Display = "系统管理";
            mgroups.Add(mg);

            Power p;
            p = new Power();
            p.UserID = 1;
            p.Pages = new ObservableCollection<MenuPage>();
            p.Pages.Add(mpages[0]);
            p.Pages.Add(mpages[1]);
            p.Pages.Add(mpages[2]);
            p.Pages.Add(mpages[3]);
            powers.Add(p);
            */
        }

        //public List<MenuGroup> GetGroups()
        //{
        //    return mgroups;
        //}
        //public Power GetPower(int userid)
        //{
        //    Power p = powers.SingleOrDefault(s => s.UserID == userid);
        //    return p;
        //}
    }
}