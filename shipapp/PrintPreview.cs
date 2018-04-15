﻿using shipapp.Connections.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extentions;

namespace shipapp
{
    public partial class PrintPreview : Form
    {
        // Class level variabels
        private string clerk = "Null!";
        private int identity = 0;
        private BindingList<Log> logs;
        private BindingList<Models.Package> packages;
        private BindingList<Models.Faculty> Faculties;
        private BindingList<Models.Vendors> vendors;
        private BindingList<Models.Carrier> carriers;
        private BindingList<Models.ModelData.BuildingClass> buildings;
        private BindingList<Models.User> users;
        private List<Models.Package> printPackages;


        /// <summary>
        /// Constructor: Set form accroding to list type
        /// </summary>
        /// <param name="list"></param>
        public PrintPreview(Object list, int identity, object packages)
        {
            InitializeComponent();

            CreateCorrectPrintForm(identity, list, packages);
        }


        /// <summary>
        /// Print the log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //
            if (identity == 1)
            {
                if (String.IsNullOrWhiteSpace(cmboClerk.SelectedItem.ToString()))
                {
                    MessageBox.Show("You must select a clerk to deleiver the packages!", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Print();
                    UpdatePackageListWithClerk();
                    this.Close();
                }
            }
            else
            {
                Print();
                this.Close();
            }
        }


