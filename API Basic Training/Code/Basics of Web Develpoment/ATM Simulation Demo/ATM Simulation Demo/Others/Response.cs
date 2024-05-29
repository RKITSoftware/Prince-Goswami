namespace ATM_Simulation_Demo.Others
{
    public class Response
    {
        public bool IsError { get; set; } = false;

        public dynamic Data { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}