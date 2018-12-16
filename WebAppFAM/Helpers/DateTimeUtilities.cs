using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFAM.Models;

namespace WebAppFAM
{
   

    public class DateTimeUtilities
    {
        public TimeSpan  EndDriveTime { get; set; }
        public TimeSpan StartDriveTime { get; set; }


        private Double _diffCollectionTime;
        private Double _diffStartTime;
        private Double _diffCompletionTime;
        private DateTime _expectedCompletionDate;
        private Double _distance;

        public Double DiffCollectionTime {
            get { return _diffCollectionTime; }
            set { }
        }
        public Double DiffStartTime {
            get { return _diffStartTime; }
            set { }
        }
        public Double DiffCompletionTime {
            get { return _diffCompletionTime; }
            set { }
        }
        public DateTime ExpectedCompletionDate
        {
            get { return _expectedCompletionDate; }
            set { }
        }

        public DateTimeUtilities(Double Distance)
        {
            EndDriveTime = new TimeSpan(20, 0, 0);
            StartDriveTime = new TimeSpan(5, 0, 0);
            _distance = Distance;
        }
       
        public void SetDiffCollectionTime(DateTime ExpectedDateTime, DateTime ActualDateTime)
        {
            if (ExpectedDateTime != DateTime.MinValue || ActualDateTime != DateTime.MinValue)
                _diffCollectionTime = getTimeDifference(ExpectedDateTime, ActualDateTime);
            else
            {
                _diffCollectionTime = 0;
            }
        }
        public void SetDiffStartTime(DateTime ExpectedDateTime, DateTime ActualDateTime)
        {
            if (ExpectedDateTime != DateTime.MinValue || ActualDateTime != DateTime.MinValue)
                _diffStartTime = getTimeDifference(ExpectedDateTime, ActualDateTime);
            else
                _diffStartTime = 0;
        }
        public void SetDiffCompletionTime(DateTime ExpectedDateTime, DateTime ActualDateTime)
        {
            if (ExpectedDateTime != DateTime.MinValue || ActualDateTime != DateTime.MinValue)
                _diffCompletionTime = getTimeDifference(ExpectedDateTime, ActualDateTime);
            else
                _diffCompletionTime = 0; 
        }

        public void SetExpectedCompletionTime(DateTime ExpectedStartDate, DateTime ActualStartDate)
        {
            Double TravelTime = getTravelTimeInHours(_distance);
            if (ActualStartDate != DateTime.MinValue)
            {
                _expectedCompletionDate= GetArrivalDateTime(ActualStartDate, TravelTime);
                return;
            }
            if (ExpectedStartDate != DateTime.MinValue)
            {
                _expectedCompletionDate = GetArrivalDateTime(ExpectedStartDate, TravelTime);
                return;
            }
            _expectedCompletionDate = DateTime.MinValue;
        }


        public void SetTripDateTimeStatistics(Trip Trip)
        {
            //Calculation Time Statistics
            this.SetExpectedCompletionTime(Trip.ExpectedStartDateTime, Trip.ActualStartDateTime);
            this.SetDiffCompletionTime(this.ExpectedCompletionDate, Trip.ActualCompletionDateTime);
            this.SetDiffCollectionTime(Trip.ExpectedCollectionDateTime, Trip.ActualCollectionDateTime);
            this.SetDiffStartTime(Trip.ExpectedStartDateTime, Trip.ActualStartDateTime);
            //Update the Trip Object with the Time Statistics
            Trip.ExpectedCompletionDateTime = this.ExpectedCompletionDate;
            Trip.DiffCollectionTimeHrs = this.DiffCollectionTime;
            Trip.DiffStartTimeHrs = this.DiffStartTime;
            Trip.DiffEndTimeHrs = this.DiffCompletionTime;
            
        }



