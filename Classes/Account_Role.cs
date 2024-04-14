using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Account_Role
    {
        private int account_id;
        private int role_id;

        public Account_Role()
        {
            account_id = 0; 
            role_id = 0;
        }

        public void SetAccountRole(int accountid)
        {
            account_id = accountid;
        }

        public int GetAccountRole()
        {
            return account_id;
        }

        public void SetRoleId( int roleid)
        {
            role_id = roleid;
        }

        public int GetRoleId()
        { 
            return role_id;
        }

        public void PrintAccountRole()
        {
            Console.WriteLine(GetAccountRole());
            Console.WriteLine(GetRoleId());
        }
    }
}
