using System.Data.SqlClient;

namespace Betawave.Classes
{
    namespace BetaWaveMultiplatform.Classes
    {
        public interface IDatabaseAccess
        {
            // Establishes a connection to the database using the specified username and password.
            // Returns the established SQL connection.
            SqlConnection Connect(string username, string password);

            // Returns the connection string used to connect to the database.
            string GetConnection();

            // Executes a query provided by the user and performs some operation with the results.
            // The specifics of the operation (e.g., printing results, storing in a list) need to be defined in the implementation.
            void Query(string userinputQuery);

            // Adds data to the database based on some user input.
            // The specifics of what data is added and where it's added need to be defined in the implementation.
            void AddData(string userInput);

            // Deletes data from the database based on some user input.
            // The specifics of what data is deleted and from where need to be defined in the implementation.
            void DeleteData(string userInput);

            // Prints details about the database connection.
            // The specifics of what details are printed need to be defined in the implementation.
            void PrintConnectionDetails();
        }
    }


}
