
namespace HotalAppLibrary.Databases
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sql, U parameters, string connectionStringName, bool isStoredProcedure = false);
        void SaveData<T>(string sqlStatement, T dataparameters, string connectionStringName, bool isStoredProcedure = false);
    }
}