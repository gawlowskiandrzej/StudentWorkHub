namespace worker.Models.DTO
{
    public class JobInfo
    {
        public string JobId { get; set; }
        public string Status { get; set; }
        public List<List<string>> BathList { get; set; }
        public int TotalBatches { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
