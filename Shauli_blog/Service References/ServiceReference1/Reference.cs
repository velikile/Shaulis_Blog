﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shauli_blog.ServiceReference1 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.daenet.de/webservices/CurrencyServer", ConfigurationName="ServiceReference1.CurrencyServerWebServiceSoap")]
    public interface CurrencyServerWebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet getDataSet(string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> getDataSetAsync(string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getXmlStream", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getXmlStream(string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getXmlStream", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getXmlStreamAsync(string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getCurrencyValue", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        double getCurrencyValue(string provider, string srcCurrency, string dstCurrency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getCurrencyValue", ReplyAction="*")]
        System.Threading.Tasks.Task<double> getCurrencyValueAsync(string provider, string srcCurrency, string dstCurrency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getDollarValue", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        double getDollarValue(string provider, string currency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getDollarValue", ReplyAction="*")]
        System.Threading.Tasks.Task<double> getDollarValueAsync(string provider, string currency);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getProviderDescription", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getProviderDescription(string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getProviderDescription", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getProviderDescriptionAsync(string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getProviderTimestamp", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getProviderTimestamp(string providerId, string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getProviderTimestamp", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getProviderTimestampAsync(string providerId, string provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getProviderList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getProviderList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.daenet.de/webservices/CurrencyServer/getProviderList", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getProviderListAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CurrencyServerWebServiceSoapChannel : Shauli_blog.ServiceReference1.CurrencyServerWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CurrencyServerWebServiceSoapClient : System.ServiceModel.ClientBase<Shauli_blog.ServiceReference1.CurrencyServerWebServiceSoap>, Shauli_blog.ServiceReference1.CurrencyServerWebServiceSoap {
        
        public CurrencyServerWebServiceSoapClient() {
        }
        
        public CurrencyServerWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CurrencyServerWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CurrencyServerWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CurrencyServerWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet getDataSet(string provider) {
            return base.Channel.getDataSet(provider);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> getDataSetAsync(string provider) {
            return base.Channel.getDataSetAsync(provider);
        }
        
        public string getXmlStream(string provider) {
            return base.Channel.getXmlStream(provider);
        }
        
        public System.Threading.Tasks.Task<string> getXmlStreamAsync(string provider) {
            return base.Channel.getXmlStreamAsync(provider);
        }
        
        public double getCurrencyValue(string provider, string srcCurrency, string dstCurrency) {
            return base.Channel.getCurrencyValue(provider, srcCurrency, dstCurrency);
        }
        
        public System.Threading.Tasks.Task<double> getCurrencyValueAsync(string provider, string srcCurrency, string dstCurrency) {
            return base.Channel.getCurrencyValueAsync(provider, srcCurrency, dstCurrency);
        }
        
        public double getDollarValue(string provider, string currency) {
            return base.Channel.getDollarValue(provider, currency);
        }
        
        public System.Threading.Tasks.Task<double> getDollarValueAsync(string provider, string currency) {
            return base.Channel.getDollarValueAsync(provider, currency);
        }
        
        public string getProviderDescription(string provider) {
            return base.Channel.getProviderDescription(provider);
        }
        
        public System.Threading.Tasks.Task<string> getProviderDescriptionAsync(string provider) {
            return base.Channel.getProviderDescriptionAsync(provider);
        }
        
        public string getProviderTimestamp(string providerId, string provider) {
            return base.Channel.getProviderTimestamp(providerId, provider);
        }
        
        public System.Threading.Tasks.Task<string> getProviderTimestampAsync(string providerId, string provider) {
            return base.Channel.getProviderTimestampAsync(providerId, provider);
        }
        
        public string getProviderList() {
            return base.Channel.getProviderList();
        }
        
        public System.Threading.Tasks.Task<string> getProviderListAsync() {
            return base.Channel.getProviderListAsync();
        }
    }
}
