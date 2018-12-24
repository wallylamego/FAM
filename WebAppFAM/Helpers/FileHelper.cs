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
            string newFileName = "Trip_" + TripID.ToString();
            DateTime FileDate = DateTime.Now;
            FileDateTimeStamp = FileDate;
            string FileDateTime = FileDate.Year + "_" + FileDate.Month + "_"+ FileDate.Day + "_" + FileDate.Hour.ToString() +
                FileDate.Minute.ToString() + "_" + FileDate.Second.ToString()+ "_";
            newFileName = newFileName + "_" + FileDateTime;
            newFileName = newFileName + CurrentFileName;
            return newFileName;
        }
    }
}
