<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExampleWF.aspx.cs" Inherits="PickListTest.UserControls.ExampleWF" %>

<%@ Register TagPrefix="My" TagName="UserInfoboxControl" Src="~/UserControls/ExampleUC.ascx" %>
<%@ Reference Control="~/UserControls/ExampleUC.ascx" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<%--    <asp:ScriptManagerProxy runat="server" ID="proxManager">
        <Scripts>
            <asp:ScriptReference Path="~/UserControls/Example.js" />
        </Scripts>
    </asp:ScriptManagerProxy>--%>
    <h1>This is displaying</h1>
    <asp:PlaceHolder runat="server" ID="phUserInfoBox"></asp:PlaceHolder>
    <script src="Example.js"></script>
    <div>
        <label>Enter Name</label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Text=""></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="submit" OnClick="generateContent" />
        <br />
        <label>
            Reference # 
        </label>
        <br />
        <asp:TextBox runat="server" Text="" ID="referenceBox"></asp:TextBox>
        <br />
        <label>
            Part #
        </label>
        <br />
        <asp:TextBox runat="server" Text="" ID="partBox"></asp:TextBox>
        <br />
        <label>
            Description | Serial #
        </label>
        <br />
        <asp:TextBox runat="server" Text="" ID="descriptionBox"></asp:TextBox>
        <br />
        <label>
            Quantity
        </label>
        <br />
        <asp:TextBox runat="server" Text="" ID="QuantityBox"></asp:TextBox>
        <br />
        <label>
            Additional Fields
        </label>
        <br />
        <%--<asp:Button ID="loadTable" OnClick="LoadTable" runat="server"/>--%>
        <br />
        <asp:TextBox runat="server" Text="" ID="Add1Box"></asp:TextBox>
        <br />
        <asp:Button ID="addTableButton" runat="server" OnClick="AddPart" Text="Add Row" />
        <br />
        <asp:Button ID="cookieButton" OnClick="StoreTableToCookie" Text="Store Cookie" runat="server" />
        <asp:Button ID="Button2" OnClick="DeleteCookie" Text="Delete Cookie" runat="server" />
        <asp:Button ID="excelButton" OnClick="ExportToExcel" Text="Download to Excel" runat="server" />
        <asp:Label runat="server" Text="" ID="dynamic"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:GridView ID="gvDetails" runat="server">
        </asp:GridView>
    </div>
    <asp:Button ID="jsButton" OnClientClick="return alert('asp.net clicked me')" runat="server" Text="Test JS"></asp:Button>
    <button id="ajax" type="button">Ajax call</button>
    <button id="storeTable" type="button">Serialize</button>
</asp:Content>
