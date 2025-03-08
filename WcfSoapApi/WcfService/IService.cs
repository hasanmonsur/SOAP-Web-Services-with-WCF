using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string GetMessage(string name);

        [OperationContract]
        UserData GetUserData(string name);
    }

    // Shared between service and client (ideally in a shared library)
    [DataContract(Namespace = "http://example.com/userdata")]
    public class UserData
    {
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public string Email { get; set; }
    }


}
