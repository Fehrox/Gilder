using Blazored.LocalStorage;
using Domain;

namespace Infrastructure;

public class LocalStorageReviewRepository : LocalStorageRepo<Review>
{
    public LocalStorageReviewRepository(ILocalStorageService localStorage) 
        : base(localStorage, "reviews") { }
}