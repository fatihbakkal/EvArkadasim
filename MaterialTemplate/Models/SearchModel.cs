using PagedList;

namespace MaterialTemplate.Models
{
    public class SearchModel
    {
        public int? Page { get; set; }
        public int? ProvinceID { get; set; }
        public int? DistrictID { get; set; }
        public int? CategoryID { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public IPagedList<Advert> SearchResults { get; set; }
        public string SearchButton { get; set; }
    }
}