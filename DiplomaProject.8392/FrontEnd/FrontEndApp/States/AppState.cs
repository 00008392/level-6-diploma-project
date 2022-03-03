using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.States
{
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
