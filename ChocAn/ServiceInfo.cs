namespace ChocAn
{
    /*
     * Class ServiceInfo
     * Simple entity class
     * holds service information for the provider directory
     */
    class ServiceInfo
    {
        protected int Code{ get; set; }
        protected string Name { get; set; }
        protected int Fee { get; set; }

        public ServiceInfo(int code, string name, int fee)
        {
            Code = code;
            Name = name;
            Fee = fee;
        }
    }
}
