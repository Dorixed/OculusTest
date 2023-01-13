using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UserRegister : MonoBehaviour, IPointerClickHandler {
 
    public TMP_InputField userNameInput;
 
    public TMP_InputField passwordInput;
    //提示用户登录信息

    public Text RM;
    private Text loginMessage;
 
    //IP地址
    public string host;
    //端口号
    public string port;
    //用户名
    public string userName;
    //密码
    public string password;
    //数据库名称
    public string databaseName;
    //封装好的数据库类
    MySqlAccess mysql;
 
 
    private void Start() {
        // loginMessage = GameObject.FindGameObjectWithTag("LoginMessage").GetComponent<Text>();
        loginMessage=RM;
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
    }
 
    public void OnPointerClick(PointerEventData eventData) {
        
        if(eventData.pointerPress.name == "registButton"){
            Debug.Log("clicked Regist button");
            OnClickedRegistButton();
        }
    }
 

    /// <summary>
    /// 按下注册按钮
    /// </summary>
    private void OnClickedRegistButton() {
        mysql.OpenSql();
        string loginMsg = "";
        DataSet ds = mysql.Insert("usertable", new string[] { "`" + "account" + "`", "`" + "password" + "`" }, new string[] { userNameInput.text, passwordInput.text });
        loginMsg = "注册成功！请返回登录";
        loginMessage.color = Color.green;
        // if (ds != null) {
        //     DataTable table = ds.Tables[0];
        //     if (table.Rows.Count > 0) {
        //         loginMsg = "注册成功！";
        //         loginMessage.color = Color.green;
               
        //     } else {
        //         loginMsg = "用户名或密码错误！";
        //         loginMessage.color = Color.red;
        //     }
        //     loginMessage.text = loginMsg;
        // }
        mysql.CloseSql();
    }
 
}
