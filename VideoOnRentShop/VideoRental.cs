
using System.Data.SqlClient;

namespace VideoOnRentShop
{
    using System;
    using System.Configuration;
    using System.Data;

    public class VideoRental
    {
        public string myConnectionString;
        public SqlConnection myDBConnection;


        public VideoRental()
        {
            myConnectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            myDBConnection = new SqlConnection(myConnectionString);
        }

        public DataTable getTable(string Query)
        {
            DataTable mTable = new DataTable();
            using (SqlCommand mCommand = new SqlCommand(Query, myDBConnection))
            {
                myDBConnection.Open();

                SqlDataReader reader = mCommand.ExecuteReader();
                mTable.Load(reader);
                myDBConnection.Close();
                return mTable;

            }
            
        }
        public DataTable getCustomerData()
        {
            return getTable("select * from customer");
        }

        public DataTable getVideoData()
        {
            return getTable("select * from videos");
        }
        public DataTable getAllRentedVideos()
        {
            return getTable("select r.RMID as RMID, c.FirstName as FirstName, c.LastName as LastName, c.Address as Address, m.Title as Title, m.Rental_Cost as RentedCost ,r.DateRented as DateRented, r.DateReturned as DateReturned from customer c, videos m,rentedvideos r where c.CustID = r.CustIDFK  and m.VideoID = r.VideoIDFK");
        }

        public DataTable getOutRentedVideos()
        {
            return getTable("select r.RMID as RMID, c.FirstName as FirstName, c.LastName as LastName, c.Address as Address, m.Title as Title, m.Rental_Cost as RentedCost ,r.DateRented as DateRented, r.DateReturned as DateReturned from customer c, videos m,rentedvideos r where c.CustID = r.CustIDFK  and m.VideoID = r.VideoIDFK and DateReturned is NULL");
        }

        public int addCustomer(string firstName, string lastname, string address, string phonenum)
        {
            string query = "INSERT INTO CUSTOMER(FirstName,LastName,Address,Phone)VALUES(@first_name,@last_name,@address,@phone_number)";
            using (SqlCommand cmd = new SqlCommand(query, myDBConnection))
            {
                cmd.Parameters.AddWithValue("@first_name", firstName);
                cmd.Parameters.AddWithValue("@last_name", lastname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone_number", SqlDbType.Int);
                cmd.Parameters["@phone_number"].Value = phonenum;
                myDBConnection.Open();
                int res = cmd.ExecuteNonQuery();
                myDBConnection.Close();
                return res;
            }
        }
        public int addVideo(string title, string year, string genre)
        {
            int date = Convert.ToInt32(year);
            int now = DateTime.Now.Year;
            int cost = 5;
            if ( now - date > 5)
            {
                cost = 2;
            }

            string query = "INSERT INTO MOVIES(Title,Genre,Year,Rental_Cost)VALUES(@title,@genre,@year,@cost)";
            using (SqlCommand mCommand = new SqlCommand(query, myDBConnection))
            {
                mCommand.Parameters.AddWithValue("@title",title );
                mCommand.Parameters.AddWithValue("@Genre", genre);
                mCommand.Parameters.AddWithValue("@year", year);
                mCommand.Parameters.AddWithValue("@cost", SqlDbType.Int);
                mCommand.Parameters["@cost"].Value = cost;
                myDBConnection.Open();
                int res = mCommand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;
            }
        }

        public int updateCustomer(string id, string firstName, string lastname, string address, string phonenum)
        {
            string query = "UPDATE CUSTOMER SET FirstName=@first_name,LastName=@last_name,Address=@address,Phone=@phone_number WHERE CustID=@custid";
            using (SqlCommand mCommand = new SqlCommand(query, myDBConnection))
            {
                mCommand.Parameters.AddWithValue("@custid", SqlDbType.Int);
                mCommand.Parameters["@custid"].Value = id;
                mCommand.Parameters.AddWithValue("@first_name", firstName);
                mCommand.Parameters.AddWithValue("@last_name", lastname);
                mCommand.Parameters.AddWithValue("@address", address);
                mCommand.Parameters.AddWithValue("@phone_number", SqlDbType.Int);
                mCommand.Parameters["@phone_number"].Value = phonenum;
                myDBConnection.Open();
                int res = mCommand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;
            }

        }
        public int updateVideo(string id, string genre, string title, string year)
        {
            string query = "UPDATE Videos SET Title= @title,Genre=@genre,Year=@year WHERE VideoID=@movid";
            using (SqlCommand mCommand = new SqlCommand(query, myDBConnection))
            {
                mCommand.Parameters.AddWithValue("@movid", SqlDbType.Int);
                mCommand.Parameters["@movid"].Value = id;
                mCommand.Parameters.AddWithValue("@title", title);
                mCommand.Parameters.AddWithValue("@genre", genre);
                mCommand.Parameters.AddWithValue("@year", year);
                myDBConnection.Open();
                int res = mCommand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;
            }

        }

        public int deleteCustomer(string id)
        {
            string query = "DELETE FROM Customer WHERE CustID=@custid";
            using (SqlCommand mCommand = new SqlCommand(query, myDBConnection))
            {
                mCommand.Parameters.AddWithValue("@custid", SqlDbType.Int);
                mCommand.Parameters["@custid"].Value = id;
                myDBConnection.Open();
                int res = mCommand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;
            }
        }
        public int deleteVideo(string id)
        {
            string query = "DELETE FROM videos WHERE VideoID=@movid";
            using (SqlCommand mCommand = new SqlCommand(query, myDBConnection))
            {
                mCommand.Parameters.AddWithValue("@movid", SqlDbType.Int);
                mCommand.Parameters["@movid"].Value = id;
                myDBConnection.Open();
                int res = mCommand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;
            }
        }

        public int rentVideo(string custid, string movid)
        {
            string query = "Insert into rentedvideos (VideoIDFK, CustIDFK, DateRented)Values(@movid, @custid, @rented)";
            using (SqlCommand mCommand = new SqlCommand(query, myDBConnection))
            {
                mCommand.Parameters.AddWithValue("@custid", SqlDbType.Int);
                mCommand.Parameters["@custid"].Value = custid;
                mCommand.Parameters.AddWithValue("@movid", SqlDbType.Int);
                mCommand.Parameters["@movid"].Value = movid;
                mCommand.Parameters.AddWithValue("@rented", DateTime.Now);
               
                myDBConnection.Open();
                int res = mCommand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;


            }
        }
        public int returnVideo(string rmid)
        {
            string query = "update rentedvideos set DateReturned=@returned where RMID=@rmid";
            using (SqlCommand mCOmmand = new SqlCommand(query, myDBConnection))
            {
                mCOmmand.Parameters.AddWithValue("@rmid", SqlDbType.Int);
                mCOmmand.Parameters["@rmid"].Value = rmid;
                mCOmmand.Parameters.AddWithValue("@returned", DateTime.Now);
                myDBConnection.Open();
                int res = mCOmmand.ExecuteNonQuery();
                myDBConnection.Close();
                return res;


            }
        }

    }
}
