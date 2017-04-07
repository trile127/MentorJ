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
using System.Collections;
//using MentorJWcfService.DataBaseEntitiesSupplement;

namespace MentorJWcfService
{


    public partial class MentorJService : IMentorJInfoService
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

        public tblUserInfo ReadRecord_UserInfo(long ID)
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






        public bool AddUpdateRecord_UserInfo(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord_UserInfo(rec.UserID);
                if (existingRec == null) //new record
                {
                    return InsertRecord_UserInfo(rec);
                }
                else //found existing, update
                {
                    return UpdateRecord_UserInfo(rec);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertRecord_UserInfo(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord_UserInfo(rec.UserID);

                if (existingRec == null)
                {
                    if (isUserNameTaken_UserInfo(rec) == true)  //if check fails, then username already taken.
                    {
                        return false;
                    }
                    if (isEmailTaken_UserInfo(rec) == true)  //if check fails, then username already taken.
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



        public bool DeleteRecord_UserInfo(long ID)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord_UserInfo(ID);
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



        public bool UpdateRecord_UserInfo(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserInfo existingRec = ReadRecord_UserInfo(rec.UserID);
                if (existingRec != null)
                {

                    rec.LastUpdatedDate = DateTime.Now;
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

        public tblUserInfo ValidateLogin_UserInfo(string email, string password)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserInfoes
                            where n.Email == email
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

        public bool isUserNameTaken_UserInfo(tblUserInfo rec)
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
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool isEmailTaken_UserInfo(tblUserInfo rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserInfoes
                            select n;
                foreach (var users in query)
                {
                    if (users.Email == rec.Email)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public long assignUserID_UserInfo()
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var item = context.tblUserInfoes.OrderByDescending(i => i.UserID).FirstOrDefault();
                if (item != null)
                {
                    return item.UserID + 1;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}

namespace MentorJWcfService
{
    public partial class MentorJService : IMentorJProfileService
    {
        public tblUserProfile ReadRecord_UserProfile(long ID)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserProfiles
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


        public bool AddUpdateRecord_UserProfile(tblUserProfile rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserProfile existingRec = ReadRecord_UserProfile(rec.UserID);
                if (existingRec == null) //new record
                {
                    return InsertRecord_UserProfile(rec);
                }
                else //found existing, update
                {
                    return UpdateRecord_UserProfile(rec);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertRecord_UserProfile(tblUserProfile rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserProfile existingRec = ReadRecord_UserProfile(rec.UserID);

                if (existingRec == null)
                {

                    //generate profile URL;
                    //if (isUserNameTaken(rec) == true)  //if check fails, then username already taken.
                    //{
                    //    return false;
                    //}
                    //if (isEmailTaken(rec) == true)  //if check fails, then username already taken.
                    //{
                    //    return false;
                    //}
                    rec.Modified = DateTime.Now;
                    context.tblUserProfiles.Add(rec);
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



        public bool DeleteRecord_UserProfile(long ID)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserProfile existingRec = ReadRecord_UserProfile(ID);
                if (existingRec != null) //there is a record
                {
                    context.tblUserProfiles.Remove(existingRec);
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



        public bool UpdateRecord_UserProfile(tblUserProfile rec)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                tblUserProfile existingRec = ReadRecord_UserProfile(rec.UserID);
                if (existingRec != null)
                {
                    rec.Modified = DateTime.Now;
                    Serializer.Clone<tblUserProfile>(rec, existingRec);
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

        public ArrayList getUserProfiles(string username)
        {
            try
            {
                MentorJEntities context = new MentorJEntities();
                var query = from n in context.tblUserInfoes
                            where n.UserName.Contains(username)
                            orderby n.UserName.IndexOf(username),
                                        n.UserName.Length ascending
                            select n.UserID;
                ArrayList list = new ArrayList();
                int count = 0;
                foreach (var id in query)
                {
                    count++;
                    list.Add(id);
                    if (count > 100)
                        break;
                }
                return list;
                //return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

        public partial class MentorJService : IMentorJFriendService
        {
            public tblFriend ReadRecord_Friend(long userID, long friendID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    var query = from n in context.tblFriends
                                where n.UserID == userID
                                where n.FriendID == friendID
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


            public bool AddUpdateRecord_Friend(long userID, long friendID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    tblFriend existingRec = ReadRecord_Friend(userID, friendID);
                    if (existingRec == null) //new record
                    {
                        return InsertRecord_Friend(userID, friendID);
                    }
                    return false;
                    //else //found existing, update
                    //{
                    //    return UpdateRecord_Friend(userID, friendID);
                    //}

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            public bool InsertRecord_Friend(long userID, long friendID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    tblFriend existingRec = ReadRecord_Friend(userID, friendID);

                    if (existingRec == null)
                    {
                        context.tblFriends.Add(existingRec);
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



            public bool DeleteRecord_Friend(long userID, long friendID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    tblFriend existingRec = ReadRecord_Friend(userID, friendID);
                    if (existingRec != null) //there is a record
                    {
                        context.tblFriends.Remove(existingRec);
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



            //public bool UpdateRecord_Friend(long userID, long friendID)
            //{
            //    try
            //    {
            //        MentorJEntities context = new MentorJEntities();
            //        tblFriend existingRec = ReadRecord_Friend(userID, friendID);
            //        if (existingRec != null)
            //        {
            //            //Serializer.Clone<tblUserProfile>(rec, existingRec);
            //            //context.SaveChangesAsync();
            //            return true;
            //        }
            //        return false;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            public ArrayList getFriends(long userID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    var query = from n in context.tblFriends
                                where n.UserID == userID
                                select n.FriendID;
                    ArrayList list = new ArrayList();
                    int count = 0;
                    foreach (var id in query)
                    {
                        count++;
                        list.Add(id);
                        if (count > 1000)
                            break;
                    }
                    return list;
                    //return query;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public tblUserProfile getFriendProfile(long friendID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    var query = from n in context.tblUserProfiles
                                where n.UserID == friendID
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

            public ArrayList getFriendsProfile(long userID)
            {
                try
                {
                    MentorJEntities context = new MentorJEntities();
                    var query = from n in context.tblFriends
                                where n.UserID == userID
                                select n.FriendID;
                    if (query != null && query.Count() > 0)
                    {
                        ArrayList IDlist = new ArrayList();
                        int count = 0;
                        foreach (var id in query)
                        {
                            count++;
                            IDlist.Add(id);
                            if (count > 1000)
                                break;
                        }

                        var query2 = from n in context.tblUserProfiles
                                     where IDlist.Contains(n.UserID)
                                     select n;
                        if (query != null && query.Count() > 0)
                        {
                            ArrayList profileList = new ArrayList();
                            count = 0;
                            foreach (var profile in query2)
                            {
                                count++;
                                profileList.Add(profile);
                                if (count > 1000)
                                    break;
                            }
                            return profileList;
                        }
                        return null;
                    }

                    return null;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }


    
}