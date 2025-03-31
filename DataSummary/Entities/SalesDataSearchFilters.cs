namespace DataSummary.Entities
{
    public class SalesDataSearchFilters
    {
        public DateOnly SalesDataFrom;
        public DateOnly SalesDataTo;
        public string Segment = "";
        public string Country = "";
        public string Product = "";
        public string DiscountBand = "";
    }
}
