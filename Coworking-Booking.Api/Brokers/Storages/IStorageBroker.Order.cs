﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the THE STANDART SHARPISTS
// Free To Use to Book Places in Coworking Zones
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Coworking_Booking.Api.Models.Orders;

namespace Coworking_Booking.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Order> InsertOrderAsync(Order order);
        IQueryable<Order> SelectAllOrders();
        ValueTask<Order> SelectOrderById(Guid id);
        ValueTask<Order> DeleteOrderAsync(Order order)
    }
}