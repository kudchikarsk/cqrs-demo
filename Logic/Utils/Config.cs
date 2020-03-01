namespace Logic.Utils
{
    public class Config
    {
        public Config(int numberOfDatabaseRetries)
        {
            NumberOfDatabaseRetries = numberOfDatabaseRetries;
        }

        public int NumberOfDatabaseRetries { get; }
    }
}


