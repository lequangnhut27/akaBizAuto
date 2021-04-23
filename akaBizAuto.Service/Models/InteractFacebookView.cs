using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Models
{
    public class InteractFacebookView
    {
        public string name { get; set; }
        public int Shopid { get; set; }       //int - - select, tài khoản thực hiện, AccountFacebook Id
        public DateTime Schedule { get; set; }     //Datetime, thời gian thực hiện
        public string Type { get; set; }     //string - select, profile/page/group
        public string Action { get; set; }       //string - select, addfriend/comment/like/heart/sendmessage
        public string Content { get; set; }
        public string Image { get; set; }
        public int CountPost { get; set; }    //int, số lượng bài post muốn tương tác tính từ bài đầu tiên
        public int TimeDelay { get; set; }    //int, thời gian tam dừng trước khi thực hiện hành động, thời gian tạm dừng giữa 2 hành động
        public string Status { get; set; }       //string - select, trạng thái thực hiện: waiting = Chờ xử lý, progressing = Đang xử lý,  finish = Hoàn thành, stop = Dừng
        public List<CustomerView> Detail { get; set; }
    }
}