        public void Print()
        {
            // Create print object
            DGVPrinter printer = new DGVPrinter();

            // Set the print obejct page settings 
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.FooterSpacing = 15;
            printer.PageSettings.Landscape = true;
            printer.PrintMargins = new System.Drawing.Printing.Margins(10, 45, 30, 20);
            printer.ShowTotalPageNumber = true;

            // Set column widths
            if (identity == 1)
            {
                //
                printer.Title = "Delivery Log";
                printer.SubTitle = "Clerk: " + clerk;
                printer.SubTitle += ", Date: " + DateTime.Today.ToShortDateString();

                //
                dataGridLog.Columns[0].Width = 35;
                dataGridLog.Columns[1].Width = 50;
                dataGridLog.Columns[2].Width = 35;
                dataGridLog.Columns[3].Width = 110;
                dataGridLog.Columns[4].Width = 35;
                dataGridLog.Columns[5].Width = 85;
                dataGridLog.Columns[6].Width = 125;

                //
                UpdatePackages();
            }
            else if (identity == 2)
            {
                //
                printer.Title = "History";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
                dataGridLog.Columns[2].Width = 50;
                dataGridLog.Columns[3].Width = 50;
                dataGridLog.Columns[4].Width = 50;
                dataGridLog.Columns[5].Width = 50;
                dataGridLog.Columns[6].Width = 50;
                dataGridLog.Columns[7].Width = 50;
                dataGridLog.Columns[8].Width = 50;
                dataGridLog.Columns[9].Width = 50;
            }
            else if (identity == 3)
            {
                //
                printer.Title = "Users";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
                dataGridLog.Columns[2].Width = 50;
                dataGridLog.Columns[3].Width = 50;
                dataGridLog.Columns[4].Width = 50;
                dataGridLog.Columns[5].Width = 50;
            }
            else if (identity == 4)
            {
                //
                printer.Title = "Vendors";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();
                printer.PageSettings.Landscape = false;

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
            }
            else if (identity == 5)
            {
                //
                printer.Title = "Faculty";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();
                printer.PageSettings.Landscape = false;

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
                dataGridLog.Columns[2].Width = 50;
                dataGridLog.Columns[3].Width = 50;
            }
            else if (identity == 6)
            {
                //
                printer.Title = "Buildings";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();
                printer.PageSettings.Landscape = false;

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
                dataGridLog.Columns[2].Width = 50;
            }
            else if (identity == 7)
            {
                //
                printer.Title = "Carriers";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();
                printer.PageSettings.Landscape = false;

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
            }
            else if (identity == 8)
            {
                //
                printer.Title = "Activity History";
                printer.SubTitle += "Date: " + DateTime.Today.ToShortDateString();

                //
                dataGridLog.Columns[0].Width = 50;
                dataGridLog.Columns[1].Width = 50;
                dataGridLog.Columns[2].Width = 50;
                dataGridLog.Columns[3].Width = 50;
            }
            else
            {
                MessageBox.Show("Something has gone wrong.\r\nPlease try again", "Uh-oh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Print the Object
            printer.PrintDataGridView(dataGridLog);
        }


        /// <summary>
        ///  Make the PO pretty
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public string PadPO(int newValue)
        {
            if (newValue <= 9)
            {
                return "0000" + newValue.ToString();
            }
            else if (newValue > 9 && newValue < 100)
            {
                return "000" + newValue.ToString();
            }
            else if (newValue > 99 && newValue < 1000)
            {
                return "00" + newValue.ToString();
            }
            else if (newValue > 999 && newValue < 10000)
            {
                return "0" + newValue.ToString();
            }
            else
            {
                return newValue.ToString();
            }
        }


        /// <summary>
        /// Returns a string containing the clerk assigned to the log
        /// </summary>
        /// <returns></returns>
        public string UpdatePackageListWithClerk()
        {
            return clerk;
        }


        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Select a clerk to deliver the package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboClerk_SelectedIndexChanged(object sender, EventArgs e)
        {
            clerk = cmboClerk.SelectedItem.ToString();
        }


        public void CreateCorrectPrintForm(int identity, Object list, Object packages)
        {
            this.identity = identity;

            // Determine list type
            if (identity == 1)
            {
                // List is Package
                this.logs = (BindingList<Log>)list;
                this.printPackages = (List<Models.Package>)packages;
                dataGridLog.DataSource = logs;
                cmboClerk.Show();
                btnPrint.Enabled = false;
            }
            else if (identity == 2)
            {
                // List is Package History TODO
                this.packages = (BindingList<Models.Package>)list;
                dataGridLog.DataSource = this.packages;
                cmboClerk.Hide();

                //
                dataGridLog.Columns["PackageId"].Visible = false;
                dataGridLog.Columns["Package_PersonId"].Visible = false;
                dataGridLog.Columns["PONumber"].HeaderText = "PO Number";
                dataGridLog.Columns["PackageCarrier"].HeaderText = "Carrier";
                dataGridLog.Columns["PackageVendor"].HeaderText = "Vendor";
                dataGridLog.Columns["PackageDeliveredTo"].HeaderText = "Delivered To";
                dataGridLog.Columns["PackageDeleveredBy"].HeaderText = "Delivered By";
                dataGridLog.Columns["PackageSignedForBy"].HeaderText = "Signed For By";
                dataGridLog.Columns["PackageTrackingNumber"].HeaderText = "Tracking Number";
                dataGridLog.Columns["PackageReceivedDate"].HeaderText = "Received Date";
                dataGridLog.Columns["PackageDeliveredDate"].HeaderText = "Delivered Date";
                dataGridLog.Columns["Status"].HeaderText = "Delivery Status";
                dataGridLog.Columns["DelivBuildingShortName"].HeaderText = "Deliver To Short Name";
            }
            else if (identity == 3)
            {
                // List is Users
                this.users = (BindingList<Models.User>)list;
                dataGridLog.DataSource = users;
                cmboClerk.Hide();

                //
                dataGridLog.Columns[0].Visible = false;
                dataGridLog.Columns[1].HeaderText = "First Name";
                dataGridLog.Columns[2].HeaderText = "Last Name";
                dataGridLog.Columns[3].HeaderText = "Role";
                dataGridLog.Columns[4].Visible = false;
                dataGridLog.Columns[6].Visible = false;

            }
            else if (identity == 4)
            {
                // List is Vendor
                this.vendors = (BindingList<Models.Vendors>)list;
                dataGridLog.DataSource = vendors;
                cmboClerk.Hide();

                //
                dataGridLog.Columns[0].Visible = false;
                dataGridLog.Columns[1].HeaderText = "Vendor";
            }
            else if (identity == 5)
            {
                // List is Faculty
                this.Faculties = (BindingList<Models.Faculty>)list;
                dataGridLog.DataSource = Faculties;
                cmboClerk.Hide();

                //
                dataGridLog.Columns[0].Visible = false;
                dataGridLog.Columns[1].Visible = false;
                dataGridLog.Columns[2].HeaderText = "First Name";
                dataGridLog.Columns[3].HeaderText = "Last Name";
                dataGridLog.Columns[4].Visible = false;
                dataGridLog.Columns[5].HeaderText = "Building";
                dataGridLog.Columns[6].HeaderText = "Room #";
            }
            else if (identity == 6)
            {
                // List is Building
                this.buildings = (BindingList<Models.ModelData.BuildingClass>)list;
                dataGridLog.DataSource = buildings;
                cmboClerk.Hide();

                //
                dataGridLog.Columns[0].Visible = false;
                dataGridLog.Columns[1].HeaderText = "Long Name";
                dataGridLog.Columns[2].HeaderText = "Short Name";
            }
            else if (identity == 7)
            {
                // List is Carrier
                this.carriers = (BindingList<Models.Carrier>)list;
                dataGridLog.DataSource = carriers;
                cmboClerk.Hide();

                //
                dataGridLog.Columns[0].Visible = false;
                dataGridLog.Columns[1].HeaderText = "Carrier";
            }
            else if (identity == 8)
            {
                // List is Activity History TODO
                this.logs = (BindingList<Log>)list;
                dataGridLog.DataSource = logs;
                cmboClerk.Hide();
            }
            else
            {
                MessageBox.Show("Something went wrong\r\nTry again.", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        public void UpdatePackages()
        {
            for (int i = 0; i < printPackages.Count; i++)
            {
                printPackages[i].PackageDeleveredBy = clerk;
                printPackages[i].Status = (Models.Package.DeliveryStatus)2;
                Connections.DataConnections.DataConnectionClass.PackageConnClass.UpdatePackage(printPackages[i]);
            }
        }

        private void PrintPreview_Load(object sender, EventArgs e)
        {
            Connections.DataConnections.DataConnectionClass.DataLists.UsersList.ForEach(i => cmboClerk.Items.Add(i));
        }

        private void cmboClerk_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("You left!");
        }

        private void cmboClerk_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnPrint.Enabled = true;
        }
    }
}
