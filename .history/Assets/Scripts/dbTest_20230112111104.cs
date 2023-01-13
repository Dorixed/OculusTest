using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using MySql.Data.MySqlClient;

public class dbTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string sqlStr   = "Database=UserDataTest;Server=127.0.0.1;Uid=test;Password=123456;pooling=false;CharSet=utf8;port=3306";

        MySqlConnection mysql = new MySqlConnection(sqlStr);

        mysql.Open();

        Debug.Log ("Mysql Success");

        mysql.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
