using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HelloWorldWcfHost
{
    [DataContract]
    public class UserInfoData
    {
        public UserInfoData()
        {
            Name = "Hello ";
            SayHello = false;
        }

        [DataMember]
        public bool SayHello { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class UserInfoTable
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string First_Name { get; set; }
        [DataMember]
        public string Last_Name { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public string StreetAddress { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastLogin { get; set; }
        [DataMember]
        public byte[] LastActive { get; set; }
        [DataMember]
        public Nullable<System.DateTime> AccountDateCreation { get; set; }
    }
}