using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Constants
{

    public static class ActionConstant
    {
        public enum Status
        {
            Default = 0,
            Waiting = 1,
            Success = 2,
            Fail = 3,
            NotPermission = 10
        };
        public const string ADDFRIEND = "addfriend";
        public const string COMMENT = "comment";
        public const string LIKE = "like";
        public const string HEART = "heart";
        public const string SENDMESSAGE = "sendmessage";
    }
}
