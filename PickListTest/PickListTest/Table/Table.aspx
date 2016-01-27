<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Table.aspx.cs" Inherits="PickListTest.Table.Table" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 50%;">
                <tr>
                    <td>Add Name</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Add Name"
                            OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="ID" EmptyDataText="There are no data here yet!"
                            OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        NameHeaderTemplate>
                                        <itemtemplate>ItemTemplate>
<asp:TemplateField>
<asp:TemplateField HeaderText="Remove">
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" CommandArgument=''
CommandName="Delete" runat="server">Removeasp:LinkButton
</asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
td>
</tr>
</table>
</div>
    </form>
</body>
</html>
