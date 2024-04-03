using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaWaveMultiplatform.Classes
{
    internal class PageFunctions
    {


        // Assuming this is called within a Page or where you have access to the current Navigation
        public static async void ReturnToPreviousPage()
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }



        private static void CloseApplication()
        {
            /*#if WINDOWS
                        Microsoft.Maui.Controls.Platform.CurrentWindow.Close();
            #elif MACCATALYST
                        // macOS specific code to close the window
                        // Note: macOS platform-specific code might require using AppKit APIs, which would need to be invoked through a dependency service or similar mechanism.
            #else
                        // Other platforms or a default behavior
            #endif
                    }

                    *//*private static void CloseApplication()
                    {
                        App.Current.Quit();
                        */
        }




    }
}
