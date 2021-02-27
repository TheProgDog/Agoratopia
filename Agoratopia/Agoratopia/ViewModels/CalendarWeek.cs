using System;
using System.Collections.Generic;
using System.Text;
using Agoratopia.Views;
using System.Collections.ObjectModel;

namespace Agoratopia.ViewModels
{
    class CalendarWeek : ObservableCollection<int[]>
    {

        public CalendarWeek() : base()
        {
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            var currDay = DateTime.Now.Day;
            var startDayOfWeek = (int)DateTime.Now.DayOfWeek;

            while (currDay >= 1)
            {
                currDay--;
                startDayOfWeek--;

                if (startDayOfWeek < 0)
                    startDayOfWeek = 6;
            }

            //int[] month = (startDayOfWeek == 5 && daysInMonth == 31) || (startDayOfWeek == 6 && daysInMonth >= 30) ? new int[42] : new int[35];

            int numOfWeeks = (startDayOfWeek == 5 && daysInMonth == 31) || (startDayOfWeek == 6 && daysInMonth >= 30) ? 6 : 5;
            int[] week = new int[7];

            for (int i = 0; i < numOfWeeks * 7; i++)
            {
                // Mod to find the day of the week
                // 0 = Sun, 1 = Mon, 2 = Tue, etc.
                int dayOfWeek = i % 7;

                // Offset value by "startDayOfWeek" since months always start on a different day of the week
                var value = i - startDayOfWeek;

                // Limit each day between 1 and max days in the month
                // Any day not valid in the month is represented with a 0
                week[i % 7] = value > 0 && value <= daysInMonth ? value : 0;


                // if = Break whenever we enter a new week
                if (dayOfWeek == 0 && week[dayOfWeek] == 0 && i > 0)
                    break;
                if (dayOfWeek == 6)
                {
                    Add(week);

                    week = new int[7];
                }


            }

        }
    }
}
