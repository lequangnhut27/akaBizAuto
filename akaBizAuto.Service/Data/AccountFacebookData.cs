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
                Username = "kjuen11@gmail.com",
                Password = "elkjuen@11",
            };
            var acc2 = new AccountFacebookView()
            {
                Username = "kjuen12@gmail.com",
                Password = "elkjuen@12",
            };
            var acc3 = new AccountFacebookView()
            {
                Username = "01678318757",
                Password = "lequangnhut",
            };
            accs.Add(acc1);
            accs.Add(acc2);
            accs.Add(acc3);
            return accs;
        }

        public static InteractFacebookView GetInteract()
        {
            InteractFacebookView interact = new InteractFacebookView()
            {
                Status = "waiting",
                Action = "addfriend",
                Detail = GetCustomers(),
            };


            return interact;
        }

        public static List<CustomerView> GetCustomers()
        {
            List<CustomerView> customers = new List<CustomerView>();
            CustomerView cus1 = new CustomerView()
            {
                Uid = "100006488944710",
                Status = 0
            };
            CustomerView cus2 = new CustomerView()
            {
                Uid = "100027318624242",
                Status = 0
            };
            CustomerView cus3 = new CustomerView()
            {
                Uid = "100009837120518",
                Status = 0
            };

            CustomerView cus4 = new CustomerView()
            {
                Uid = "100054517342715",
                Status = 0
            };
            CustomerView cus5 = new CustomerView()
            {
                Uid = "100019026620756",
                Status = 0
            };
            customers.Add(cus1);
            customers.Add(cus2);
            customers.Add(cus3);
            customers.Add(cus4);
            customers.Add(cus5);

            return customers;
        }
    }
}
