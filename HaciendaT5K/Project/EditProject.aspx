<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="EditProject.aspx.cs" Inherits="Eblue.Project.EditProject" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerEdit" runat="server"></asp:ScriptManager>
    <br />
    <div id="ErrorMessage" runat="server" class="alert alert-danger align-content-center col-md-6 container" visible="false">
        <asp:Label ID="Message" runat="server"></asp:Label>
    </div>
    <div id="InfoMessage" runat="server" style="text-align: center" class="alert alert-info align-content-center col-md-6 container" visible="false">
        <asp:Label ID="LabelInfo" runat="server"></asp:Label>
    </div>
    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <h1 class="text-center">E-BLUE</h1>
                <h2 class="text-center">Edit Project</h2>
            </div>
        </div>
    </div>
    <hr />
    <section class="content">
        <div class="container">
            <div style="text-align: center">
                <asp:Button runat="server" ID="btnSaveChanges1" CssClass="btn btn-primary" Text="Save Changes" OnClick="SaveChanges_Click" />
            </div>
            <% 
                var sections = this.targetSections;
                Eblue.Code.TargetSection section_a1 = null;
                Eblue.Code.TargetSection section_a2 = null;
                Eblue.Code.TargetSection section_a3 = null;
                Eblue.Code.TargetSection section_a4 = null;
                Eblue.Code.TargetSection section_a5 = null;
                Eblue.Code.TargetSection section_a6 = null;
                Eblue.Code.TargetSection section_a7 = null;
                Eblue.Code.TargetSection section_a8 = null;
                Eblue.Code.TargetSection section_a9 = null;
                Eblue.Code.TargetSection section_b1 = null;
                Eblue.Code.TargetSection section_b2 = null;

                char circle = char.MinValue;
                string title = string.Empty;

                bool whenData = false;
                bool dataCapEdit = false;
                bool dataCapDetail = false;

                bool canDataDetail = false;
                bool canDataEdit = false;
                bool canDataEditAndDetail = false;

                // bool whenList = false;
                //bool listCapDetail = false;
                //bool listCapAdd = false;
                //bool listCapRemove = false;
                //bool listCapEdit = false;

                //bool canListDetail = false;
                //bool canListAdd = false;
                //bool canListRemove = false;
                //bool canListEdit = false;
                //bool canListEditAndDetail = false;

                if (sections == null)
                {
                    //var stop = true;
                }
                else
                {
                    var tagier_section_a1 = Eblue.Utils.ConstantsTools.tagier_project_section_a1;
                    var tagier_section_a2 = Eblue.Utils.ConstantsTools.tagier_project_section_a2;
                    var tagier_section_a3 = Eblue.Utils.ConstantsTools.tagier_project_section_a3;
                    var tagier_section_a4 = Eblue.Utils.ConstantsTools.tagier_project_section_a4;
                    var tagier_section_a5 = Eblue.Utils.ConstantsTools.tagier_project_section_a5;
                    var tagier_section_a6 = Eblue.Utils.ConstantsTools.tagier_project_section_a6;
                    var tagier_section_a7 = Eblue.Utils.ConstantsTools.tagier_project_section_a7;
                    var tagier_section_a8 = Eblue.Utils.ConstantsTools.tagier_project_section_a8;
                    var tagier_section_a9 = Eblue.Utils.ConstantsTools.tagier_project_section_a9;
                    var tagier_section_b1 = Eblue.Utils.ConstantsTools.tagier_project_section_b1;
                    var tagier_section_b2 = Eblue.Utils.ConstantsTools.tagier_project_section_b2;


                    if (sections.ContainsKey(tagier_section_a1))
                    {
                        section_a1 = sections[tagier_section_a1];
                    }
                    if (sections.ContainsKey(tagier_section_a2))
                    {
                        section_a2 = sections[tagier_section_a2];
                    }
                    if (sections.ContainsKey(tagier_section_a3))
                    {
                        section_a3 = sections[tagier_section_a3];
                    }

                    if (sections.ContainsKey(tagier_section_a4))
                    {
                        section_a4 = sections[tagier_section_a4];
                    }

                    if (sections.ContainsKey(tagier_section_a5))
                    {
                        section_a5 = sections[tagier_section_a5];
                    }
                    if (sections.ContainsKey(tagier_section_a6))
                    {
                        section_a6 = sections[tagier_section_a6];
                    }
                    if (sections.ContainsKey(tagier_section_a7))
                    {
                        section_a7 = sections[tagier_section_a7];
                    }
                    if (sections.ContainsKey(tagier_section_a8))
                    {
                        section_a8 = sections[tagier_section_a8];
                    }
                    if (sections.ContainsKey(tagier_section_a9))
                    {
                        section_a9 = sections[tagier_section_a9];
                    }
                    if (sections.ContainsKey(tagier_section_b1))
                    {
                        section_b1 = sections[tagier_section_b1];
                    }
                    if (sections.ContainsKey(tagier_section_b2))
                    {
                        section_b2 = sections[tagier_section_b2];
                    }
                }

                //<div class="card card-default">


                //for diabled when whenData and dataDetail || dataEdit

                //  <div class="overlay">

                //</div>

                //for loading
                //  <div class="overlay">
                //  <i class="fa fa-refresh fa-spin"></i>
                //</div>
                bool hasSection1 = ViewState["HasSection1"] == null ? false : bool.Parse(ViewState["HasSection1"].ToString()); %>

            <% if (section_a1 != null)
                { %>

            <% 
                     whenData = section_a1.whenData;
                     dataCapEdit = section_a1.dataCapEdit && whenData;
                     dataCapDetail = section_a1.dataCapDetail && whenData;

                     canDataDetail = dataCapDetail || dataCapEdit;
                     canDataEdit =  dataCapEdit;
                     canDataEditAndDetail = dataCapEdit && dataCapDetail;

                    if (canDataDetail){ %>
            <%--<div class="card">--%>
                <% }  %>
                <% else
                            {  %>
                <%--<div class="card" style="display:none">--%>
                
                <% }%>
            
                    <div class="card" style = "<%= (canDataDetail) ? "" : string.Format("display:none") %>" >
                

                <div class="card-header"  >
                    <h3 class="card-title"> <%= Eblue.Utils.ConstantsTools.BlackNumbers[section_a1.rowNumber] %> First Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" ID="labelProjectnumber" CssClass="form-control-lg">Work Plan For Project: </asp:Label>
                                <asp:Label runat="server" ID="labelProjectnumberResult"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" ID="labelFisicalYear" CssClass="form-control-lg">Fiscal Year: </asp:Label>
                                <asp:Label runat="server" ID="labelFiscalYearResult"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-control-lg" ID="labelLeader">Leader: </asp:Label>
                                <asp:Label runat="server" ID="labelLeaderResault"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-control-lg" ID="labelStatus">Status: </asp:Label>
                                <asp:Label runat="server" ID="labelStatusResult"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-control-lg" ID="label14">Contract number: </asp:Label>
                                <asp:TextBox ID="TextBoxContractNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-control-lg" ID="label16">ORCID: </asp:Label>
                                <asp:TextBox ID="TextBoxORCID" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                    <% if (canDataEdit){ %>
           
                    <div class="card-footer">
                <%--<form action="#" method="post">--%>
                  <div class="input-group">
                    
                    <span class="input-group-append">
                     <asp:Button ID="btndataCapEditsecta1Enabled" runat="server" CssClass="btn btn-primary" Text="Save Changes" OnClick="SaveChangesSection_a1_Click" />
                    </span>
                  </div>
                <%--</form>--%>
              </div>

                <% }  %>
                <% else
                            {  %>
                <div class="card-footer">
                <%--<form action="#" method="post">--%>
                  <div class="input-group">
                    
                    <span class="input-group-append">
                     <asp:Button ID="btndataCapEditsecta1Disabled" Enabled="false" runat="server" CssClass="btn btn-primary" Text="Save Changes" OnClick="SaveChangesSection_a1_Click" />
                    </span>
                  </div>
                <%--</form>--%>
              </div>
                 <div class="overlay">

                </div>
                <% }%>

                <%--<button type ="button" id="btnsecta1" class="btn btn-tool" >Save</button>--%>
                
                    
            </div>
            <% } %>

            <% if (hasSection1)
                { %>
            <div class="card">

                
            </div>
            <% } %>

            <% if (section_a4 != null)
                { %>
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title"> Fourth Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelLocation" CssClass="form-control-lg" runat="server" Text="Location:"></asp:Label>
                                <asp:DropDownList ID="DropDownListLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelArea" CssClass="form-control-lg" runat="server" Text="Area:"></asp:Label>

                                <asp:TextBox ID="fldWork_Area" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStart" CssClass="form-control-lg" runat="server" Text="Start:"></asp:Label>
                                <asp:TextBox ID="TextBoxStart" autocomplete="off" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="TextBoxStart_CalendarExtender" runat="server" BehaviorID="TextBoxStart_CalendarExtender" TargetControlID="TextBoxStart" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelEnd" CssClass="form-control-lg" runat="server" Text="End:"></asp:Label>
                                <asp:TextBox ID="TextBoxEnd" autocomplete="off"  CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="TextBoxEnd_CalendarExtender" runat="server" BehaviorID="TextBoxEnd_CalendarExtender" TargetControlID="TextBoxEnd" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-auto">
                                <asp:CheckBox ID="ChkInProgress" Text="In Progress" CssClass="minimal" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-auto">
                                <asp:CheckBox ID="ChkInitiated" CssClass="minimal" Text="Initiated" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-auto">
                                <asp:Button ID="ButtonWorkInprogresInitiated" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonWorkInprogresInitiated_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Work In Progress</h4>
                                <asp:GridView  ID="gvP3Progress" runat="server" 
                                    DataKeyNames="FieldWorkID" DataSourceID="sdsFWInProgress" AutoGenerateColumns="False"
                                    CssClass="gridview table table-bordered table-striped"
                                    >
                                    <%--<RowStyle CssClass="data-row"CssClass="gridview table-bordered table table-hover table-striped" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Location Name" SortExpression="LocationName">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsLocation" DataTextField="LocationName"
                                                    DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [LocationID], [LocationName] FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField  DataField="Area" HeaderText="Area" SortExpression="Area" />
                                        <asp:TemplateField HeaderText="Started" SortExpression="dateStarted">
                                            <EditItemTemplate>
                                                <div style="z-index: 9;">
                                                    <asp:TextBox ID="txtStarted" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:TextBox>
                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ended" SortExpression="dateEnded">
                                            <EditItemTemplate>
                                                <div style="z-index: 1;">
                                                    <asp:TextBox ID="txtEnded" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:TextBox>
                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsFWInProgress" runat="server" ConflictDetection="CompareAllValues"
                                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" DeleteCommand="DELETE FROM [FieldWork] WHERE [FieldWorkID] = @original_FieldWorkID "
                                    InsertCommand="INSERT INTO [FieldWork] ([ProjectID], [LocationID], [Area], [dateStarted], [dateEnded], [Inprogress]) VALUES (@ProjectID, @LocationID, @Area, @dateStarted, @dateEnded, @Inprogress)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT FieldWorkID, ProjectID, LocationID, Area, dateStarted, dateEnded, Inprogress, (SELECT LocationName FROM Location AS L WHERE (LocationID = FW.LocationID)) AS LocationName FROM FieldWork AS FW 
WHERE Inprogress=1 AND ProjectID=@ProjectID
ORDER BY LocationName"
                                    UpdateCommand="UPDATE [FieldWork] SET [LocationID] = @LocationID, [Area] = @Area, [dateStarted] = @dateStarted, [dateEnded] = @dateEnded WHERE [FieldWorkID] = @original_FieldWorkID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_FieldWorkID" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="original_FieldWorkID" Type="Int32" />
                                        <asp:Parameter Name="original_ProjectID" Type="Object" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="Inprogress" Type="Boolean" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Work Initiated</h4>
                                <asp:GridView   ID="gvP3BeInitiated" runat="server" 
                                    DataKeyNames="FieldWorkID" DataSourceID="sdsFWBeInitiated" AutoGenerateColumns="False"
                                    CssClass="gridview table table-bordered table-striped">
                                    <%--<RowStyle CssClass="data-row" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Location Name" SortExpression="LocationName">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsLocation" DataTextField="LocationName"
                                                    DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [LocationID], [LocationName] FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Area" HeaderText="Area" SortExpression="Area" />
                                        <asp:TemplateField HeaderText="Started" SortExpression="dateStarted">
                                            <EditItemTemplate>
                                                <div style="z-index: 9;">
                                                    <asp:TextBox ID="txtStarted" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:TextBox>
                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ended" SortExpression="dateEnded">
                                            <EditItemTemplate>
                                                <div style="z-index: 1;">
                                                    <asp:TextBox ID="txtEnded" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:TextBox>
                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsFWBeInitiated" runat="server" ConflictDetection="CompareAllValues"
                                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" DeleteCommand="DELETE FROM [FieldWork] WHERE [FieldWorkID] = @original_FieldWorkID "
                                    InsertCommand="INSERT INTO [FieldWork] ([ProjectID], [LocationID], [Area], [dateStarted], [dateEnded], [Inprogress]) VALUES (@ProjectID, @LocationID, @Area, @dateStarted, @dateEnded, @Inprogress)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT FieldWorkID, ProjectID, LocationID, Area, dateStarted, dateEnded, Inprogress, (SELECT LocationName FROM Location AS L WHERE (LocationID = FW.LocationID)) AS LocationName FROM FieldWork AS FW 
WHERE ToBeInitiated=1 AND ProjectID=@ProjectID
ORDER BY LocationName"
                                    UpdateCommand="UPDATE [FieldWork] SET [LocationID] = @LocationID, [Area] = @Area, [dateStarted] = @dateStarted, [dateEnded] = @dateEnded WHERE [FieldWorkID] = @original_FieldWorkID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_FieldWorkID" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="original_FieldWorkID" Type="Int32" />
                                        <asp:Parameter Name="original_ProjectID" Type="Object" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="Inprogress" Type="Boolean" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
            <% } %>

            <% if (section_a5 != null)
                { %>
            <div class="card">

                <div class="card-header">
                    <h3 class="card-title"> Fifth Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <%--style = "<%= (canDataDetail) ? "" : string.Format("display:none") %>"--%>
                    <%--<%= (!section_a5.dataCapEdit) ? string.Empty : "disabled" %>--%>
                    <asp:Panel ID="panelSection5" Enabled="false" runat="server" >
                        <div class="row" >
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelWorkPlanned2" CssClass="form-control-lg" runat="server" Text="Work Planned (2): Laboratory"></asp:Label>
                                <asp:TextBox ID="TextBoxWorkPlanned2" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelDescription" CssClass="form-control-lg" runat="server" Text="Description:"></asp:Label>
                                <asp:TextBox ID="TextBoxDescription" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelEstimatedTime" CssClass="form-control-lg" runat="server" Text="Estimated Result Time"></asp:Label>
                                <asp:TextBox ID="TextBoxEstimatedTime" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelFacilitieNeeded" CssClass="form-control-lg" runat="server" Text="Facilities Needed:"></asp:Label>
                                <asp:TextBox ID="TextBoxFacilitieNeeded" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Services of Central Analytical Laboratory:</h4>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelAnalisysRequired" CssClass="form-control-lg" runat="server" Text="Analisys Required:"></asp:Label>
                                <asp:TextBox ID="TextBoxAnalisysRequired" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabeNumSample" CssClass="form-control-lg" runat="server" Text="No. of Samples:"></asp:Label>
                                <asp:TextBox ID="TextBoxNumSample" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelProDate" CssClass="form-control-lg" runat="server" Text="Probable Date:"></asp:Label>
                                <asp:TextBox ID="TextBoxProDate" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderProDate" runat="server" BehaviorID="TextBoxProDate_CalendarExtender" TargetControlID="TextBoxProDate" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonNewLaboratory" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewLaboratory_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <asp:GridView  ID="gvLab" runat="server" AutoGenerteColumns="False"
                                    DataKeyNames="LID" DataSourceID="sdsLaboratory" 
                                    CssClass="gridview table table-bordered table-striped"
                                   >
                                    <%--<RowStyle CssClass="data-row" />--%>
                                    <Columns>
                                        <asp:BoundField  DataField="AReq" HeaderText="Analisys Req." SortExpression="AReq" />
                                        <asp:BoundField DataField="NoSamples" HeaderText="No. Samples" SortExpression="NoSamples" />
                                        <asp:BoundField DataField="SamplesDate" DataFormatString="{0:d}" HeaderText="Probable Date"
                                            SortExpression="SamplesDate" />
                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True"  ButtonType="Button"/>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsLaboratory" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [Laboratory] WHERE [LID] = @original_LID" InsertCommand="INSERT INTO [Laboratory] ([AReq], [NoSamples], [SamplesDate], [ProjectID]) VALUES (@AReq, @NoSamples, @SamplesDate, @ProjectID)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [LID], [AReq], [NoSamples], [SamplesDate], [ProjectID] FROM [Laboratory] WHERE ([ProjectID] = @ProjectID) ORDER BY [LID]"
                                    UpdateCommand="UPDATE [Laboratory] SET [AReq] = @AReq, [NoSamples] = @NoSamples, [SamplesDate] = @SamplesDate WHERE [LID] = @original_LID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_LID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="AReq" Type="String" />
                                        <asp:Parameter Name="NoSamples" Type="String" />
                                        <asp:Parameter Name="SamplesDate" Type="DateTime" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="original_LID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="AReq" Type="String" />
                                        <asp:Parameter Name="NoSamples" Type="String" />
                                        <asp:Parameter Name="SamplesDate" Type="DateTime" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>

                
                
            </div>
            <% } %>

            <%--class="content-wrapper"--%>
            

            <%

                if (section_a2 != null)
                { 
                circle = Eblue.Utils.ConstantsTools.BlackNumbers[2];
                title = section_a2.description;
                whenData = section_a2.whenData;
                dataCapEdit = section_a2.dataCapEdit && whenData;
                dataCapDetail = section_a2.dataCapDetail && whenData;

                canDataDetail = dataCapDetail || dataCapEdit;
                canDataEdit =  dataCapEdit;
                canDataEditAndDetail = dataCapEdit && dataCapDetail;
                
                }
                
                %>

            <%--<%= Eblue.Utils.ConstantsTools.BlackNumbers[section_a1.rowNumber] %> 
            <%= Eblue.Utils.ConstantsTools.BlackNumbers[section_a1.rowNumber] %>
                style = "<%= (canDataDetail) ? "" : string.Format("display:none") %>"
                
                <% if (section_a2 != null)
                { %>

            <%} %>
                
                --%>

            
             
            <div  style = "<%= (section_a2 != null) ? "" : string.Format("display:none") %>"  >
                <nav class="main-header navbar navbar-expand bg-white navbar-light border-bottom">
                    <!-- Left navbar links -->
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" data-widget="pushmenu" href="#"><i class="fa fa-bars"></i></a>
                        </li>
                        <li class="nav-item d-none d-sm-inline-block">
                            <a class="nav-link" > <%= circle %>  <%= title %>  </a>
                        </li>

                    </ul>


                </nav>



                <section class="content">
                    <div class="container-fluid">

                        <div class="card card-default">
                            <div class="card-header">
                                <h3 class="card-title">Data Info</h3>

                                <div class="card-tools">
                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="card-body">
                                
                                <!-- /.row -->

                                <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelProgramaticArea" CssClass="form-control-lg" runat="server" Text="Programatic Area:"></asp:Label>
                                <asp:DropDownList ID="DropDownListProgramaticArea" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceProgramaticArea" DataTextField="ProgramAreaName" DataValueField="ProgramAreaID"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceProgramaticArea" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [ProgramArea]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelCommodity" CssClass="form-control-lg" runat="server" Text="Commmodity:"></asp:Label>
                                <asp:DropDownList ID="DropDownListCommodity" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceCommodity"
                                    DataTextField="CommName" DataValueField="CommID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceCommodity" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Commodity]"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelProjectShortTitle" CssClass="form-control-lg" runat="server" Text="Project Short Title:"></asp:Label>
                                <asp:TextBox ID="TextBoxProjectShortTitle" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelDepartment" CssClass="form-control-lg" runat="server" Text="Department:"></asp:Label>
                                <asp:DropDownList ID="DropDownListDepartment" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceDepartment"
                                    DataTextField="DepartmentName" DataValueField="DepartmentID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelSubstation" CssClass="form-control-lg" runat="server" Text="Substation or Region:"></asp:Label>
                                <asp:DropDownList ID="DropDownListSubstation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    SelectCommand="SELECT * FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                            </div>

                            <div class="card-footer">
                                
                                <asp:Button TabIndex="6" ID="buttonSave_a2"  OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />
                               

                                
                            </div>

                            <% if (!canDataEdit)
                                {%>
                            <div class="overlay"   >
                            </div>
                            <% } %>
                        </div>

                        
                    </div>
                </section>

            </div>

            
         
            

            

            <div class="card">

                



                <div class="card-header">
                    <h3 class="card-title">Third Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelProjectObjective" CssClass="form-control-lg" runat="server" Text="Project Objective(s):"></asp:Label>
                                <asp:TextBox ID="TextBoxProjectObjective" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Labelobjectivefortheyear" CssClass="form-control-lg" runat="server" Text="Objective of Work Planned for the Year:"></asp:Label>
                                <asp:TextBox ID="TextBoxobjectivefortheyear" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelWorkAccomplished" CssClass="form-control-lg" runat="server" Text="Work Accomplished and Present Outlook:"></asp:Label>
                                <asp:TextBox ID="TextBoxWorkAccomplished" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label4" CssClass="form-control-lg" runat="server" Text="Work Planned (1) (Field Work):  "></asp:Label>
                                <asp:TextBox ID="TextBoxFieldWork" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>

                

                

                <div class="card-header">
                    <h3 class="card-title">Sixth Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Add Scientists working in the project:</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelScientest" CssClass="form-control-lg" runat="server" Text="Scientist:"></asp:Label>
                                <asp:DropDownList ID="DropDownListScientest" CssClass="form-control" runat="server" DataSourceID="sdsRoster" DataTextField="RosterName" DataValueField="RosterID"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceScientist" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Scientest]"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="sdsRoster" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    SelectCommand="SELECT [RosterID], [RosterName] FROM [Roster] WHERE ([CanBePI] = @CanBePI)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="True" Name="CanBePI" Type="Boolean" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelRoleName" CssClass="form-control-lg" runat="server" Text="RoleName"></asp:Label>
                                <asp:DropDownList ID="DropDownListRole" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceRole" DataTextField="RoleName" DataValueField="RoleID"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceRole" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Roles]"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center" style="overflow: scroll; width: 100%">
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td>Regular</td>
                                        <td>Additional Compensation</td>
                                        <td>Ad Honorem</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTR" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtCA" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtHA" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonNewScientest" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewScientest_Click" />
            
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <asp:GridView  ID="gvSci" runat="server"  OnRowUpdated="GridView_RowUpdate"
                                    DataKeyNames="SciID" DataSourceID="sdsSci" AutoGenerateColumns="False" 
                                    CssClass="gridview table table-bordered table-striped"
                                   >
                                    <RowStyle CssClass="data-row"  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Scientist" SortExpression="RosterName">
                                            <EditItemTemplate>                                                
                                                <asp:DropDownList ID="DropDownListRoster" runat="server" DataSourceID="sdsRoster" 
                                                    DataTextField="RosterName"
                                                    DataValueField="RosterID" SelectedValue='<%# Bind("RosterID") %>' >
                                                    
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="LabelRosterNameItem" runat="server" Text='<%# Bind("RosterName") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RoleName" SortExpression="RoleName">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsRoleEdit" DataTextField="RoleName"
                                                    DataValueField="RoleId" SelectedValue='<%# Bind("Role") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsRoleEdit" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [RoleId], [RoleName] FROM [Roles] ORDER BY [RoleID]"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="TR" HeaderText="TR" SortExpression="TR" ControlStyle-Width="100px" />
                                        <asp:BoundField DataField="CA" HeaderText="CA" SortExpression="CA" ControlStyle-Width="100px" />
                                        <asp:BoundField DataField="AH" HeaderText="AH" SortExpression="AH" ControlStyle-Width="100px" />
                                        <asp:TemplateField HeaderText="Total" SortExpression="SumCredits">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSumCredits" runat="server" Text='<%# Bind("SumCredits") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"/>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsSci" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [SciProjects] WHERE [SciID] = @original_SciID" InsertCommand="INSERT INTO [SciProjects] ([RosterID], [Role], [Credits], [AdHonorem], [ProjectID]) VALUES (@RosterID, @Role, @Credits, @AdHonorem, @ProjectID)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [SciID], [RosterID], [Role], [TR], [CA], [AH], [ProjectID], (SUM(TR+CA+AH)) as SumCredits,
