using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA
{
    public class WorkerRemoteDataTransport : LibLogicalAccess.DummyDataTransport
    {
        bool _isConnected = false;
        byte[]? _response = null;
        WorkerRemoteReaderUnit _readerUnit;

        public WorkerRemoteDataTransport(WorkerRemoteReaderUnit readerUnit) : base()
        {
            _readerUnit = readerUnit;
        }

        public WorkerRemoteReaderUnit GetWorkerReaderUnit()
        {
            return _readerUnit;
        }

        public override string getTransportType()
        {
            return GetWorkerReaderUnit().getRPType();
        }

        public override bool connect()
        {
            _isConnected = true;
            return _isConnected;
        }

        public override void disconnect()
        {
            _isConnected = false;
        }

        public override bool isConnected()
        {
            return _isConnected;
        }

        public override string getName()
        {
            return base.getName();
        }

        protected override void send(ByteVector cmd)
        {
            _response = GetWorkerReaderUnit().sendRawCmd(cmd.ToArray());
        }

        protected override ByteVector? receive(int timeout)
        {
            ByteVector? ret = null;
            if (_response != null)
            {
                ret = new ByteVector(_response);
                _response = null;
            }
            return ret;
        }
    }
}
