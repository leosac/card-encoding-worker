using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Worker;
using log4net;
using System.Diagnostics;
using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingProcess : CredentialProcess<EncodingFragmentTemplateContent>
    {
        static readonly ILog logger = LogManager.GetLogger(typeof(EncodingProcess));

        Assembly assembly;

        public EncodingProcess()
        {
            this.assembly = Assembly.GetCallingAssembly();
        }

        public EncodingProcess(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public override async Task Run(DeviceContext context)
        {
            Trace.Assert(CredentialContext?.Credentials != null, "Credentials cannot be null");
            if (context is EncodingDeviceContext deviceCtx)
            {
                if (CredentialContext?.TemplateContent != null)
                {
                    if (!await deviceCtx.Initialize())
                        throw new EncodingException("Device initialization failed.");

                    foreach (var credential in CredentialContext.Credentials)
                    {
                        logger.Info(string.Format("Starting new encoding process for credential `{0}` with template `{1}`", credential.Label, CredentialContext.TemplateId));

                        var cardCtx = await deviceCtx.PrepareCard(credential);
                        if (cardCtx == null)
                            throw new EncodingException("Card preparation failed.");

                        await HandleAction(CredentialContext.TemplateContent.FirstAction, CredentialContext, cardCtx);
                        await deviceCtx.CompleteCard(cardCtx);
                        await OnCredentialCompleted(GetFieldChanges(cardCtx));
                    }
                    await deviceCtx.UnInitialize();
                    await OnProcessCompleted(ProvisioningState.Completed);
                }
                else
                {
                    throw new EncodingException("Template cannot be null");
                }
            }
            else
            {
                throw new EncodingException("Unexpected device context type.");
            }
        }

        private IDictionary<string, object>? GetFieldChanges(CardContext cardCtx)
        {
            if (cardCtx.Credential == null || cardCtx.FieldsChanged == null)
                return null;

            var changes = new Dictionary<string, object>();
            var fields = cardCtx.Credential as IDictionary<string, object>;
            if (fields != null)
            {
                foreach (var fieldName in cardCtx.FieldsChanged)
                {
                    if (fields.ContainsKey(fieldName))
                    {
                        changes.Add(fieldName, fields[fieldName]);
                    }
                }
            }
            return changes;
        }

        private async Task HandleAction(EncodingActionProperties? actionProp, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            if (actionProp != null)
            {
                logger.Info(string.Format("Handling encoding action `{0}`, labeled `{1}`", actionProp.Name, actionProp.Label));

                try
                {
                    var action = CreateAction(actionProp);
                    if (action != null)
                    {
                        CreateAndRunServices(actionProp.ServicesBefore, cardCtx, action);
                        action.Run(encodingCtx, cardCtx);
                        CreateAndRunServices(actionProp.ServicesAfter, cardCtx, action);

                        logger.Info("Action passed, running OnSuccess trigger");
                        ActionTrigger(actionProp.OnSuccess, encodingCtx, cardCtx);
                    }
                    else
                    {
                        throw new EncodingException("Cannot initialize the targeted action.");
                    }
                }
                catch (EncodingException ex)
                {
                    logger.Info("Action failed, running OnFailure trigger");
                    ActionTrigger(actionProp.OnFailure, encodingCtx, cardCtx, ex);
                    await OnProcessCompleted(ProvisioningState.Failed);
                }
            }
        }

        private void CreateAndRunServices(IEnumerable<EncodingServiceProperties>? servicesProp, CardContext cardCtx, EncodingAction currentAction)
        {
            if (servicesProp != null)
            {
                foreach (var serviceProp in servicesProp)
                {
                    var service = CreateEncodingService(serviceProp);
                    if (service != null)
                    {
                        service.Run(cardCtx, currentAction);
                    }
                }
            }
        }

        private EncodingAction? CreateAction(EncodingActionProperties actionProp)
        {
            return CreateMiddlewareImpl<EncodingAction, EncodingActionProperties>(typeof(EncodingAction<>), actionProp);
        }

        private EncodingService? CreateEncodingService(EncodingServiceProperties serviceProp)
        {
            return CreateMiddlewareImpl<EncodingService, EncodingServiceProperties>(typeof(EncodingService<>), serviceProp);
        }

        private T1? CreateMiddlewareImpl<T1, T2>(Type baseGeneric, T2 properties) where T1 : class
        {
            var baseType = baseGeneric.MakeGenericType(properties.GetType());
            var type = this.assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t)).FirstOrDefault();
            if (type != null)
                return Activator.CreateInstance(type) as T1;
            else
            {
                logger.Error(string.Format("Cannot found dedicated {0} with properties type `{1}` on assembly `{2}`.", nameof(T1), properties.GetType().FullName, this.assembly.FullName));
                return default(T1);
            }
        }

        private void ActionTrigger(EncodingActionProperties.ActionTrigger trigger, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx, EncodingException? ex = null)
        {
            if (trigger.CallAction != null)
            {
                HandleAction(trigger.CallAction, encodingCtx, cardCtx);
            }
            if (trigger.Throw)
            {
                if (!string.IsNullOrEmpty(trigger.ThrowCustomMessage))
                {
                    throw new EncodingException(trigger.ThrowCustomMessage, ex);
                }
                else if (ex != null)
                {
                    throw ex;
                }
                else
                {
                    throw new EncodingException();
                }
            }
        }
    }
}
