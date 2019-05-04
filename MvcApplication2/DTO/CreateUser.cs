using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.DTO
{
    public class CreateUser
    {
        public CreateUser(string name, string sex)
        {
            this.Name = name;
            this.Sex = sex;
        }

        public string Name
        {
            get;
            private set;
        }
        public string Sex
        {
            get;
            private set;
        }

        public string FullName
        {
            get
            {
                return Name + "_" + Sex;
            }
        }
        public void ValidName()
        {
            if (this.Name == "非法")
            {
                throw new ArgumentException("你的姓名是非法字");
            }
        }
    }
}