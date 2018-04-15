﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shipapp.Models.ModelData;

namespace shipapp.Models
{
    /// <summary>
    /// User class are employees of receiving department
    /// </summary>
    class User
    {
        /// <summary>
        /// Employee Id generated from the database -- Do not self assign, Do not modify
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Employee First name as string
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Employee Last name as string
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Employee Role id/level as Role
        /// </summary>
        public Role Level { get; set; }
        /// <summary>
        /// Employee Application Password as string
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// Employee Username as string
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// List of notes on Employee as List of Note
        /// </summary>
        public List<Note> Notes { get; set; }
        /// <summary>
        /// Id value unique across all main tables to reference support tables AS String
        /// </summary>
        public string Person_Id { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        public User()
        {
            Notes = new List<Note>() { };
        }
        /// <summary>
        /// Returns users name in standard english read out (first name last name) with a space between the first and last name
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
        /// <summary>
        /// Formatted string for lists (last name, first name)
        /// </summary>
        /// <returns>String</returns>
        public string ToFormattedString()
        {
            return LastName + ", " + FirstName;
        }
    }
}
