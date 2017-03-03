using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MentorJWcfService.Utilities;
using System.Collections;

namespace MentorJWcfService
{
    public partial class tblUserInfo
    {
       
        
        public static tblUserInfo ReadRecord(MentorJEntities db, int ID)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                var query = from p in context.tblUserInfoes
                            where p.UserID == ID
                            select p;
                if (query != null && query.Count() > 0)
                {
                    return query.First();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool AddUpdateRecord(MentorJEntities db, tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                tblUserInfo existingRec = ReadRecord(db, rec.UserID);
                if (existingRec == null) //new record
                    {
                        return InsertRecord(context, rec);
                    }
                    else //found existing, update
                    {
                        return UpdateRecord(context, rec);
                    }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool DeleteRecord(MentorJEntities db, long ID)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                tblUserInfo existingRec = ReadRecord(context, ID);
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

        public static bool InsertRecord(MentorJEntities db, tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                tblUserInfo existingRec = ReadRecord(context, rec.UserID);

                if (existingRec == null)
                {
                    if ( checkLoginName(context, rec) == false)  //if check fails, then username already taken.
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

        public static bool UpdateRecord(MentorJEntities db, tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                tblUserInfo existingRec = ReadRecord(context, rec.UserID);
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

        public static tblUserInfo ReadRecord(MentorJEntities db, long ID)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                var query = from p in context.tblUserInfoes
                            where p.UserID == ID
                            select p;
                if (query != null && query.Count() > 0)
                {
                    return query.First();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static tblUserInfo ValidateLogin(MentorJEntities db, string username, string password)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
                var query = from n in context.tblUserInfoes
                            where n.UserName == username
                            where n.Password == password
                            select n;
                if (query != null && query.Count() > 0)
                {
                    return query.First();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool checkLoginName(MentorJEntities db, tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = (db == null ? new MentorJEntities() : db);
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