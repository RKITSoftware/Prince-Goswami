namespace Controller_Initialization.BL
{
    public interface IBLDateTime
    {
        public Guid guid { get; }
        DateTime GetCurrentDateTime();
    }
}
