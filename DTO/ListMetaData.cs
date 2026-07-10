using E_Commerce_api.Utils;

namespace E_Commerce_api.DTO
{
    public class ListMetaData
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)GlobalConstants.PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
