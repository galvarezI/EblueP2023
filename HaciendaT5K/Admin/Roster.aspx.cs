using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace HaciendaT5K.Admin
{
    public partial class RosterAdmin : Eblue.Code.PageBasic
    {
        protected new  void Page_Load(object sender, EventArgs e)
        {
            MarkTabIndexWebControls(TextBoxRosterName, DropDownListUser, DropDownListUserType, DropDownListDepartment, cmbLocation, DropDownListCanBePI
                , FileSignature, UploadButtonSignature, FilePicture, UploadButtonPicture,
                buttonNewModel, buttonClearModel, gv);

            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                SignatureValue.Value = string.Empty;
                ImageSignature.Visible = false;
                PictureValue.Value = string.Empty;
                ImagePicture.Visible = false;
            }
            else {
                //gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                ////here function to clear literalcontrol from gridview commands
                //if (string.IsNullOrEmpty( DropDownListUser.SelectedValue))
                //{
                //    //DropDownListUser.Items.Clear();
                //    //DropDownListUser.Items.Add(new ListItem(" ", ""));
                //    //DropDownListUser.DataBind();
                //}
                
                

            }

            base.PageEventLoadPostBackForGridViewHeaders(gv);

        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            base.OnSaveStateCompleteExtend(this);
            //if (!Page.IsPostBack)
            //{
                //var statementJS = "var tbl = $('table'); $(tbl).DataTable({'paging': true,'lengthChange': false,'searching': false,'ordering': true,'info': true,'autoWidth': false }); " +
                //" $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡');";

                //var script = $"$(document).ready(function () {{{statementJS}}});";
                //ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);


            //}
        }

        public void BindRecallUsers()
        {

            DropDownListUser.Items.Clear();


            SqlDataSourceUser.DataBind();
            DropDownListUser.DataBind();
            DropDownListUser.Items.Insert(0, new ListItem("", " "));

        }

        protected void Clear()
        {
            //DropDownListParent.Items.Clear();


            //SqlDataSourceParent.DataBind();
            //DropDownListParent.DataBind();
            //DropDownListParent.Items.Insert(0, new ListItem("None", ""));
            BindRecallUsers();
            ClearWebControls(TextBoxRosterName, DropDownListUser, DropDownListUserType, DropDownListDepartment, cmbLocation, DropDownListCanBePI
                , FileSignature, UploadButtonSignature, FilePicture, UploadButtonPicture,
                buttonNewModel, buttonClearModel, gv);
        }

        protected void ButtonNewModel_Click(object sender, EventArgs e)
        {
            Guid NewID = Guid.NewGuid();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string InsertNewProject = "INSERT INTO Roster (RosterID, RosterName, DepartmentID, LocationID, CanBePI, UID, RosterCategoryId, Signature, picture) VALUES (@RosterID, @RosterName, @DepartmentID, @LocationID, @CanBePI, @UID, @RosterCategoryId, @Signature, @picture, @RoleCategoryId)";
                var userTypeSelected = DropDownListUserType.SelectedValue;
                var roletypeSelected = DropDownListRoleType.SelectedValue;
                SqlCommand cmd = new SqlCommand(InsertNewProject, cn);
                cmd.Parameters.AddWithValue("@RosterID", NewID);
                cmd.Parameters.AddWithValue("@RosterName", TextBoxRosterName.Text);
                cmd.Parameters.AddWithValue("@DepartmentID",DropDownListDepartment.SelectedValue);
                cmd.Parameters.AddWithValue("@LocationID", cmbLocation.SelectedValue);
                cmd.Parameters.AddWithValue("@CanBePI", DropDownListCanBePI.SelectedValue);
                cmd.Parameters.AddWithValue("@UID", DropDownListUser.SelectedValue);
                cmd.Parameters.AddWithValue("@RosterCategoryId", userTypeSelected);
                var signatureValue = SignatureValue.Value?.Trim();
                var pictureValue = PictureValue.Value?.Trim();
                cmd.Parameters.AddWithValue("@RoleCategoryId", roletypeSelected);

                if (!string.IsNullOrEmpty(signatureValue))
                {
                    cmd.Parameters.AddWithValue("@Signature", signatureValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Signature", string.Empty);
                }

                if (!string.IsNullOrEmpty(pictureValue))
                {
                    cmd.Parameters.AddWithValue("@picture", pictureValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@picture", string.Empty);
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                var listitemSelected = DropDownListUser.Items.FindByValue(DropDownListUser.SelectedValue);
                var indexSelected = 0;
                if (listitemSelected != null)
                {
                    indexSelected = DropDownListUser.Items.IndexOf(listitemSelected);
                }

                //DropDownListUser.ClearSelection();

                //DropDownListUser.Items.RemoveAt(indexSelected);

                //DropDownListUserType.ClearSelection();
                //DropDownListDepartment.ClearSelection();
                //cmbLocation.ClearSelection();
                //DropDownListCanBePI.ClearSelection();
                
                SignatureStatusLabel.Text = string.Empty;
                SignatureValue.Value = string.Empty;
                ImageSignature.Visible = false;

                PictureStatusLabel.Text = string.Empty;
                PictureValue.Value = string.Empty;
                ImagePicture.Visible = false;

                Clear();
                //gv.Databind();
                gv.DataBind();
                SetFocus(TextBoxRosterName);

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add a roster", ex);
            }
            
        }

        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(TextBoxRosterName);
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            SignatureStatusLabel.Text = string.Empty;
            ImageSignature.Visible = false;
            if (FileSignature.HasFile)
            {
                try
                {

                    //using (System.Drawing.Image img = System.Drawing.Image.FromStream(FileSignature.FileContent))
                    //{
                    //    using (System.IO. MemoryStream m = new System.IO.MemoryStream())
                    //    {
                    //        img.Save(m, img.RawFormat);
                    //        byte[] imageBytes = m.ToArray();

                    //        // Convert byte[] to Base64 String
                    //        string signString = Convert.ToBase64String(imageBytes);
                    //        string contentType = FileSignature.PostedFile.ContentType;
                    //        //return base64String;
                    //        //var bytes = FileSignature.FileBytes ?? new byte[] { };
                    //        //var signString = Convert.ToBase64String(bytes);
                    //        string imageUrl = $"data:{contentType};base64,{signString}";
                    //        ImageSignature.Width = img.Width;
                    //        ImageSignature.Height = img.Height;
                    //        ImageSignature.ImageUrl = imageUrl;

                    FileUpload fu = FileSignature;
                    Image img = ImageSignature;

                    SetUrlImageFromFileUploadAndDimensions(fileUpload: fu, image: img);

                    ImageSignature.Visible = true;
                    //string filename = Path.GetFileName(FileUploadControl.FileName);
                    //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    //StatusLabel.Text = "Upload status: File uploaded!";
                    SignatureValue.Value = img.ImageUrl; // imageUrl;
                    SignatureStatusLabel.Text = "Signature Upload status: The file has been uploaded ";
                    //}

                    //}
                    //    var bytes = FileSignature.FileBytes ?? new byte[] { };
                    //var signString = Convert.ToBase64String(bytes);

                    //ImageSignature.ImageUrl = $"{signString}";
                    ////string filename = Path.GetFileName(FileUploadControl.FileName);
                    ////FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    ////StatusLabel.Text = "Upload status: File uploaded!";
                    //SignatureValue.Value = signString;
                    //SignatureStatusLabel.Text = "Signature Upload status: The file ha be uploaded ";
                }
                catch (Exception ex)
                {
                    SignatureStatusLabel.Text = "Signature Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else {
                SignatureValue.Value = string.Empty;
                SignatureStatusLabel.Text = "Signature Upload status: You must select a image file ";
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton lnkBtnDelete = e.Row.FindControl("lnkBtnDelete") as LinkButton;
                System.Web.UI.Control ctrl = e.Row.Cells[7].Controls[2];

                if (((System.Web.UI.WebControls.Button)ctrl).Text == "Delete" || ((System.Web.UI.WebControls.Button)ctrl).Text=="-")
                {
                    ((System.Web.UI.WebControls.Button)ctrl).OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";

                    ((System.Web.UI.LiteralControl)e.Row.Cells[7].Controls[1]).Text = "";
                }
                
                // Use whatever control you want to show in the confirmation message
                //Label lblContactName = e.Row.FindControl("lblContactName") as Label;

                //lnkBtnDelete.Attributes.Add("onclick", string.Format("return confirm('Are you sure you want to delete the contact {0}?');", lblContactName.Text));

            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.TableSection = TableRowSection.TableHeader;


            }

        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (int.TryParse(e.CommandArgument?.ToString(), out int rowIndex))
            {
                //Reference the GridView Row.
                GridViewRow row = gv.Rows[rowIndex];
                FileUpload fu  ;
                Image img  ;

                switch (e.CommandName)
                {
                    case "ChangeSignatureEdition":
                        try
                        {
                             fu = row.FindControl("FileSignatureEdition") as FileUpload;
                             img = row.FindControl("ImageSignatureEdition") as Image;
                            SetUrlImageFromFileUpload(fileUpload: fu, image: img);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"At (ChangeSignature) an error occurred while trying to upload a file to update the roster's signature", ex);
                        }
                        break;

                    case "RemoveSignatureEdition":
                        try
                        {                            
                            img = row.FindControl("ImageSignatureEdition") as Image;
                            img.ImageUrl = null;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"At (RemoveSignature) an error occurred while trying to remove a signature to update the roster's signature", ex);
                        }
                        break;

                    case "ChangePictureEdition":
                        try
                        {
                            fu = row.FindControl("FilePictureEdition") as FileUpload;
                            img = row.FindControl("ImagePictureEdition") as Image;
                            SetUrlImageFromFileUpload(fileUpload: fu, image: img);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"At (ChangePicture) an error occurred while trying to upload a file to update the roster's Picture", ex);
                        }
                        break;

                    case "RemovePictureEdition":
                        try
                        {
                            img = row.FindControl("ImagePictureEdition") as Image;
                            img.ImageUrl = null;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"At (RemovePicture) an error occurred while trying to remove a Picture to update the roster's Picture", ex);
                        }
                        break;

                    case "Delete":
                        //BindRecallUsers();
                        //SqlDataSourceUser.DataBind();
                        //DropDownListUser.Items.Clear();
                        //DropDownListUser.Items.Add(new ListItem(" ", ""));
                        //DropDownListUser.DataBind();
                        break;
                }

                


            }
            
            //if (e.CommandName == "ChangeSignatureEdition")
            //{
            //    //Determine the RowIndex of the Row whose Button was clicked.
            //    int rowIndex;

                     

            //    //Fetch value of Name.
            //    //string name = (row.FindControl("txtName") as TextBox).Text;

            //    ////Fetch value of Country
            //    //string country = row.Cells[1].Text;

            //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            //}
        }

        private void SetUrlImageFromFileUpload(FileUpload fileUpload, Image image)
        {
            //string result = string.Empty;
            //SignatureStatusLabel.Text = string.Empty;
            //ImageSignature.Visible = false;
            if (fileUpload.HasFile)
            {
                

                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(fileUpload.FileContent))
                    {
                        using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                        {
                            img.Save(m, img.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string signString = Convert.ToBase64String(imageBytes);
                            string contentType = FileSignature.PostedFile.ContentType;
                            //return base64String;
                            //var bytes = FileSignature.FileBytes ?? new byte[] { };
                            //var signString = Convert.ToBase64String(bytes);
                            string imageUrl = $"data:{contentType};base64,{signString}";
                            
                            image.ImageUrl = imageUrl;
                            image.Visible = true;
                            //string filename = Path.GetFileName(FileUploadControl.FileName);
                            //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                            //StatusLabel.Text = "Upload status: File uploaded!";
                            //SignatureValue.Value = imageUrl;
                            //SignatureStatusLabel.Text = "Signature Upload status: The file has been uploaded ";
                        }

                    }
                    //    var bytes = FileSignature.FileBytes ?? new byte[] { };
                    //var signString = Convert.ToBase64String(bytes);

                    //ImageSignature.ImageUrl = $"{signString}";
                    ////string filename = Path.GetFileName(FileUploadControl.FileName);
                    ////FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    ////StatusLabel.Text = "Upload status: File uploaded!";
                    //SignatureValue.Value = signString;
                    //SignatureStatusLabel.Text = "Signature Upload status: The file ha be uploaded ";
                
                
            }
            else
            {
                //SignatureValue.Value = string.Empty;
                //SignatureStatusLabel.Text = "Signature Upload status: You must select a image file ";
            }

        }

        private void SetUrlImageFromFileUploadAndDimensions(FileUpload fileUpload, Image image)
        {            
            //SignatureStatusLabel.Text = string.Empty;
            //ImageSignature.Visible = false;
            if (fileUpload.HasFile)
            {
                
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(fileUpload.FileContent))
                    {
                        using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                        {
                            img.Save(m, img.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string signString = Convert.ToBase64String(imageBytes);
                            string contentType = FileSignature.PostedFile.ContentType;
                            //return base64String;
                            //var bytes = FileSignature.FileBytes ?? new byte[] { };
                            //var signString = Convert.ToBase64String(bytes);
                            string imageUrl = $"data:{contentType};base64,{signString}";
                            image.Width = img.Width;
                            image.Height = img.Height;
                            image.ImageUrl = imageUrl;
                            image.Visible = true;
                            //string filename = Path.GetFileName(FileUploadControl.FileName);
                            //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                            //StatusLabel.Text = "Upload status: File uploaded!";
                            //SignatureValue.Value = imageUrl;
                            //SignatureStatusLabel.Text = "Signature Upload status: The file has been uploaded ";
                        }

                    }
                    //    var bytes = FileSignature.FileBytes ?? new byte[] { };
                    //var signString = Convert.ToBase64String(bytes);

                    //ImageSignature.ImageUrl = $"{signString}";
                    ////string filename = Path.GetFileName(FileUploadControl.FileName);
                    ////FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    ////StatusLabel.Text = "Upload status: File uploaded!";
                    //SignatureValue.Value = signString;
                    //SignatureStatusLabel.Text = "Signature Upload status: The file ha be uploaded ";               
                
            }
            else
            {
                //SignatureValue.Value = string.Empty;
                //SignatureStatusLabel.Text = "Signature Upload status: You must select a image file ";
            }

        }

        protected void DropDownListUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void UploadButtonPicture_Click(object sender, EventArgs e)
        {
            PictureStatusLabel.Text = string.Empty;
            ImagePicture.Visible = false;
            if (FilePicture.HasFile)
            {
                try
                {

                    //using (System.Drawing.Image img = System.Drawing.Image.FromStream(FileSignature.FileContent))
                    //{
                    //    using (System.IO. MemoryStream m = new System.IO.MemoryStream())
                    //    {
                    //        img.Save(m, img.RawFormat);
                    //        byte[] imageBytes = m.ToArray();

                    //        // Convert byte[] to Base64 String
                    //        string signString = Convert.ToBase64String(imageBytes);
                    //        string contentType = FileSignature.PostedFile.ContentType;
                    //        //return base64String;
                    //        //var bytes = FileSignature.FileBytes ?? new byte[] { };
                    //        //var signString = Convert.ToBase64String(bytes);
                    //        string imageUrl = $"data:{contentType};base64,{signString}";
                    //        ImageSignature.Width = img.Width;
                    //        ImageSignature.Height = img.Height;
                    //        ImageSignature.ImageUrl = imageUrl;

                    FileUpload fu = FilePicture;
                    Image img = ImagePicture;

                    SetUrlImageFromFileUploadAndDimensions(fileUpload: fu, image: img);

                    ImagePicture.Visible = true;
                    //string filename = Path.GetFileName(FileUploadControl.FileName);
                    //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    //StatusLabel.Text = "Upload status: File uploaded!";
                    PictureValue.Value = img.ImageUrl; // imageUrl;
                    PictureStatusLabel.Text = "Signature Upload status: The file has been uploaded ";
                    //}

                    //}
                    //    var bytes = FileSignature.FileBytes ?? new byte[] { };
                    //var signString = Convert.ToBase64String(bytes);

                    //ImageSignature.ImageUrl = $"{signString}";
                    ////string filename = Path.GetFileName(FileUploadControl.FileName);
                    ////FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    ////StatusLabel.Text = "Upload status: File uploaded!";
                    //SignatureValue.Value = signString;
                    //SignatureStatusLabel.Text = "Signature Upload status: The file ha be uploaded ";
                }
                catch (Exception ex)
                {
                    PictureStatusLabel.Text = "Signature Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else
            {
                PictureValue.Value = string.Empty;
                PictureStatusLabel.Text = "Signature Upload status: You must select a image file ";
            }
        }

        protected void GridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

            if (e.AffectedRows > 0)
            {
                var olprimaryK = e.Keys;
                var liprimaryKeys = olprimaryK.Keys;
                var liprimaryValues = olprimaryK.Values;

                var oldataV = e.Values;
                var lidataKeys = oldataV.Keys;
                var lidataValues = oldataV.Values;

                byte[] args = new byte[] {1, 2, 4 };

                Type keyType = default(Type);
                TypeCode keyTypeCode = TypeCode.Empty;
                Type valueType = default(Type);
                TypeCode valueTypeCode = TypeCode.Empty;

                var type = args.GetType();
                var typeCode = Type.GetTypeCode(type);

                //int index = 0;
                var objectPKeys = liprimaryKeys.OfType<object>();
                var objectPValues = liprimaryValues.OfType<object>();

                var objectDKeys = lidataKeys.OfType<object>();
                var objectDValues = lidataValues.OfType<object>();

                //var objectKeys = liKeys.OfType<object>();//.Where(item=> item != null);
                //var objectValues = liValues.OfType<object>();

                var dateDictionary = new Dictionary<string, DateTime>();
                var stringDictionary = new Dictionary<string, string>();
                var guidDictionary = new Dictionary<string, Guid>();
                var intDictionary = new Dictionary<string, int>();
                var boolDictionary = new Dictionary<string, bool>();


                var keys = objectPKeys;
                var values = objectPValues;

                for (int index = 0; index < keys.Count(); index++)
                {
                    var key = keys.ElementAt(index);

                    if (key != null)
                    {
                        keyType = key.GetType();
                        keyTypeCode = Type.GetTypeCode(keyType);
                        string keyName = (key is string) ? key as string : key.ToString();

                        var value = values.ElementAt(index);
                        if (value != null)
                        {
                            valueType = value.GetType();
                            valueTypeCode = Type.GetTypeCode(valueType);

                            switch (valueTypeCode)
                            {
                                case TypeCode.Int32:
                                    intDictionary.Add(keyName, Convert.ToInt32(value) );
                                    break;
                                case TypeCode.String:
                                    stringDictionary.Add(keyName, value as string);
                                    break;
                                case TypeCode.DateTime:
                                    dateDictionary.Add(keyName, Convert.ToDateTime(value));
                                    break;
                                case TypeCode.Boolean:
                                    boolDictionary.Add(keyName, Convert.ToBoolean(value));
                                    break;
                                default:
                                    var valueString = value.ToString() ;
                                    if (Guid.TryParse(valueString, out Guid valueGuid))
                                    {
                                        guidDictionary.Add(keyName, valueGuid);
                                    }
                                    break;
                            
                            }

                        }
                    }                  

                }

                keys = objectDKeys;
                values = objectDValues;

                for (int index = 0; index < 0; index++)
                {
                    var key = keys.ElementAt(index);

                    if (key != null)
                    {
                        keyType = key.GetType();
                        keyTypeCode = Type.GetTypeCode(keyType);
                        string keyName = (key is string) ? key as string : key.ToString();

                        var value = values.ElementAt(index);
                        if (value != null)
                        {
                            valueType = value.GetType();
                            valueTypeCode = Type.GetTypeCode(valueType);

                            switch (valueTypeCode)
                            {
                                case TypeCode.Int32:
                                    intDictionary.Add(keyName, Convert.ToInt32(value));
                                    break;
                                case TypeCode.String:

                                    stringDictionary.Add(keyName, value as string);
                                    break;
                                case TypeCode.DateTime:
                                    dateDictionary.Add(keyName, Convert.ToDateTime(value));
                                    break;
                                case TypeCode.Boolean:
                                    boolDictionary.Add(keyName, Convert.ToBoolean(value));
                                    break;
                                default:
                                    var valueString = value as string;
                                    if (Guid.TryParse(valueString, out Guid valueGuid))
                                    {
                                        guidDictionary.Add(keyName, valueGuid);
                                    }
                                    
                                    break;

                            }

                        }
                    }

                }

                BindRecallUsers();
            }

        }
    }
}