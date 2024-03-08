namespace SampleApplication.Service
{
    /// <summary>
    /// This is just an example of how you can inject things other than ILog and IHelper.
    /// </summary>
    public interface ICustomDependency
    {
        void DoCustomWork();
    }
}
