using MentorJWcfService.Utilities;
using MentorJWcfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//using MentorJWcfService.DataBaseEntitiesSupplement;

namespace MentorJWcfService
{
    public partial class MentorJService : IMentorJService
    {

        //private tblUserInfo TranslatetblUserInfoTotblUserInfo(tblUserInfo user)
        //{
        //    tblUserInfo newuser = new tblUserInfo();

        //    newuser.UserID = user.UserID;
        //    newuser.UserName = user.UserName;

        //    newuser.UserID = user.UserID;
        //    newuser.UserName = user.UserName;
        //    newuser.Email = user.Email;
        //    newuser.Password = user.Password;
        //    newuser.First_Name = user.First_Name;
        //    newuser.Middle_Name = user.Middle_Name;
        //    newuser.Last_Name = user.Last_Name;
        //    newuser.UserName = user.UserName;
        //    newuser.Sex = user.Sex;
        //    newuser.Birthday = user.Birthday;
        //    newuser.Age = user.Age;
        //    newuser.Street_Address = user.Street_Address;

        //    newuser.City = user.City;
        //    newuser.State = user.State;
        //    newuser.ZipCode = user.ZipCode;
        //    newuser.Country = user.Country;
        //    newuser.PhoneNumber = user.PhoneNumber;
        //    newuser.isPremium = user.isPremium;
        //    newuser.isMentor = user.isMentor;
        //    newuser.isAdmin = user.isAdmin;
        //    newuser.LastUpdatedDate = user.LastUpdatedDate;
        //    newuser.LastLoginDate = user.LastLoginDate;
        //    newuser.LastActiveDate = user.LastActiveDate;
        //    newuser.AccountCreationDate = user.AccountCreationDate;

        //    newuser.FailedLoginAttempts = user.FailedLoginAttempts;
        //    newuser.LastFailedLoginDate = user.LastFailedLoginDate;
        //    newuser.AccountLocked = user.AccountLocked;
        //    return newuser;
        //}

        //private tblUserInfo TranslatetblUserInfoTotblUserInfo(tblUserInfo user)
        //{
        //    tblUserInfo newuser = new tblUserInfo();

        //    newuser.UserID = user.UserID;
        //    newuser.UserName = user.UserName;

        //    newuser.UserID = user.UserID;
        //    newuser.UserName = user.UserName;
        //    newuser.Email = user.Email;
        //    newuser.Password = user.Password;
        //    newuser.First_Name = user.First_Name;
        //    newuser.Middle_Name = user.Middle_Name;
        //    newuser.Last_Name = user.Last_Name;
        //    newuser.UserName = user.UserName;
        //    newuser.Sex = user.Sex;
        //    newuser.Birthday = user.Birthday;
        //    newuser.Age = user.Age;
        //    newuser.Street_Address = user.Street_Address;

        //    newuser.City = user.City;
        //    newuser.State = user.State;
        //    newuser.ZipCode = user.ZipCode;
        //    newuser.Country = user.Country;
        //    newuser.PhoneNumber = user.PhoneNumber;
        //    newuser.isPremium = user.isPremium;
        //    newuser.isMentor = user.isMentor;
        //    newuser.isAdmin = user.isAdmin;
        //    newuser.LastUpdatedDate = user.LastUpdatedDate;
        //    newuser.LastLoginDate = user.LastLoginDate;
        //    newuser.LastActiveDate = user.LastActiveDate;
        //    newuser.AccountCreationDate = user.AccountCreationDate;

        //    newuser.FailedLoginAttempts = user.FailedLoginAttempts;
        //    newuser.LastFailedLoginDate = user.LastFailedLoginDate;
        //    newuser.AccountLocked = user.AccountLocked;
        //    return newuser;
        //}


        public tblUserInfo ReadRecord(long ID)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserInfoes
                            where n.UserID == ID
                            select n;
                if (query != null && query.Count() > 0)
                {
                    //return TranslatetblUserInfoTotblUserInfo(query.First());
                    return query.First();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public bool AddUpdateRecord(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord(rec.UserID);
                if (existingRec == null) //new record
                {
                    return InsertRecord(rec);
                }
                else //found existing, update
                {
                    return UpdateRecord(rec);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertRecord(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord(rec.UserID);

                if (existingRec == null)
                {
                    if (checkLoginName(rec) == false)  //if check fails, then username already taken.
                    {
                        return false;
                    }
                    rec.AccountCreationDate = DateTime.Now;
                    rec.LastUpdatedDate = DateTime.Now;
                    rec.FailedLoginAttempts = 0;
                    rec.LastFailedLoginDate = DateTime.Now;

                    context.tblUserInfoes.Add(rec);
                    context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   

        public bool DeleteRecord(long ID)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord(ID);
                if (existingRec != null) //there is a record
                {


                    context.tblUserInfoes.Remove(existingRec);
                    context.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public bool UpdateRecord(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord(rec.UserID);
                if (existingRec != null)
                {
                    rec.AccountCreationDate = DateTime.Now;
                    rec.LastUpdatedDate = DateTime.Now;
                    rec.FailedLoginAttempts = 0;
                    rec.LastFailedLoginDate = DateTime.Now;
                    Serializer.Clone<tblUserInfo>(rec, existingRec);
                    context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblUserInfo ValidateLogin(string username, string password)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserInfoes
                            where n.UserName == username
                            where n.Password == password
                            select n;
                if (query != null && query.Count() > 0)
                {
                    //return TranslatetblUserInfoTotblUserInfo(query.First());
                    return query.First();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool checkLoginName(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserInfoes
                            select n;
                foreach (var users in query)
                {
                    if (users.UserName == rec.UserName)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}