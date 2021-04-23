using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Constants
{
    public static class VarConstant
    {
        public static class Status
        {
            public static string Waiting = "Chờ xử lý";
            public static string Progressing = "Đang xử lý";
            public static string Finish = "Hoàn thành";
            public static string Stop = "Dừng";
            public static string Loggedin = "Đã đăng nhập";
            public static string NotLogin = "Chưa đăng nhập";
        }

        public static class Action
        {
            public static string AddFriend = "addfriend";
            public static string Comment = "comment";
            public static string Like = "like";
            public static string Heart = "heart";
            public static string SendMessage = "sendmessage";

        }
    }
}
