using Leosac.CredentialProvisioning.Core;
using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Worker;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Collections.Specialized.BitVector32;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLAServer
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

        public override void Run(DeviceContext deviceCtx)
        {
            Trace.Assert(CredentialContext?.Credential != null, "Credential cannot be null");

            if (CredentialContext?.TemplateContent != null)
            {
                logger.Info(String.Format("Starting new encoding process for credential `{0}` ({1}) with template `{2}` ({3})", CredentialContext.Credential.Id, CredentialContext.Credential.Name, CredentialContext.TemplateId));
                HandleAction(CredentialContext.TemplateContent.FirstAction, CredentialContext, deviceCtx);
            }
            else
            {
                throw new EncodingException("Template cannot be null");
            }
        }

        private void HandleAction(EncodingActionProperties? actionProp, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, DeviceContext deviceCtx)
        {
            if (actionProp != null)
            {
                logger.Info(String.Format("Handling encoding action `{0}` (`{1}`), labeled `{2}`", actionProp.Id, actionProp.Name, actionProp.Label));

                try
                {
                    var action = CreateAction(actionProp);
                    if (action != null)
                    {
                        action.Run(encodingCtx, deviceCtx);
                        logger.Info("Action passed, running OnSuccess trigger");
                        ActionTrigger(actionProp.OnSuccess, encodingCtx, deviceCtx);
                    }
                    else
                    {
                        throw new EncodingException("Cannot initialize the targeted action.");
                    }
                }
                catch (EncodingException ex)
                {
                    logger.Info("Action failed, running OnFailure trigger");
                    ActionTrigger(actionProp.OnFailure, encodingCtx, deviceCtx, ex);
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
                logger.Error(String.Format("Cannot found dedicated EncodingAction with properties type `{0}` on assembly `{1}`.", actionProp.GetType().FullName, this.assembly.FullName));
                return null;
            }
        }

        private void ActionTrigger(EncodingActionProperties.ActionTrigger trigger, CredentialContext<EncodingFragmentTemplateContent> encodingCtx, DeviceContext deviceCtx, EncodingException? ex = null)
        {
            if (trigger.CallAction != null)
            {
                HandleAction(trigger.CallAction, encodingCtx, deviceCtx);
            }
            if (trigger.Throw)
            {
                if (!String.IsNullOrEmpty(trigger.ThrowCustomMessage))
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
