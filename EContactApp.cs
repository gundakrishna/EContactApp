using EContactApp.econtactAppClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContactApp
{
    public partial class EContactApp : Form
    {
        public EContactApp()
        {
            InitializeComponent();
        }

        ContactClass c = new ContactClass();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EContactApp_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Get the value from text box
            string keyword = txtSearch.Text;
            DataTable records = c.Search(keyword);
            load(records);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // call the clear method
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the contactId from the App
            if(txtContactId.Text != "")
            {
                c.ContactId = Convert.ToInt32(txtContactId.Text);
                bool success = c.Delete(c);
                if (success == true)
                {
                    //successfully deleted
                    MessageBox.Show("Contact Successfully Deleted!");
                    Clear();
                }
                else
                {
                    // Failed to delete the contact
                    MessageBox.Show("Failed to delete the Contact. Try Again!");
                }
                loadData();
            } else
            {
                MessageBox.Show("Please select the record to delete");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get the values from the input fields
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNo.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.Text;
            /*cmbGender.SelectedValue.ToString();*/

            // Inserting Data into Database using the method 
            bool success = c.Insert(c);
            if (success == true)
            {
                //successfully inserted
                MessageBox.Show("New Contact Successfully Inserted!");
                Clear();
            } else
            {
                // Failed to add the contact
                MessageBox.Show("Failed to add new Contact. Try Again!");
            }

            loadData();
        }
        // Method to clear all the fields
        private void Clear()
        {
            txtContactId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
        }
        //Load data on listview
        private void loadData()
        {
            //Load data on listview
            DataTable records = c.Select();
            load(records);
        }

        private void load(DataTable records)
        {
            // Clear the ListView control
            lvList.Items.Clear();

            // Set the view to show details. including column headers
            lvList.View = View.Details;
            // Allow the user to edit item text.
            lvList.LabelEdit = true;
            // Allow the user to rearrange columns.
            lvList.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            lvList.FullRowSelect = true;
            // Display grid lines.
            lvList.GridLines = true;
            // Sort the items in the list in ascending order.
            lvList.Sorting = SortOrder.Ascending;
            //lvList.Font = new Font(lvList.Font, FontStyle.Bold); // to make everything bold

            foreach (DataRow row in records.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                lvList.Columns[0].Width = 80; // First column minimum width
                lvList.Columns[0].Text.ToUpper();

                for (int i = 1; i < records.Columns.Count; i++)
                {
                    lvList.Columns[i].Width = 120; // rest of the columns minimum width
                    lvList.Columns[i].Text.ToUpper();
                    item.SubItems.Add(row[i].ToString());
                }
                lvList.Items.Add(item);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the contactId from the App
            if (txtContactId.Text != "")
            {
                // Get the data from text boxes
                c.ContactId = int.Parse(txtContactId.Text);
                c.FirstName = txtFirstName.Text;
                c.LastName = txtLastName.Text;
                c.ContactNo = txtContactNo.Text;
                c.Address = txtAddress.Text;
                c.Gender = cmbGender.Text;
            
                //Update the data in Database
                bool success = c.Update(c);
            
                if(success == true)
                {
                    //successfully updated
                    MessageBox.Show("Contact Successfully Updated!");
                    Clear();
                } else
                {
                    // Failed to update the contact
                    MessageBox.Show("Failed to update the Contact. Try Again!");
                }

                loadData();
            }
            else
            {
                MessageBox.Show("Please select the record to update");
            }
        }


        private void lvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Identify the row on which mouse is double clicked
            // and Get the data from listview and load it in text boxes
            ListViewItem item = lvList.SelectedItems[0];
            txtContactId.Text = item.SubItems[0].Text; //item.Text;
            txtFirstName.Text = item.SubItems[1].Text;
            txtLastName.Text = item.SubItems[2].Text;
            txtContactNo.Text = item.SubItems[3].Text;
            txtAddress.Text = item.SubItems[4].Text;
            cmbGender.Text = item.SubItems[5].Text;
        }
    }
}
