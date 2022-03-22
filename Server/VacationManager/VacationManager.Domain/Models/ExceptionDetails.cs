namespace VacationManager.Domain.Models
{
    using Newtonsoft.Json;

    public class ExceptionDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
