<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="Application.AdminDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="StyleSheet.css" rel="stylesheet" />

    <title>Admin Dashboard</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <h1>Admin Dashboard</h1>

            <!-- Add Supplier Button -->
            <div class="add-supplier-button">
                <button type="button" class="button" onclick="openModal()">Add Supplier</button>
            </div>

            <!-- Supplier Modal -->
<div id="supplierModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="closeModal()">&times;</span>
        <h3>Add Supplier</h3>
        <asp:TextBox ID="txtSupplierUsername" runat="server" Placeholder="Supplier Username"></asp:TextBox><br /><br />
        <asp:TextBox ID="txtSupplierPassword" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox><br /><br />
        <asp:TextBox ID="txtSupplierConfirmPassword" runat="server" Placeholder="Confirm Password" TextMode="Password"></asp:TextBox><br /><br />
        <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" OnClick="btnAddSupplier_Click" CssClass="button" /><br /><br />

        <!-- Existing Suppliers GridView -->
        <h3>Existing Suppliers</h3>
        <asp:GridView ID="GridViewSuppliers" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewSuppliers_RowCommand">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Supplier Username" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDeleteSupplier" runat="server" CommandName="DeleteSupplier" CommandArgument='<%# Eval("Username") %>' Text="X" CssClass="button" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>

      
                     <!-- View Stock Section with UpdatePanel for partial refresh -->
<div class="section">
    <h3>View Stock</h3>
    <div class="refresh-timer">
        Refresh in: <span id="timer">20</span> seconds
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelStock" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridViewStock" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewStock_RowCommand">
    <Columns>
        <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Price" HeaderText="Price" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnRequestQuotation" runat="server" CommandName="RequestQuotation" CommandArgument='<%# Eval("ProductID") + "|" + Eval("ProductName") %>' Text="Request Quotation" CssClass="button" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnHidden" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnHidden" runat="server" OnClick="btnHidden_Click" Style="display:none;" />
</div>

<!-- Quotation Modal -->
<div id="quotationModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="closeQuotationModal()">&times;</span>
        <h3>Request Quotation</h3>
        <asp:TextBox ID="txtModalProductID" runat="server" ReadOnly="True" Placeholder="Product ID"></asp:TextBox><br /><br />
        <asp:TextBox ID="txtModalProductName" runat="server" ReadOnly="True" Placeholder="Product Name"></asp:TextBox><br /><br />
        <asp:TextBox ID="txtModalQuantity" runat="server" Placeholder="Quantity"></asp:TextBox><br /><br />
        <asp:Button ID="btnModalCreateQuotation" runat="server" Text="Create Quotation" OnClick="btnCreateQuotation_Click" CssClass="button" />
    </div>
</div>



           

            <!-- View Created Quotations Section -->
<div class="section">
    <h3>View Created Quotations</h3>
    <asp:UpdatePanel ID="UpdatePanelQuotations" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridViewQuotations" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewQuotations_RowCommand">
                <Columns>
                    <asp:BoundField DataField="QuotationID" HeaderText="Quotation ID" />
                    <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                    <asp:BoundField DataField="CreatedAt" HeaderText="Created At" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnUpdateQuotation" runat="server" CommandName="UpdateQuotation" CommandArgument='<%# Eval("QuotationID") + "|" + Eval("Amount") %>' Text="Update" CssClass="button" />
                            <asp:Button ID="btnDeleteQuotation" runat="server" CommandName="DeleteQuotation" CommandArgument='<%# Eval("QuotationID") %>' Text="Delete" CssClass="button" OnClientClick="return confirm('Are you sure you want to delete this quotation?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<!-- Update Quotation Modal -->
<div id="updateQuotationModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="closeUpdateQuotationModal()">&times;</span>
        <h3>Update Quotation</h3>
        <asp:TextBox ID="txtModalUpdateQuotationID" runat="server" ReadOnly="True" Placeholder="Quotation ID"></asp:TextBox><br /><br />
        <asp:TextBox ID="txtModalUpdateAmount" runat="server" Placeholder="New Amount"></asp:TextBox><br /><br />
        <asp:Button ID="btnModalUpdateQuotation" runat="server" Text="Update Quotation" OnClick="btnModalUpdateQuotation_Click" CssClass="button" />
    </div>
</div>


<script type="text/javascript">
    var countdown = 20;

    function updateTimer() {
        document.getElementById('timer').innerText = countdown;
        countdown--;

        if (countdown < 0) {
            countdown = 20;
            __doPostBack('<%= btnHidden.ClientID %>', '');
        }
    }

    setInterval(updateTimer, 1000);

    // Quotation Create Model
    function openQuotationModal(productId, productName) {
        document.getElementById('<%= txtModalProductID.ClientID %>').value = productId;
        document.getElementById('<%= txtModalProductName.ClientID %>').value = productName;
        document.getElementById('quotationModal').style.display = "block";
    }

    function closeQuotationModal() {
        document.getElementById('quotationModal').style.display = "none";
    }

    // Supplier Model View
    function openModal() {
        document.getElementById('supplierModal').style.display = "block";
    }

    function closeModal() {
        document.getElementById('supplierModal').style.display = "none";
    }
</script>



<script type="text/javascript">
    // Qurtations Update Model
    function openUpdateQuotationModal(quotationId, amount) {
        document.getElementById('<%= txtModalUpdateQuotationID.ClientID %>').value = quotationId;
        document.getElementById('<%= txtModalUpdateAmount.ClientID %>').value = amount;
        document.getElementById('updateQuotationModal').style.display = "block";
    }

    function closeUpdateQuotationModal() {
        document.getElementById('updateQuotationModal').style.display = "none";
    }
</script>
    </form>
</body>
</html>