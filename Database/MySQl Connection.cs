using System.Data;
using MySql.Data.MySqlClient;

//This namespace is used to handle database connections and calls
namespace MainFunction.Database
{
	public class DbConnection
	{
		public int IntDump;
		private Dictionary <int, string> _databaseCalls = new Dictionary<int, string>() {
			{1, $"SELECT * FROM spells" }
		};
		private DataTable ConnectToDatabase(string requestedDatabase)
		{
			DataTable ReValue = new();
			string Connstring = $"server = localhost; uid = root; pwd = VeryLazyPAssword; database = {requestedDatabase}; Persist Security Info = True;";

			using MySqlConnection Conn = new MySqlConnection(Connstring);
			Conn.Open();
			string Cmd = _databaseCalls[1];

			using MySqlCommand Comand = new MySqlCommand(Cmd, Conn);
			using MySqlDataReader  Reader = Comand.ExecuteReader();
			ReValue.Load(Reader);

			return ReValue;
		}

		public DataTable Dboutput(string requestedDatbase)
		{
			DbConnection Connection = new DbConnection();
			DataTable DbReturnValue = Connection.ConnectToDatabase(requestedDatbase);

			try
			{
				if (DbReturnValue == null)
				{
					throw new Exception("null request"); 
				}

				else
				{
					return DbReturnValue;
				}
			}

			catch
			{
				throw new Exception("null request");
			}

		}
	}
}
