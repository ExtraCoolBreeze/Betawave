using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Role
    {
        private int role_id;
        private int admin;

        public Role()
        {
            admin = 0;
            role_id = 0;
        }

        public void SetRoleId(int role)
        {
            role_id = role;
            role_id++;
        }

        public int GetRoleId()
        {
            return role_id;
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
