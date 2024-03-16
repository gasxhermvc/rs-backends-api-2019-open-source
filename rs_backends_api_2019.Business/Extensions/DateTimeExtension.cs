using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace rs_backends_api_2019.Business.Extensions
{
    public static class DateTimeExtension
    {
        // ตัวแปรสำหรับแปลงค่าเวลา ค.ศ. ไปเป็นปี พ.ศ.
        // for convert 2018 to 2561
        // example string dt = DateTime.Parse("2018-01-01").ToString("dd MMMM yyyy",format_th)
        // output => 01 มกราคม 2561
        public static IFormatProvider format_th = new CultureInfo("th-TH");

        // ตัวแปรสำหรับแปลงค่าเวลา พ.ศ. ไปเป็นปี ค.ศ.
        // for convert 2561 to 2018
        // example string dt = DateTime.Parse("13/01/2561", GlobalFunctions.format_th).ToString("yyyy-MM-dd")
        // output => 2018-13-01
        // or example DateTime dt = DateTime.ParseExact("13/01/2561","dd/MM/yyyy",GlobalFunctions.format_th)
        // output => 2018-01-13 00:00:00
        public static IFormatProvider format_us = new CultureInfo("en-US");

        public static DateTime FindStartDate(int year, int month)
        {
            return DateTime.Parse(string.Format("{0}-{1}-{2}", year, month, 1));
        }

        public static int DayInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static DateTime FindStopDate(int year, int month)
        {
            return DateTime.Parse(string.Format("{0}-{1}-{2}", year, month, DayInMonth(year, month)));
        }

        public static DateTime FromWeb(string dt)
        {
            return DateTime.Parse(dt, DateTimeExtension.format_us);
        }

        public static string FromWeb(string dt, string formatter = "dd/MM/yyyy HH:mm:ss")
        {
            return DateTime.Parse(dt, DateTimeExtension.format_th).ToString(formatter);
        }

        public static DateTime FromWeb(DateTime dt)
        {
            return DateTime.Parse(dt.ToString("dd/MM/yyyy HH:mm:ss"), DateTimeExtension.format_th);
        }

        public static string FromWeb(DateTime dt, string formatter = "dd/MM/yyyy HH:mm:ss")
        {
            return DateTime.Parse(dt.ToString("dd/MM/yyyy HH:mm:ss"), DateTimeExtension.format_th)
                .ToString(formatter, DateTimeExtension.format_us);
        }

        public static string DisplayDuration(DateTime dt, string startTime, string stopTime)
        {
            DateTime startDate = DateTime.Parse(string.Format("{0} {1}:00", dt.ToString("yyyy-MM-dd"), startTime));
            DateTime stopDate = DateTime.Parse(string.Format("{0} {1}:00", dt.ToString("yyyy-MM-dd"), stopTime));

            TimeSpan t = stopDate - startDate;

            if (t == null)
            {
                return "-";
            }

            return string.Format("{0:00} ชั่วโมง {1:00} นาที {2:00} วินาที", t.Hours, t.Minutes, t.Seconds);
        }

        public static TimeSpan? TimerDuration(DateTime dt, string startTime, string stopTime)
        {
            DateTime startDate = DateTime.Parse(string.Format("{0} {1}:00", dt.ToString("yyyy-MM-dd"), startTime));
            DateTime stopDate = DateTime.Parse(string.Format("{0} {1}:00", dt.ToString("yyyy-MM-dd"), stopTime));

            TimeSpan t = stopDate - startDate;

            if (t == null)
            {
                return null;
            }

            return t;
        }

        // ตั้งค่าเวลาเริ่มต้นของแอปพลิเคชัน
        public static void SetDateEnv()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo info = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            info.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            info.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = info;
        }
    }
}