using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Helpers.Helper
{
    public class GenericRepository
    {
        public static DataTable GetSingleDataTable(string commandText, Dictionary<string, object> parameters)
        {
            DataTable _dataTable = new DataTable();

            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(_dataTable);
                        }
                    }
                }
            }

            return _dataTable;
        }
        public static bool IsEmailExist(string commandText, Dictionary<string, object> parameters)

        {

            DataTable dataTable = new DataTable();

            bool isExist = false;

            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())

            {

                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))

                    {

                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandTimeout = 120;

                        if (parameters != null)

                        {

                            foreach (var parameter in parameters)

                            {

                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);

                            }

                        }

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int EmailExistsInt))

                        {

                            isExist = EmailExistsInt == 1;

                        }

                    }

                }

            }

            return isExist;

        }
        public static string getUserName(string commandText, Dictionary<string, object> parameters)

        {

            string UserName = null;

            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())

            {

                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))

                    {

                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandTimeout = 120;

                        if (parameters != null)

                        {
                            foreach (var parameter in parameters)

                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }

                        }

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int EmailExistsInt))

                        {
                            UserName = EmailExistsInt.ToString();
                        }

                    }

                }

            }

            return UserName;
        }

        public static bool IsPasswordCurrect(string commandText, Dictionary<string, object> parameters)

        {

            DataTable dataTable = new DataTable();

            bool NotValid = false;

            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())

            {

                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))

                    {

                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandTimeout = 120;

                        if (parameters != null)

                        {

                            foreach (var parameter in parameters)

                            {

                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);

                            }

                        }

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int NotValidPassword))

                        {

                            NotValid = NotValidPassword == 0;

                        }

                    }

                }

            }

            return NotValid;

        }


        public static RegisterModel CheckingUserIsValidOrNotLogin(string commandText, Dictionary<string, object> parameters)
        {

            RegisterModel result = new RegisterModel();
            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read each row
                                while (reader.Read())
                                {
                                    int user_id = Convert.ToInt32(reader["User_id"].ToString());
                                    string Password = reader["Password_hash"].ToString();
                                    string email = reader["Email"].ToString();
                                    string username = reader["Username"].ToString();

                                    var user = new { User_id = user_id, Email = email, Password = Password, Username = username };


                                    result.User_id = user_id;
                                    result.Email = email;
                                    result.Password = Password;
                                    result.Username = username;
                                }
                            }
                        }

                    }
                }
            }

            return result;
        }



        public static AdminModel CheckingAdminLogin(string commandText, Dictionary<string, object> parameters)
        {

            AdminModel result = new AdminModel();
            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Read each row
                                while (reader.Read())
                                {
                                    int Admin_id = Convert.ToInt32(reader["Admin_id"].ToString());
                                    string Password = reader["Password"].ToString();
                                    string email = reader["Email"].ToString();
                                    string username = reader["Username"].ToString();

                                    var user = new { Admin_id = Admin_id, Email = email, Password = Password, Username = username };


                                    result.Admin_id = Admin_id;
                                    result.Email = email;
                                    result.Password = Password;
                                    result.Username = username;
                                }
                            }
                        }

                    }
                }
            }

            return result;
        }

        public static void AddQuestion(string commandText, Dictionary<string, object> parameters)
        {


            //QuizzeModel result = new QuizzeModel();
            List<QuizzeModel> _QuizzeModelList = new List<QuizzeModel>();
            using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())
            {
                string connectionString = _context.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 120;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        command.ExecuteNonQuery();


                    }
                }
            }
        }

        //public static void UpdateQuestion(string commandText, Dictionary<string, object> parameters)
        //{
        //    using (QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities())
        //    {
        //        string connectionString = _context.Database.Connection.ConnectionString;

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            using (SqlCommand command = new SqlCommand(commandText, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandTimeout = 120;

        //                if (parameters != null)
        //                {
        //                    foreach (var parameter in parameters)
        //                    {
        //                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
        //                    }
        //                }
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}

        public static void UpdateQuestionsAndOptions(int quizId, List<QuestionOptionModel> questionOptions)
        {
            using (var db = new QuizeManagement_0406Entities())
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("Question_id", typeof(int));
                dataTable.Columns.Add("Question_text", typeof(string));
                dataTable.Columns.Add("Option_text", typeof(string));
                dataTable.Columns.Add("Is_correct", typeof(bool));

                foreach (var qo in questionOptions)
                {
                    dataTable.Rows.Add(qo.QuestionId, qo.QuestionText, qo.OptionText, qo.IsCorrect);
                }

                var quizIdParam = new SqlParameter("@quiz_id", quizId);
                var questionsParam = new SqlParameter("@Questions", SqlDbType.Structured)
                {
                    TypeName = "dbo.QuestionOptionType",
                    Value = dataTable
                };

                db.Database.ExecuteSqlCommand("EXEC UpdateQuestionsAndOptions @quiz_id, @Questions", quizIdParam, questionsParam);
            }
        }

    }

}
    