        public DateTime GetArrivalDateTime(DateTime StartDate, Double TimeToDestinationInHours)
        {
            Double ExpiredMinutes;

            DateTime DailyStartDate;
            DateTime DailyEndDate;
            DateTime ArrivalDate = new DateTime();

            ExpiredMinutes = TimeToDestinationInHours * 60;

            DailyStartDate = StartDate;
            DailyEndDate = StartDate.Date.Add(EndDriveTime);
            do {
                if (DailyStartDate.AddMinutes(ExpiredMinutes) < DailyEndDate)
                {
                    ArrivalDate = DailyStartDate.AddMinutes(ExpiredMinutes);
                    ExpiredMinutes = 0;
                }
                else
                {
                    TimeSpan diff = DailyEndDate - DailyStartDate;
                    //TravelTimePerDay = diff.TotalMinutes;
                    ExpiredMinutes = ExpiredMinutes - diff.TotalMinutes;
                    DailyStartDate = DailyStartDate.Date.Add(StartDriveTime).AddDays(1);
                    DailyEndDate = DailyEndDate.Date.Add(EndDriveTime).AddDays(1);
                }
                  
            } while (ExpiredMinutes > 0);
                 
            return ArrivalDate;

        }

        public TimeSpan getNonAvailableTime(DateTime StartDate,DateTime EndDate )
        {
            DateTime DailyStartDate = StartDate;
            DateTime DailyBegTimeSlot = new DateTime();
            DateTime DailyEndTimeSlot = new DateTime();
            TimeSpan nonAvailableTime = new TimeSpan();

            DateTime StartDateDay = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
            DailyBegTimeSlot = StartDateDay.Add(EndDriveTime);
            DailyEndTimeSlot = StartDateDay.Add(StartDriveTime).AddDays(1);

            do
            {
                if (EndDate > DailyBegTimeSlot && EndDate > DailyEndTimeSlot)
                {
                    nonAvailableTime = nonAvailableTime.Add(DailyEndTimeSlot - DailyBegTimeSlot);
                    DailyStartDate = DailyEndTimeSlot;
                    DailyBegTimeSlot = DailyBegTimeSlot.AddDays(1);
                    DailyEndTimeSlot = DailyEndTimeSlot.AddDays(1);
                }
                if (EndDate >= DailyBegTimeSlot && EndDate <= DailyEndTimeSlot)
                {
                    nonAvailableTime = nonAvailableTime.Add(EndDate - DailyBegTimeSlot);
                    DailyStartDate = DailyEndTimeSlot;
                }

                if (EndDate < DailyBegTimeSlot)
                {
                    DailyStartDate = DailyEndTimeSlot;
                }
            } while (DailyStartDate < EndDate);

            return nonAvailableTime;
        }

        public Double getTimeDifference(DateTime StartDate , DateTime EndDate)
        {
            TimeSpan DateDifference = new TimeSpan();
            TimeSpan nonAvailableTime = new TimeSpan();
            nonAvailableTime = getNonAvailableTime(StartDate, EndDate);
            //DateDifference = DateDifference.Add(EndDate - StartDate).Subtract(nonAvailableTime);
            DateDifference = EndDate - StartDate - nonAvailableTime;
            return DateDifference.TotalHours;
        }

        public Double getTravelTimeInHours(Double Distance)
        {
            Double TravelHours = 0;
            switch (Distance)
            {
                case Double n when (n >= 500):
                    TravelHours = Distance / 50;
                    break;
                case Double n when (n < 500 && n >= 200):
                    TravelHours = Distance / 45;
                    break;
                case Double n when (n < 200 && n >= 100):
                    TravelHours = Distance / 40;
                    break;
                case Double n when (n < 100 && n >= 50):
                    TravelHours = Distance / 35;
                    break;
                case Double n when (n < 50 && n >= 40):
                    TravelHours = Distance / 30;
                    break;
                case Double n when (n < 40 && n >= 30):
                    TravelHours = Distance / 25;
                    break;
                case Double n when (n < 30 && n >= 20):
                    TravelHours = Distance / 20;
                    break;
                case Double n when (n < 20 && n >= 10):
                    TravelHours = Distance / 15;
                    break;
                case Double n when (n < 10 ):
                    TravelHours = Distance / 10;
                    break;
            }
            return TravelHours;
        }


