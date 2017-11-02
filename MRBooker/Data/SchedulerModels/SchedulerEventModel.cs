using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace MRBooker.Data.SchedulerModels
{
    [DataContract]
    public class SchedulerEventModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
