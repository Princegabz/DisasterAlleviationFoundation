﻿using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using DisasterAlleviation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DisasterAlleviation.Models
{
    public class DisplayRecords //Used for getting and setting records from the database like the monetary donations, disasters, and goods donations. 
    {
        //Connection string to my Database
        SqlConnection con = new SqlConnection("Server=tcp:daf.database.windows.net,1433;Initial Catalog=Disaster Alleviation Foundation;Persist Security Info=False;User ID=st10085443;Password=Foxishsith76;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public string user_id { get; set; }
        // [Required(ErrorMessage = "Username is required.")]
        public string user_name { get; set; }
        public string user_surname { get; set; }
        public string Amount { get; set; }

        public string newCategory { get; set; }
        public string DonationType { get; set; }
        public string NoOfItems { get; set; }
        public string DonationId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string DisasterId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string dDescription { get; set; }
        public string AllocationId { get; set; }
        public string MoneyDonated { get; set; }
        public string PurchasedGoods { get; set; }
        public string Disaster { get; set; }
        public string Type { get; set; }
        public string AllocationAmount { get; set; }
        public string Price { get; set; }

        public int GetDonationCountForUser() //Count for goods donations
        {
            int donationCount = 0;

            using (SqlConnection connection = new SqlConnection("Server=tcp:daf.database.windows.net,1433;Initial Catalog=Disaster Alleviation Foundation;Persist Security Info=False;User ID=st10085443;Password=Foxishsith76;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(UserId) FROM Donations WHERE DonationType='Goods Donation'", connection))
                {
                    donationCount = (int)command.ExecuteScalar();
                }
            }

            return donationCount;
        }
        public decimal GetSumOfMonetaryDonationsForUser()
        {
            decimal sumOfMonetaryDonations = 0;

            using (SqlConnection connection = new SqlConnection("Server=tcp:daf.database.windows.net,1433;Initial Catalog=Disaster Alleviation Foundation;Persist Security Info=False;User ID=st10085443;Password=Foxishsith76;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT SUM(CAST(NoOfItems AS decimal(18,2))) FROM Donations WHERE UserId = 0 AND DonationType = 'Monetary Donation'", connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        sumOfMonetaryDonations = Convert.ToDecimal(result);
                    }
                }
            }

            return sumOfMonetaryDonations;
        }

        public DisplayRecords()
        {
            this.user_id = "";
            this.user_name = "No name";
            this.user_surname = "No surname";
        }
        public DisplayRecords(string id, string name, string surname)
        {
            user_id = id;
            user_name = name;
            user_surname = surname;
        }

        // Retrieves a list of users from the database.
        public List<DisplayRecords> getStudents()
        {
            List<DisplayRecords> ls = new List<DisplayRecords>();
            SqlDataAdapter cmdSelect = new SqlDataAdapter($"SELECT * FROM Users", con);
            DataTable obj = new DataTable();
            DataRow dr;

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {

                for (int i = 0; i < obj.Rows.Count; i++)
                {
                    dr = obj.Rows[i];
                    ls.Add(new DisplayRecords((string)dr[0], (string)dr[1], (string)dr[2]));
                }
            }

            con.Close();

            return ls;

        }
        public bool verified(string user, string password) // Verifies user credentials against the database
        {
            bool valid;
            SqlDataAdapter cmdSelect = new SqlDataAdapter($"SELECT * FROM Users WHERE Username = '{user}' AND Password = '{password}'", con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        public bool Register(string name, string surname, string username, string pass)// Registers a new user in the database
        {
            bool valid;
            string sql = $"INSERT INTO  Users (Name, Surname,Username,Password) VALUES ('{name}','{surname}','{username}','{pass}')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        // Captures monetary donations in the database
        public bool CaptureMonetary(string dateT, int surNoOfItemsname, string Category, string Description)
        {
            bool valid;
            string sql = $"INSERT INTO  Donations (UserId, Date,NoOfItems,Category,Description,DonationType) VALUES (0,'{dateT}','{surNoOfItemsname}','Money','{Description}','Monetary Donation')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        // Captures monetary allocations in the database
        public bool CaptureMonetaryAllocation(int AllocationAmount, string Description)
        {
            bool valid;
            string sql = $"INSERT INTO  Allocations (AllocationAmount,Description,Type) VALUES ('{AllocationAmount}','{Description}','Money')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        // Captures Goods allocations in the database
        public bool CaptureGoodsAllocation(int AllocationAmount, string Description, string Type)
        {
            bool valid;
            string sql = $"INSERT INTO  Allocations (AllocationAmount,Description,Type) VALUES ('{AllocationAmount}','{Description}','{Type}')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        // Captures Goods allocations in the database
        public bool PurchaseGoods(int Price, string Description, string Category)
        {
            bool valid;
            string sql = $"INSERT INTO  PurchaseGoods (Price,Description,Category) VALUES ('{Price}','{Description}','{Category}')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        // Captures goods donations in the database
        public bool CaptureGoods(string dateT, int surNoOfItemsname, string Category, string Description)
        {
            bool valid;
            string sql = $"INSERT INTO  Donations (UserId, Date,NoOfItems,Category,Description,DonationType) VALUES (0,'{dateT}','{surNoOfItemsname}','{Category}','{Description}','Goods Donation')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }
        // Captures disaster information in the database
        public bool CaptureD(string stat_date, string end_date, string location, string description)
        {
            bool valid;
            string sql = $"INSERT INTO  Disasters (StartDate,EndDate,Location,Description) VALUES ('{stat_date}','{end_date}','{location}','{description}')";
            SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, con);
            DataTable obj = new DataTable();

            con.Open();
            cmdSelect.Fill(obj);

            if (obj.Rows.Count > 0)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            con.Close();
            return valid;
        }


        // Retrieves information about monetary donations
        public List<DisplayRecords> MonetaryInformation()
        {
            List<DisplayRecords> show2;

            try
            {
                string sql = $"select UserId,DonationType,NoOfItems,DonationId,Date,Description,Category from Donations";
                SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
                DataSet dataset = new DataSet();
                con.Open();
                cmd.Fill(dataset);
                show2 = new List<DisplayRecords>();
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    show2.Add(new DisplayRecords
                    {
                        /* Donation details*/
                        user_id = dr["UserId"].ToString(),

                        /* Donation details*/
                        DonationType = dr["DonationType"].ToString(),
                        NoOfItems = dr["NoOfItems"].ToString(),
                        DonationId = dr["DonationId"].ToString(),
                        Date = dr["Date"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return show2;
        }
        public List<DisplayRecords> DisasterInformation()
        {
            List<DisplayRecords> show2;

            try
            {
                SqlDataAdapter cmd2 = new SqlDataAdapter($"select DisasterId,StartDate,EndDate,Location,Description from Disasters", con);
                DataSet dataset2 = new DataSet();
                con.Open();
                cmd2.Fill(dataset2);
                show2 = new List<DisplayRecords>();
                foreach (DataRow dr2 in dataset2.Tables[0].Rows)
                {
                    show2.Add(new DisplayRecords
                    {
                        /* Disaster details*/
                        DisasterId = dr2["DisasterId"].ToString(),
                        StartDate = dr2["StartDate"].ToString(),
                        EndDate = dr2["EndDate"].ToString(),
                        Location = dr2["Location"].ToString(),
                        dDescription = dr2["Description"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return show2;
        }
        public List<DisplayRecords> AllocationInformation()
        {
            List<DisplayRecords> show2;

            try
            {
                SqlDataAdapter cmd2 = new SqlDataAdapter($"select AllocationAmount,Description, Type from Allocations", con);
                DataSet dataset2 = new DataSet();
                con.Open();
                cmd2.Fill(dataset2);
                show2 = new List<DisplayRecords>();
                foreach (DataRow dr2 in dataset2.Tables[0].Rows)
                {
                    show2.Add(new DisplayRecords
                    {
                        /* Allocation details*/
                        AllocationAmount = dr2["AllocationAmount"].ToString(),
                        Description = dr2["Description"].ToString(),
                        Type = dr2["Type"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return show2;
        }

        public List<DisplayRecords> PurchaseInformation()
        {
            List<DisplayRecords> show2;

            try
            {
                SqlDataAdapter cmd2 = new SqlDataAdapter($"select Price,Description,Category from PurchaseGoods", con);
                DataSet dataset2 = new DataSet();
                con.Open();
                cmd2.Fill(dataset2);
                show2 = new List<DisplayRecords>();
                foreach (DataRow dr2 in dataset2.Tables[0].Rows)
                {
                    show2.Add(new DisplayRecords
                    {
                        /* Allocation details*/
                        Price = dr2["Price"].ToString(),
                        Description = dr2["Description"].ToString(),
                        Category = dr2["Category"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return show2;
        }
        public List<DisplayRecords> AvailableMoney()
        {
            List<DisplayRecords> show2;

            try
            {
                SqlDataAdapter cmd2 = new SqlDataAdapter($"select MoneyDonated,PurchasedGoods,Disaster from Allocation", con);
                DataSet dataset2 = new DataSet();
                con.Open();
                cmd2.Fill(dataset2);
                show2 = new List<DisplayRecords>();
                foreach (DataRow dr2 in dataset2.Tables[0].Rows)
                {
                    show2.Add(new DisplayRecords
                    {
                        /* Allocation details */
                        MoneyDonated = dr2["MoneyDonated"].ToString(),
                        PurchasedGoods = dr2["PurchasedGoods"].ToString(),
                        Disaster = dr2["Disaster"].ToString(),
                    });
                }
                con.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return show2;

        }

        public List<string> GetDisasterNames()
        {
            List<string> disasterNames = new List<string>();
            try
            {
                string sql = "SELECT Description FROM Disasters";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string disasterName = reader["Description"].ToString();
                    disasterNames.Add(disasterName);
                }
            }
            catch (Exception)
            {
                // Handle exceptions as needed
            }
            finally
            {
                con.Close();
            }

            return disasterNames;
        }
    }
}