        //Public Function getNonAvailableTime(StartDate As Date, EndDate As Date) As Double

        //Dim DailyStartDate As Date
        //Dim DailyBegTimeSlot As Date
        //Dim DailyEndTimeSlot As Date
        //Dim nonAvailableTime As Double
        //DailyStartDate = StartDate

        //Dim StartDateDay As Date

        //StartDateDay = DateSerial(Year(StartDate), Month(StartDate), Day(StartDate))

        //DailyBegTimeSlot = DateAdd("h", 20, StartDateDay)
        //DailyEndTimeSlot = DateAdd("d", 1, DateAdd("h", 5, StartDateDay))

        //nonAvailableTime = 0

        //While DailyStartDate<EndDate
        //        If EndDate > DailyBegTimeSlot And EndDate > DailyEndTimeSlot Then
        //            nonAvailableTime = nonAvailableTime + DailyEndTimeSlot - DailyBegTimeSlot
        //            DailyStartDate = DailyEndTimeSlot
        //            DailyBegTimeSlot = DailyBegTimeSlot + 1
        //            DailyEndTimeSlot = DailyEndTimeSlot + 1
        //        End If
        //        If EndDate >= DailyBegTimeSlot And EndDate <= DailyEndTimeSlot Then
        //            nonAvailableTime = nonAvailableTime + EndDate - DailyBegTimeSlot
        //            DailyStartDate = DailyEndTimeSlot
        //        End If
        //        If EndDate < DailyBegTimeSlot Then
        //            'nonAvailableTime = nonAvailableTime + EndDate - DailyStartDate
        //            DailyStartDate = DailyEndTimeSlot
        //        End If
        //Wend

        //getNonAvailableTime = nonAvailableTime

        //End Function









        //'Get the DateDifference in Hours
        //Public Function getTimeDifference(StartDate As Date, EndDate As Date) As Double

        //Dim DateDifference As Double
        //Dim DateEnd As Date
        //Dim DateStart As Date
        //Dim nonAvailableTime As Double

        //nonAvailableTime = getNonAvailableTime(StartDate, EndDate)

        //DateDifference = EndDate - StartDate - nonAvailableTime

        //getTimeDifference = DateDifference




        //End Function


        //Public Function getNonAvailableTime(StartDate As Date, EndDate As Date) As Double

        //Dim DailyStartDate As Date
        //Dim DailyBegTimeSlot As Date
        //Dim DailyEndTimeSlot As Date
        //Dim nonAvailableTime As Double
        //DailyStartDate = StartDate

        //Dim StartDateDay As Date

        //StartDateDay = DateSerial(Year(StartDate), Month(StartDate), Day(StartDate))

        //DailyBegTimeSlot = DateAdd("h", 20, StartDateDay)
        //DailyEndTimeSlot = DateAdd("d", 1, DateAdd("h", 5, StartDateDay))

        //nonAvailableTime = 0

        //While DailyStartDate<EndDate
        //        If EndDate > DailyBegTimeSlot And EndDate > DailyEndTimeSlot Then
        //            nonAvailableTime = nonAvailableTime + DailyEndTimeSlot - DailyBegTimeSlot
        //            DailyStartDate = DailyEndTimeSlot
        //            DailyBegTimeSlot = DailyBegTimeSlot + 1
        //            DailyEndTimeSlot = DailyEndTimeSlot + 1
        //        End If
        //        If EndDate >= DailyBegTimeSlot And EndDate <= DailyEndTimeSlot Then
        //            nonAvailableTime = nonAvailableTime + EndDate - DailyBegTimeSlot
        //            DailyStartDate = DailyEndTimeSlot
        //        End If
        //        If EndDate < DailyBegTimeSlot Then
        //            'nonAvailableTime = nonAvailableTime + EndDate - DailyStartDate
        //            DailyStartDate = DailyEndTimeSlot
        //        End If
        //Wend

        //getNonAvailableTime = nonAvailableTime

        //End Function

    }
}
