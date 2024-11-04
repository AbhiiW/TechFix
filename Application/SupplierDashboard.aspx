<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierDashboard.aspx.cs" Inherits="Application.SupplierDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title>Supplier Dashboard</title>   
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <h1>Supplier Dashboard</h1>

            <!-- View Stock Section -->
            <div class="section">
                <h3>View Stock</h3>
                <asp:GridView ID="GridViewStock" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnRefreshStock" runat="server" Text="Refresh Stock" OnClick="btnRefreshStock_Click" />
            </div>

           <!-- Insert Product Section -->
            <div class="section">
                <h3>Insert Product</h3>
                <asp:TextBox ID="txtProductName" runat="server" placeholder="Product Name" />
                <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" />
                <asp:TextBox ID="txtPrice" runat="server" placeholder="Price" />
                <asp:TextBox ID="txtQuantity" runat="server" placeholder="Quantity" />
                <asp:HiddenField ID="hfUsername" runat="server" Value="<%= User.Identity.Name %>" />

                <asp:Button ID="btnInsertProduct" runat="server" Text="Insert Product" OnClick="btnInsertProduct_Click" />
                </asp:Content>
            </div>


            <!-- Delete Product Section -->
            <div class="section">
                <h3>Delete Product</h3>
                <asp:TextBox ID="txtDeleteProductId" runat="server" Placeholder="Product ID"></asp:TextBox><br />
                <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete Product" OnClick="btnDeleteProduct_Click" CssClass="button" />
            </div>

            <!-- Update Product Section -->
            <div class="section">
                <h3>Update Product</h3>
                <asp:TextBox ID="txtUpdateProductId" runat="server" Placeholder="Product ID"></asp:TextBox><br />
                <asp:TextBox ID="txtUpdateProductName" runat="server" Placeholder="New Product Name"></asp:TextBox><br />
                <asp:TextBox ID="txtUpdateDescription" runat="server" Placeholder="New Description"></asp:TextBox><br />
                <asp:TextBox ID="txtUpdatePrice" runat="server" Placeholder="New Price"></asp:TextBox><br />
                <asp:TextBox ID="txtUpdateQuantity" runat="server" Placeholder="New Quantity"></asp:TextBox><br />
                <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" OnClick="btnUpdateProduct_Click" CssClass="button" />
            </div>

            <!-- View Created Quotations Section -->
 <div class="section">
     <h3>View Created Quotations</h3>
     <asp:GridView ID="GridViewQuotations" runat="server" AutoGenerateColumns="False">
         <Columns>
             <asp:BoundField DataField="QuotationID" HeaderText="Quotation ID" />
             <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
             <asp:BoundField DataField="Amount" HeaderText="Amount" />
             <asp:BoundField DataField="CreatedAt" HeaderText="Created At" />
         </Columns>
     </asp:GridView>
     <asp:Button ID="btnRefreshQuotations" runat="server" Text="Refresh Quotations" OnClick="btnRefreshQuotations_Click" CssClass="button" />
 </div>

          

        </div>
    </form>
</body>
</html>
