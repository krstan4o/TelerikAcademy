using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SinkBreaker
{
    public class DBDispatcher
    {
        SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\PROGRAMMING\Dropbox\PC-Magazine\05_SinkBreaker\SinkBreaker\App_Data\SinkBreakerDB.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public DBDispatcher()
        {
            cmd.Connection = cn;
        }

        private void InsertEntry(string name, int score)
        {
            cn.Open();
            cmd.CommandText = "insert into Players (name,score) values ('" + name + "','" + score + "')";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            cn.Close();
        }

        public void HandleEntry(string name, int score)
        {
            cn.Open();
            cmd.CommandText = string.Format("select name from Players where name = '{0}'", name);
            dr = cmd.ExecuteReader();
            int result = 0;
            
            if (dr.HasRows)
            {
                result++;
            }
            cn.Close();

            if (result == 0)
            {
                InsertEntry(name, score);
            }
            else
            {
                UpdateEntry(name, score);
            }
        }

        private void UpdateEntry(string name, int score)
        {
            cn.Open();
            cmd.CommandText = string.Format("UPDATE Players SET name='{0}', score='{1}' WHERE name='{0}'", name, score);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public KeyValuePair<string, int>[] GetAllEntries()
        {
            List<KeyValuePair<string, int>> entries = new List<KeyValuePair<string, int>>();

            cn.Open();
            cmd.CommandText = "select top 10 * from Players ORDER BY score DESC";
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    entries.Add(new KeyValuePair<string, int>(dr[0].ToString(), int.Parse(dr[1].ToString())));
                }
            }
            cn.Close();

            return entries.ToArray();
        }

        public void ClearDB()
        {
            cn.Open();
            cmd.CommandText = "DELETE FROM Players";
            dr = cmd.ExecuteReader();
            cn.Close();
        }
    }
}