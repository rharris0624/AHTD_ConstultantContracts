﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.431
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AHTD.Logging.AppLogService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AppLogService.IAppLogService")]
    public interface IAppLogService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppLogService/LogException", ReplyAction="http://tempuri.org/IAppLogService/LogExceptionResponse")]
        void LogException(string appName, string text);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IAppLogService/LogException", ReplyAction="http://tempuri.org/IAppLogService/LogExceptionResponse")]
        System.IAsyncResult BeginLogException(string appName, string text, System.AsyncCallback callback, object asyncState);
        
        void EndLogException(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppLogService/LogUnhandledException", ReplyAction="http://tempuri.org/IAppLogService/LogUnhandledExceptionResponse")]
        void LogUnhandledException(string appName, string text);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IAppLogService/LogUnhandledException", ReplyAction="http://tempuri.org/IAppLogService/LogUnhandledExceptionResponse")]
        System.IAsyncResult BeginLogUnhandledException(string appName, string text, System.AsyncCallback callback, object asyncState);
        
        void EndLogUnhandledException(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppLogService/LogTraceMessage", ReplyAction="http://tempuri.org/IAppLogService/LogTraceMessageResponse")]
        void LogTraceMessage(string appName, string text);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IAppLogService/LogTraceMessage", ReplyAction="http://tempuri.org/IAppLogService/LogTraceMessageResponse")]
        System.IAsyncResult BeginLogTraceMessage(string appName, string text, System.AsyncCallback callback, object asyncState);
        
        void EndLogTraceMessage(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppLogService/LogDebugMessage", ReplyAction="http://tempuri.org/IAppLogService/LogDebugMessageResponse")]
        void LogDebugMessage(string appName, string text);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IAppLogService/LogDebugMessage", ReplyAction="http://tempuri.org/IAppLogService/LogDebugMessageResponse")]
        System.IAsyncResult BeginLogDebugMessage(string appName, string text, System.AsyncCallback callback, object asyncState);
        
        void EndLogDebugMessage(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAppLogServiceChannel : AHTD.Logging.AppLogService.IAppLogService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AppLogServiceClient : System.ServiceModel.ClientBase<AHTD.Logging.AppLogService.IAppLogService>, AHTD.Logging.AppLogService.IAppLogService {
        
        private BeginOperationDelegate onBeginLogExceptionDelegate;
        
        private EndOperationDelegate onEndLogExceptionDelegate;
        
        private System.Threading.SendOrPostCallback onLogExceptionCompletedDelegate;
        
        private BeginOperationDelegate onBeginLogUnhandledExceptionDelegate;
        
        private EndOperationDelegate onEndLogUnhandledExceptionDelegate;
        
        private System.Threading.SendOrPostCallback onLogUnhandledExceptionCompletedDelegate;
        
        private BeginOperationDelegate onBeginLogTraceMessageDelegate;
        
        private EndOperationDelegate onEndLogTraceMessageDelegate;
        
        private System.Threading.SendOrPostCallback onLogTraceMessageCompletedDelegate;
        
        private BeginOperationDelegate onBeginLogDebugMessageDelegate;
        
        private EndOperationDelegate onEndLogDebugMessageDelegate;
        
        private System.Threading.SendOrPostCallback onLogDebugMessageCompletedDelegate;
        
        public AppLogServiceClient() {
        }
        
        public AppLogServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AppLogServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AppLogServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AppLogServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogExceptionCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogUnhandledExceptionCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogTraceMessageCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogDebugMessageCompleted;
        
        public void LogException(string appName, string text) {
            base.Channel.LogException(appName, text);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLogException(string appName, string text, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogException(appName, text, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndLogException(System.IAsyncResult result) {
            base.Channel.EndLogException(result);
        }
        
        private System.IAsyncResult OnBeginLogException(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string appName = ((string)(inValues[0]));
            string text = ((string)(inValues[1]));
            return this.BeginLogException(appName, text, callback, asyncState);
        }
        
        private object[] OnEndLogException(System.IAsyncResult result) {
            this.EndLogException(result);
            return null;
        }
        
        private void OnLogExceptionCompleted(object state) {
            if ((this.LogExceptionCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogExceptionCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogExceptionAsync(string appName, string text) {
            this.LogExceptionAsync(appName, text, null);
        }
        
        public void LogExceptionAsync(string appName, string text, object userState) {
            if ((this.onBeginLogExceptionDelegate == null)) {
                this.onBeginLogExceptionDelegate = new BeginOperationDelegate(this.OnBeginLogException);
            }
            if ((this.onEndLogExceptionDelegate == null)) {
                this.onEndLogExceptionDelegate = new EndOperationDelegate(this.OnEndLogException);
            }
            if ((this.onLogExceptionCompletedDelegate == null)) {
                this.onLogExceptionCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogExceptionCompleted);
            }
            base.InvokeAsync(this.onBeginLogExceptionDelegate, new object[] {
                        appName,
                        text}, this.onEndLogExceptionDelegate, this.onLogExceptionCompletedDelegate, userState);
        }
        
        public void LogUnhandledException(string appName, string text) {
            base.Channel.LogUnhandledException(appName, text);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLogUnhandledException(string appName, string text, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogUnhandledException(appName, text, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndLogUnhandledException(System.IAsyncResult result) {
            base.Channel.EndLogUnhandledException(result);
        }
        
        private System.IAsyncResult OnBeginLogUnhandledException(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string appName = ((string)(inValues[0]));
            string text = ((string)(inValues[1]));
            return this.BeginLogUnhandledException(appName, text, callback, asyncState);
        }
        
        private object[] OnEndLogUnhandledException(System.IAsyncResult result) {
            this.EndLogUnhandledException(result);
            return null;
        }
        
        private void OnLogUnhandledExceptionCompleted(object state) {
            if ((this.LogUnhandledExceptionCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogUnhandledExceptionCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogUnhandledExceptionAsync(string appName, string text) {
            this.LogUnhandledExceptionAsync(appName, text, null);
        }
        
        public void LogUnhandledExceptionAsync(string appName, string text, object userState) {
            if ((this.onBeginLogUnhandledExceptionDelegate == null)) {
                this.onBeginLogUnhandledExceptionDelegate = new BeginOperationDelegate(this.OnBeginLogUnhandledException);
            }
            if ((this.onEndLogUnhandledExceptionDelegate == null)) {
                this.onEndLogUnhandledExceptionDelegate = new EndOperationDelegate(this.OnEndLogUnhandledException);
            }
            if ((this.onLogUnhandledExceptionCompletedDelegate == null)) {
                this.onLogUnhandledExceptionCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogUnhandledExceptionCompleted);
            }
            base.InvokeAsync(this.onBeginLogUnhandledExceptionDelegate, new object[] {
                        appName,
                        text}, this.onEndLogUnhandledExceptionDelegate, this.onLogUnhandledExceptionCompletedDelegate, userState);
        }
        
        public void LogTraceMessage(string appName, string text) {
            base.Channel.LogTraceMessage(appName, text);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLogTraceMessage(string appName, string text, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogTraceMessage(appName, text, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndLogTraceMessage(System.IAsyncResult result) {
            base.Channel.EndLogTraceMessage(result);
        }
        
        private System.IAsyncResult OnBeginLogTraceMessage(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string appName = ((string)(inValues[0]));
            string text = ((string)(inValues[1]));
            return this.BeginLogTraceMessage(appName, text, callback, asyncState);
        }
        
        private object[] OnEndLogTraceMessage(System.IAsyncResult result) {
            this.EndLogTraceMessage(result);
            return null;
        }
        
        private void OnLogTraceMessageCompleted(object state) {
            if ((this.LogTraceMessageCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogTraceMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogTraceMessageAsync(string appName, string text) {
            this.LogTraceMessageAsync(appName, text, null);
        }
        
        public void LogTraceMessageAsync(string appName, string text, object userState) {
            if ((this.onBeginLogTraceMessageDelegate == null)) {
                this.onBeginLogTraceMessageDelegate = new BeginOperationDelegate(this.OnBeginLogTraceMessage);
            }
            if ((this.onEndLogTraceMessageDelegate == null)) {
                this.onEndLogTraceMessageDelegate = new EndOperationDelegate(this.OnEndLogTraceMessage);
            }
            if ((this.onLogTraceMessageCompletedDelegate == null)) {
                this.onLogTraceMessageCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogTraceMessageCompleted);
            }
            base.InvokeAsync(this.onBeginLogTraceMessageDelegate, new object[] {
                        appName,
                        text}, this.onEndLogTraceMessageDelegate, this.onLogTraceMessageCompletedDelegate, userState);
        }
        
        public void LogDebugMessage(string appName, string text) {
            base.Channel.LogDebugMessage(appName, text);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLogDebugMessage(string appName, string text, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogDebugMessage(appName, text, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndLogDebugMessage(System.IAsyncResult result) {
            base.Channel.EndLogDebugMessage(result);
        }
        
        private System.IAsyncResult OnBeginLogDebugMessage(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string appName = ((string)(inValues[0]));
            string text = ((string)(inValues[1]));
            return this.BeginLogDebugMessage(appName, text, callback, asyncState);
        }
        
        private object[] OnEndLogDebugMessage(System.IAsyncResult result) {
            this.EndLogDebugMessage(result);
            return null;
        }
        
        private void OnLogDebugMessageCompleted(object state) {
            if ((this.LogDebugMessageCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogDebugMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogDebugMessageAsync(string appName, string text) {
            this.LogDebugMessageAsync(appName, text, null);
        }
        
        public void LogDebugMessageAsync(string appName, string text, object userState) {
            if ((this.onBeginLogDebugMessageDelegate == null)) {
                this.onBeginLogDebugMessageDelegate = new BeginOperationDelegate(this.OnBeginLogDebugMessage);
            }
            if ((this.onEndLogDebugMessageDelegate == null)) {
                this.onEndLogDebugMessageDelegate = new EndOperationDelegate(this.OnEndLogDebugMessage);
            }
            if ((this.onLogDebugMessageCompletedDelegate == null)) {
                this.onLogDebugMessageCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogDebugMessageCompleted);
            }
            base.InvokeAsync(this.onBeginLogDebugMessageDelegate, new object[] {
                        appName,
                        text}, this.onEndLogDebugMessageDelegate, this.onLogDebugMessageCompletedDelegate, userState);
        }
    }
}
