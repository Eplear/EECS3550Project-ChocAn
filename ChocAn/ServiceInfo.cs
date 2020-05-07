namespace ChocAn
{
    /*
     * Class ServiceInfo
     * Simple entity class
     * holds service information for the provider directory
     */
    public class ServiceInfo
    {
        public int Code{ get; set; }
        public string Name { get; set; }
        public int Fee { get; set; }

        public ServiceInfo(int code, string name, int fee)
        {
            Code = code;
            Name = name;
            Fee = fee;
        }
    }
}
