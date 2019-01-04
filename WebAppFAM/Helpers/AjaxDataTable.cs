using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM
{ 
public class DataTableAjaxPostModel
{
    // properties are not capital due to json mapping
    public int draw { get; set; }
    public int start { get; set; }
    public int length { get; set; }
    public List<Column> columns { get; set; }
    public Search search { get; set; }
    public List<Order> order { get; set; }
    

    public static string GetOrderByString(List<Order> OrderList, List<Column> ColumnList, string DefaultColumnSort)
        {
            string orderByString = "";
            Column column;
            if (OrderList != null)
            {
                foreach (var Order in OrderList)
                {
                    column = ColumnList[Order.column];

                    orderByString += orderByString != String.Empty ? "," : "";
                    orderByString += (column.data) + (Order.dir.ToLower() == "asc" ? " asc" : " desc");
                }
            }
            else
            {
                orderByString = DefaultColumnSort + " asc";
            }
            return orderByString;
        }

        public static void  GetOrderByParameters(List<Order> OrderList, List<Column> ColumnList, string DefaultSortBy,
                                               out bool SortDir, out string SortBy)
        {
            Column column;

            SortBy = DefaultSortBy;
            SortDir = true;

            if (OrderList != null)
            {
                foreach (var Order in OrderList)
                {
                    column = ColumnList[Order.column];
                    SortBy = DataTableAjaxPostModel.UppercaseFirst(column.data);
                    SortDir = Order.dir.ToLower() == "asc" ?  true : false ; 
                }
            }

        }

        
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        
    }

    



    public class Column
{
    public string data { get; set; }
    public string name { get; set; }
    public bool searchable { get; set; }
    public bool orderable { get; set; }
    public Search search { get; set; }
}

    public class Search
{
    public string value { get; set; }
    public string regex { get; set; }
}

    public class Order
{
    public int column { get; set; }
    public string dir { get; set; }
}
    /// End- JSon class sent from Datatables
    /// 


}
