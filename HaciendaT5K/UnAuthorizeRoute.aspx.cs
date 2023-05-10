using System;


namespace Eblue
{
    public partial class UnAuthorizeRoute : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Eblue.Code.LoadEventArgs eventArgs = new Code.LoadEventArgs(isUnAuthorize: true);

                base.Page_Load(sender, eventArgs);

                this.frienlymessage.Text = "You are trying to access a page that does not have authorization.";
            }
            else 
            {
                base.GoToHome();
            }
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            base.OnSaveStateCompleteExtend(this);

        }
        //protected override void OnSaveStateComplete(EventArgs e)
        //{
        //    var script = "$(document).ready(function () { $('[data-widget = \"pushmenu\"]').click(); });";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", script, true);
        //}
    }
}