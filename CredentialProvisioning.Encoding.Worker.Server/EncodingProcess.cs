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
                        action.Run(encodingCtx, cardCtx);
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

        private EncodingAction? CreateAction(EncodingActionProperties actionProp)
        {
            var baseType = typeof(EncodingAction<>).MakeGenericType(actionProp.GetType());
            var actionType = this.assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t)).FirstOrDefault();
            if (actionType != null)
                return Activator.CreateInstance(actionType) as EncodingAction;
            else
            {
                logger.Error(string.Format("Cannot found dedicated EncodingAction with properties type `{0}` on assembly `{1}`.", actionProp.GetType().FullName, this.assembly.FullName));
                return null;
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
