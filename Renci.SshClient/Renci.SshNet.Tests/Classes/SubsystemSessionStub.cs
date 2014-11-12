﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Renci.SshNet.Common;

namespace Renci.SshNet.Tests.Classes
{
    internal class SubsystemSessionStub : SubsystemSession
    {
        private int _onChannelOpenInvocationCount;

        public SubsystemSessionStub(ISession session, string subsystemName, TimeSpan operationTimeout, Encoding encoding) : base(session, subsystemName, operationTimeout, encoding)
        {
            OnDataReceivedInvocations = new List<ChannelDataEventArgs>();
        }

        public int OnChannelOpenInvocationCount
        {
            get { return _onChannelOpenInvocationCount; }
        }

        public IList<ChannelDataEventArgs> OnDataReceivedInvocations { get; private set; }

        public Exception OnChannelOpenException { get; set; }

        public Exception OnDataReceivedException { get; set; }

        protected override void OnChannelOpen()
        {
            Interlocked.Increment(ref _onChannelOpenInvocationCount);

            if (OnChannelOpenException != null)
                throw OnChannelOpenException;
        }

        protected override void OnDataReceived(uint dataTypeCode, byte[] data)
        {
            OnDataReceivedInvocations.Add(new ChannelDataEventArgs(0, data, dataTypeCode));

            if (OnDataReceivedException != null)
                throw OnDataReceivedException;
        }
    }
}
