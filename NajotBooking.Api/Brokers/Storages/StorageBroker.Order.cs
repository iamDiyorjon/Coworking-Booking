﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the THE STANDART SHARPISTS
// Free To Use to Book Places in Coworking Zones
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using NajotBooking.Api.Models.Orders;

namespace NajotBooking.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Order> InsertOrderAsync(Order order) =>
            await InsertAsync(order);

        public IQueryable<Order> SelectAllOrders() =>
            SelectAll<Order>();

        public async ValueTask<Order> SelectOrderById(Guid id) =>
            await SelectAsync<Order>(id);

        public async ValueTask<Order> UpdateOrderAsync(Order order) =>
            await UpdateAsync(order);

        public async ValueTask<Order> DeleteOrderAsync(Order order) =>
            await DeleteAsync(order);
    }
}