﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCF.DTO
{
    public class Table
    {
        public Table(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string status, name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
