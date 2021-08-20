using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace nanoSDK_APIClient
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            OnProgramStart.Initialize("nanoSDK Api", "859404", "ZrGWTpiQV8WGqC6zIPozy1zyC0LVyOlryUx", "1.0");
            App app = new App();
            app.InitializeComponent();
            app.Run();
            //everything after that wont be executed
        }
    }
}
