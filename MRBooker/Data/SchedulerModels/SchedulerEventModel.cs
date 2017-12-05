using MRBooker.Data.Models.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MRBooker.Data.SchedulerModels
{
    [DataContract]
    public class SchedulerEventModel
    {
        [DataMember(Name = "id")]
        public string IdStr => Id.ToString();

        public long Id { get; set; }

        [DataMember(Name = "text")]
        public string Title { get; set; }
        
        public string Description { get; set; }
       
        public string Status { get; set; }
        
        //public string StartDateStr => StartDate.ToString("dd/MM/yyyy HH:mm:ss");
        [DataMember(Name = "start_date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss}")]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "end_date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss}")]
        public DateTime EndDate { get; set; }
        //public string EndDateStr => EndDate.ToString("dd/MM/yyyy HH:mm:ss");

        [DataMember(Name = "type")]
        public string TypeStr => Type.ToString();

        public long Type { get; set; }

        public Room Room { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
