using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;

public class MySqlAccess {
 
    //连接类对象
    private static MySqlConnection mySqlConnection;
    //IP地址
    private static string host;
    //端口号
    private static string port;
    //用户名
    private static string userName;
    //密码
    private static string password;
    //数据库名称
    private static string databaseName;
 
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_host">ip地址</param>
    /// <param name="_userName">用户名</param>
    /// <param name="_password">密码</param>
    /// <param name="_databaseName">数据库名称</param>
    public MySqlAccess(string _host,string _port, string _userName, string _password, string _databaseName) {
        host = _host;
        port = _port;
        userName = _userName;
        password = _password;
        databaseName = _databaseName;
        OpenSql();
    }
 
    /// <summary>
    /// 打开数据库
    /// </summary>
    public void OpenSql() {
        try {
            string mySqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4}"
                , databaseName, host, userName, password, port);
            // Debug.Log("databaseName "+databaseName);
            // Debug.Log("host "+host);
            // Debug.Log("userName "+userName);
            // Debug.Log("password "+password);
            // string sqlStr   = "Database=UserDataTest;Server=127.0.0.1;Uid=test;Password=123456;pooling=false;CharSet=utf8;port=3306";
            
            mySqlConnection = new MySqlConnection(mySqlString);
            
            //if(mySqlConnection.State == ConnectionState.Closed)
            mySqlConnection.Open();
            Debug.Log ("Mysql State: " + mySqlConnection.State);
 
        } catch (Exception e) {
            throw new Exception("服务器连接失败，请重新检查MySql服务是否打开。" + e.Message.ToString());
        }
 
    }
 
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseSql() {
        if (mySqlConnection != null) {
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            mySqlConnection = null;
        }
    }
 
    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">要查询的列</param>
    /// <param name="whereColumnName">查询的条件列</param>
    /// <param name="operation">条件操作符</param>
    /// <param name="value">条件的值</param>
    /// <returns></returns>
    public DataSet Select(string tableName, string[] items, string[] whereColumnName,
        string[] operation, string[] value) {
 
        if (whereColumnName.Length != operation.Length || operation.Length != value.Length) {
            throw new Exception("输入不正确：" + "要查询的条件、条件操作符、条件值 的数量不一致！");
        }
        string query = "Select " + items[0];
        for (int i = 1; i <items.Length; i++) {
            query += "," + items[i];
        }
 
        query += " FROM " + tableName + " WHERE " + whereColumnName[0] + " " + operation[0] + " '" + value[0] + "'";
        for (int i = 1; i < whereColumnName.Length; i++) {
            query += " and " + whereColumnName[i] + " " + operation[i] + " '" + value[i] + "'";
        }
        return QuerySet(query);
 
    }
 
    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sqlString">sql语句</param>
    /// <returns></returns>
    private DataSet QuerySet(string sqlString) {
        if (mySqlConnection.State == ConnectionState.Open) {
            DataSet ds = new DataSet();
            try {
                MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                mySqlAdapter.Fill(ds);
            } catch (Exception e) {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            } finally {
            }
            return ds;
        }
        return null;
    }





    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">要查询的列</param>
    /// <param name="whereColumnName">查询的条件列</param>
    /// <param name="operation">条件操作符</param>
    /// <param name="value">条件的值</param>
    /// <returns></returns>
    public DataSet Insert(string tableName, string[] items, string[] whereColumnName,
        string[] operation, string[] value) {
 
        if (whereColumnName.Length != operation.Length || operation.Length != value.Length) {
            throw new Exception("输入不正确：" + "要查询的条件、条件操作符、条件值 的数量不一致！");
        }
        string query = "Insert INTO " + items[0];
        for (int i = 1; i <items.Length; i++) {
            query += "," + items[i];
        }
 
        query += " FROM " + tableName + " WHERE " + whereColumnName[0] + " " + operation[0] + " '" + value[0] + "'";
        for (int i = 1; i < whereColumnName.Length; i++) {
            query += " and " + whereColumnName[i] + " " + operation[i] + " '" + value[i] + "'";
        }
        return InsertSet(query);
 
    }
 
    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sqlString">sql语句</param>
    /// <returns></returns>
    private DataSet InsertSet(string sqlString) {
        if (mySqlConnection.State == ConnectionState.Open) {
            DataSet ds = new DataSet();
            try {
                MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                mySqlAdapter.Fill(ds);
            } catch (Exception e) {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            } finally {
            }
            return ds;
        }
        return null;
    }
}