(SELECT RosterName FROM Roster R Where RosterID=Sci.RosterID) as RosterName,
(SELECT RoleName FROM Roles PR WHERE RoleId=Sci.Role) as RoleName
FROM SciProjects Sci 
WHERE ProjectID =  @ProjectID 
GROUP BY [SciID], [RosterID], [Role], [TR], [CA], [AH], [ProjectID]
ORDER BY [SciID]"
                                    UpdateCommand="UPDATE [SciProjects] SET [RosterID] = @RosterID, [Role] = @Role, [TR]=@TR, [CA]=@CA, [AH]=@AH WHERE [SciID] = @original_SciID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_SciID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="RosterID"  />
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                        <asp:Parameter Name="Role" Type="Int32" />
                                        <asp:Parameter Name="TR" Type="Decimal" />
                                        <asp:Parameter Name="CA" Type="Decimal" />
                                        <asp:Parameter Name="AH" Type="Decimal" />
                                        <asp:Parameter Name="original_SciID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="RosterID" />
                                        <asp:Parameter Name="Role" Type="Int32" />
                                        <asp:Parameter Name="Credits" Type="Int32" />
                                        <asp:Parameter Name="AdHonorem" Type="Boolean" />
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-header">
                    <h3 class="card-title">Seventh Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Other personnel working in the project:</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelOtherPersonal" CssClass="form-control-lg" runat="server" Text="Other personnel:"></asp:Label>
                                <asp:TextBox ID="TextBoxOtherPersonal" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelPercentage" CssClass="form-control-lg" runat="server" Text="% of Time:"></asp:Label>
                                <asp:TextBox ID="TextBoxPercentage" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelOtherPersonalLocation" CssClass="form-control-lg" runat="server" Text="Location:"></asp:Label>
                                <asp:DropDownList ID="DropDownListOtherPersonalLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonOtherPersonal" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonOtherPersonal_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <asp:GridView  ID="gvOP" 
                                    runat="server"  DataKeyNames="OPID"
                                    DataSourceID="sdsOtherPersonel" AutoGenerateColumns="False" 
                                    CssClass="gridview table table-bordered table-striped"
                                   >
                                    <%--<RowStyle CssClass="data-row" />--%>
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                        <asp:BoundField DataField="PerTime" HeaderText="% of Time" SortExpression="PerTime" />
                                        <asp:TemplateField HeaderText="Location" SortExpression="LocationName">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsOPLocEdit" DataTextField="LocationName"
                                                    DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsOPLocEdit" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [LocationID], [LocationName] FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsOtherPersonel" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [OtherPersonel] WHERE [OPID] = @original_OPID" InsertCommand="INSERT INTO [OtherPersonel] ([Name], [PerTime], [ProjectID], [LocationID]) VALUES (@Name, @PerTime, @ProjectID, @LocationID)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [OPID], [Name], [PerTime], [ProjectID], [LocationID],
