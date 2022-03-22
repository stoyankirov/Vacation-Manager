namespace VacationManager.Domain.Models
{
    using System.Net;
    using System.Text.Json.Serialization;
    using VacationManager.Domain.Enums;

    public class Message
    {
        public Message(MessageCode messageCode)
        {
            this.MessageCode = messageCode;
        }

        public Message(int statusCode, MessageCode messageCode)
        {
            this.StatusCode = statusCode;
            this.MessageCode = messageCode;
        }

        [JsonPropertyName("StatusCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int StatusCode { get; set; }

        [JsonPropertyName("MessageCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public MessageCode MessageCode { get; set; }
    }
}
