using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.States
{
    //app state stores title that should be displayed in page header
    public class AppState
    {
        public string PageTitle { get; set; }
        public Action OnChange;
        public void SetPageTitle(string title)
        {
            PageTitle = title;
            OnChange?.Invoke();
        }
    }
}
