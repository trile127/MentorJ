//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MentorJWcfService
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblGroup
    {
        public long GroupID { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public string WebGroupLink { get; set; }
        public byte[] LargePicture { get; set; }
        public byte[] MediumPicture { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    }
}