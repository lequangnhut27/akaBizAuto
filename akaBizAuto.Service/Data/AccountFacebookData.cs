using akaBizAuto.Service.Constants;
using akaBizAuto.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Data
{
    public static class AccountFacebookData
    {
        public static List<AccountFacebookView> GetAccFbs()
        {
            List<AccountFacebookView> accs = new List<AccountFacebookView>();
            var acc1 = new AccountFacebookView()
            {
                ShopId = 1111,
                Username = "kjuen11@gmail.com",
                Password = "elkjuen@11",
            };
            var acc2 = new AccountFacebookView()
            {
                ShopId = 2222,
                Username = "kjuen12@gmail.com",
                Password = "elkjuen@1222222222",
            };
            var acc3 = new AccountFacebookView()
            {
                ShopId = 3333,
                Username = "01678318757",
                Password = "lequangnhut",
            };
            accs.Add(acc1);
            accs.Add(acc2);
            accs.Add(acc3);
            return accs;
        }

        public static List<InteractFacebookView> GetInteracts()
        {
            List<InteractFacebookView> interacts = new List<InteractFacebookView>();
            InteractFacebookView interact1 = new InteractFacebookView()
            {
                ShopId = 3333,
                Status = ProcessStatusConstant.WAITING,
                Action = ActionConstant.ADDFRIEND,
                Detail = GetCustomers(),
                Schedule = DateTime.Now.Date,
                TimeDelay = 3000,
            };

            InteractFacebookView interact2 = new InteractFacebookView()
            {
                ShopId = 3333,
                Status = ProcessStatusConstant.WAITING,
                Action = ActionConstant.SENDMESSAGE,
                Content = "Hello",
                Image = @"C:\Users\Nitrogen\Pictures\Screenshots\Screenshot (1).png",
                Detail = GetCustomers(),
                Schedule = DateTime.Now.Date,
                TimeDelay = 3000
            };

            InteractFacebookView interact3 = new InteractFacebookView()
            {
                ShopId = 3333,
                Status = ProcessStatusConstant.WAITING,
                Action = ActionConstant.COMMENT,
                Content = "Hello",
                Image = @"C:\Users\Nitrogen\Pictures\Screenshots\Screenshot (1).png",
                Detail = GetCustomers(),
                Schedule = DateTime.Now.Date,
                TimeDelay = 3000,
                CountPost = 2,
                Type = TypeConstant.PROFILE
            };

            InteractFacebookView interact4 = new InteractFacebookView()
            {
                ShopId = 3333,
                Status = ProcessStatusConstant.WAITING,
                Action = ActionConstant.COMMENT,
                Content = "Hello",
                Image = @"C:\Users\Nitrogen\Pictures\Screenshots\Screenshot (1).png",
                Detail = GetPages(),
                Schedule = DateTime.Now.Date,
                TimeDelay = 3000,
                CountPost = 2,
                Type = TypeConstant.PROFILE
            };
            interacts.Add(interact1);
            interacts.Add(interact2);
            //interacts.Add(interact3);
            interacts.Add(interact4);
            return interacts;
        }

        public static List<CustomerView> GetCustomers()
        {
            List<CustomerView> customers = new List<CustomerView>();
            CustomerView cus1 = new CustomerView()
            {
                Uid = "100006488944710",
                Status = 1
            };
            CustomerView cus2 = new CustomerView()
            {
                Uid = "100027318624242",
                Status = 1
            };
            CustomerView cus3 = new CustomerView()
            {
                Uid = "100009837120518",
                Status = 1
            };

            CustomerView cus4 = new CustomerView()
            {
                Uid = "100019026620756",
                Status = 1
            };
            CustomerView cus5 = new CustomerView()
            {
                Uid = "100054517342715",
                Status = 1
            };
            customers.Add(cus1);
            customers.Add(cus2);
            customers.Add(cus3);
            customers.Add(cus4);
            customers.Add(cus5);

            return customers;
        }

        public static List<CustomerView> GetPages()
        {
            List<CustomerView> customers = new List<CustomerView>();
            CustomerView cus1 = new CustomerView()
            {
                Uid = "182531825472288",
                Status = 1
            };
            CustomerView cus2 = new CustomerView()
            {
                Uid = "100044302182296",
                Status = 1
            };
            //customers.Add(cus1);
            customers.Add(cus2);

            return customers;
        }
    }
}