(SELECT LocationName FROM Location L WHERE LocationID=OP.LocationID) as LocationName
FROM OtherPersonel OP WHERE ([ProjectID] = @ProjectID) ORDER BY [OPID]"
                                    UpdateCommand="UPDATE [OtherPersonel] SET [Name] = @Name, [PerTime] = @PerTime, [LocationID] = @LocationID WHERE [OPID] = @original_OPID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_OPID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="PerTime" Type="Int32" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="original_OPID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="PerTime" Type="Int32" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Add Graduates and undergraduates assistanships:</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStudentName" CssClass="form-control-lg" runat="server" Text="Name:"></asp:Label>
                                <asp:TextBox ID="TextBoxStudentName" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStudentID" CssClass="form-control-lg" runat="server" Text="Student ID:"></asp:Label>
                                <asp:TextBox ID="TextBoxStudentID" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelThesisTitle" CssClass="form-control-lg" runat="server" Text=" Thesis title:"></asp:Label>
                                <asp:TextBox ID="TextBoxThesisTittle" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStudentAmount" CssClass="form-control-lg" runat="server" Text="Amount:"></asp:Label>
                                <asp:TextBox ID="TextBoxStudentAmount" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonStudent" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonStudent_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <asp:GridView  ID="gvGradAss" 
                                    runat="server" 
                                    DataKeyNames="GAID" DataSourceID="sdsGradAss" AutoGenerateColumns="False" 
                                    CssClass="gridview table table-bordered table-striped"
                                    >
                                    <%--<RowStyle CssClass="data-row"  AllowSorting="True"/>--%>
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                        <asp:BoundField DataField="Thesis" HeaderText="Thesis or Project" SortExpression="Thesis" />
                                        <asp:BoundField DataField="StudentID" HeaderText="Student ID" SortExpression="StudentID" />
                                        <asp:BoundField DataField="Amountp" HeaderText="Amount" SortExpression="Amountp" />
                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsGradAss" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [GradAss] WHERE [GAID] = @original_GAID" InsertCommand="INSERT INTO [GradAss] ([Name], [Thesis], [ProjectID], [StudentID], [Amountp]) VALUES (@Name, @Thesis, @ProjectID, @StudentID, @Amountp)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [GAID], [Name], [Thesis], [ProjectID], [StudentID], [Amountp] FROM [GradAss] WHERE ([ProjectID] = @ProjectID) ORDER BY [GAID]"
                                    UpdateCommand="UPDATE [GradAss] SET [Name] = @Name, [Thesis] = @Thesis, [StudentID] = @StudentID, [Amountp] = @Amountp WHERE [GAID] = @original_GAID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_GAID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="Thesis" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="StudentID" Type="String" />
                                        <asp:Parameter Name="Amountp" Type="Decimal" />
                                        <asp:Parameter Name="original_GAID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="Thesis" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="StudentID" Type="String" />
                                        <asp:Parameter Name="Amountp" Type="Decimal" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-header">
                    <h3 class="card-title">Eigth Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Funds for Fiscal Year :</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelFundLocation" CssClass="form-control-lg" runat="server" Text="Location:"></asp:Label>
                                <asp:DropDownList ID="DropDownListFundLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelSalaries" CssClass="form-control-lg" runat="server" Text="Salaries:"></asp:Label>
                                <asp:TextBox ID="TextBoxSalaries" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelWages" CssClass="form-control-lg" runat="server" Text="Wages:"></asp:Label>
                                <asp:TextBox ID="TextBoxWages" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelBenifit" CssClass="form-control-lg" runat="server" Text="Benefits:"></asp:Label>
                                <asp:TextBox ID="TextBoxBenifit" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelAssistant" CssClass="form-control-lg" runat="server" Text="Assistantships:"></asp:Label>
                                <asp:TextBox ID="TextBoxAssistant" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelMaterials" CssClass="form-control-lg" runat="server" Text="Materials:"></asp:Label>
                                <asp:TextBox ID="TextBoxMaterials" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelEquipment" CssClass="form-control-lg" runat="server" Text="Equipment:"></asp:Label>
                                <asp:TextBox ID="TextBoxEquipment" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelTravel" CssClass="form-control-lg" runat="server" Text="Travel :"></asp:Label>
                                <asp:TextBox ID="TextBoxTravel" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelAbroad" CssClass="form-control-lg" runat="server" Text="Abroad:"></asp:Label>
                                <asp:TextBox ID="TextBoxAbroad" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelSubcontracts" CssClass="form-control-lg" runat="server" Text="Subcontracts:"></asp:Label>
                                <asp:TextBox ID="TextBoxSubcontracts" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label6" CssClass="form-control-lg" runat="server" Text="Indirect Costs:"></asp:Label>
                                <asp:TextBox ID="TextBoxIndirectCosts" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label5" CssClass="form-control-lg" runat="server" Text="Others:"></asp:Label>
                                <asp:TextBox ID="TextBoxOthers" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonFunds" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonFunds_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <asp:DataList ID="dlFunds" runat="server" CellPadding="4" DataKeyField="FundID" DataSourceID="sdsFunds"
                                    Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif" Font-Overline="False"
                                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                    RepeatDirection="Horizontal" CssClass="yui-datatable-theme" RepeatColumns="6"
                                    OnEditCommand="dlFunds_Edit_Command"
                                    OnUpdateCommand="dlFunds_Update_Command"
                                    OnCancelCommand="dlFunds_Cancel_Command"
                                    OnDeleteCommand="dlFunds_Delete_Command">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemTemplate>
                                        <asp:Label ID="FundIdLabel" Visible="false" Text='<%# Eval("FundId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationIdLabel" Visible="false" Text='<%# Eval("LocationId") %>' runat="server"></asp:Label>
                                        <br />
                                        <table  align="left" class="style1" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:Label ID="LocationNameLabel" runat="server" Text='<%# Eval("LocationName") %>'
                                                        Font-Bold="True" Font-Size="14px" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>Salaries:
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="SalariesLabel" runat="server" Text='<%# Eval("Salaries", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Wages:
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="WagesLabel" runat="server" Text='<%# Eval("Wages", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Benefits:
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="BenefitsLabel" runat="server" Text='<%# Eval("Benefits", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assistant:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="AssistantLabel" runat="server" Text='<%# Eval("Assistant", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Materials:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="MaterialsLabel" runat="server" Text='<%# Eval("Materials", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Equipment:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="EquipmentLabel" runat="server" Text='<%# Eval("Equipment", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Travel:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="TravelLabel" runat="server" Text='<%# Eval("Travel", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Abroad:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="AbroadLabel" runat="server" Text='<%# Eval("Abroad", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subcontracts:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="SubcontractsLabel" runat="server" Text='<%# Eval("Subcontracts", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indirect Costs:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("IndirectCosts", "{0:N}") %>' />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Others:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="OthersLabel" runat="server" Text='<%# Eval("Others", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <b>Total:</b>
                                                </td>
                                                <td class="DataListData">
                                                    <b>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalLoc", "{0:C}") %>'></asp:Label>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <%--<asp:LinkButton ID="lbEdit" CommandName="Edit" runat="server">Edit</asp:LinkButton>
                                                    |
                                            <asp:LinkButton ID="lbDelete" CommandName="Delete" runat="server">Delete</asp:LinkButton>--%>
                                                    <asp:Button ID="btnEdit" CommandName="Edit" Text="Edit" runat="server"/>
                                                    <asp:Button ID="btnDelete" CommandName="Delete" Text="Delete" runat="server"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </AlternatingItemTemplate>
                                    <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif"
                                        Font-Overline="False" Font-Size="12px" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#284775" HorizontalAlign="Left" VerticalAlign="Top" CssClass="alt-data-row" />
                                    <ItemStyle BackColor="#F7F6F3" Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif"
                                        Font-Overline="False" Font-Size="12px" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="Top" CssClass="data-row" />
                                    <EditItemTemplate>
                                        <asp:Label ID="FundIdLabel" Visible="false" Text='<%# Eval("FundId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationIdLabel" Visible="false" Text='<%# Eval("LocationId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationNameLabel" runat="server" Visible="false" Font-Bold="True" Font-Size="14px"
                                            Text='<%# Eval("LocationName") %>' />
                                        <asp:DropDownList ID="ComboBoxFundLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                            DataTextField="LocationName" DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                        </asp:DropDownList>
                                        <br />
                                        <table align="left" cellpadding="3" cellspacing="3" class="style1">
                                            <tr>
                                                <td>Salaries:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rsa" runat="server" Text='<%# Eval("Salaries", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Wages:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rwa" runat="server" Text='<%# Eval("Wages", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Benefits:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rbe" runat="server" Text='<%# Eval("Benefits", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assistant:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="ras" runat="server" Text='<%# Eval("Assistant", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Materials:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rma" runat="server" Text='<%# Eval("Materials", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Equipment:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="req" runat="server" Text='<%# Eval("Equipment", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Travel:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rtr" runat="server" Text='<%# Eval("Travel", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Abroad:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rab" runat="server" Text='<%# Eval("Abroad", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subcontracts:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rsu" runat="server" Text='<%# Eval("Subcontracts", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Indirect Costs:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rin" runat="server" Text='<%# Eval("IndirectCosts", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Others:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rot" runat="server" Text='<%# Eval("Others", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:LinkButton ID="lbUpdate" CommandName="Update" runat="server">Update</asp:LinkButton>
                                                    &nbsp;|
                                            <asp:LinkButton ID="lbCancel" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                    <SeparatorStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderTemplate>
                                        Budget for Current Project
                                    </HeaderTemplate>
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <ItemTemplate>
                                        <asp:Label ID="FundIdLabel" Visible="false" Text='<%# Eval("FundId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationIdLabel" Visible="false" Text='<%# Eval("LocationId") %>' runat="server"></asp:Label>
                                        <br />
                                        <table align="left" class="style1" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:Label ID="LocationNameLabel" runat="server" Text='<%# Eval("LocationName") %>'
                                                        Font-Bold="True" Font-Size="14px" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>Salaries:
                                                </td>
                                                <td align="right" class="DataListData">
                                                    <asp:Label ID="SalariesLabel" runat="server" Text='<%# Eval("Salaries", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Wages:
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="WagesLabel" runat="server" Text='<%# Eval("Wages", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Benefits:
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="BenefitsLabel" runat="server" Text='<%# Eval("Benefits", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assistant:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="AssistantLabel" runat="server" Text='<%# Eval("Assistant", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Materials:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="MaterialsLabel" runat="server" Text='<%# Eval("Materials", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Equipment:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="EquipmentLabel" runat="server" Text='<%# Eval("Equipment", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Travel:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="TravelLabel" runat="server" Text='<%# Eval("Travel", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Abroad:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="AbroadLabel" runat="server" Text='<%# Eval("Abroad", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subcontracts:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="SubcontractsLabel" runat="server" Text='<%# Eval("Subcontracts", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indirect Costs:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("IndirectCosts", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Others:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="OthersLabel" runat="server" Text='<%# Eval("Others", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style3">
                                                    <b>Total:</b>
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <b>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalLoc", "{0:C}") %>'></asp:Label>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnEdit" CommandName="Edit" Text="Edit" runat="server"/>
                                                    <asp:Button ID="btnDelete" CommandName="Delete" Text="Delete" runat="server"/>
                                                    <%--<asp:LinkButton  ID="lbEdit" CommandName="Edit" runat="server">Edit</asp:LinkButton>--%>
                                                   <%-- |--%>
                                                    <%--<asp:LinkButton ID="lbDelete" CommandName="Delete" runat="server">Delete</asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:SqlDataSource ID="sdsFunds" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    SelectCommand="SELECT [FundID], [LocationID], [Salaries], [Wages], [Benefits], [Assistant], [Materials], [Equipment], [Travel], [Abroad], [Subcontracts], [IndirectCosts], [Others], [ProjectID],
