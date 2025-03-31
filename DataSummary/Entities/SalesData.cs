namespace DataSummary.Entities
{
    public class SalesData
    {
        public string? _Segment = "";
        private string? _Country = "";
        private string? _Product = "";
        private string? _DiscountBand = "";
        private double? _UnitsSold = 0;
        private double? _ManufacturingPrice = 0;
        private double? _SalePrice = 0;
        private DateTime? _Date = null;


        public string? Segment { get { return _Segment; } set { _Segment = value; } }
        public string? Country { get { return _Country; } set { _Country = value; } }
        public string? Product { get { return _Product; } set { _Product = value; } }
        public string? DiscountBand { get { return _DiscountBand; } set { _DiscountBand = value; } }
        public double? UnitsSold { get { return _UnitsSold; } set { _UnitsSold = value; } }
        public double? ManufacturingPrice { get { return _ManufacturingPrice; } set { _ManufacturingPrice = value; } }
        public double? SalePrice { get { return _SalePrice; } set { _SalePrice = value; } }
        public DateTime? Date { get { return _Date; } set { _Date = value; } }


    }
}
