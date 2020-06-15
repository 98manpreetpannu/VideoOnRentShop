using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace VideoOnRentShop
{
    public partial class VideoOnRentShop : Form
    {
        VideoRental rental = new VideoRental();
        public VideoOnRentShop()
        {
            InitializeComponent();
            InitializeDataGrids();
        }
        private void InitializeDataGrids()
        {
            
            CustomersDataGridView.DataSource = rental.getCustomerData();
            VideoDataGridView.DataSource = rental.getVideoData();
            RentedVideosDataGridView.DataSource = rental.getAllRentedVideos();
        }

        private void cleartextboxes()
        {
            FirstName.Clear();
            LastName.Clear();
            Address.Clear();
            PhoneNumber.Clear();
            CustomerID.Clear();
            VideoId.Clear();
            RentedCost.Clear();
            Year.Clear();
            Title.Clear();
            Genre.Clear();

        }


        private void RentedVideosDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerID.Text = RentedVideosDataGridView.Rows[e.RowIndex].Cells["RMID"].Value.ToString();
            FirstName.Text = RentedVideosDataGridView.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
            LastName.Text = RentedVideosDataGridView.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
            Address.Text = RentedVideosDataGridView.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            Title.Text = RentedVideosDataGridView.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            RentedCost.Text = RentedVideosDataGridView.Rows[e.RowIndex].Cells["RentedCost"].Value.ToString();
        }


        private void CustomersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerID.Text = CustomersDataGridView.Rows[e.RowIndex].Cells["CustID"].Value.ToString();
            FirstName.Text = CustomersDataGridView.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
            LastName.Text = CustomersDataGridView.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
            Address.Text = CustomersDataGridView.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            PhoneNumber.Text = CustomersDataGridView.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
        }

        private void VideoDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            VideoId.Text = VideoDataGridView.Rows[e.RowIndex].Cells["VideoID"].Value.ToString();
            RentedCost.Text = VideoDataGridView.Rows[e.RowIndex].Cells["Rental_Cost"].Value.ToString();
            Title.Text = VideoDataGridView.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            Year.Text = VideoDataGridView.Rows[e.RowIndex].Cells["Year"].Value.ToString();
            Genre.Text = VideoDataGridView.Rows[e.RowIndex].Cells["Genre"].Value.ToString();
        }

        
        private void AddCustomer_Click(object sender, EventArgs e)
        {   
            if (FirstName.Text != "" && LastName.Text != "" && Address.Text != "" && PhoneNumber.Text != ""){

                int result = rental.addCustomer(FirstName.Text, LastName.Text, Address.Text, PhoneNumber.Text);
                if (result == 1)
                {
                    MessageBox.Show("customer added successfully!");
                    CustomersDataGridView.DataSource = rental.getCustomerData();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot add customer");
                }
            }
            else
            {
                string message = "fields are empty";
                MessageBox.Show(message);
            }
        }

        private void AddVideo_Click(object sender, EventArgs e)
        {
            if (Title.Text != "" && Genre.Text != "" && Year.Text != "")
            {

                int result = rental.addVideo(Title.Text, Year.Text, Genre.Text);
                if (result == 1)
                {
                    MessageBox.Show("Video added successfully!");
                    CustomersDataGridView.DataSource = rental.getCustomerData();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot add Video");
                }
            }
            else
            {
                string message = "fields are empty";
                MessageBox.Show(message);
            }
        }

        private void UpdateCustomer_Click(object sender, EventArgs e)
        {
            if (CustomerID.Text != "" && FirstName.Text != "" && LastName.Text != "" && Address.Text != "" && PhoneNumber.Text != "")
            {
                int result = rental.updateCustomer(CustomerID.Text, FirstName.Text, LastName.Text, Address.Text, PhoneNumber.Text);
                if (result == 1)
                {
                    MessageBox.Show("customer updated successfully!");
                    CustomersDataGridView.DataSource = rental.getCustomerData();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot update customer");
                }
            }
            else
            {
                string message = "fields are empty, please select the row to update from table";
                MessageBox.Show(message);
            }
        }

        private void UpdateVideo_Click(object sender, EventArgs e)
        {
            if (VideoId.Text != "" && Genre.Text != "" && Title.Text != "" && Year.Text != "")
            {
                int result = rental.updateVideo(VideoId.Text, Genre.Text, Title.Text, Year.Text);
                if (result == 1)
                {
                    MessageBox.Show("Video updated successfully!");
                    VideoDataGridView.DataSource = rental.getVideoData();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot update Video");
                }
            }
            else
            {
                string message = "fields are empty, please select the row to update from table";
                MessageBox.Show(message);
            }
        }

        private void DeleteCustomer_Click(object sender, EventArgs e)
        {
            if (CustomerID.Text != "" && FirstName.Text != "" && LastName.Text != "" && Address.Text != "" && PhoneNumber.Text != "")
            {
                int result = rental.deleteCustomer(CustomerID.Text);
                if (result == 1)
                {
                    MessageBox.Show("customer deleted successfully!");
                    CustomersDataGridView.DataSource = rental.getCustomerData();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot delete customer");
                }
            }
            else
            {
                string message = "fields are empty, please select the row to delete from table";
                MessageBox.Show(message);
            }
        }

        private void DeleteVideo_Click(object sender, EventArgs e)
        {
            if (VideoId.Text != "" && Genre.Text != "" && Title.Text != "" && Year.Text != "")
            {
                int result = rental.deleteVideo(VideoId.Text);
                if (result == 1)
                {
                    MessageBox.Show("Video deleted successfully!");
                    VideoDataGridView.DataSource = rental.getVideoData();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot delete Video");
                }
            }
            else
            {
                string message = "fields are empty, please select the row to delete from table";
                MessageBox.Show(message);
            }
        }

        private void IssueVideo_Click(object sender, EventArgs e)
        {
            if (CustomerID.Text != "" && VideoId.Text != "")
            {
                int result = rental.rentVideo(CustomerID.Text, VideoId.Text);
                if (result == 1)
                {
                    MessageBox.Show("success!");
                    RentedVideosDataGridView.DataSource = rental.getAllRentedVideos();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot issue Video");
                }
            }
            else
            {
                string message = "fields are empty, please select the customer and Video rows to issue Video or enter customer and Video id";
                MessageBox.Show(message);
            }
        }

        private void ReturnVideo_Click(object sender, EventArgs e)
        {
            if (CustomerID.Text != "")
            {
                int result = rental.returnVideo(CustomerID.Text);
                if (result == 1)
                {
                    MessageBox.Show("success!");
                    RentedVideosDataGridView.DataSource = rental.getAllRentedVideos();
                    cleartextboxes();
                }
                else
                {
                    MessageBox.Show("Error: cannot return Video");
                }
            }
            else
            {
                string message = "fields are empty, please select the rented Video entry row or enter the RMID";
                MessageBox.Show(message);
            }
        }

        private void OutRented_CheckedChanged(object sender, EventArgs e)
        {
            RentedVideosDataGridView.DataSource = rental.getOutRentedVideos();
        }

        private void AllRented_CheckedChanged(object sender, EventArgs e)
        {
            RentedVideosDataGridView.DataSource = rental.getAllRentedVideos();
        }

    }
}
