using Property_rental_management_system.Controllers;
using Property_rental_management_system.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Web;

namespace Property_rental_management_system.Repository
{
    public class homepageRepository
    {
        private SqlConnection sqlConnection;
        Common.Password EncryptData = new Common.Password();

        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["GetConn"].ToString();
            sqlConnection = new SqlConnection(constring);
        }

        /// <summary>
        /// Adding user details
        /// </summary>
        /// <param name="Signup"></param>
        /// <returns></returns>
        public bool AddDetails(Signup Signup)
        {

            try
            {
                connection();
                Common.Password EncryptData = new Common.Password();
                SqlCommand command = new SqlCommand("spsignup", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Firstname", Signup.Firstname);
                command.Parameters.AddWithValue("@Lastname", Signup.Lastname);
                command.Parameters.AddWithValue("@Dateofbirth", Signup.Dateofbirth);
                command.Parameters.AddWithValue("@Gender", Signup.Gender);
                command.Parameters.AddWithValue("@Phonenumber", Signup.Phonenumber);
                command.Parameters.AddWithValue("@Email", Signup.Email);
                command.Parameters.AddWithValue("@Address", Signup.Address);
                command.Parameters.AddWithValue("@State", Signup.State);
                command.Parameters.AddWithValue("@City", Signup.City);
                command.Parameters.AddWithValue("@Pincode", Signup.Pincode);
                command.Parameters.AddWithValue("@Username ", Signup.Username);
                command.Parameters.AddWithValue("@Password ", EncryptData.Encode(Signup.Password));
                command.Parameters.AddWithValue("@type ", "sp_insert");

                sqlConnection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlConnection.Close();

            }
        }
        /// <summary>
        /// get user details
        /// </summary>
        /// <returns></returns>
        public List<Signup> GetDetails()
        {
            try
            {
                connection();
                List<Signup> signupList = new List<Signup>();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_select";
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlConnection.Open();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    signupList.Add(
                        new Signup
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Firstname = Convert.ToString(dr["Firstname"]),
                            Lastname = Convert.ToString(dr["Lastname"]),
                            Dateofbirth = Convert.ToString(dr["Dateofbirth"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            Phonenumber = Convert.ToString(dr["Phonenumber"]),
                            Email = Convert.ToString(dr["Email"]),
                            Address = Convert.ToString(dr["Address"]),
                            State = Convert.ToString(dr["State"]),
                            City = Convert.ToString(dr["City"]),
                            Pincode = Convert.ToString(dr["Pincode"]),
                            Username = Convert.ToString(dr["Username"]),
                            Password = Convert.ToString(dr["Password"]),

                        }
                        );

                }
                return signupList;
            }
            finally
            {
                sqlConnection.Close();

            }

        }

        /// <summary>
        /// signin
        /// </summary>
        /// <param name="signin"></param>
        /// <returns></returns>
        public bool Signin(Signin signin)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SP_Signup", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", signin.Username);
                command.Parameters.AddWithValue("@Password", EncryptData.Encode(signin.Password));

                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                bool isValidUser = reader.HasRows;

