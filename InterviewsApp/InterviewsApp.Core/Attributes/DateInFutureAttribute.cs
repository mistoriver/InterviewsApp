using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.Attributes
{
    public class DateInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                return (DateTime)value > DateTime.Now;
            }
            return false;
        }
    }
}
