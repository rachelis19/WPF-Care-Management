﻿using CareManagment.BL;
using CareManagment.BL.Interfaces;
using CareManagment.DP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareManagment.Models
{
    public class SignUpM
    {
        public IBL BL { get; set; }

        public SignUpM()
        {
            BL = new BLImp();
        }

        public void SignUp(User user)
        {
            if (BL.GetAllPersons(x => x.PersonId == user.PersonId || x.MailAddress==user.MailAddress).Count != 0)
                throw new Exception("המשתמש כבר קיים במערכת");
            if (new Tools.VerifyAddress().IsValidAddress())
                throw new Exception("הכתובת שהזנת לא קיימת");
            BL.AddPerson(user);
        }
    }
}
