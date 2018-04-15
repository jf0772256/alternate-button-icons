﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shipapp
{
    /// <summary>
    /// This class will allow the users to add edit users
    /// </summary>
    public partial class AddVendor : Form
    {
        // Class level variables
        private string message;
        private Models.Vendors vendorToBeEdited;


        public AddVendor(string message)
        {
            InitializeComponent();
            this.message = message;
        }


        public AddVendor(string message, object vendorToBeEdited)
        {
            InitializeComponent();
            this.message = message;
            this.vendorToBeEdited = (Models.Vendors)vendorToBeEdited;
        }


        /// <summary>
        /// IF the user trys to edit a vendor set the form to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddVendor_Load(object sender, EventArgs e)
        {
            // If EDIT set to edit mode
            if (message == "EDIT")
            {
                // Set textbox
                txtName.Text = vendorToBeEdited.VendorName;

                // Set button
                btnAdd.Text = "EDIT";
            }
        }


        /// <summary>
        /// Whene the user clicks this button it will check the data, add it to the DB, and close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Reset any errors on the form
            ResetError();

            // Call the correct method depeding on validation and the message
            if (ValidateData() && message == "ADD")
            {
                // Add a vendor
                AddVendorToDB();
                this.Close();
            }
            else if (ValidateData() && message == "EDIT")
            {
                // Edit a vendor
                EditVendor();
                this.Close();
            }
            else if(message != "ADD" && message != "EDIT")
            {
                // If something is wrong with the message
                MessageBox.Show("Their was a problem with the form loading./r/Try Again.", "Uh-oh", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }
            else
            {
                // If the form does not validate
                MessageBox.Show("All fields must have correct data!", "Uh-oh", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        /// <summary>
        /// Reset the back color after an error
        /// </summary>
        private void ResetError()
        {
            txtName.BackColor = Color.White;
        }


        /// <summary>
        /// Test the data before writing it to the database
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            // Method level variables
            bool pass = true;
            string errorMsg = "Check that all fields have correct data.\r\n";

            // Test data
            if (txtName.Text == "" || txtName.Text == null)
            {
                txtName.BackColor = Color.LightPink;
                pass = false;
                errorMsg += "\t-Must provide a name.\r\n";
            }

            // If pass fails provide the user with a n error message
            if (!pass)
            {
                MessageBox.Show(errorMsg, "Uh-oh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return pass;
        }


        /// <summary>
        /// Add vendor to the database
        /// </summary>
        public void AddVendorToDB()
        {
            // Create a vendor object
            Models.Vendors vendorToBeAdded = new Models.Vendors();

            // Fill vendor object
            vendorToBeAdded.VendorName = txtName.Text;

            // Write the data to the DB
            Connections.DataConnections.DataConnectionClass.VendorConn.AddVendor(vendorToBeAdded);
            Connections.DataConnections.DataConnectionClass.DataLists.Vendors.Add(Connections.DataConnections.DataConnectionClass.VendorConn.GetVendor(vendorToBeAdded.VendorId));
        }

        
        /// <summary>
        /// Edit a vendor in the DB
        /// </summary>
        public void EditVendor()
        {
            // Set new info
            vendorToBeEdited.VendorName = txtName.Text;

            // Edit the vendor
            Connections.DataConnections.DataConnectionClass.VendorConn.UpdateVendor(vendorToBeEdited);
        }
    }
}