                return isValidUser;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// admin signin
        /// </summary>
        /// <param name="signin"></param>
        /// <returns></returns>
        public bool SigninAdmin(Signin signin)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SP_AdminSignin", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", signin.Username);
                command.Parameters.AddWithValue("@Password", EncryptData.Encode(signin.Password));
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                bool isValidAdmin = reader.HasRows;
                return isValidAdmin;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// get user details by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Signup> GetDetailsById(int Id)
        {
            try
            {
                connection();
                List<Signup> signupList = new List<Signup>();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetDetailsById";
                command.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlConnection.Open();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    signupList.Add(
                        new Signup
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Firstname = Convert.ToString(dr["Firstname"]),
                            Lastname = Convert.ToString(dr["Lastname"]),
                            Dateofbirth = Convert.ToString(dr["Dateofbirth"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            Phonenumber = Convert.ToString(dr["Phonenumber"]),
                            Email = Convert.ToString(dr["Email"]),
                            Address = Convert.ToString(dr["Address"]),
                            State = Convert.ToString(dr["State"]),
                            City = Convert.ToString(dr["City"]),
                            Pincode = Convert.ToString(dr["Pincode"]),
                        }
                        );
                }
                return signupList;
            }
            finally
            {
                sqlConnection.Close();

            }
        }
        /// <summary>
        /// update user details
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>

        public bool UpdateUserDetails(Signup profile)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("spsignup", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", profile.Id);
                command.Parameters.AddWithValue("@Firstname", profile.Firstname);
                command.Parameters.AddWithValue("@Lastname", profile.Lastname);
                command.Parameters.AddWithValue("@Dateofbirth", profile.Dateofbirth);
                command.Parameters.AddWithValue("@Gender", profile.Gender);
                command.Parameters.AddWithValue("@Phonenumber", profile.Phonenumber);
                command.Parameters.AddWithValue("@Email", profile.Email);
                command.Parameters.AddWithValue("@Address", profile.Address);
                command.Parameters.AddWithValue("@State", profile.State);
                command.Parameters.AddWithValue("@City", profile.City);
                command.Parameters.AddWithValue("@Pincode", profile.Pincode);
                command.Parameters.AddWithValue("@type ", "sp_update");

                sqlConnection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public string DeleteUser(int Id)
        {
            try
            {
                string result = "";
                connection();
                {
                    SqlCommand command = new SqlCommand("sp_deleteUserDetails", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.Add("@outputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    result = command.Parameters["@outputMessage"].Value.ToString();
                }
                return result;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// Get profile of user by username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<Signup> GetDetailsByusername(string Username)
        {
            try
            {
                List<Signup> signupList = new List<Signup>();
                connection();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_User_Profile";
                command.Parameters.AddWithValue("@Username", Username);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlConnection.Open();
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    signupList.Add(
                        new Signup
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Firstname = Convert.ToString(dr["Firstname"]),
                            Lastname = Convert.ToString(dr["Lastname"]),
                            Dateofbirth = Convert.ToString(dr["Dateofbirth"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            Phonenumber = Convert.ToString(dr["Phonenumber"]),
                            Email = Convert.ToString(dr["Email"]),
                            Address = Convert.ToString(dr["Address"]),
                            State = Convert.ToString(dr["State"]),
                            City = Convert.ToString(dr["City"]),
                            Pincode = Convert.ToString(dr["Pincode"]),

                        }
                        );

                }
                return signupList;
            }
            finally
            {
                sqlConnection.Close();

            }

        }

        /// <summary>
        /// add new admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool AddAdmin(Admin admin)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SP_InsertAdminDetails", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", admin.Name);
                command.Parameters.AddWithValue("@Email_address", admin.Email);
                command.Parameters.AddWithValue("@Username", admin.Username);
                command.Parameters.AddWithValue("@Password", EncryptData.Encode(admin.Password));

                sqlConnection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// add messages
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public bool ContactMessages(Messages messages)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SP_Messages", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Full_name", messages.Full_name);
                command.Parameters.AddWithValue("@Email", messages.Email);
                command.Parameters.AddWithValue("@Phone_number", messages.Phone_number);
                command.Parameters.AddWithValue("@Message", messages.Message);

                sqlConnection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlConnection.Close();

            }
        }
        /// <summary>
        /// display messages
        /// </summary>
        /// <returns></returns>
        public List<Messages> GetMessages()
        {
            try
            {
                connection();
                List<Messages> messageList = new List<Messages>();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetMessages";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlConnection.Open();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    messageList.Add(new Messages
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Full_name = Convert.ToString(row["Full_name"]),
                        Email = Convert.ToString(row["Email"]),
                        Phone_number = Convert.ToString(row["Phone_number"]),
                        Message = Convert.ToString(row["Message"])

                    });

                }
                return messageList;
            }
            finally
            {
                sqlConnection.Close();

            }

        }
        /// <summary>
        /// get messages by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public List<Messages> GetMessagesById(int Id)
        {
            try
            {
                connection();
                List<Messages> messageList = new List<Messages>();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetMessageDetailsById";
                command.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlConnection.Open();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    messageList.Add(new Messages
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Full_name = Convert.ToString(row["Full_name"]),
                        Email = Convert.ToString(row["Email"]),
                        Phone_number = Convert.ToString(row["Phone_number"]),
                        Message = Convert.ToString(row["Message"])

                    });

                }
                return messageList;
            }
            finally
            {
                sqlConnection.Close();

            }

        }

        /// <summary>
        /// delete messages
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteMessages(int Id)
        {
            try
            {
                string result = "";
                connection();
                {
                    SqlCommand command = new SqlCommand("SP_deleteMessageDetails", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.Add("@outputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    result = command.Parameters["@outputMessage"].Value.ToString();
                }
                return result;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// get password 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public string GetExistingPassword(string Username)
        {
            try
            {
                string existingPassword = null;
                connection();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetPasswordByUsername";
                command.Parameters.AddWithValue("@Username", Username);
                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        existingPassword = reader.GetString(0);
                    }
                }
                return existingPassword;
            }
            finally
            {
                sqlConnection.Close();

            }

        }

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool UpdatePassword(string Username, string Password)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SP_ChangePassword", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", EncryptData.Encode(Password));
                sqlConnection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlConnection.Close();

            }

        }

        /// <summary>
        /// Getting admin password
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>

        public string GetExistingAdminPassword(string Username)
        {
            try
            {
                string existingPassword = null;
                connection();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetPasswordAdminByUsername";
                command.Parameters.AddWithValue("@Username", Username);
                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        existingPassword = reader.GetString(0);
                    }
                }
                return existingPassword;
            }
            finally
            {
                sqlConnection.Close();

            }

        }

        /// <summary>
        /// Update admin password
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>

        public bool UpdateAdminPassword(string Username, string Password)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SP_ChangeAdminPassword", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", EncryptData.Encode(Password));
                sqlConnection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlConnection.Close();

            }

        }

    }



}