(SELECT LocationName FROM Location WHERE LocationID=F.LocationID) as LocationName,
(isnull(Salaries,0)+isnull(Wages,0)+isnull(Benefits,0)+isnull(Assistant,0)+ isnull(Materials,0)+ isnull(Equipment, 0)+ isnull(Travel,0)+ isnull(Abroad,0)+ isnull(Subcontracts,0) + isnull(IndirectCosts,0) + isnull(others,0)) as TotalLoc
FROM Funds F WHERE ([ProjectID] = @ProjectID)

union all
SELECT
	FundID = null,
	LocationID = null,
	sum( isnull(fnds.Salaries, 0)) Salaries, 
	sum( isnull(fnds.Wages, 0)) Wages,
	sum( isnull(fnds.Benefits, 0)) Benefits,
	sum( isnull(fnds.Assistant, 0)) Assistant,
	sum( isnull(fnds.Materials, 0)) Materials,
	sum( isnull(fnds.Equipment, 0)) Equipment,
	sum( isnull(fnds.Travel, 0)) Travel,
	sum( isnull(fnds.Abroad, 0)) Abroad,
	sum( isnull(fnds.Subcontracts, 0)) Subcontracts,
    sum( isnull(fnds.IndirectCosts, 0)) IndirectCosts,
	sum( isnull(fnds.Others, 0)) Others,
	fnds.ProjectID,
	LocationName = 'All Locations',
	(	sum( isnull(fnds.Salaries, 0))+
		sum( isnull(fnds.Wages, 0))+
		sum( isnull(fnds.Benefits, 0))+
		sum( isnull(fnds.Assistant, 0))+ 
		sum( isnull(fnds.Materials, 0))+ 
		sum( isnull(fnds.Equipment, 0))+ 
		sum( isnull(fnds.Travel, 0))+ 
		sum( isnull(fnds.Abroad, 0))+ 
		sum( isnull(fnds.Subcontracts, 0)) + 
        sum( isnull(fnds.IndirectCosts, 0)) + 
		sum( isnull(fnds.Others, 0))
	) as TotalLoc
