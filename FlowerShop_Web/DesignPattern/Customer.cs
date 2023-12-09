using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class Customer : IObserver
    {
        public string id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string messages { get; set; }
        public Customer() { }

        public Customer(string lastName,string firstname, string messages, string id)
        {
            this.lastName = lastName;
            this.messages = messages;
            this.firstName = firstname;
            this.id = id;
        }
        public void Update(string mess)
        {
            messages = mess  ;
        }

        public string getMess()
        {
            return messages;
        }
    }
}
