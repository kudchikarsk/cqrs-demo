namespace Logic.Utils
{
    public class Config
    {
        public Config(int value)
        {
            NumberOfDatabaseRetries = value;
        }

        public int NumberOfDatabaseRetries { get; }
    }
}


