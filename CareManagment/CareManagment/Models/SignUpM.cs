﻿using CareManagment.BL;
using CareManagment.BL.Interfaces;
using CareManagment.DP;
using CareManagment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


        public void SignUp(IUser user, UserType userType)
        {
            if (new VerifyString().ArePropertiesNull(user))
                throw new Exception("אנא מלא את כל השדות");
            if (BL.GetAllAdmins(x => x.MailAddress == user.GetMailAddress()).Count != 0 || BL.GetAllVolunteers(x => x.MailAddress == user.GetMailAddress()).Count != 0)
                throw new Exception("המשתמש כבר קיים במערכת");
            if (!new VerifyAddress().IsValidAddress(user.GetAddress()))
                throw new Exception("הכתובת שהזנת לא קיימת");
            double[] location = new Tools.VerifyAddress().GetLocation(user.GetAddress());
            user.SetAddress(location[0], location[1]);
            if (userType == UserType.Admin)
                BL.AddAdmin(user as Admin);
            else BL.AddVolunteer(user as Volunteer);

            // send mail
            string to = user.GetMailAddress();
            string subject = "ברוך הבא לעמותת יד ביד";
            string body = "שלום " + user.GetFirstName() + "," + "\n\n" + "הרשמתך נקלטה במערכת בהצלחה." + "\n\n" + "אנו שמחים שהצטרפת למשפחת יד ביד!" + "\n\n" + "הסיסמה למערכת היא: " + user.GetPassword();
            Thread thread = new Thread(() => new MailSender().SendMail(to, subject, body));
            thread.Start();
        }
       
    }
}
