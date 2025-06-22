using System;
using System.ComponentModel.DataAnnotations;

namespace BAIS3230Project.Validation
{

        public class FutureDateAttribute : ValidationAttribute
        {
            // Dynamically calculate season end: September 30th of the current year
            private DateTime SeasonEnd => new DateTime(DateTime.Today.Year, 9, 30);

            public override bool IsValid(object value)
            {
                if (value is DateTime date)
                {
                    return date.Date >= DateTime.Today && date.Date <= SeasonEnd;
                }

                return false;
            }
        }
        //public override bool IsValid(object value)
        //{
        //    if (value is DateTime date)
        //    {
        //        return date.Date >= DateTime.Today;
        //    }

        //    return false;
        //}
} 


