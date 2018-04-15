﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shipapp.Models;
using System.Windows.Forms;
using shipapp.Connections.HelperClasses;

namespace shipapp.Connections.DataConnections.Classes
{
    class UserConnClass:DatabaseConnection
    {
        object Sender { get; set; }
        public Authenticating Authenticate { get; set; }
        public UserConnClass():base() { Authenticate = new Authenticating(); }
        public User Get1User(long id)
        {
            return GetUser(id);
        }
        public User Get1User(string username)
        {
            return GetUser(username);
        }
        public void Write1User(User user)
        {
            Write(user);
        }
        public void Update1User(User u)
        {
            Update(u);
        }
        public async void GetManyUsers(object sender = null)
        {
            if (String.IsNullOrWhiteSpace(DataConnectionClass.ConnectionString))
            {
                return;
            }
            Sender = sender;
            SortableBindingList<User> users = await Task.Run(() => GetUserList());
            if (Sender is Manage)
            {
                Manage t = (Manage)Sender;
                DataConnectionClass.DataLists.UsersList = users;
                BindingSource bs = new BindingSource
                {
                    DataSource = DataConnectionClass.DataLists.UsersList
                };
                t.dataGridView1.DataSource = bs;
                try
                {
                    t.dataGridView1.Columns["Id"].Visible = false;
                    t.dataGridView1.Columns["PassWord"].Visible = false;
                    t.dataGridView1.Columns["Person_Id"].Visible = false;
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                DataConnectionClass.DataLists.UsersList = users;
            }
        }
        public bool CheckAuth(User tester)
        {
            if (tester.Username == Authenticate.UserName && tester.PassWord == Authenticate.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteUser(User u)
        {
            Delete(u);
        }
    }
    class Authenticating
    {
        public Authenticating() { }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
