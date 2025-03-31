using DataSummary.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Diagnostics;
using static DataSummary.Pages.Index;
using DataSummary.Services;
using System.ComponentModel;

namespace DataSummary.Pages
{
    public partial class Index
    {

        private SalesDataSearchFilters? SalesDataFilters;
        DateTime filterSalesDateFrom = DateTime.MinValue;
        DateTime filterSalesDateTo = DateTime.MaxValue;
        string filterSegment = "";
        string filterCountry = "";
        string filterProduct = "";
        string filterDiscountBand = "";

        private Dictionary<string, string> Segments = new Dictionary<string, string>();
        private Dictionary<string, string> Countries = new Dictionary<string, string>();
        private Dictionary<string, string> Products = new Dictionary<string, string>();
        private Dictionary<string, string> DiscountBands = new Dictionary<string, string>();

        List<SalesData> salesData = new List<SalesData>();

        protected override async void OnInitialized()
        {
            SalesDataFilters = new SalesDataSearchFilters();
            SalesDataBuild(filterSalesDateFrom, filterSalesDateTo, "", "", "", "");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        private void Search()
        {
            SalesDataBuild(filterSalesDateFrom, filterSalesDateTo, filterSegment, filterCountry, filterProduct, filterDiscountBand);
        }

        private void SalesDataBuild(DateTime dateFrom, DateTime dateTo, string segment, string country, string product, string discountBand)
        {
            salesData = Service.ReadSalesData(ref Segments, ref Countries, ref Products, ref DiscountBands, dateFrom, dateTo, segment, country, product, discountBand);
        }

        public string formatDate(DateTime? dttmDateToConvert)
        {
            string formattedDate = "";
            if (dttmDateToConvert != null)
            {
                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                int MonthNumber = dttmDateToConvert?.Month ?? 0;
                //@mfi.GetMonthName(MonthNumber).ToString().Substring(0, 3);
                formattedDate = dttmDateToConvert?.Day + " " + mfi.GetMonthName(MonthNumber).ToString().Substring(0, 3) + " " + dttmDateToConvert?.Year.ToString();
            }
            return formattedDate;
        }

    }
}