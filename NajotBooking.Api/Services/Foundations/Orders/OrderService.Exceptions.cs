﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the THE STANDART SHARPISTS
// Free To Use to Book Places in Coworking Zones
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NajotBooking.Api.Models.Orders;
using NajotBooking.Api.Models.Orders.Exceptions;
using Xeptions;

namespace NajotBooking.Api.Services.Foundations.Orders
{
    public partial class OrderService
    {
        private delegate ValueTask<Order> ReturningOrderFunction();

        private async ValueTask<Order> TryCatch(ReturningOrderFunction returningOrderFunction)
        {
            try
            {
                return await returningOrderFunction();
            }
            catch (NullOrderException nullOrderException)
            {
                throw CreateAndLogValidationException(nullOrderException);
            }
            catch (InvalidOrderException invalidOrderException)
            {
                throw CreateAndLogValidationException(invalidOrderException);
            }
            catch (SqlException sqlException)
            {
                var failedOrderStorageException = new FailedOrderStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedOrderStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsOrderException =
                    new AlreadyExistsOrderException(duplicateKeyException);

                throw CreateAndDependencyValidationException(alreadyExistsOrderException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedOrderException = new LockedOrderException(dbUpdateConcurrencyException);

                throw CreateAndDependencyValidationException(lockedOrderException);
            }
            catch (Exception exception)
            {
                var failedOrderServiceException = new FailedOrderServiceException(exception);

                throw CreateAndLogServiceException(failedOrderServiceException);
            }
        }

        private OrderValidationException CreateAndLogValidationException(Xeption exception)
        {
            var orderValidationException = new OrderValidationException(exception);
            this.loggingBroker.LogError(orderValidationException);

            return orderValidationException;
        }

        private OrderDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var orderDependencyException = new OrderDependencyException(exception);
            this.loggingBroker.LogCritical(orderDependencyException);

            return orderDependencyException;
        }

        private OrderDependencyValidationException CreateAndDependencyValidationException(Xeption exception)
        {
            var orderDependencyValidationException =
                new OrderDependencyValidationException(exception);

            this.loggingBroker.LogError(orderDependencyValidationException);

            return orderDependencyValidationException;
        }

        private OrderServiceException CreateAndLogServiceException(Xeption exception)
        {
            var orderServiceException = new OrderServiceException(exception);
            this.loggingBroker.LogError(orderServiceException);

            return orderServiceException;
        }
    }
}