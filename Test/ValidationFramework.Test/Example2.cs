namespace ValidationFramework.Test
{
    /// <summary>
    /// The example.
    /// </summary>
    /// <seealso cref="ValidationFramework.Validatable" />
    public sealed class Example2 : Validatable
    {
        [CannotBeNull]
        public string Name
        {
            get;
            set;
        }
    }
}