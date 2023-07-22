using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Property_rental_management_system.Models;
using System.IO;
using System.Web.Optimization;
using System.Web.UI.WebControls;
using System.Net.Sockets;

namespace Property_rental_management_system.Repository
{
   
    public class property_DAL
    {
        private SqlConnection sqlConnection;

        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["GetConn"].ToString();
            sqlConnection = new SqlConnection(constring);
        }
        /// <summary>
        /// Get all property details
        /// </summary>
        /// <returns></returns>
        public List<property>GetAllProperties()
        {
            try
            {
                List<property> propertyList = new List<property>();

                connection();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProperty";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    propertyList.Add(new property
                    {
                        propertyId = Convert.ToInt32(dr["propertyId"]),
                        property_type = Convert.ToString(dr["property_type"]),
                        property_name = Convert.ToString(dr["property_name"]),
                        available_for = Convert.ToString(dr["available_for"]),
                        available_from = Convert.ToString(dr["available_from"]),
                        rent_amount = Convert.ToString(dr["rent_amount"]),
                        rent_type = Convert.ToString(dr["rent_type"]),
                        address = Convert.ToString(dr["address"]),
                        state = Convert.ToString(dr["state"]),
                        city = Convert.ToString(dr["city"]),
                        phone_number = Convert.ToString(dr["phone_number"]),
                        property_image = Convert.ToString(dr["property_image"]),
                        property_description = Convert.ToString(dr["property_description"]),
                        status = Convert.ToString(dr["status"])

                    });
                }



                return propertyList;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// insert property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool InsertProperty(property property,HttpPostedFileBase file )
        {

            try
            {
                int id = 0;
                connection();
                SqlCommand command = new SqlCommand("SP_InsertProperty", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@property_type", property.property_type);
                command.Parameters.AddWithValue("@property_name", property.property_name);
                command.Parameters.AddWithValue("@available_for", property.available_for);
                command.Parameters.AddWithValue("@available_from", property.available_from);
                command.Parameters.AddWithValue("@rent_amount", property.rent_amount);
                command.Parameters.AddWithValue("@rent_type", property.rent_type);
                command.Parameters.AddWithValue("@address", property.address);
                command.Parameters.AddWithValue("@state", property.state);
                command.Parameters.AddWithValue("@city", property.city);
                command.Parameters.AddWithValue("@phone_number", property.phone_number);

                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string imagepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), filename);
                        file.SaveAs(imagepath);
                        command.Parameters.AddWithValue("@property_image", "~/Images/" + filename);
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@property_image", DBNull.Value);
                }
                command.Parameters.AddWithValue("@property_description", property.property_description);
                command.Parameters.AddWithValue("@status", property.status);

                sqlConnection.Open();
                id = command.ExecuteNonQuery();

                if (id > 0)
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
        /// Get  property details By Id 
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public List<property> GetPropertiesById(int propertyId)
        {
            try
            {
                List<property> propertyList = new List<property>();

                connection();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetPropertyById";
                command.Parameters.AddWithValue("@propertyId", propertyId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlConnection.Open();
                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    propertyList.Add(new property
                    {
                        propertyId = Convert.ToInt32(dr["propertyId"]),
                        property_type = Convert.ToString(dr["property_type"]),
                        property_name = Convert.ToString(dr["property_name"]),
                        available_for = Convert.ToString(dr["available_for"]),
                        available_from = Convert.ToString(dr["available_from"]),
                        rent_amount = Convert.ToString(dr["rent_amount"]),
                        rent_type = Convert.ToString(dr["rent_type"]),
                        address = Convert.ToString(dr["address"]),
                        state = Convert.ToString(dr["state"]),
                        city = Convert.ToString(dr["city"]),
                        phone_number = Convert.ToString(dr["phone_number"]),
                        property_image = Convert.ToString(dr["property_image"]),
                        property_description = Convert.ToString(dr["property_description"]),
                        status = Convert.ToString(dr["status"])

                    });
                }



                return propertyList;
            }
            finally
            {
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// update property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool UpdateProperty(property property, HttpPostedFileBase file,string image)
        {

            try
            {
                int i = 0;
                connection();
                SqlCommand command = new SqlCommand("SP_UpdateProperty", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@propertyId", property.propertyId);
                command.Parameters.AddWithValue("@property_type", property.property_type);
                command.Parameters.AddWithValue("@property_name", property.property_name);
                command.Parameters.AddWithValue("@available_for", property.available_for);
                command.Parameters.AddWithValue("@available_from", property.available_from);
                command.Parameters.AddWithValue("@rent_amount", property.rent_amount);
                command.Parameters.AddWithValue("@rent_type", property.rent_type);
                command.Parameters.AddWithValue("@address", property.address);
                command.Parameters.AddWithValue("@state", property.state);
                command.Parameters.AddWithValue("@city", property.city);
                command.Parameters.AddWithValue("@phone_number", property.phone_number);

                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string imagepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), filename);
                        file.SaveAs(imagepath);
                        command.Parameters.AddWithValue("@property_image", "~/Images/" + filename);
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@property_image",image);
                }
                command.Parameters.AddWithValue("@property_description", property.property_description);
                command.Parameters.AddWithValue("@status", property.status);

                sqlConnection.Open();
                i = command.ExecuteNonQuery();
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
        /// delete property
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public string DeleteProperty(int propertyId)
        {
            try
            {
                string result = "";
                connection();
                SqlCommand command = new SqlCommand("sp_deletePropertyDetails", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@propertyId", propertyId);
                command.Parameters.Add("@outputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                sqlConnection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@outputMessage"].Value.ToString();

                return result;
            }
            finally
            {
                sqlConnection.Close();

            }
        }


       



    }
}