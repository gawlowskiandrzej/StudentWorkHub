using UnifiedOfferSchema;

namespace offer_manager.Models.PaginationService
{
    public class PaginationService
    {
        public PaginationResponse CreatePagedResult(
            IEnumerable<UOS> query,
            int page,
            int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                IsLastPage = page >= totalPages
            };
        }
    }
}
