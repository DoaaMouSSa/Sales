using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
    [DataContract]
    public class Response<T>
    {
        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public new T payload { get; set; }
 
        [DataMember]
        public bool IsSuccess { get; set; }
    }
}
