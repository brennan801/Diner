using Dapper;
using JCsDiner;
using Npgsql;
using System.Collections.Generic;

namespace DinerWebApp.Services
{
    public class DbService : IDbService
    {
        public DbService()
        {
            var connection = new NpgsqlConnection("User ID=postgres;Password=password;Host=pgsql_db;Port=5432;Database=DinerWebDb;");
            using (connection)
            {
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS SimulationRuns(" +
                    "id SERIAL PRIMARY KEY," +
                    "name VARCHAR(64)," +
                    "runtime INTEGER," +
                    "numberOfCustomers INTEGER," +
                    "numberOfWaiters INTEGER," +
                    "numberOfCooks INTEGER," +
                    "numberOfTables INTEGER," +
                    "setAveragePartySize INTEGER," +
                    "actualAveragePartySize INTEGER);"
                    );
            }
        }
        public void addRun(SimulatorResults results)
        {
            var connection = new NpgsqlConnection("User ID=postgres;Password=password;Host=pgsql_db;Port=5432;Database=DinerWebDb;");
            using (connection)
            {
                connection.Execute(
                    "INSERT INTO SimulationRuns (" +
                    "name, runtime, numberOfCustomers, numberOfWaiters, numberOfCooks, numberOfTables, setAveragePartySize, actualAveragePartySize)" +
                    "VALUES(" +
                    "@Name, @Runtime, @NumberOfCustomers, @NumberOfWaiters, @NumberOfCooks, @NumberOfTables, @SetAveragePartySize, @ActualAveragePartySize);",
                    results);
            }
        }

        public List<SimulatorResults> GetPreviousRuns()
        {
            var connection = new NpgsqlConnection("User ID=postgres;Password=password;Host=pgsql_db;Port=5432;Database=DinerWebDb;");
            using (connection)
            {
                var prevRuns = connection.Query<SimulatorResults>(
                    "SELECT name, runtime, numberOfCustomers, numberOfWaiters, numberOfCooks, numberOfTables, setAveragePartySize, actualAveragePartySize FROM SimulationRuns"
                    ).AsList();
                return prevRuns;
            }
        }
    }
}
