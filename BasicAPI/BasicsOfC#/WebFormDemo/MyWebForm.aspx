<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyWebForm.aspx.cs" Inherits="WebFormDemo.MyWebForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee Form</h2>
            <div>
                First Name:&nbsp;&nbsp;
                <asp:TextBox ID="FName" runat="server"></asp:TextBox>
            </div>
            <div>
                Last Name:&nbsp;&nbsp;
                <asp:TextBox ID="LName" runat="server"></asp:TextBox>
            </div>
            <div>
                Description:&nbsp;&nbsp;
                <asp:TextBox ID="Description" runat="server"></asp:TextBox>
            </div>
            <div>
                Salary:&nbsp;&nbsp;
                <asp:TextBox ID="Salary" runat="server"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
            </div>
            <br />
            <div>
                <h3>Employee Information</h3>
                <asp:GridView ID="EmployeeGrid" runat="server" AutoGenerateColumns="false" OnRowDeleting="EmployeeGrid_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" />
                        <asp:CommandField ShowDeleteButton="true" HeaderText="Actions" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
