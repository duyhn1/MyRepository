﻿using MyKintaiInsight.DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyKintaiInsight.BusinessLayer
{
    public class UserBusiness
    {
        public int LoginSucess(string username, string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));

            var sqlDa = MySqlDa.GetInstance();
            var stp = "stp_loginSucess";
            List<MySqlParameter> paras = new List<MySqlParameter>();
            MySqlParameter paraUserName = new MySqlParameter("@username", username);
            MySqlParameter paraPassword = new MySqlParameter("@password", hashedBytes);
            paras.Add(paraUserName);
            paras.Add(paraPassword);
            return sqlDa.ExecuteNonQuery(stp, CommandType.StoredProcedure, paras);
        }
    }
}