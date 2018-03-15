namespace ValidationFramework.Test
{
    /// <summary>
    /// The example.
    /// </summary>
    /// <seealso cref="ValidationFramework.Validatable" />
    public sealed class Example3 : Validatable
    {
        [FaultyValidator]
        public string Name
        {
            get;
            set;
        }
    }
}