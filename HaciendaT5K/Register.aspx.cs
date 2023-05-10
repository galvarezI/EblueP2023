using System;

using System.Web.UI;

using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;


namespace HaciendaT5K
{
    public partial class Registrarte : Eblue.Code.PageBasic
    {
        public static void SendEmail(string emailbody)
        {
            MailMessage mailmessage = new MailMessage("fmontanoxp@gmail.com", "fmontanoxp@gmail.com");
            //MailMessage mailmessage = new MailMessage("ebosquez1988@gmail.com", "ebosquez1988@gmail.com");
            mailmessage.Subject = "Confirmation NoReply: Eblue";
            mailmessage.Body = emailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailmessage);
        }

        public static string CreatRanSecurityCode(int PasswordLength)
        {
            string allowedChars = "0123456789abcdefghijkmnlopqrstuvwxyzABCDEFGHIJKMNLOPQRSTUVWXYZ";
            Random randnum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = allowedChars[(int)((allowedChars.Length) * randnum.NextDouble())];
            }
            return new string(chars);
        }
        protected new void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);

        }


        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            var Passwordvalue = Encryption.hashstring(Password.Text);
            var salt = Encryption.Genereatesalt();
            var hashedandsalted = Encryption.hashstring(string.Format("{0}{1}", Passwordvalue, salt));
            

            bool email_conf = false;
            var Action = "Register no verified";
            DateTime timestap = DateTime.Now;
            if (!string.IsNullOrEmpty(Email.Text))
            {
                if (CheckUsername(Email.Text.Trim()))
                    ErrorMessage.Text = ("This Account Already Exist");
                else
                    try
                    {
                        Guid newguid = Guid.NewGuid();
                        var securitycode = CreatRanSecurityCode(4);
                        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                        cn.Open();
                        string insertuser = "insert into Users (UID,Email,Salt,Password,SignUpDate,Action,ActionDate,EmailVerified,SecurityCode) values (@UID ,@email ,@Salt ,@password ,@SignUpDate ,@Action ,@ActionDate ,@EmailVerified ,@SecurityCode)";
                        SqlCommand cmd = new SqlCommand(insertuser, cn);
                        cmd.Parameters.AddWithValue("@UID", newguid);
                        cmd.Parameters.AddWithValue("@email", Email.Text);
                        cmd.Parameters.AddWithValue("@password", hashedandsalted);

                        //#if DEBUG
                        //                        
                        //#endif
                        email_conf = true;
                        cmd.Parameters.AddWithValue("@EmailVerified", email_conf);
                        cmd.Parameters.AddWithValue("@SignUpDate", timestap);
                        cmd.Parameters.AddWithValue("@Action", Action);
                        cmd.Parameters.AddWithValue("@ActionDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Salt", salt);
                        cmd.Parameters.AddWithValue("@SecurityCode", securitycode);

                        cmd.ExecuteNonQuery();
                        string emailmessage = ("Your account for " + Email.Text + " has been register please verify your e-mail using the following code" + securitycode + " please use your activation code in the following link") ;
                        SendEmail(emailmessage.ToString());
                        ErrorMessage.Text = "Your account as been created and your email has been sent.";
                        cn.Close();

                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Error Message " + ex + "')</script>");
                    }
            }

        }

        public bool CheckUsername(string Email_text)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Users where Email = @email", cn))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@email";
                    param.Value = Email_text;
                    cmd.Parameters.Add(param);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                    else
                        return false;
                }
            }
        }
    }




}