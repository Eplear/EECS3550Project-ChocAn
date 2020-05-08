namespace ChocAn
{
    class Program
    {
        const bool PROVIDER_TERMINAL = false;
        const bool MANAGER_TERMINAL  = true;

        static public DataCenter database = new DataCenter();
        static public Report report = new Report();
        static public BankRecord bankrecord = new BankRecord();

        public static void Main(string[] args)
        {
            database.Populate();
            //enter if terminal should be provider
            if (PROVIDER_TERMINAL)
            {
                ProviderClient client = new ProviderClient();
            }

            //enter if terminal should be provider
            if (MANAGER_TERMINAL)
            {
                ManagerClient client = new ManagerClient();
            }
        }        
    }
}
