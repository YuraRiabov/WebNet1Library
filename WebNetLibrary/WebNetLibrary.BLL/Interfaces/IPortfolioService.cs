﻿namespace WebNetLibrary.BLL.Interfaces;

public interface IPortfolioService
{
    public Task<List<long>> GetBookIds(long userId);
    public Task<bool> Add(long userId, long bookId);
    public Task<bool> Remove(long userId, long bookId);
}