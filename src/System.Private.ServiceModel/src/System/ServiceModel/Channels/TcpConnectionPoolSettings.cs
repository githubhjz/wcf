// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime;

namespace System.ServiceModel.Channels
{
    public sealed class TcpConnectionPoolSettings
    {
        private string _groupName;
        private TimeSpan _idleTimeout;
        private TimeSpan _leaseTimeout;
        private int _maxOutboundConnectionsPerEndpoint;

        internal TcpConnectionPoolSettings()
        {
            _groupName = ConnectionOrientedTransportDefaults.ConnectionPoolGroupName;
            _idleTimeout = ConnectionOrientedTransportDefaults.IdleTimeout;
            _leaseTimeout = TcpTransportDefaults.ConnectionLeaseTimeout;
            _maxOutboundConnectionsPerEndpoint = ConnectionOrientedTransportDefaults.MaxOutboundConnectionsPerEndpoint;
        }

        internal TcpConnectionPoolSettings(TcpConnectionPoolSettings tcp)
        {
            _groupName = tcp._groupName;
            _idleTimeout = tcp._idleTimeout;
            _leaseTimeout = tcp._leaseTimeout;
            _maxOutboundConnectionsPerEndpoint = tcp._maxOutboundConnectionsPerEndpoint;
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (value == null)
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");

                _groupName = value;
            }
        }

        public TimeSpan IdleTimeout
        {
            get { return _idleTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value,
                        SR.SFxTimeoutOutOfRange0));
                }

                if (TimeoutHelper.IsTooLarge(value))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value,
                        SR.SFxTimeoutOutOfRangeTooBig));
                }

                _idleTimeout = value;
            }
        }

        public TimeSpan LeaseTimeout
        {
            get { return _leaseTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value,
                        SR.SFxTimeoutOutOfRange0));
                }

                if (TimeoutHelper.IsTooLarge(value))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value,
                        SR.SFxTimeoutOutOfRangeTooBig));
                }

                _leaseTimeout = value;
            }
        }

        public int MaxOutboundConnectionsPerEndpoint
        {
            get { return _maxOutboundConnectionsPerEndpoint; }
            set
            {
                if (value < 0)
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value,
                        SR.ValueMustBeNonNegative));

                _maxOutboundConnectionsPerEndpoint = value;
            }
        }

        internal TcpConnectionPoolSettings Clone()
        {
            return new TcpConnectionPoolSettings(this);
        }

        internal bool IsMatch(TcpConnectionPoolSettings tcp)
        {
            if (_groupName != tcp._groupName)
                return false;

            if (_idleTimeout != tcp._idleTimeout)
                return false;

            if (_leaseTimeout != tcp._leaseTimeout)
                return false;

            if (_maxOutboundConnectionsPerEndpoint != tcp._maxOutboundConnectionsPerEndpoint)
                return false;

            return true;
        }
    }
}
