//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WelcomeToVietnam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hotel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Price { get; set; }
        public Nullable<int> EmptyRooms { get; set; }
        public Nullable<int> Rating { get; set; }
        public byte[] Photos { get; set; }
    
        public virtual Place Place1 { get; set; }
    }
}
