using System;

namespace VyatkinSchool.Models
{
    public sealed class UserHostAddress
    {
        public int Id { get; set; }
        public DateTime LastVisit { get; set; }
        public string HostAddress { get; set; }
    }
}