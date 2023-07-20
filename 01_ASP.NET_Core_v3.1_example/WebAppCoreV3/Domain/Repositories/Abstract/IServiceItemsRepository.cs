using System;
using System.Linq;
using WebAppCoreV3.Domain.Entities;

namespace WebAppCoreV3.Domain.Repositories.Abstract {

    public interface IServiceItemsRepository {
    
        IQueryable<ServiceItem> GetServiceItems();
        ServiceItem GetServiceItemById(Guid id);
        void SaveServiceItem(ServiceItem entity);
        void DeleteServiceItem(Guid id);
    }
}
