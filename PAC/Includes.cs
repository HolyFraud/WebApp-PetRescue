using System;
using System.Web;

namespace PAC
{
    public class Includes
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        

        public enum UserPermission
        {
            
            AddUsers = 1,
            EditUsers = 2,
            DeleteUsers = 4,
            AddAds = 8,
            EditAds = 16,
            DeleteAds = 32
        }

        public bool CanAddUsers(int securitymask)
        {
            int AuthNum = (int)UserPermission.AddUsers;
            if ((securitymask & AuthNum) == 0)
            {
                return false;
            }
            return true;
        }
        
        public bool CanEditUsers(int securitymask)
        {
            int AuthNum = (int)UserPermission.EditUsers;
            if ((securitymask & AuthNum) == 0)
            {
                return false;
            }
            return true;
        }

        public bool CanDeleteUsers(int securitymask)
        {
            int AuthNum = (int)UserPermission.DeleteUsers;
            if ((securitymask & AuthNum) == 0)
            {
                return false;
            }
            return true;
        }

        public bool CanAddAds(int sercuritymask)
        {
            int AuthNum = (int)UserPermission.AddAds;
            if ((sercuritymask & AuthNum) == 0)
            {
                return false;
            }
            return true;
        }

        public bool CanEditAds(int securitymask)
        {
            int AuthNum = (int)UserPermission.EditAds;
            if ((securitymask & AuthNum) == 0)
            {
                return false;
            }
            return true;
        }

        public bool CanDeleteAds(int securitymask)
        {
            int AuthNum = (int)UserPermission.DeleteAds;
            if ((securitymask & AuthNum) == 0)
            {
                return false;
            }
            return true;
        }
    }
}

