using MRBooker.Data.Models.Entities;
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

        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "start_date")]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public DateTime EndDate { get; set; }

        [DataMember(Name = "Room")]
        public Room Room { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
