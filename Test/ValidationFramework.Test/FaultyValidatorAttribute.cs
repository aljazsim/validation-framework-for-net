using System;

namespace ValidationFramework.Test
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class FaultyValidatorAttribute : ValidationAttribute
    {
        public override string GetDefaultMessage()
        {
            return null;
        }

        public override string GetDefaultMessageKey()
        {
            return null;
        }

        public override bool IsValid(object value)
        {
            throw new NotImplementedException();
        }
    }
}