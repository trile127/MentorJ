using MentorJWcfService;
using MentorJWcfService.Utilities;
using System;
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
    public interface IMentorJService
    {

        [OperationContract]
        tblUserInfo ReadRecord(long ID);
        [OperationContract]
        bool AddUpdateRecord(tblUserInfo rec);
        [OperationContract]
        bool InsertRecord(tblUserInfo rec);
        [OperationContract]
        bool DeleteRecord(long ID);
        [OperationContract]
        bool UpdateRecord(tblUserInfo rec);
        [OperationContract]
        tblUserInfo ValidateLogin(string username, string password);
        [OperationContract]
        bool isUserNameTaken(tblUserInfo rec);
        [OperationContract]
        bool isEmailTaken(tblUserInfo rec);
        [OperationContract]
        long assignUserID();
    }
}