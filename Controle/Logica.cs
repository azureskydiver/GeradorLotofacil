using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle
{
    public class Logica
    {
        public static string path = Directory.GetCurrentDirectory() + "\\ResultadoDB.sqlite";
        private static SQLiteConnection sqliteConnection;

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + path);
            sqliteConnection.Open();
            return sqliteConnection;
        }
        private static SQLiteConnection DbConnection2()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + path);
            sqliteConnection.Close();
            return sqliteConnection;
        }
        public static void CriarBancoSQLite()
        {
            try
            {
                if (File.Exists(path) == false)
                {
                    SQLiteConnection.CreateFile(path);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabelaSQLite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TBResultado(Concurso Varchar(5),_01 Varchar(2),_02 Varchar(2),_03 Varchar(2),_04 Varchar(2),_05 Varchar(2), _06 Varchar(2)," +
                        "_07 Varchar(2),_08 Varchar(2),_09 Varchar(2),_10 Varchar(2),_11 Varchar(2), _12 Varchar(2),_13 Varchar(2),_14 Varchar(2), _15 Varchar(2))";
                    
                    cmd.ExecuteNonQuery();
                    DbConnection().Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetResultados()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TBResultado ORDER BY Concurso DESC";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetResultados(string concurso)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TBResultado where Concurso like '%" + concurso + "%'";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        /* I tried to do the search in the form frmSalvaResultados but I didn't get a result in filling the array*/
        //public static DataTable GetBuscar(int concurso)
        //{
        //    SQLiteDataAdapter da = null;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        using (var cmd = DbConnection().CreateCommand())
        //        {
        //            cmd.CommandText = "SELECT _01,_02,_03,_04,_05,_06,_07,_08,_09,_10,_11,_12,_13,_14,_15 FROM TBResultado Where Concurso=" + concurso;
        //            da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
        //            da.Fill(dt);
        //            return dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static bool VerificarExistencia(int concurso) //Check if the Contest already exists
        {
            bool ConcursoEncontrado = false;

            DataTable Tabela;
            SQLiteCommand cmd = new SQLiteCommand();
            
            using (cmd = DbConnection().CreateCommand())
            {
                Tabela = new DataTable("TBResultado");
                cmd.CommandText = "select * from TBResultado where concurso = @Concurso";
                cmd.Parameters.AddWithValue("@Concurso", concurso);
                SQLiteDataReader dr;

                try
                {
                    dr = cmd.ExecuteReader();
                    DbConnection().Close();
                    if (dr.HasRows)
                    {
                        ConcursoEncontrado = true;
                    }
                }
                catch (Exception)
                {
                    ConcursoEncontrado = false;

                }
                return ConcursoEncontrado;
            }
        }

        public static string Add(Resultado number)
        {
            string Saida = "";
            //SQLiteCommand cmd = new SQLiteCommand();
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "insert into TBResultado(Concurso,_01,_02,_03,_04,_05,_06,_07,_08,_09,_10,_11,_12,_13,_14,_15)" +
         " Values(@Concurso,@_01,@_02,@_03,@_04,@_05,@_06,@_07,@_08,@_09,@_10,@_11,@_12,@_13,@_14,@_15)";

                cmd.Parameters.AddWithValue("@Concurso", number.Concurso);
                cmd.Parameters.AddWithValue("@_01", number._01.ToString("D2"));
                cmd.Parameters.AddWithValue("@_02", number._02.ToString("D2"));
                cmd.Parameters.AddWithValue("@_03", number._03.ToString("D2"));
                cmd.Parameters.AddWithValue("@_04", number._04.ToString("D2"));
                cmd.Parameters.AddWithValue("@_05", number._05.ToString("D2"));
                cmd.Parameters.AddWithValue("@_06", number._06.ToString("D2"));
                cmd.Parameters.AddWithValue("@_07", number._07.ToString("D2"));
                cmd.Parameters.AddWithValue("@_08", number._08.ToString("D2"));
                cmd.Parameters.AddWithValue("@_09", number._09.ToString("D2"));
                cmd.Parameters.AddWithValue("@_10", number._10.ToString("D2"));
                cmd.Parameters.AddWithValue("@_11", number._11.ToString("D2"));
                cmd.Parameters.AddWithValue("@_12", number._12.ToString("D2"));
                cmd.Parameters.AddWithValue("@_13", number._13.ToString("D2"));
                cmd.Parameters.AddWithValue("@_14", number._14.ToString("D2"));
                cmd.Parameters.AddWithValue("@_15", number._15.ToString("D2"));

                if (VerificarExistencia(number.Concurso) == false)
                {
                    try
                    {
                        //conexao.Conectar();
                        number.Concurso = Convert.ToInt32(cmd.ExecuteNonQuery());
                        DbConnection().Close();

                        Saida = "Resultado registrado com sucesso!";
                    }
                    catch (Exception exc)
                    {
                        Saida = "Ocorreu um erro inesperado: " + exc.Message;
                    }
                    return Saida;
                }
                else
                {
                    //If the Result is already registered, this code will return and nothing will be saved.
                    Saida = "Resultado já cadastrado.";
                    return Saida;                    
                }
            }
        }

        public static string Update(Resultado result)
        {
            string Saida = "";
            //SQLiteCommand cmd = new SQLiteCommand();
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "UPDATE TBResultado SET _01=@_01,_02=@_02,_03=@_03,_04=@_04,_05=@_05,_06=@_06," +
                    " _07=@_07,_08=@_08,_09=@_09,_10=@_10,_11=@_11,_12=@_12,_13=@_13,_14=@_14,_15=@_15 WHERE Concurso = @Concurso";
                cmd.Parameters.AddWithValue("@Concurso", result.Concurso);
                cmd.Parameters.AddWithValue("@_01", result._01.ToString("D2"));
                cmd.Parameters.AddWithValue("@_02", result._02.ToString("D2"));
                cmd.Parameters.AddWithValue("@_03", result._03.ToString("D2"));
                cmd.Parameters.AddWithValue("@_04", result._04.ToString("D2"));
                cmd.Parameters.AddWithValue("@_05", result._05.ToString("D2"));
                cmd.Parameters.AddWithValue("@_06", result._06.ToString("D2"));
                cmd.Parameters.AddWithValue("@_07", result._07.ToString("D2"));
                cmd.Parameters.AddWithValue("@_08", result._08.ToString("D2"));
                cmd.Parameters.AddWithValue("@_09", result._09.ToString("D2"));
                cmd.Parameters.AddWithValue("@_10", result._10.ToString("D2"));
                cmd.Parameters.AddWithValue("@_11", result._11.ToString("D2"));
                cmd.Parameters.AddWithValue("@_12", result._12.ToString("D2"));
                cmd.Parameters.AddWithValue("@_13", result._13.ToString("D2"));
                cmd.Parameters.AddWithValue("@_14", result._14.ToString("D2"));
                cmd.Parameters.AddWithValue("@_15", result._15.ToString("D2"));
                cmd.ExecuteNonQuery();              
            }
            try
            {
                Saida = "Concurso editado com sucesso!";
            }
            catch (Exception exc)
            {
                Saida = "Ocorreu um erro inesperado: " + exc.Message;
            }
            //sqliteConnection.Close();
            return Saida;
        }


        public static string Delete(string Concurso)
        {
            string Saida = "";
            //SQLiteCommand cmd = new SQLiteCommand();
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TBResultado Where Concurso=@Concurso";
                cmd.Parameters.AddWithValue("@Concurso", Concurso);                
                cmd.ExecuteNonQuery();                
            }    
                                   
            return Saida;
        }
        /*------------Counts occurrences of numbers (How many times a number came out ------------*/
        public IDictionary<int, int> GetResult(IEnumerable<int> src)
        {
            var dict = Enumerable.Range(11, 5).ToDictionary(k => k, v => 0);

            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT _01, _02, _03, _04, _05, _06, _07, " +
                        "_08, _09, _10, _11, _12, _13, _14, _15 FROM TBResultado";

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            object[] arr = new object[rdr.FieldCount];
                            rdr.GetValues(arr);
                            IEnumerable<int> values = arr.Select(x => Convert.ToInt32(x));
                            int matches = values.Where(x => src.Contains(x)).Count();
                            if (dict.ContainsKey(matches)) dict[matches]++;
                        }
                    }
                    DbConnection().Close();
                }
            }
            return dict;
        }
        //public static string SearchResult(string Contest)
        //{
        //    string Result = "";
        //    SQLiteCommand cmd = new SQLiteCommand();
        //    using (cmd = DbConnection().CreateCommand())
        //    {
        //        cmd.CommandText = "Select _01,_02,_03,_04,_05,_06,_07,_08,_09,_10,_11,_12,_13,_14,_15 from TBResultado where Concurso =" + Contest;
        //        try
        //        {
        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception exc)
        //        {
        //            Result = string.Format("An error occurred while fetching the result {0}", exc.Message);
        //        }
        //        return Result;
        //    }
        //}

        //struct Result
        //{
        //    public int Contest = 0;
        //    public int[] Number = new int[15];
        //    public bool IsValid => Contest != 0;
        //}

        public static Resultado SearchResult(int contest)  //Fill in the form array frm SaveResults
        {
            var result = new Resultado();
            
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT _01,_02,_03,_04,_05,_06,_07,_08,_09,_10,_11,_12,_13,_14,_15 from TBResultado WHERE Concurso = @concurso";

                cmd.Parameters.AddWithValue("@concurso", contest);
                using (var reader = cmd.ExecuteReader())
                {                   
                    if (reader.Read())
                    {
                        result.Concurso = contest;

                        result._01 = Convert.ToInt32(reader[0]);
                        result._02 = Convert.ToInt32(reader[1]);
                        result._03 = Convert.ToInt32(reader[2]);
                        result._04 = Convert.ToInt32(reader[3]);
                        result._05 = Convert.ToInt32(reader[4]);
                        result._06 = Convert.ToInt32(reader[5]);
                        result._07 = Convert.ToInt32(reader[6]);
                        result._08 = Convert.ToInt32(reader[7]);
                        result._09 = Convert.ToInt32(reader[8]);
                        result._10 = Convert.ToInt32(reader[9]);
                        result._11 = Convert.ToInt32(reader[10]);
                        result._12 = Convert.ToInt32(reader[11]);
                        result._13 = Convert.ToInt32(reader[12]);
                        result._14 = Convert.ToInt32(reader[13]);
                        result._15 = Convert.ToInt32(reader[14]);
                    }

                }
                DbConnection().Close();
            }
            
            return result;
            
        }
    }
}
