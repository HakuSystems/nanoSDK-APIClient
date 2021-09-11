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

namespace nanoSDK_APIClient.Windows.AdminPanel
{
    /// <summary>
    /// Interaction logic for OthersUserControl.xaml
    /// </summary>
    public partial class OthersUserControl : UserControl
    {
        public OthersUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            doBtnCheckings();
        }

        private void doBtnCheckings()
        {
            ((AdminPanel)Window.GetWindow(this)).adminInfoBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).adminUserBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).createLicenseBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).OthersBtn.IsChecked = true;
        }

        private void copyReqjectionBtn_Click(object sender, RoutedEventArgs e)
        {
            string Currentreason = reasonBox.Text;
            if (reasonBox.Text == string.Empty)
            {
                Currentreason = "UNSPECIFIED";
            }

            string reqjection = $@"```                 == License ==
          nanoSDK License Rejection

# Unfortunately your not be able to revive your licence key
  Reason:[{Currentreason}]
 
[nanoSDK development team]
```";
            Clipboard.SetText(reqjection);
        }

        private void reloadAppBtn_Click(object sender, RoutedEventArgs e)
        {
            ((AdminPanel)Window.GetWindow(this)).CurrentFrame.Source = new Uri("HomeUserControl.xaml", UriKind.Relative);
        }

        private void copyRequireBtn_Click(object sender, RoutedEventArgs e)
        {
            string requires = @"
```                         == License ==
                  nanoSDK License Requirements

Please answer those questions before getting your license.

# Did you ever created or cracked client for VRchat?
# How good are you with SDK 2.0 and 3.0?
# Do you know how to code in coding languages such as: c# c++ java python rust etc.
# Did you ever modified a standard SDK before?
# Do you ever cracked a program before with 3rd party tools?

Note:
If you didn't receive your license you might been rejected.

        == Common questions about licensing system ==

# Why nanoSDK ask me for my password, email and my username?

- To make sure we give our tools to the right and trusted hands.

# Does nanoSDK collects user data such as ip addresses and hwid (hardware id)?

- Yes we do but your password is encrypted and looks like this [SHU6y56EGDGghsdtyw6DG]
And only people that can see your information are the developers and they still can't see it fully.

# Do i use my VRChat information to register?

- We suggest TO NOT USE the same password as your VRChat account
We only ask your VRChat information in upload panel.

Note:
If you have any questions about our licensing system feel free ask it in nanoSDK discord server.

- https://nanosdk.net/
- https://discord.com/invite/tCj8MNH

[nanoSDK development team]
```";
            Clipboard.SetText(requires);
        }
    }
}
