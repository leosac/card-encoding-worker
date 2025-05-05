using Microsoft.AspNetCore.Mvc;
using System;
using System.CommandLine;
using System.CommandLine.Binding;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class OptionsBinder : BinderBase<Options>
    {
        private readonly Options? _options;
        private readonly Option<string?> _repositoryOption;
        private readonly Option<string?> _keyStoreOption;
        private readonly Option<bool?> _mgtapiOption;
        private readonly Option<string?> _apikeyOption;
        private readonly Option<string?> _integritykeyOption;
        private readonly Option<ReaderType> _readerTypeOption;
        private readonly Option<string> _contactlessReaderOption;
        private readonly Option<string> _samReaderOption;
        private readonly Option<string?> _pkcs11LibraryOption;
        private readonly Option<string?> _pkcs11PasswordOption;

        public OptionsBinder(Options? options, Option<string?> repositoryOption, Option<string?> keyStoreOption, Option<bool?> mgtapiOption, Option<string?> apikeyOption, Option<string?> integritykeyOption, Option<ReaderType> readerTypeOption, Option<string> contactlessReaderOption, Option<string> samReaderOption, Option<string?> pkcs11LibraryOption, Option<string?> pkcs11PasswordOption)
        {
            _options = options;
            _repositoryOption = repositoryOption;
            _keyStoreOption = keyStoreOption;
            _mgtapiOption = mgtapiOption;
            _apikeyOption = apikeyOption;
            _integritykeyOption = integritykeyOption;
            _readerTypeOption = readerTypeOption;
            _contactlessReaderOption = contactlessReaderOption;
            _samReaderOption = samReaderOption;
            _pkcs11LibraryOption = pkcs11LibraryOption;
            _pkcs11PasswordOption = pkcs11PasswordOption;
        }

        protected override Options GetBoundValue(BindingContext bindingContext)
        {
            var options = _options ?? new Options();
            options.TemplateRepository = bindingContext.ParseResult.GetValueForOption(_repositoryOption);
            options.KeyStore = bindingContext.ParseResult.GetValueForOption(_keyStoreOption);
            options.ManagementApi = bindingContext.ParseResult.GetValueForOption(_mgtapiOption);
            options.APIKey = bindingContext.ParseResult.GetValueForOption(_apikeyOption);
            options.DataIntegrityKey = bindingContext.ParseResult.GetValueForOption(_integritykeyOption);
            options.ReaderType = bindingContext.ParseResult.GetValueForOption(_readerTypeOption);
            options.ContactlessReader = bindingContext.ParseResult.GetValueForOption(_contactlessReaderOption);
            options.SAMReader = bindingContext.ParseResult.GetValueForOption(_samReaderOption);
            options.PKCS11Library = bindingContext.ParseResult.GetValueForOption(_pkcs11LibraryOption);
            options.PKCS11Password = bindingContext.ParseResult.GetValueForOption(_pkcs11PasswordOption);
            return options;
        }
    }
}
