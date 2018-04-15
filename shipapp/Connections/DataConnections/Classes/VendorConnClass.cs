﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shipapp.Models;
using System.Windows.Forms;
using shipapp.Connections.HelperClasses;

namespace shipapp.Connections.DataConnections.Classes
{
    /// <summary>
    /// Vendor connection interface access only through DataConnectionClass
    /// </summary>
    class VendorConnClass:DatabaseConnection
    {
        object Sender { get; set; }
        public VendorConnClass():base() { }
        public Vendors GetVendor(long id)
        {
            return GetVendor_From_Database(id);
        }
        public async void GetVendorList(object sender = null)
        {
            if (String.IsNullOrWhiteSpace(DataConnectionClass.ConnectionString))
            {
                return;
            }
            Sender = sender;
            SortableBindingList<Vendors> vend = await Task.Run(() => GetVendorsList());
            if (Sender is Manage)
            {
                Manage t = (Manage)Sender;
                DataConnectionClass.DataLists.Vendors = vend;
                BindingSource bs = new BindingSource
                {
                    DataSource = DataConnectionClass.DataLists.Vendors
                };
                t.dataGridView1.DataSource = bs;
                try
                {
                    t.dataGridView1.Columns["VendorId"].Visible = false;
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                DataConnectionClass.DataLists.Vendors = vend;
            }
        }
        public void AddVendor(Vendors value)
        {
            Write(value);
        }
        /// <summary>
        /// do not use yet.
        /// </summary>
        public void WriteAllVendors() { }
        public void UpdateVendor(Vendors value)
        {
            Update(value);
        }
        public void DeleteVendor(Vendors v)
        {
            Delete(v);
        }
    }
}
