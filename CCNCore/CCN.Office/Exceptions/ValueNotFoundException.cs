using System;

namespace CCN.Office.Exceptions 
{
    class ValueNotFoundException: Exception
    {
        public ValueNotFoundException(string message) : base(message)
        {
        }
    }
}
