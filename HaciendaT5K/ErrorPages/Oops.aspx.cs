using System;


namespace Eblue.ErrorPages
{
    public partial class Oops : Eblue.Code.PageBasic
    //System.Web.UI.Page
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Eblue.Code.LoadEventArgs eventArgs = new Code.LoadEventArgs(isError: true);

                base.Page_Load(sender, eventArgs);
                ExceptionMessageHandle();
            }
            else
            {
                base.GoToHome();
            }

            

        }


        protected void ExceptionMessageHandle()
        {


            // Create safe error messages.
            string generalErrorMsg = "A problem has occurred on this web site. Please try again. " +
                "If this error continues, please contact support.";
            //string httpErrorMsg = "An HTTP error occurred. Page Not found. Please try again.";
            //string unhandledErrorMsg = "";// utils.SessionTools.UnhandledErrorMsg;

            // Display safe error message.
            //FriendlyErrorMsg.Text = generalErrorMsg;
            frienlymessage.Text = generalErrorMsg;

            // Determine where error was handled.
            //string errorHandler = Request.QueryString["handler"];
            //if (errorHandler == null)
            //{
            //    errorHandler = "Error Page";
            //}

            // Get the last error from the server.
            Exception ex = Server.GetLastError();
            string errorMessage = string.Empty;
            if (ex != null)
            {

                errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                #region deprecated for the momment
                //if (ex.InnerException != null)
                //{
                //    if (ex.InnerException.InnerException != null)
                //    {
                //        frienlymessage.Text = $"{ex.InnerException.Message}, for more technical information: {ex.InnerException.InnerException.Message}";
                //    }
                //    else

                //    {
                //        frienlymessage.Text = $"{ex.InnerException.Message}, for more technical information: {ex.Message}";
                //    }

                //}
                //else
                //{
                //    errorMessage = ex.Message;
                //    //frienlymessage.Text = ex.Message;
                //}
                #endregion


                frienlymessage.Text = errorMessage;
            }

            else
            {

                var stop = true;
                if (stop)
                { }

                // Get the error number passed as a querystring value.
                //string errorMsg = Request.QueryString["msg"];
                //if (errorMsg == "404")
                //{
                //    ex = new HttpException(404, httpErrorMsg, ex);
                //    //FriendlyErrorMsg.Text = ex.Message;
                //    //frienlymessage.Text = ex.Message;
                //    unhandledErrorMsg = "the resource that you request not found";
                //}

                //if (string.IsNullOrEmpty(unhandledErrorMsg))
                //{
                //    unhandledErrorMsg = "first time that occurred this kind of error";
                //}

                //if (Request.IsLocal)
                //{
                //    frienlymessage.Text = $"Local Error: {unhandledErrorMsg} ";
                //}
                //else
                //{
                //    frienlymessage.Text = $"Production Error: {unhandledErrorMsg} ";
                //}

            }
            // Log the exception.
            //ExceptionUtility.LogException(ex, errorHandler);


            //utils.SessionTools.UnhandledErrorMsg = string.Empty;
            // Clear the error from the server.
            Server.ClearError();
        }

        //protected override void OnSaveStateComplete(EventArgs e)
        //{
        //    var script = "$(document).ready(function () { $('[data-widget = \"pushmenu\"]').click(); });";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", script, true);
        //}

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            base.OnSaveStateCompleteExtend(this);

        }
    }
}