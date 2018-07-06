using Ivy.Data.Core.Interfaces.Init;
using System.Collections.Generic;

namespace Ivy.Migration.Core.Interfaces.Services
{
    public interface IDatabaseMigrationService : IInitializeByConnectionString
    {
        /// <summary>
        /// Create the Test User on the server for creating databases
        /// </summary>
        /// <param name="tc">Optional TranConn to preven transactional integrity</param>
        void CreateUser();

        /// <summary>
        /// Use this function to create a database with the above TestUser
        /// </summary>
        /// <param name="dbName">Name of the database you wish to create</param>
        /// <param name="absoluteFilePath">The absolute path to the migration scripts directory</param>
        /// <param name="fileSort">Optional comparable interface to sort our scripts</param>
        /// <param name="tc">Optional TranConn to preven transactional integrity</param>
        /// <returns>Connection string to attach to the created database</returns>
        string CreateDatabase(string dbName, string absoluteFilePath, 
            IComparer<string> fileSort = null);

        /// <summary>
        /// Use this function to delete a database with the above TestUser
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tc"></param>
        void CleanDatabase(string dbName);

        /// <summary>
        /// Remove the Test User from the server
        /// </summary>
        /// <param name="tc">Optional TranConn to preven transactional integrity</param>
        void CleanUser();
    }
}
