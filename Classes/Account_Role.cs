﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Account_Role
    {
        private int fkaccount_id;
        private int fkrole_id;

        public Account_Role()
        {
            fkaccount_id = 0;
            fkrole_id = 0;
        }

        public void SetAccountId(int accountid)
        {
            fkaccount_id = accountid;
        }

        public int GetAccountId()
        {
            return fkaccount_id;
        }

        public void SetRoleId( int roleid)
        {
            fkrole_id = roleid;
        }

        public int GetRoleId()
        { 
            return fkrole_id;
        }

        public void PrintAccountRole()
        {
            Console.WriteLine(GetAccountId());
            Console.WriteLine(GetRoleId());
        }
    }
}