FROM Funds fnds
WHERE (fnds.ProjectID = @ProjectID)
group by fnds.ProjectID                                    
                                    ">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-header">
                    <h3 class="card-title">Ninth Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelProjectImpact" CssClass="form-control-lg" runat="server" Text="Project Impact:"></asp:Label>
                                <asp:TextBox ID="TextBoxProjectImpact" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelSalariesDesc" CssClass="form-control-lg" runat="server" Text="Salaries (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxSalariesDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label10" CssClass="form-control-lg" runat="server" Text="Wages (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxWagesDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label13" CssClass="form-control-lg" runat="server" Text="Benefits (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxBenefitsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label9" CssClass="form-control-lg" runat="server" Text="Assistantships (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxAssistantshipsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelMaterialDesc" CssClass="form-control-lg" runat="server" Text="Materials (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxMaterialDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelEquipmentDesc" CssClass="form-control-lg" runat="server" Text="Equipment (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxEquipmentDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelTravelDesc" CssClass="form-control-lg" runat="server" Text="Travel (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxTravelDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelAbroadDesc" CssClass="form-control-lg" runat="server" Text="Abroad (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxAbroadDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label12" CssClass="form-control-lg" runat="server" Text="Subcontracts (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxSubcontractsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label11" CssClass="form-control-lg" runat="server" Text="Indirect Costs (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxIndirectCostsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelOthersDesc" CssClass="form-control-lg" runat="server" Text="Others (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxOthersDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-header">
                    <h3 class="card-title">Tenth Section</h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="card-body">
                </div>
            </div>


            <div style="text-align: center">
                <asp:Button runat="server" ID="btnSaveChanges2" CssClass="btn btn-primary" Text="Save Changes" OnClick="SaveChanges_Click" />
            </div>
        </div>
    </section>
</asp:Content>