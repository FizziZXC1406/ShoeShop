using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFinalExam
{
    public class UserSession
    {
        private static UserSession instance;
        private int currentUserID;

        // Private constructor to prevent instantiation
        private UserSession() { }

        // Public static method to get the single instance of the class
        public static UserSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSession();
                }
                return instance;
            }
        }

        public int CurrentUserID
        {
            get { return currentUserID; }
            set { currentUserID = value; }
        }
    }
}
