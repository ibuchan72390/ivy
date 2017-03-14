namespace IBFramework.Core.Data
{
    public interface IInitializeByConnectionString
    {
        //string ConnectionString { get; }

        void InitializeByConnectionString(string connectionString);
    }
}
