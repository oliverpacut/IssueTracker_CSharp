using System;

namespace BL.Utils
{
    public static class HelperFunctions
    {
        public static bool IsNearDate(this DateTime dateA, DateTime? dateB)
        {
            if(!dateB.HasValue)
            {
                return false;
            }

            return ((dateA.Day - ((DateTime)dateB).Day) <= 1) || ((((DateTime)dateB).Day - dateA.Day) <= 1);
        }
    }
}
