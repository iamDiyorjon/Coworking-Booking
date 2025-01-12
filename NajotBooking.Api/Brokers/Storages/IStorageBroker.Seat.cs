﻿// ---------------------------------------------------------------
// Copyright (c) Coalition Of The THE STANDART SHARPISTS
// Free To Use To Book Places In Coworking Zones
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using NajotBooking.Api.Models.Seats;

namespace NajotBooking.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Seat> InsertSeatAsync(Seat seat);
        IQueryable<Seat> SelectAllSeats();
        ValueTask<Seat> SelectSeatByIdAsync(Guid id);
        ValueTask<Seat> UpdateSeatAsync(Seat seat);
        ValueTask<Seat> DeleteSeatAsync(Seat seat);
    }
}
