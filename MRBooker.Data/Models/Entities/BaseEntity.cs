using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MRBooker.Data.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Int64 Id
        {
            get;
            set;
        }

        public DateTime AddedDate
        {
            get;
            set;
        }

        public DateTime ModifiedDate
        {
            get;
            set;
        }

        public string IPAddress
        {
            get;
            set;
        }
    }
}
