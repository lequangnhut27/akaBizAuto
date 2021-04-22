using DevExpress.Xpo;
using System;

namespace akaBizAuto.Service.Models
{
    public class SendMessageView
    {
        public string Name { get; set; }    //string, tên khách hàng cần gửi
	    public string Content { get; set; }  //sttring, nội dung gửi
        public string Image { get; set; }   //hình ảnh gửi
	    public int TimeDelay { get; set; }   //intt, thời gian deley trước khi gửi
    }
}