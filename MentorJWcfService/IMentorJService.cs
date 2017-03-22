using MentorJWcfService;
using MentorJWcfService.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//using MentorJWcfService.DataBaseEntitiesSupplement;


namespace MentorJWcfService
{

    [ServiceContract]
    public interface IMentorJInfoService
    {

        [OperationContract]
        tblUserInfo ReadRecord_UserInfo(long ID);
        [OperationContract]
        bool AddUpdateRecord_UserInfo(tblUserInfo rec);
        [OperationContract]
        bool InsertRecord_UserInfo(tblUserInfo rec);
        [OperationContract]
        bool DeleteRecord_UserInfo(long ID);
        [OperationContract]
        bool UpdateRecord_UserInfo(tblUserInfo rec);
        [OperationContract]
        tblUserInfo ValidateLogin_UserInfo(string username, string password);
        [OperationContract]
        bool isUserNameTaken_UserInfo(tblUserInfo rec);
        [OperationContract]
        bool isEmailTaken_UserInfo(tblUserInfo rec);
        [OperationContract]
        long assignUserID_UserInfo();
    }

    [ServiceContract]
    public interface IMentorJProfileService
    {
        [OperationContract]
        tblUserProfile ReadRecord_UserProfile(long ID);
        [OperationContract]
        bool AddUpdateRecord_UserProfile(tblUserProfile rec);
        [OperationContract]
        bool InsertRecord_UserProfile(tblUserProfile rec);
        [OperationContract]
        bool DeleteRecord_UserProfile(long ID);
        [OperationContract]
        bool UpdateRecord_UserProfile(tblUserProfile rec);
        [OperationContract]
        ArrayList getUserProfiles(string username);
    }
}