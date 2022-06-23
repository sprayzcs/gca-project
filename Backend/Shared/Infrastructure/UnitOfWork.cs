﻿using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly INotificationHandler _notificationHandler;
    private readonly DbContext _context;

    public UnitOfWork(INotificationHandler notificationHandler, DbContext context)
    {
        _notificationHandler = notificationHandler;
        _context = context;
    }

    public async Task<bool> CommitAsync()
    {
        if (_notificationHandler.HasErrors())
        {
            return false;
        }

        var savedChanges = await _context.SaveChangesAsync();
        if (savedChanges != 0)
        {
            return true;
        }

        _notificationHandler.RaiseError(GenericErrorCodes.CouldNotSave);
        return false;

    }
}
