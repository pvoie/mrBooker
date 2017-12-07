using MRBooker.Data.Models.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace MRBooker.Data.SchedulerModels
{
    [DataContract]
    public class SchedulerEventModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "text")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        private DateTime? _startDate { get; set; }
        [DataMember(Name = "start_date")]
        public string StartDate
        {
            get
            {
                DateTime.TryParse(_startDate.ToString(), out DateTime date);
                return date.ToString("dd/MM/yyyy HH:mm:ss");
            }
            set => _startDate = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }


        public DateTime? _endDate { get; set; }
        [DataMember(Name = "end_date")]
        public string EndDate
        {
            get
            {
                DateTime.TryParse(_endDate.ToString(), out DateTime date);
                return date.ToString("dd/MM/yyyy HH:mm:ss");
            }
            set => _endDate = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        [DataMember(Name = "type")]
        public string TypeStr => Type.ToString();

        public long Type { get; set; }

        public Room Room { get; set; }
    }
}
