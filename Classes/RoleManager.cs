/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: The role manager class was designed to load in a store the role information from the database, it was going to be used for checks and to verify user authority. 
It was created but again never full implemented due to cuts and time constraints  */

/*
using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    public class RoleManager
    {
        private List<Role> roles = new List<Role>();
        private DatabaseAccess dbAccess;

        public RoleManager(DatabaseAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        public async Task LoadRolesAsync()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT role_id, admin FROM role", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var role = new Role();
                        role.SetRoleId(reader.GetInt32("role_id"));
                        role.SetAdmin(reader.GetInt32("admin"));
                        roles.Add(role);
                    }
                }
            }
        }

        public void AddRole(Role role)
        {
            roles.Add(role);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO role (admin) VALUES (@Admin)", connection);
                command.Parameters.AddWithValue("@Admin", role.GetAdmin());
                command.ExecuteNonQuery();
            }
        }

        public void UpdateRole(Role role)
        {
            for (int i = 0; i < roles.Count; i++)
            {
                if (roles[i].GetRoleId() == role.GetRoleId())
                {
                    roles[i].SetAdmin(role.GetAdmin());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE role SET admin = @Admin WHERE role_id = @RoleId", connection);
                command.Parameters.AddWithValue("@RoleId", role.GetRoleId());
                command.Parameters.AddWithValue("@Admin", role.GetAdmin());
                command.ExecuteNonQuery();
            }
        }

        public void DeleteRole(int roleId)
        {
            for (int i = roles.Count - 1; i >= 0; i--)
            {
                if (roles[i].GetRoleId() == roleId)
                {
                    roles.RemoveAt(i);
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("DELETE FROM role WHERE role_id = @RoleId", connection);
                command.Parameters.AddWithValue("@RoleId", roleId);
                command.ExecuteNonQuery();
            }
        }

        public List<Role> GetAllRoles()
        {
            return roles;
        }

        public Role GetRoleById(int roleId)
        {
            for (int i = 0; i < roles.Count; i++)
            {
                if (roles[i].GetRoleId() == roleId)
                {
                    return roles[i];
                }
            }
            return null; // Return null if no role is found
        }
    }
}
*/