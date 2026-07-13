namespace E_Commerce_api.Utils.QueryParams
{
    public class ProductQueryParams
    {
        public int Page { get; set; } = 1;

        public string SearchText { get; set; } = string.Empty;

        public int CategoryId { get; set; } = 0;

        public string Sort { get; set; } = string.Empty; // price, price_desc
    }
}
