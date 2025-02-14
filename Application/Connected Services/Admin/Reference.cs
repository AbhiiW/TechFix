﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Admin {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Admin.AdminServiceSoap")]
    public interface AdminServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ViewItems", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ViewItems();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ViewItems", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ViewItemsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateQuotation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool CreateQuotation(int productId, decimal amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateQuotation", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> CreateQuotationAsync(int productId, decimal amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ViewQuotations", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ViewQuotations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ViewQuotations", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ViewQuotationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateQuotation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool UpdateQuotation(int quotationId, decimal newAmount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateQuotation", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> UpdateQuotationAsync(int quotationId, decimal newAmount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeleteQuotation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool DeleteQuotation(int quotationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeleteQuotation", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> DeleteQuotationAsync(int quotationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddSupplier", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool AddSupplier(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddSupplier", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> AddSupplierAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ViewSuppliers", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ViewSuppliers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ViewSuppliers", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ViewSuppliersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeleteSupplier", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool DeleteSupplier(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeleteSupplier", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> DeleteSupplierAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateProductQuantity", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool UpdateProductQuantity(int productId, int quantityToReduce);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateProductQuantity", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> UpdateProductQuantityAsync(int productId, int quantityToReduce);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AdminServiceSoapChannel : Application.Admin.AdminServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdminServiceSoapClient : System.ServiceModel.ClientBase<Application.Admin.AdminServiceSoap>, Application.Admin.AdminServiceSoap {
        
        public AdminServiceSoapClient() {
        }
        
        public AdminServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AdminServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataTable ViewItems() {
            return base.Channel.ViewItems();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ViewItemsAsync() {
            return base.Channel.ViewItemsAsync();
        }
        
        public bool CreateQuotation(int productId, decimal amount) {
            return base.Channel.CreateQuotation(productId, amount);
        }
        
        public System.Threading.Tasks.Task<bool> CreateQuotationAsync(int productId, decimal amount) {
            return base.Channel.CreateQuotationAsync(productId, amount);
        }
        
        public System.Data.DataTable ViewQuotations() {
            return base.Channel.ViewQuotations();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ViewQuotationsAsync() {
            return base.Channel.ViewQuotationsAsync();
        }
        
        public bool UpdateQuotation(int quotationId, decimal newAmount) {
            return base.Channel.UpdateQuotation(quotationId, newAmount);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateQuotationAsync(int quotationId, decimal newAmount) {
            return base.Channel.UpdateQuotationAsync(quotationId, newAmount);
        }
        
        public bool DeleteQuotation(int quotationId) {
            return base.Channel.DeleteQuotation(quotationId);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteQuotationAsync(int quotationId) {
            return base.Channel.DeleteQuotationAsync(quotationId);
        }
        
        public bool AddSupplier(string username, string password) {
            return base.Channel.AddSupplier(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> AddSupplierAsync(string username, string password) {
            return base.Channel.AddSupplierAsync(username, password);
        }
        
        public System.Data.DataTable ViewSuppliers() {
            return base.Channel.ViewSuppliers();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ViewSuppliersAsync() {
            return base.Channel.ViewSuppliersAsync();
        }
        
        public bool DeleteSupplier(string username) {
            return base.Channel.DeleteSupplier(username);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteSupplierAsync(string username) {
            return base.Channel.DeleteSupplierAsync(username);
        }
        
        public bool UpdateProductQuantity(int productId, int quantityToReduce) {
            return base.Channel.UpdateProductQuantity(productId, quantityToReduce);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateProductQuantityAsync(int productId, int quantityToReduce) {
            return base.Channel.UpdateProductQuantityAsync(productId, quantityToReduce);
        }
    }
}
