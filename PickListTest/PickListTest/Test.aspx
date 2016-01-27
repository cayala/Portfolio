<%@ Page Language="C#" Title="Test" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="PickListTest.Test" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="color: green">Testing</h1>
    <br />
    <label>Enter Name</label>
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Text=""></asp:TextBox>
    &nbsp;<asp:Button ID="Button1" runat="server" Text="submit" OnClick="Button1_Click" />

    <asp:Label runat="server" Text="" ID="dynamic"></asp:Label>
    <br />
    <br />
    <br />
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
    <asp:TextBox runat="server" Text="" ID="Add1Box"></asp:TextBox>
    <br />
    <label>
        Additional Fields
    </label>
    <br />
    <asp:TextBox runat="server" Text="" ID="Add2Box"></asp:TextBox>
    <asp:Button ID="addTableButton" runat="server" OnClick="AddPart" Text="Add Row"/>
    <br />
    <asp:Table ID="PartsTable" runat="server">
        <asp:TableHeaderRow>

            <asp:TableHeaderCell Text="">
                <p>Reference #</p>
            </asp:TableHeaderCell>

            <asp:TableHeaderCell Text="">
            <p>Part #</p>
            </asp:TableHeaderCell>

            <asp:TableHeaderCell Text="">
            <p>Description | Serial #</p>
            </asp:TableHeaderCell>

            <asp:TableHeaderCell Text="">
            <p>Quantity</p>
            </asp:TableHeaderCell>

            <asp:TableHeaderCell Text="">
            <p>Additional Fields</p>
            </asp:TableHeaderCell>

            <asp:TableHeaderCell Text="">
            <p>Additional Fields</p>
            </asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell >
                <p>123</p>
            </asp:TableCell>
            
            <asp:TableCell>
            <p>456</p>
            </asp:TableCell>
                        
            <asp:TableCell >
            <p>This is a test of a table in asp.net | 789</p>
            </asp:TableCell>
                        
            <asp:TableCell >
            <p>10</p>
            </asp:TableCell>
                        
            <asp:TableCell >
            <p>Nothing worth noting</p>
            </asp:TableCell>
                        
            <asp:TableCell >
            <p>Nothing worth noting</p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
