using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;



namespace MentorJ.Android.Models
{
    [Serializable]
    public partial class sqltblUserInfo
    {
        [PrimaryKey, AutoIncrement, Column("UserId")]
        public long UserID { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(25)]
        public string First_Name { get; set; }
        [MaxLength(25)]
        public string Middle_Name { get; set; }
        [MaxLength(25)]
        public string Last_Name { get; set; }
        [MaxLength(10)]
        public string Sex { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> Age { get; set; }
        [MaxLength(50)]
        public string Street_Address { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public Nullable<bool> isPremium { get; set; }
        public Nullable<bool> isMentor { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public System.DateTime LastActiveDate { get; set; }
        public System.DateTime AccountCreationDate { get; set; }
        public Nullable<long> FailedLoginAttempts { get; set; }
        public Nullable<System.DateTime> LastFailedLoginDate { get; set; }
        public Nullable<bool> AccountLocked { get; set; }




        }

    }