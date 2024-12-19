﻿using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IReviewRepository
{
    Task AddAsync(Review entity, CancellationToken cancellationToken);
    Task AddCommentAsync(Comment entity, CancellationToken cancellationToken);
    Task<Review?> GetLatestReviewByBookIdAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
    Task<Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Review>> GetCurrentlyReadingBooksAsync(Guid userId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}