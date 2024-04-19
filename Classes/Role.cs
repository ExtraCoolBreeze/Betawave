using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Role
    {
        private int roleId;
        private int admin;

        public Role()
        {
            admin = 0;
            roleId = 0;
        }

        public void SetRoleId(int data)
        {
            roleId = data;
        }

        public int GetRoleId()
        {
            return roleId;
        }

        public void SetAdmin(int isAdmin)
        {
            admin = isAdmin;
        }

        public int GetAdmin()
        {
            return admin;
        }

        public void PrintRole()
        {
            Console.WriteLine("Role ID: " + GetRoleId());
            Console.WriteLine("Admin: " + GetAdmin());
        }
    }
}
