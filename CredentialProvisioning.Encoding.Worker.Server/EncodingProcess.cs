using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Worker;
using System.Diagnostics;
using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingProcess : CredentialProcess<EncodingFragmentTemplateContent>
    {
        ILogger? _logger;
        Assembly _assembly;

        public EncodingProcess()
        {
            _assembly = Assembly.GetCallingAssembly();
        }

        public EncodingProcess(Assembly assembly, ILogger logger)
        {
            _assembly = assembly;
            _logger = logger;
        }

        public override async Task Run(DeviceContext context)
        {
            Trace.Assert(CredentialContext?.Credentials != null, "Credentials cannot be null");
            try
            {
                if (context is EncodingDeviceContext deviceCtx)
                {
                    if (CredentialContext?.TemplateContent != null)
                    {
                        if (!await deviceCtx.Initialize(CredentialContext?.TemplateContent.ForceCardType))
                            throw new EncodingException("Device initialization failed.");

                        foreach (var credential in CredentialContext.Credentials)
                        {
                            _logger?.LogInformation("Starting new encoding process for credential `{0}` with template `{1}`", credential.Label, CredentialContext.TemplateId);

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
            catch(Exception)
            {
                //await OnProcessCompleted(ProvisioningState.Failed);
                throw;
            }
        }

        private IDictionary<string, object>? GetFieldChanges(CardContext cardCtx)
        {
            if (cardCtx.Credential == null || cardCtx.FieldsChanged == null)
                return null;

            var changes = new Dictionary<string, object>();
            var fields = cardCtx.Credential?.Data as IDictionary<string, object>;
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

        private async Task HandleActions(EncodingActionProperties[] actionProps, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            foreach (var actionProp in actionProps)
            {
                await HandleAction(actionProp, encodingCtx, cardCtx);
            }
        }

        private async Task HandleAction(EncodingActionProperties? actionProp, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            if (actionProp != null)
            {
                _logger?.LogInformation("Handling encoding action `{0}`, labeled `{1}`", actionProp.Name, actionProp.Label);

                try
                {
                    var action = CreateAction(actionProp);
                    if (action != null)
                    {
                        CreateAndRunServices(actionProp.ServicesBefore, cardCtx, (encodingCtx as EncodingContext)?.Keys, action);
                        action.Run(encodingCtx, cardCtx);
                        CreateAndRunServices(actionProp.ServicesAfter, cardCtx, (encodingCtx as EncodingContext)?.Keys, action);

                        _logger?.LogInformation("Action passed, running OnSuccess trigger");
                        if (actionProp.OnSuccess != null)
                        {
                            await ActionTrigger(actionProp.OnSuccess, encodingCtx, cardCtx);
                        }
                    }
                    else
                    {
                        throw new EncodingException("Cannot initialize the targeted action.");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogInformation("Action failed with error `{0}`, running OnFailure trigger", ex.Message);
                    if (actionProp.OnFailure == null)
                    {
                        actionProp.OnFailure = new EncodingActionProperties.ActionTrigger() { Throw = true };
                    }

                    await ActionTrigger(actionProp.OnFailure, encodingCtx, cardCtx, ex);
                }
            }
        }

        private void CreateAndRunServices(IEnumerable<EncodingServiceProperties>? servicesProp, CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            if (servicesProp != null)
            {
                foreach (var serviceProp in servicesProp)
                {
                    var service = CreateEncodingService(serviceProp);
                    if (service != null)
                    {
                        service.Run(cardCtx, keystore, currentAction);
                    }
                }
            }
        }

        private EncodingAction? CreateAction(EncodingActionProperties actionProp)
        {
            var instance = CreateMiddlewareImpl<EncodingAction, EncodingActionProperties>(typeof(EncodingAction<>), actionProp);
            return instance;
        }

        private EncodingService? CreateEncodingService(EncodingServiceProperties serviceProp)
        {
            return CreateMiddlewareImpl<EncodingService, EncodingServiceProperties>(typeof(EncodingService<>), serviceProp);
        }

        private T1? CreateMiddlewareImpl<T1, T2>(Type baseGeneric, T2 properties) where T1 : class where T2 : class, ICloneable
        {
            var cp = properties.Clone() as T2;
            var baseType = baseGeneric.MakeGenericType(cp.GetType());
            var type = _assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t)).FirstOrDefault();
            if (type != null)
            {
                return Activator.CreateInstance(type, cp) as T1;
            }
            else
            {
                _logger?.LogError("Cannot found dedicated {0} with properties type `{1}` on assembly `{2}`.", nameof(T1), properties.GetType().FullName, _assembly.FullName);
                return default(T1);
            }
        }

        private async Task ActionTrigger(EncodingActionProperties.ActionTrigger trigger, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx, Exception? ex = null)
        {
            if (trigger.CallActions != null)
            {
                await HandleActions(trigger.CallActions, encodingCtx, cardCtx);
            }
            if (trigger.Throw)
            {
                await OnProcessCompleted(ProvisioningState.Failed);
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
