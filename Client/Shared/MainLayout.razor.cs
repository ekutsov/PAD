using PFA.Client.Services;

namespace PFA.Client.Shared
{
    public partial class MainLayout
    {
        protected bool isSidebarExpanded = true;

        void SidebarToggleClick()
        {
            isSidebarExpanded = !isSidebarExpanded;
        }
    }
}
