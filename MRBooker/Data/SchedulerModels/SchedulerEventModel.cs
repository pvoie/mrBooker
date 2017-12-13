using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MRBooker.Data.SchedulerModels
{
    [DataContract]
    public class SchedulerEventModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        private DateTime? _startDate { get; set; }
        [DataMember(Name = "start_date")]
        public string StartDate
        {
            get
            {
                DateTime.TryParse(_startDate.ToString(), out DateTime date);
                return date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set => _startDate = ParseDate(value);
        }


        public DateTime? _endDate { get; set; }
        [DataMember(Name = "end_date")]
        public string EndDate
        {
            get
            {
                DateTime.TryParse(_endDate.ToString(), out DateTime date);
                return date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set => _endDate = ParseDate(value);
        }

        [DataMember(Name = "type")]
        public string TypeStr => Type.ToString();

        public long Type { get; set; }
        [DataMember(Name = "roomId")]
        public long RoomId { get; set; }

        public string UserId { get; set; }

        public string IpAddress { get; set; }
        private static DateTime ParseDate(string value)
        {
            var charsToRemove = new[] { "T", "Z", ".000"};
            foreach (var c in charsToRemove)
            {
                value = value.Replace(c, " ");
            }
            var trimEnd = value.TrimEnd(' ', ' ');
            string[] dateFormat ={"yyyy-MM-dd HH:mm:ss" };
            
            var enUs = new CultureInfo("en-US");
            DateTime.TryParseExact(trimEnd, dateFormat, enUs, DateTimeStyles.None, out DateTime date);
                return date.ToUniversalTime();

        }
    }
}
