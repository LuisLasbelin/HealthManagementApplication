using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using Newtonsoft.Json;

namespace SOAPService
{
    /// <summary>
    /// Descripción breve de webservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class webservice : System.Web.Services.WebService
    {

        [WebMethod]
        public bool LoginDoctor(string userName, string password)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string checkuser = "SELECT * FROM doctors WHERE DNI='" + userName + "' AND Password='" + password + "'";
            SQLiteCommand cmd = new SQLiteCommand(checkuser, conn);

            bool done = cmd.ExecuteReader().Read();

            conn.Close();

            return done;
        }

        [WebMethod]
        public bool LoginPatient(string userName, string password)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string checkuser = "SELECT * FROM patients WHERE DNI='" + userName + "' AND Password='" + password + "'";
            SQLiteCommand cmd = new SQLiteCommand(checkuser, conn);

            bool done = cmd.ExecuteReader().Read();

            conn.Close();

            return done;
        }

        [WebMethod]
        public string AddPatient(string dni, string name, string telephone, string observations, string doctor, string password)
        {
            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sqlCommand = "INSERT INTO patients (DNI, Name, Telephone, Observations, Doctor, Password)  VALUES ('" + dni + "','" + name + "','" + telephone + "','" + observations + "',(SELECT DNI from doctors WHERE DNI='" + doctor + "'),'" + password + "')";
            SQLiteCommand cmd = new SQLiteCommand(sqlCommand, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            var values = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }

        [WebMethod]
        public bool AddDoctor(string dni, string name, string telephone, string password)
        {
            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sqlCommand = "INSERT INTO doctors VALUES ('" + dni + "','" + name + "','" + telephone + "','" + password + "')";
            SQLiteCommand cmd = new SQLiteCommand(sqlCommand, conn);

            bool done = cmd.ExecuteReader().Read();

            conn.Close();

            return done;
        }

        [WebMethod]
        public string GetDoctor(string dni)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "SELECT * FROM doctors WHERE DNI='" + dni + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();
        
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }

        [WebMethod]
        public string GetPatient(string dni)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "SELECT * FROM patients WHERE DNI='" + dni + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }



        [WebMethod]
        public string GetDoctorPatients(string doctor)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "SELECT * FROM patients WHERE Doctor='" + doctor + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }

        [WebMethod]
        public string UpdatePatient(string dni, string name, string telephone, string observations)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "UPDATE patients SET Name = '" + name + "', Telephone = '" + telephone + "', Observations = '" + observations + "' WHERE DNI = '" + dni + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }

        [WebMethod]
        public string UpdateDoctor(string dni, string name, string telephone)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "UPDATE doctors SET Name = '" + name + "', Telephone = '" + telephone + "' WHERE DNI = '" + dni + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }

        [WebMethod]
        public string UpdatePatientPassword(string dni, string password)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "UPDATE patients SET Password = '" + password + "' WHERE DNI = '" + dni + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }

        [WebMethod]
        public string UpdateDoctorPassword(string dni, string password)
        {
            var values = new List<Dictionary<string, object>>();

            String DBpath = Server.MapPath("~/database.db");

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + DBpath + ";Version=3;");
            conn.Open();
            string sql = "UPDATE doctors SET Password = '" + password + "' WHERE DNI = '" + dni + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var fieldValues = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                values.Add(fieldValues);

            }

            string text = JsonConvert.SerializeObject(values);

            conn.Close();

            return text;
        }
    }
}
