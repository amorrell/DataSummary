using DataSummary.Entities;
using System.Globalization;
using System.IO;
using static System.Net.WebRequestMethods;

namespace DataSummary.Services
{
    public class CommaSeparatedValuesFileService
    {
        //TODO refactor this into a collection of Sales Data methods
        //and build the list from the actual data in the CSV file
        //public Dictionary<string, string>? SegmentsGet()
        //{
        //    Dictionary<string, string>? Segments = new Dictionary<string, string>();

        //    Segments.Add("Government", "Government");
        //    Segments.Add("Midmarket", "Midmarket");
        //    return Segments;
        //}

        //TODO refactor this into a collection of Sales Data methods
        public List<SalesData> ReadSalesData(ref Dictionary<string, string> Segments, ref Dictionary<string, string> Countries, ref Dictionary<string, string> Products
            , ref Dictionary<string, string> DiscountBands, DateTime dateFrom, DateTime dateTo, string segment, string country, string product, string discountBand)
        {
            List<SalesData> salesData = new List<SalesData>();

            string[] csvLines = ReadAllCSVLines();

            SalesData sale;
            string[] salesDateParts;
            int lineIndex = 0;
            foreach (string saleLine in csvLines)
            {
                if (lineIndex > 0)
                {
                    string[] saleDetails = saleLine.Split(',');
                    sale = new SalesData();
                    sale.Segment = saleDetails[0];

                    if (Segments.ContainsKey(sale.Segment) == false)
                    {
                        Segments.Add(sale.Segment, sale.Segment);
                    }

                    sale.Country = saleDetails[1];
                    if (Countries.ContainsKey(sale.Country) == false)
                    {
                        Countries.Add(sale.Country, sale.Country);
                    }

                    sale.Product = saleDetails[2];
                    if (Products.ContainsKey(sale.Product) == false)
                    {
                        Products.Add(sale.Product, sale.Product);
                    }

                    sale.DiscountBand = saleDetails[3];
                    if (DiscountBands.ContainsKey(sale.DiscountBand) == false)
                    {
                        DiscountBands.Add(sale.DiscountBand, sale.DiscountBand);
                    }

                    if ((sale.Segment != segment) && (segment != ""))
                    {
                        continue;
                    }
                    if ((sale.Country != country) && (country != ""))
                    {
                        continue;
                    }
                    if ((sale.Product != product) && (product != ""))
                    {
                        continue;
                    }
                    if ((sale.DiscountBand != discountBand) && (discountBand != ""))
                    {
                        continue;
                    }

                    sale.UnitsSold = DataCleanseAsDouble(saleDetails[4], false);
                        sale.ManufacturingPrice = DataCleanseAsDouble(saleDetails[5], true);
                        sale.SalePrice = DataCleanseAsDouble(saleDetails[6], true);

                        salesDateParts = saleDetails[7].Split('/');
                        sale.Date = new DateTime(Convert.ToInt16(salesDateParts[2]), Convert.ToInt16(salesDateParts[0]), Convert.ToInt16(salesDateParts[1]));

                        salesData.Add(sale);
                }
                lineIndex++;
            }

            return salesData;
        }

        private double DataCleanseAsDouble(string dataValue, bool removeCurrencySymbolIfFound)
        {
            if (removeCurrencySymbolIfFound)
            {
                int intFirstCharacterOfDouble;
                if (Int32.TryParse(dataValue.Substring(0,1), out intFirstCharacterOfDouble) == false)
                {
                    dataValue = dataValue.Substring(1);
                }
            }
            if (dataValue == "") { dataValue = "0"; }
            double dataCleansed = Convert.ToDouble(dataValue.Replace(" ", ""));
            return dataCleansed;
        }

        private string[] ReadAllCSVLines()
        {
            string[] csvData = System.IO.File.ReadAllLines("Files/Data.csv");
            //List<string> pizzaRows = csvPizza.Split("\n").ToList();
            //for (int i = 0; i < pizzaRows.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        List<string> pizzaStringList = pizzaRows[i].Split(";").ToList();

            //        Pizza pizza = new Pizza(
            //            Int32.Parse(pizzaStringList[0]),
            //            pizzaStringList[1],
            //            float.Parse(pizzaStringList[2], CultureInfo.InvariantCulture),
            //            Int32.Parse(pizzaStringList[3])
            //        );
            //        listOfPizzasBlazor.Add(pizza);
            //    }
            //}
            return csvData;
        }
    }
}
