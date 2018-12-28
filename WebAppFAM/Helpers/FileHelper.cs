using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Helpers
{
    public static class FileHelper
    {
        public static string newFileName(int TripID,string CurrentFileName, out DateTime FileDateTimeStamp)
        {
            string newFileName = "No" + TripID.ToString();
            DateTime FileDate = DateTime.Now;
            FileDateTimeStamp = FileDate;
            string yyyy = FileDate.Year.ToString();
            string mth = FileDate.Month < 10 ? "0" + FileDate.Month.ToString() : FileDate.Month.ToString();
            string dd = FileDate.Day < 10 ? "0" + FileDate.Day.ToString() : FileDate.Day.ToString();
            string hh = FileDate.Hour < 10 ? "0" + FileDate.Hour.ToString() : FileDate.Hour.ToString();
            string mm = FileDate.Minute < 10 ? "0" + FileDate.Minute.ToString() : FileDate.Minute.ToString();
            string ss = FileDate.Second < 10 ? "0" + FileDate.Second.ToString() : FileDate.Second.ToString();
            string FileDateTime = yyyy + mth + dd + hh +mm+ ss +"_"; 
            newFileName = newFileName + "_" + FileDateTime;
            newFileName = newFileName + CurrentFileName.Substring(0,Math.Min(CurrentFileName.Length,5));
            return newFileName.Replace(" ","");
        }
    }
}
