using System;

namespace Library.StringEnums
{
    public class StringEnum : Attribute
    {
        public StringEnum(string stringValue)
        {
            StringValue = stringValue;
        }

        public string StringValue { get; }
    }
}