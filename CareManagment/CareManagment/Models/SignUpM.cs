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
            if (new Tools.VerifyString().ArePropertiesNull<User>(user))
                throw new Exception("אנא מלא את כל השדות");
            if (BL.GetAllUsers(x => x.MailAddress == user.MailAddress).Count != 0)
                throw new Exception("המשתמש כבר קיים במערכת");
            if (!new Tools.VerifyAddress().IsValidAddress(user.Address))
                throw new Exception("הכתובת שהזנת לא קיימת");
            double[] location = new Tools.VerifyAddress().GetLocation(user.Address);
            user.Address.Lat = location[0];
            user.Address.Lon = location[1];
            BL.AddPerson(user);
        }
        
    }
}
