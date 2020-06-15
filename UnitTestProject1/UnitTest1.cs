using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace VideoOnRentShop
{
    [TestClass]

    public class UnitTest1
    {
        [TestMethod]
        public void testaddCustomer()
        {
            VideoRental rentals = new VideoRental();
            int expectedflag = 0;
            rentals.addCustomer("steve", "roger", "brooklyn, NY", "22345542");
            int flag = 1;
            using (SqlCommand cmd = new SqlCommand("select * from customer", rentals.myDBConnection))
            {
                rentals.myDBConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(1) == "steve")
                    {
                        flag = 0;
                    }

                }

            }
            Assert.AreEqual(expectedflag, flag, "customer steve not added");
        }


    }
}
