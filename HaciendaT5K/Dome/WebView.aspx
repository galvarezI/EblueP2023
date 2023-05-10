<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="WebView.aspx.cs" Inherits="Eblue.Dome.WebView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <script  type="text/javascript">  
        function enablePostBack() {
            //T1 is the first argument(name of our control) I mentioned earlier and give the  
            // value of second argument as "" that's all  
            __TdoPostBack("T1", "");
        }

        function SelectTabFromServer(name, line)
        {
            __TdoPostBack('tab_'+name, line);
       }

       //function OnPostBack(eventTarget, eventArgument) {

       //}

       function OnPostBack(eventTarget) {
           alert("1");
           var flagButton = (eventTarget instanceof HTMLButtonElement);
           
               alert("2");
               var onClick = $(eventTarget).attr("uuid-serverClick");
               var sender = $(eventTarget).attr("id");
               var uuid = Symbol("uuid");

               var onClick = $(eventTarget).attr("uuid-serverClick");
               var e = { click: onClick };
           e[uuid] = sender;
           var jobj = JSON.stringify(e);

           EventPostBack(sender, jobj);
           
           //if (flagButton)
           //{   
           //    alert("2");
           //    var onClick = $(eventTarget).attr("uuid-serverClick");
           //    var sender = $(eventTarget).attr("id");
           //    var uuid = Symbol("uuid");

           //    var onClick = $(eventTarget).attr("uuid-serverClick");
           //    var e = { click: onClick };
           //    eventArgument[uuid] = sender;
           //    EventPostBack(sender, e);
           //}
       }


       function EventPostBack(eventTarget, eventArgument) {
           alert("3");
           document.aspnetForm.__EVENTTARGET.value = eventTarget;
           document.aspnetForm.__EVENTARGUMENT.value = eventArgument;
           document.aspnetForm.submit();
       }

        //function __TdoPostBack(eventTarget, eventArgument) {
        //    document.aspnetForm.__EVENTTARGET.value = eventTarget;
        //    document.aspnetForm.__EVENTARGUMENT.value = eventArgument;
        //    document.aspnetForm.submit();
        //}
   </script>

   <%-- [uuid ="btnOne"]--%>
    <asp:Button ID="controlButton" runat="server" Text ="Submit" OnClick="controlButton_Click"  />

    <%-- [ uuid ="cmbOne"]--%>
    <asp:DropDownList ID="controlDropDownList" runat="server" OnSelectedIndexChanged="controlDropDownList_SelectedIndexChanged"    AppendDataBoundItems="true"  AutoPostBack="true">
        <asp:ListItem Selected="True" Enabled="false">[placeholder]</asp:ListItem>
        <asp:ListItem Value=0>None</asp:ListItem>
        <asp:ListItem Value=1>One</asp:ListItem>
        <asp:ListItem Value=2>Two</asp:ListItem>
    </asp:DropDownList>

    <button id="tagButton" name="tagButton" onclick="OnPostBack(this)" uuid-serverClick="tagbutton_click" >tagButton</button>
</asp:Content>


