﻿// Реализация интерфейса IServiceItemsRepository (управление услугами на сайте)
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using WebAppCoreV3.Domain.Entities;
using WebAppCoreV3.Domain.Repositories.Abstract;

namespace WebAppCoreV3.Domain.Repositories.EntityFramework {

    public class EFServiceItemsRepository : IServiceItemsRepository {
        private readonly AppDbContext context;
        public EFServiceItemsRepository(AppDbContext context) {
            this.context = context;
        }

        public IQueryable<ServiceItem> GetServiceItems() {
            return context.ServiceItems;
        }

        public ServiceItem GetServiceItemById(Guid id) {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveServiceItem(ServiceItem entity) {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteServiceItem(Guid id) {
            context.ServiceItems.Remove(new ServiceItem() { Id = id });
            context.SaveChanges();
        }
    }
}