using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppCoreV3.Domain;

namespace WebAppCoreV3.Models.ViewComponents {

    public class SidebarViewComponent : ViewComponent {
        private readonly DataManager dataManager;

        public SidebarViewComponent(DataManager dataManager) {
            this.dataManager = dataManager;
        }

        public Task<IViewComponentResult> InvokeAsync() {
            return Task.FromResult((IViewComponentResult)View("Default", dataManager.ServiceItems.GetServiceItems()));
        }
    }
}