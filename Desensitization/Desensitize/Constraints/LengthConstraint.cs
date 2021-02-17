using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    public class LengthConstraint : IConstraint
    {
        public LengthConstraint(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length, "length not allow less than 0");
            }
            Length = length;
        }

        public LengthConstraint(int minLength, int maxLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentOutOfRangeException("minLength", minLength, "minLength not allow less than 0");
            }

            if (maxLength < 0)
            {
                throw new ArgumentOutOfRangeException("maxLength", maxLength, "maxLength not allow less than 0");
            }

            MinLength = minLength;
            MaxLength = maxLength;
        }

        public int? Length { get; private set; }

        public int? MinLength { get; private set; }

        public int? MaxLength { get; private set; }


        public bool Match(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            int length = valueString.Length;
            if (Length.HasValue)
            {
                return length == Length.Value;
            }
            else
            {
                return length >= MinLength.Value && length <= MaxLength.Value;
            }
        }
    }
}