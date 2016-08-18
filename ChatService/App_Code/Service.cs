using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://dsweb.tmd.hv.se/users/wsi400/ChatService/", Description = "WebService för chatclient")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    int iLastMessageId;
    int iLastMessId;
    int iMessDiff;
    string[] Mess = new string[11];
    string[] arrMessages1 = new string[11];
    string[] arrMessages2 = new string[11];


    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetMessages(int iLastMessId)
    {
        //arrMessages[i] = i.ToString();
        int i;

        if (arrMessages2[10] != null)
        {
            if (iLastMessId != null)
            {
                Mess = RetrieveMessages();
                iLastMessId = Convert.ToInt32(arrMessages2[10]);
                iMessDiff = Convert.ToInt32(Mess[10]);
                iMessDiff = iMessDiff - iLastMessId;

            }
        }

        if (iMessDiff <= 10)
        {
            iMessDiff = 10;
        }

        for (i = iMessDiff; i <= 0; i++)
        {
            if (arrMessages2[i] != null)
            {
                arrMessages1[i] = arrMessages2[i];
            }
        }

        arrMessages2 = RetrieveMessages();

        return arrMessages2;
    }

    [WebMethod]
    public int LastMessageId()
    {
       
        if (Application["LastMessageId"] != null)
        {
            iLastMessageId = int.Parse(Application["LastMessageId"].ToString());
            
        }
        else
        {
            StoreMessageId(0);
        }

        return iLastMessageId;
    }

    [WebMethod]
    public string SendMessage(string Pass, string sUserName, string sMessage)
    {
        if (Pass == "babbel")
        {
            string[] arrMessages = new string[10];
            int iLastMessageId = LastMessageId();
            string sNow = System.DateTime.Now.ToString();

            arrMessages = RetrieveMessages();

            for (int i = 9; i >= 1; i--)
            {
                arrMessages[i] = arrMessages[i - 1];
            }
            
            arrMessages[0] = sUserName + " (" + sNow + "): " + sMessage;
            iLastMessageId++;

            StoreMessageId(iLastMessageId);

            arrMessages[10] = iLastMessageId.ToString();

            StoreMessages(arrMessages);

            return "1";
        }
        else
        {
            return "-1";
        }
    }

    [WebMethod]
    public string Test()
    {
        return "OK from Linus";
    }

    public void StoreMessageId(int iIn)
    {
        Application["LastMessageId"] = iIn;
    }

    public void StoreMessages(string[] arrMessagesIn)
    {
        Application["Messages"] = arrMessagesIn;
    }

    public string[] RetrieveMessages()
    {
        string[] arrOut = new string[11];

        if (Application["Messages"] != null)
        {
            arrOut = (string[])Application["Messages"];
        }
        else
        {
            StoreMessages(arrOut);
        }

        return arrOut;
    }
}