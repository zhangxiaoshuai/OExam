using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using OExam.Client.ViewModel;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;
using OExam.Client.Utils;
using OExam.App.ViewModels;

namespace OExam.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private UserData _udata;
        public MainWindow(UserData udata)
        {
            InitializeComponent();
            _udata = udata;
            CreateMenu(udata.WindowGroups,udata.TitlePages);
            SetText();
            RegisterMessageToRedirect();
        }
        #region 初始化界面
        private void CreateMenu(ICollection<WindowGroup> mgroups,ICollection<WindowPage> titles)
        {
            if (mgroups == null)
                return;

            foreach (var g in mgroups)
            {
                LinkGroup lg = new LinkGroup();
                lg.DisplayName = g.Display;
                if (g.Pages == null)
                    continue;
                foreach (var p in g.Pages)
                {
                    Link l = new Link();
                    l.DisplayName = p.Display;
                    if (!string.IsNullOrWhiteSpace(p.PageUri))
                        l.Source = new Uri(p.PageUri, UriKind.Relative);
                    lg.Links.Add(l);
                }

                if (lg.Links.Count > 0)
                    this.MenuLinkGroups.Add(lg);
            }

            if (titles == null)
                return;

            foreach (var p in titles)
            {
                Link l = new Link();
                l.DisplayName = p.Display;
                if (!string.IsNullOrWhiteSpace(p.PageUri))
                    l.Source = new Uri(p.PageUri, UriKind.Relative);
                this.TitleLinks.Add(l);
            }
            /*
            int userid = 1;
            MainViewModel mvm = new MainViewModel();
            var groups = mvm.GetGroups();
            var power = mvm.GetPower(userid);

            foreach (var g in groups)
            {
                LinkGroup lg = new LinkGroup();
                lg.DisplayName = g.Display;
                if (g.Pages == null)
                    continue;
                foreach (var p in g.Pages)
                {
                    foreach (var pp in power.Pages)
                    {
                        if (p == pp)
                        {
                            Link l = new Link();
                            l.DisplayName = p.Display;
                            if (!string.IsNullOrWhiteSpace(p.PageUri))
                                l.Source = new Uri(p.PageUri, UriKind.Relative);
                            lg.Links.Add(l);

                            break;
                        }
                    }
                    power.Pages.Remove(p);
                }
                if (lg.Links.Count > 0)
                    this.MenuLinkGroups.Add(lg);

                if (power.Pages.Count > 0)
                {
                    foreach (var p in power.Pages)
                    {
                        Link l = new Link();
                        l.DisplayName = p.Display;
                        if (!string.IsNullOrWhiteSpace(p.PageUri))
                            l.Source = new Uri(p.PageUri, UriKind.Relative);
                        this.TitleLinks.Add(l);
                    }

                    power.Pages.Clear();
                }
            }
            */
        }
        private void SetText()
        {
            
        }
        #endregion

        #region 注册页面跳转消息
        private void RegisterMessageToRedirect()
        {
            Messenger.Default.Register<string>(this, MessageToken.RedirectPage, pageuri =>
            {
                if(!string.IsNullOrWhiteSpace(pageuri))
                {
                    var frame = GetDescendantFromName(this, "ContentFrame") as ModernFrame;

                    // Set the frame source, which initiates navigation  
                    if (frame != null)
                    {
                        frame.Source = new Uri(pageuri, UriKind.Relative);
                    }
                }
                
            });

        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }
        #endregion
    }


}
