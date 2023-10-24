using System;
using System.Collections.Generic;

namespace HRMapp;

public class DateTimeformatCustom
{
    /* Param string with format "datetime - datetime"
     * Retun List<DateTime> with [0] = start , [1] = end
     */
    public static List<DateTime> DateRangeToDateTime(string daterange)
    {
        List<DateTime> result = new List<DateTime>();
        var newdaterange = daterange.Split("-");
        result.Add(DateTime.Parse(newdaterange[0]));
        result.Add(DateTime.Parse(newdaterange[1]));
        return result;
    }
}