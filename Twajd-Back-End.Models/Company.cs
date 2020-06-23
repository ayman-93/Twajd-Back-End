using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Twajd_Back_End.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public string Package { get; set; }
        public bool IsActive { get; set; }
        public DataType SubscriptionData { get; set; }

    }
}
