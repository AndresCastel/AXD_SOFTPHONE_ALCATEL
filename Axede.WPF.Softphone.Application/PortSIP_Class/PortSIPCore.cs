﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Axede.WPF.Softphone.Applications.PortSIP_Class;
using Axede.WPF.Softphone.Applications.GUI.Principal;

namespace Axede.WPF.Softphone.Applications.PortSIP_Class
{
   public unsafe class PortSIPCore
    {

        private SIPCallbackEvents _SIPCallbackEvents;


        public unsafe PortSIPCore(Int32 callbackObject, SIPCallbackEvents calbackevents)
        {
            initializeCallbackFunctions();

            _callbackHandle = IntPtr.Zero;
            _SIPCoreHandle = IntPtr.Zero;
            _callbackObject = callbackObject;

            _SIPCallbackEvents = calbackevents;
        }




        public Boolean createCallbackHandlers() // This must called before initialize
        {
            if (_callbackHandle != IntPtr.Zero)
            {
                return false;
            }

            if (createSIPCallbackHandle() == false)
            {
                return false;
            }

            setCallbackHandlers();
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            return true;

        }

        public void shutdownCallbackHandlers() // This must called before unInitialize
        {
            if (_callbackHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.SIPEV_shutdownSIPCallbackHandle(_callbackHandle);
        }


        public void releaseCallbackHandlers() // This must called after unInitialize
        {
            if (_callbackHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.SIPEV_releaseSIPCallbackHandle(_callbackHandle);
            _callbackHandle = IntPtr.Zero;
        }




        public Boolean initialize(TRANSPORT_TYPE transportType,
                                          PORTSIP_LOG_LEVEL appLogLevel,
                                          String logFilePath,
                                          Int32 maximumLines,
                                          String agent,
                                          String STUNSever,
                                          Int32 STUNServerPort,
                                          Boolean useVirtualAudioDevice,
                                          Boolean useVirtualVideoDevice,
                                          out Int32 errorCode)
        {
            if (_callbackHandle == IntPtr.Zero || _SIPCoreHandle != IntPtr.Zero)
            {
                errorCode = PortSIP_Errors.ECoreSDKObjectNull;
                return false;
            }


            _SIPCoreHandle = PortSIP_NativeMethods.PortSIP_initialize(_callbackHandle,
                                                           (Int32)transportType,
                                                           (Int32)appLogLevel,
                                                           logFilePath,
                                                           maximumLines,
                                                           agent,
                                                           STUNSever,
                                                           STUNServerPort,
                                                           useVirtualAudioDevice,
                                                           useVirtualVideoDevice,
                                                           out errorCode);

            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return false;
            }

            return true;
        }


        public Int32 setUserInfo(String userName,
                                 String displayName,
                                 String authName,
                                 String password,
                                 String localIp,
                                 Int32 localSipPort,
                                 String userDomain,
                                 String SIPServer,
                                 Int32 SIPServerPort,
                                 String outboundServer,
                                 Int32 outboundServerPort)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setUserInfo(_SIPCoreHandle,
                                                             userName,
                                                             displayName,
                                                             authName,
                                                             password,
                                                             localIp,
                                                             localSipPort,
                                                             userDomain,
                                                             SIPServer,
                                                             SIPServerPort,
                                                             outboundServer,
                                                             outboundServerPort);
        }



        public void unInitialize()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_unInitialize(_SIPCoreHandle);
            _SIPCoreHandle = IntPtr.Zero;
        }


        /// <summary>
        /// Note: Must special the StringBuilder.Length when call this function.
        /// for example:
        /// StringBuilder value = new StringBuilder();
        /// value.Length = 16;
        /// getLocalIP(0, ip, 16);
        /// </summary>


        public Int32 getLocalIP(Int32 index, StringBuilder ip, Int32 ipLength)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getLocalIP(_SIPCoreHandle, index, ip, ipLength);
        }

        /// <summary>
        /// Note: Must special the StringBuilder.Length when call this function.
        /// for example:
        /// StringBuilder value = new StringBuilder();
        /// value.Length = 64;
        /// getLocalIP6(0, ip, 64);
        /// </summary>


        public Int32 getLocalIP6(Int32 index, StringBuilder ip, Int32 ipLength)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getLocalIP6(_SIPCoreHandle, index, ip, ipLength);
        }

        public Int32 getNICNums()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNICNums(_SIPCoreHandle);
        }



        public void setLicenseKey(String key)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setLicenseKey(_SIPCoreHandle, key);
        }


        public void getVersion(out Int32 majorVersion, out Int32 minorVersion)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                majorVersion = 0;
                minorVersion = 0;

                return;
            }

            PortSIP_NativeMethods.PortSIP_getVersion(_SIPCoreHandle, out majorVersion, out minorVersion);
        }




        public void enableStackLog(String logFilePath)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableStackLog(_SIPCoreHandle, logFilePath);
        }



        public Int32 enableSessionTimer(Int32 timerSeconds, SESSION_REFRESH_MODE refreshMode)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableSessionTimer(_SIPCoreHandle, timerSeconds, (Int32)refreshMode);
        }



        public void disableSessionTimer()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_disableSessionTimer(_SIPCoreHandle);
        }


        public void setKeepAliveTime(Int32 keepAliveTime)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setKeepAliveTime(_SIPCoreHandle, keepAliveTime);

        }


        public void setReliableProvisionalMode(PROVISIONAL_MODE mode)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setReliableProvisionalMode(_SIPCoreHandle, (Int32)mode);
        }



        public void enableCheckMwi(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableCheckMwi(_SIPCoreHandle, state);
        }



        public void detectMwi()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_detectMwi(_SIPCoreHandle);
        }

        public void setSrtpPolicy(SRTP_POLICY srtPolicy)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setSrtpPolicy(_SIPCoreHandle, (Int32)srtPolicy);
        }




        public Int32 setRtpPortRange(Int32 minimumRtpAudioPort, Int32 maximumRtpAudioPort, Int32 minimumRtpVideoPort, Int32 maximumRtpVideoPort)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRtpPortRange(_SIPCoreHandle,
                                                         minimumRtpAudioPort,
                                                         maximumRtpAudioPort,
                                                         minimumRtpVideoPort,
                                                         maximumRtpVideoPort);
        }


        public Int32 setRtcpPortRange(Int32 minimumRtcpAudioPort, Int32 maximumRtcpAudioPort, Int32 minimumRtcpVideoPort, Int32 maximumRtcpVideoPort)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRtcpPortRange(_SIPCoreHandle,
                                                         minimumRtcpAudioPort,
                                                         maximumRtcpAudioPort,
                                                         minimumRtcpVideoPort,
                                                         maximumRtcpVideoPort);
        }


        public void setRtpKeepAlive(Boolean state, Int32 keepAlivePayloadType, Int32 deltaTransmitTimeMS)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setRtpKeepAlive(_SIPCoreHandle, state, keepAlivePayloadType, deltaTransmitTimeMS);
        }


        public void setRtpCallback(Int32 callbackObject, Boolean enable)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            if (enable == true)
            {
                PortSIP_NativeMethods.PortSIP_setRtpCallback(_SIPCoreHandle, (IntPtr)callbackObject, _rrc, _src);
            }
            else
            {
                PortSIP_NativeMethods.PortSIP_setRtpCallback(_SIPCoreHandle, (IntPtr)callbackObject, null, null);
            }

        }



        public Int32 registerServer(Int32 expires)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_registerServer(_SIPCoreHandle, expires);
        }


        public void unRegisterServer()
        {
            try
            {
                if (_SIPCoreHandle == IntPtr.Zero)
                {
                    return;
                }

                PortSIP_NativeMethods.PortSIP_unRegisterServer(_SIPCoreHandle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void addAudioCodec(AUDIOCODEC_TYPE codecType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_addAudioCodec(_SIPCoreHandle, (Int32)codecType);
        }


        public void addVideoCodec(VIDEOCODEC_TYPE codecType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_addVideoCodec(_SIPCoreHandle, (Int32)codecType);
        }


        void setAudioCodecPayloadType(AUDIOCODEC_TYPE codecType, Int32 payloadType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setAudioCodecPayloadType(_SIPCoreHandle, (Int32)codecType, payloadType);
        }


        void setVideoCodecPayloadType(VIDEOCODEC_TYPE codecType, Int32 payloadType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setVideoCodecPayloadType(_SIPCoreHandle, (Int32)codecType, payloadType);
        }


        public Int32 setAudioCodecParameter(AUDIOCODEC_TYPE codecType, String parameter)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioCodecParameter(_SIPCoreHandle, (Int32)codecType, parameter);
        }

        public Int32 setVideoCodecParameter(VIDEOCODEC_TYPE codecType, String parameter)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoCodecParameter(_SIPCoreHandle, (Int32)codecType, parameter);
        }




        public void clearAudioCodec()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_clearAudioCodec(_SIPCoreHandle);
        }


        public void clearVideoCodec()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_clearVideoCodec(_SIPCoreHandle);
        }




        public Boolean isAudioCodecEmpty()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return true;
            }

            return PortSIP_NativeMethods.PortSIP_isAudioCodecEmpty(_SIPCoreHandle);
        }



        public Boolean isVideoCodecEmpty()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return true;
            }

            return PortSIP_NativeMethods.PortSIP_isVideoCodecEmpty(_SIPCoreHandle);
        }




        public int setAudioDeviceId(Int32 recordingDeviceId, Int32 playoutDeviceId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioDeviceId(_SIPCoreHandle, recordingDeviceId, playoutDeviceId);
        }


        public int setVideoDeviceId(Int32 deviceId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoDeviceId(_SIPCoreHandle, deviceId);
        }


        public void setVideoBitrate(Int32 bitrateKbps)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setVideoBitrate(_SIPCoreHandle, bitrateKbps);
        }


        public void setVideoFrameRate(Int32 frameRate)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setVideoFrameRate(_SIPCoreHandle, frameRate);
        }



        public void setVideoResolution(VIDEO_RESOLUTION resolution)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setVideoResolution(_SIPCoreHandle, (Int32)resolution);
        }


        public void setLocalVideoWindow(IntPtr localVideoWindow)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setLocalVideoWindow(_SIPCoreHandle, localVideoWindow);
        }


        public Int32 setRemoteVideoWindow(Int32 sessionId, IntPtr remoteVideoWindow)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRemoteVideoWindow(_SIPCoreHandle, sessionId, remoteVideoWindow);
        }



        public Int32 viewLocalVideo(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_viewLocalVideo(_SIPCoreHandle, state);
        }


        public Int32 startVideoSending(Int32 sessionId, Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_startVideoSending(_SIPCoreHandle, sessionId, state);
        }


        public Int32 call(String callTo, Boolean sendSdp, out Int32 errorCode)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                errorCode = PortSIP_Errors.ECoreSDKObjectNull;
                return PortSIP_Errors.INVALID_SESSION_ID;
            }

            return PortSIP_NativeMethods.PortSIP_call(_SIPCoreHandle, callTo, sendSdp, out errorCode);

        }


        public Int32 rejectCall(Int32 sessionId, int code, String reason)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_rejectCall(_SIPCoreHandle, sessionId, code, reason);
        }



        public Int32 answerCall(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_answerCall(_SIPCoreHandle, sessionId);
        }


        public Int32 terminateCall(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_terminateCall(_SIPCoreHandle, sessionId);
        }


        public Int32 forwardCall(Int32 sessionId, String forwardTo)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_forwardCall(_SIPCoreHandle, sessionId, forwardTo);

        }


        public Int32 enableCallForwarding(Boolean forBusyOnly, String forwardingTo)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableCallForwarding(_SIPCoreHandle, forBusyOnly, forwardingTo);
        }


        public void disableCallForwarding()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_disableCallForwarding(_SIPCoreHandle);
        }




        public Int32 updateInvite(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_updateInvite(_SIPCoreHandle, sessionId);

        }


        public Int32 hold(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_hold(_SIPCoreHandle, sessionId);
        }


        public Int32 unHold(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_unHold(_SIPCoreHandle, sessionId);
        }



        public Int32 refer(Int32 sessionId, String referTo)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_refer(_SIPCoreHandle, sessionId, referTo);
        }


        public Int32 attendedRefer(Int32 sessionId, Int32 replaceSessionId, String referTo)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_attendedRefer(_SIPCoreHandle, sessionId, replaceSessionId, referTo);
        }


        public Int32 setAudioJitterBufferLevel(JITTER_BUFFER_LEVEL level)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioJitterBufferLevel(_SIPCoreHandle, (Int32)level);
        }


        public Int32 getNetworkStatistics(Int32 sessionId,
                                         out Int32 currentBufferSize,
                                         out Int32 preferredBufferSize,
                                         out Int32 currentPacketLossRate,
                                         out Int32 currentDiscardRate,
                                         out Int32 currentExpandRate,
                                         out Int32 urrentPreemptiveRate,
                                         out Int32 currentAccelerateRate)
        {

            if (_SIPCoreHandle == IntPtr.Zero)
            {
                currentBufferSize = 0;
                preferredBufferSize = 0;
                currentPacketLossRate = 0;
                currentDiscardRate = 0;
                currentExpandRate = 0;
                urrentPreemptiveRate = 0;
                currentAccelerateRate = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNetworkStatistics(_SIPCoreHandle,
                                                sessionId,
                                                out currentBufferSize,
                                                out preferredBufferSize,
                                                out currentPacketLossRate,
                                                out currentDiscardRate,
                                                out currentExpandRate,
                                                out urrentPreemptiveRate,
                                                out currentAccelerateRate);
        }


        public Int32 getAudioRtpStatistics(Int32 sessionId,
                                           out Int32 averageJitterMs,
                                           out Int32 maxJitterMs,
                                           out Int32 discardedPackets)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                averageJitterMs = 0;
                maxJitterMs = 0;
                discardedPackets = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getAudioRtpStatistics(_SIPCoreHandle,
                                                                       sessionId,
                                                                       out averageJitterMs,
                                                                       out maxJitterMs,
                                                                       out discardedPackets);
        }



        public Int32 PortSIP_getAudioRtcpStatistics(Int32 sessionId,
                                                    out Int32 bytesSent,
                                                    out Int32 packetsSent,
                                                    out Int32 bytesReceived,
                                                    out Int32 packetsReceived)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                bytesSent = 0;
                packetsSent = 0;
                bytesReceived = 0;
                packetsReceived = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getAudioRtcpStatistics(_SIPCoreHandle,
                                                                       sessionId,
                                                                       out bytesSent,
                                                                       out packetsSent,
                                                                       out bytesReceived,
                                                                       out packetsReceived);
        }


        public Int32 getVideoRtpStatistics(Int32 sessionId,
                                            out Int32 bytesSent,
                                            out Int32 packetsSent,
                                            out Int32 bytesReceived,
                                            out Int32 packetsReceived)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                bytesSent = 0;
                packetsSent = 0;
                bytesReceived = 0;
                packetsReceived = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getVideoRtpStatistics(_SIPCoreHandle,
                                                                       sessionId,
                                                                       out bytesSent,
                                                                       out packetsSent,
                                                                       out bytesReceived,
                                                                       out packetsReceived);
        }





        public void enableAEC(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableAEC(_SIPCoreHandle, state);
        }


        public void enableVAD(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableVAD(_SIPCoreHandle, state);
        }

        public void enableCNG(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableCNG(_SIPCoreHandle, state);
        }

        public void enableAGC(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableAGC(_SIPCoreHandle, state);
        }


        public Int32 setAudioSamples(Int32 ptime, Int32 maxPtime)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioSamples(_SIPCoreHandle, ptime, maxPtime);
        }




        public void setDtmfSamples(Int32 samples)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setDTMFSamples(_SIPCoreHandle, samples);
        }



        public void enableDTMFOfRFC2833(Int32 RTPPayloadOf2833)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableDTMFOfRFC2833(_SIPCoreHandle, RTPPayloadOf2833);
        }

        public void enableDTMFOfInfo()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableDTMFOfInfo(_SIPCoreHandle);
        }


        public Int32 sendDtmf(Int32 sessionId, Char code)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendDTMF(_SIPCoreHandle, sessionId, code);
        }



        public Int32 startAudioRecording(String filePath,
                                        String fileName,
                                        Boolean appendTimestamp,
                                        AUDIO_RECORDING_FILEFORMAT fileFormat,
                                        RECORD_MODE recordMode)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_startAudioRecording(_SIPCoreHandle, filePath, fileName, appendTimestamp, (Int32)fileFormat, (Int32)recordMode);
        }


        public Int32 startVideoRecording(String filePath, String fileName, Boolean appendTimestamp, VIDEOCODEC_TYPE codecType, RECORD_MODE recordMode)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_startVideoRecording(_SIPCoreHandle, filePath, fileName, appendTimestamp, (Int32)codecType, (Int32)recordMode);
        }




        public Int32 stopAudioRecording()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_stopAudioRecording(_SIPCoreHandle);
        }



        public Int32 stopVideoRecording()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_stopVideoRecording(_SIPCoreHandle);
        }



        public void muteMicrophone(Boolean mute)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_muteMicrophone(_SIPCoreHandle, mute);
        }


        public void muteSpeaker(Boolean mute)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_muteSpeaker(_SIPCoreHandle, mute);
        }


        /// <summary>
        /// Note: Must special the StringBuilder.Length when call this function.
        /// for example:
        /// StringBuilder value = new StringBuilder();
        /// value.Length = 512;
        /// getExtensionHeaderValue(message, name, value);
        /// </summary>
        public Int32 getExtensionHeaderValue(String sipMessage, String headerName, StringBuilder headerValue, Int32 headerValueLength)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getExtensionHeaderValue(_SIPCoreHandle, sipMessage, headerName, headerValue, headerValueLength);
        }



        public Int32 addExtensionHeader(String headerName, String headerValue)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_addExtensionHeader(_SIPCoreHandle, headerName, headerValue);
        }


        public void clearAddExtensionHeaders()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_clearAddExtensionHeaders(_SIPCoreHandle);
        }


        public Int32 modifyHeaderValue(String headerName, String headerValue)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_modifyHeaderValue(_SIPCoreHandle, headerName, headerValue);
        }



        public void clearModifyHeaders()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_clearModifyHeaders(_SIPCoreHandle);
        }


        public void enableCallbackSentSignaling(Boolean state)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableCallbackSentSignaling(_SIPCoreHandle, state);
        }


        public Int32 setAudioQos(Boolean state, Int32 DSCPValue, Int32 priority)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioQos(_SIPCoreHandle, state, DSCPValue, priority);
        }


        public Int32 setVideoQos(Boolean state, Int32 DSCPValue)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoQos(_SIPCoreHandle, state, DSCPValue);
        }



        public void getDynamicVolumeLevel(out Int32 speakerVolume, out Int32 microphoneVolume)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                speakerVolume = 0;
                microphoneVolume = 0;

                return;
            }

            PortSIP_NativeMethods.PortSIP_getDynamicVolumeLevel(_SIPCoreHandle, out speakerVolume, out microphoneVolume);
        }



        public Int32 enableSendPcmStreamToRemote(Int32 sessionId, Boolean state, Int32 streamSamplesPerSec)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableSendPcmStreamToRemote(_SIPCoreHandle, sessionId, state, streamSamplesPerSec);
        }




        public Int32 sendPcmStreamToRemote(Int32 sessionId, byte[] data, Int32 dataLength, Int32 dataSamplesPerSec)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendPcmStreamToRemote(_SIPCoreHandle, sessionId, data, dataLength);
        }


        public void discardAudio(Boolean discardIncomingAudio, Boolean discardOutgoingAudio)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_discardAudio(_SIPCoreHandle, discardIncomingAudio, discardOutgoingAudio);
        }



        public void setPlayWaveFileToRemote(Int32 callbackObject, Int32 sessionId, String waveFile, Boolean enableState, Int32 playMode, Int32 fileSamplesPerSec, Boolean eventReport)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }


            if (eventReport == true)
            {
                PortSIP_NativeMethods.PortSIP_setPlayWaveFileToRemote(_SIPCoreHandle, sessionId, waveFile, enableState, playMode, fileSamplesPerSec, (IntPtr)callbackObject, _pwf);
            }
            else
            {
                PortSIP_NativeMethods.PortSIP_setPlayWaveFileToRemote(_SIPCoreHandle, sessionId, waveFile, enableState, playMode, fileSamplesPerSec, (IntPtr)callbackObject, null);
            }
        }




        public Int32 setPlayAviFileToRemote(Int32 callbackObject, Int32 sessionId, String aviFile, Boolean enableState, Boolean loop, Boolean playAudio, Boolean eventReport)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }


            if (eventReport == true)
            {
                return PortSIP_NativeMethods.PortSIP_setPlayAviFileToRemote(_SIPCoreHandle, sessionId, aviFile, enableState, loop, playAudio, (IntPtr)callbackObject, _paf);
            }
            else
            {
                return PortSIP_NativeMethods.PortSIP_setPlayAviFileToRemote(_SIPCoreHandle, sessionId, aviFile, enableState, loop, playAudio, (IntPtr)callbackObject, null);
            }
        }


        public Int32 enableAudioStreamCallback(Int32 callbackObject, Int32 sessionId, Boolean enable, AUDIOSTREAM_CALLBACK_MODE callbackType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableAudioStreamCallback(_SIPCoreHandle, sessionId, enable, (Int32)callbackType, (IntPtr)callbackObject, _arc);
        }



        public Int32 enableVideoStreamCallback(Int32 callbackObject, Int32 sessionId, VIDEOSTREAM_CALLBACK_MODE callbackType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }


            return PortSIP_NativeMethods.PortSIP_enableVideoStreamCallback(_SIPCoreHandle, sessionId, (Int32)callbackType, (IntPtr)callbackObject, _vrc);
        }


        public Int32 createConference(IntPtr conferenceVideoWindow, VIDEO_RESOLUTION videoResolution, Boolean viewLocalVideoImage)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_createConference(_SIPCoreHandle, conferenceVideoWindow, (Int32)videoResolution, viewLocalVideoImage);
        }


        public Int32 createConferenceEx(IntPtr conferenceVideoWindow, VIDEO_RESOLUTION videoResolution, Boolean viewLocalVideoImage, Int32[] sessionIds, Int32 sessionIdNums)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_createConferenceEx(_SIPCoreHandle, conferenceVideoWindow, (Int32)videoResolution, viewLocalVideoImage, sessionIds, sessionIdNums);
        }



        public void destroyConference()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_destroyConference(_SIPCoreHandle);
        }



        public Int32 joinToConference(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_joinToConference(_SIPCoreHandle, sessionId);
        }


        public Int32 removeFromConference(Int32 sessionId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_removeFromConference(_SIPCoreHandle, sessionId);
        }



        public Int32 addSupportedMimeType(String methodName, String mimeType, String subMimeType)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_addSupportedMimeType(_SIPCoreHandle, methodName, mimeType, subMimeType);
        }



        public Int32 sendInfo(Int32 sessionId, String mimeType, String subMimeType, String infoContents)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendInfo(_SIPCoreHandle, sessionId, mimeType, subMimeType, infoContents);
        }



        public Int32 sendOptions(String to, String sdp)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendOptions(_SIPCoreHandle, to, sdp);
        }




        public Int32 sendMessage(Int32 sessionId, String mimeType, String subMimeType, String message)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendMessage(_SIPCoreHandle, sessionId, mimeType, subMimeType, message);
        }


        public Int32 sendMessageEx(Int32 sessionId, String mimeType, String subMimeType, byte[] message, Int32 messageLength)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendMessageEx(_SIPCoreHandle, sessionId, mimeType, subMimeType, message, messageLength);
        }



        public Int32 sendOutOfDialogMessage(String to, String mimeType, String subMimeType, String message)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendOutOfDialogMessage(_SIPCoreHandle, to, mimeType, subMimeType, message);
        }


        public Int32 sendOutOfDialogMessageEx(String to, String mimeType, String subMimeType, byte[] message, Int32 messageLength)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendOutOfDialogMessageEx(_SIPCoreHandle, to, mimeType, subMimeType, message, messageLength);
        }


        public Int32 sendPagerMessage(String to, String message)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendPagerMessage(_SIPCoreHandle, to, message);
        }




        public Int32 presenceSubscribeContact(String contact, String subject)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceSubscribeContact(_SIPCoreHandle, contact, subject);
        }




        public Int32 presenceRejectSubscribe(Int32 subscribeId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceRejectSubscribe(_SIPCoreHandle, subscribeId);
        }


        public Int32 presenceAcceptSubscribe(Int32 subscribeId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceAcceptSubscribe(_SIPCoreHandle, subscribeId);
        }



        public Int32 presenceOffline(Int32 subscribeId)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceOffline(_SIPCoreHandle, subscribeId);
        }



        public Int32 presenceOnline(Int32 subscribeId, String stateText)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceOnline(_SIPCoreHandle, subscribeId, stateText);
        }


        public Int32 getNumOfRecordingDevices()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNumOfRecordingDevices(_SIPCoreHandle);
        }


        public Int32 getNumOfPlayoutDevices()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNumOfPlayoutDevices(_SIPCoreHandle);
        }




        public Int32 getRecordingDeviceName(Int32 deviceIndex, StringBuilder nameUTF8, Int32 nameUTF8Length)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getRecordingDeviceName(_SIPCoreHandle, deviceIndex, nameUTF8, nameUTF8Length);
        }

        public Int32 getPlayoutDeviceName(Int32 deviceIndex, StringBuilder nameUTF8, Int32 nameUTF8Length)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getPlayoutDeviceName(_SIPCoreHandle, deviceIndex, nameUTF8, nameUTF8Length);
        }




        public Int32 setSpeakerVolume(Int32 volume)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSpeakerVolume(_SIPCoreHandle, volume);
        }


        public Int32 getSpeakerVolume()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getSpeakerVolume(_SIPCoreHandle);
        }



        public Int32 setSystemOutputMute(Boolean enable)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSystemOutputMute(_SIPCoreHandle, enable);
        }



        public Boolean getSystemOutputMute()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return false;
            }

            return PortSIP_NativeMethods.PortSIP_getSystemOutputMute(_SIPCoreHandle);
        }


        public Int32 setMicVolume(Int32 volume)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setMicVolume(_SIPCoreHandle, volume);
        }


        public Int32 getMicVolume()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getMicVolume(_SIPCoreHandle);
        }



        public Int32 setSystemInputMute(Boolean enable)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSystemInputMute(_SIPCoreHandle, enable);
        }



        public Boolean getSystemInputMute()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return false;
            }

            return PortSIP_NativeMethods.PortSIP_getSystemInputMute(_SIPCoreHandle);
        }




        public Int32 playLocalWaveFile(String waveFile, Boolean loop)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_playLocalWaveFile(_SIPCoreHandle, waveFile, loop);
        }


        public void audioPlayLoopbackTest(Boolean enable)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_audioPlayLoopbackTest(_SIPCoreHandle, enable);
        }


        public Int32 getNumOfVideoCaptureDevices()
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNumOfVideoCaptureDevices(_SIPCoreHandle);
        }




        public Int32 getVideoCaptureDeviceName(Int32 deviceIndex,
                                               StringBuilder uniqueIdUTF8,
                                               Int32 uniqueIdUTF8Length,
                                               StringBuilder deviceNameUTF8,
                                               Int32 deviceNameUTF8Length)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getVideoCaptureDeviceName(_SIPCoreHandle,
                                                                           deviceIndex,
                                                                           uniqueIdUTF8,
                                                                           uniqueIdUTF8Length,
                                                                           deviceNameUTF8,
                                                                           deviceNameUTF8Length);
        }




        public Int32 showVideoCaptureSettingsDialogBox(String uniqueIdUTF8,
                                                                    Int32 uniqueIdUTF8Length,
                                                                    String dialogTitle,
                                                                    IntPtr parentWindow,
                                                                    Int32 x,
                                                                    Int32 y)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_showVideoCaptureSettingsDialogBox(_SIPCoreHandle,
                                                                                    uniqueIdUTF8,
                                                                                    uniqueIdUTF8Length,
                                                                                    dialogTitle,
                                                                                    parentWindow,
                                                                                    x,
                                                                                    y);
        }



        //////////////////////////////////////////////////////////////////////////
        //
        //
        // Extend functions, don't use it
        //
        //
        //////////////////////////////////////////////////////////////////////////


        public Int32 setAMRCodecParameter(Int32 codecType, AMR_RATE_CHANGE_MODE changeMode, Int32 fixedRate)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAMRCodecParameter(_SIPCoreHandle, codecType, (Int32)changeMode, fixedRate);
        }

        public Int32 getAvailableAudioCodecs(Int32 sessionId, Int32[] audioCodecsType, Int32[] localRtpMapValues, Int32[] remoteRtpMapValues, Int32 CodecsLength)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getAvailableAudioCodecs(_SIPCoreHandle, sessionId, audioCodecsType, localRtpMapValues, remoteRtpMapValues, CodecsLength);
        }

        public Int32 changeSendingAudioCodec(Int32 sessionId, Int32 localRtpmapForSending)
        {
            if (_SIPCoreHandle == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_changeSendingAudioCodec(_SIPCoreHandle, sessionId, localRtpmapForSending);
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Private members and methods
        /// </summary>



        private PortSIP_NativeMethods.audioRawCallback _arc;
        private PortSIP_NativeMethods.videoRawCallback _vrc;
        private PortSIP_NativeMethods.receivedRTPCallback _rrc;
        private PortSIP_NativeMethods.sendingRTPCallback _src;
        private PortSIP_NativeMethods.playAviFileToRemoteFinished _paf;
        private PortSIP_NativeMethods.playWaveFileToRemoteFinished _pwf;



        private PortSIP_NativeMethods.registerSuccess _rgs;
        private PortSIP_NativeMethods.registerFailure _rgf;
        private PortSIP_NativeMethods.inviteIncoming _ivi;
        private PortSIP_NativeMethods.inviteTrying _ivt;
        private PortSIP_NativeMethods.inviteRinging _ivr;
        private PortSIP_NativeMethods.inviteAnswered _iva;
        private PortSIP_NativeMethods.inviteFailure _ivf;
        private PortSIP_NativeMethods.inviteClosed _ivc;
        private PortSIP_NativeMethods.inviteUpdated _ivu;
        private PortSIP_NativeMethods.inviteUASConnected _ivsc;
        private PortSIP_NativeMethods.inviteUACConnected _ivcc;
        private PortSIP_NativeMethods.inviteBeginingForward _ivbf;
        private PortSIP_NativeMethods.remoteHold _rth;
        private PortSIP_NativeMethods.remoteUnHold _rtu;
        private PortSIP_NativeMethods.transferTrying _tst;
        private PortSIP_NativeMethods.transferRinging _tsr;
        private PortSIP_NativeMethods.PASVTransferSuccess _pts;
        private PortSIP_NativeMethods.PASVTransferFailure _ptf;
        private PortSIP_NativeMethods.ACTVTransferSuccess _ats;
        private PortSIP_NativeMethods.ACTVTransferFailure _atf;
        private PortSIP_NativeMethods.recvPagerMessage _rpm;
        private PortSIP_NativeMethods.sendPagerMessageSuccess _spms;
        private PortSIP_NativeMethods.sendPagerMessageFailure _spmf;
        private PortSIP_NativeMethods.arrivedSignaling _asg;
        private PortSIP_NativeMethods.sentSignaling _ssg;
        private PortSIP_NativeMethods.waitingVoiceMessage _wvm;
        private PortSIP_NativeMethods.waitingFaxMessage _wfm;
        private PortSIP_NativeMethods.recvDtmfTone _rdt;
        private PortSIP_NativeMethods.presenceRecvSubscribe _prs;
        private PortSIP_NativeMethods.presenceOnline _pon;
        private PortSIP_NativeMethods.presenceOffline _pof;
        private PortSIP_NativeMethods.recvOptions _ro;
        private PortSIP_NativeMethods.recvInfo _ri;
        private PortSIP_NativeMethods.recvMessage _rms;
        private PortSIP_NativeMethods.recvBinaryMessage _rbm;
        private PortSIP_NativeMethods.recvBinaryPagerMessage _rbpm;

        private void initializeCallbackFunctions()
        {
            _arc = new PortSIP_NativeMethods.audioRawCallback(onAudioRawCallback);
            _vrc = new PortSIP_NativeMethods.videoRawCallback(onVideoRawCallback);
            _rrc = new PortSIP_NativeMethods.receivedRTPCallback(onReceivedRtpPacket);
            _src = new PortSIP_NativeMethods.sendingRTPCallback(onSendingRtpPacket);
            _paf = new PortSIP_NativeMethods.playAviFileToRemoteFinished(onPlayAviFileFinished);
            _pwf = new PortSIP_NativeMethods.playWaveFileToRemoteFinished(onPlayWaveFileFinished);



            _rgs = new PortSIP_NativeMethods.registerSuccess(onRegisterSuccess);
            _rgf = new PortSIP_NativeMethods.registerFailure(onRegisterFailure);
            _ivi = new PortSIP_NativeMethods.inviteIncoming(onInviteIncoming);
            _ivt = new PortSIP_NativeMethods.inviteTrying(onInviteTrying);
            _ivr = new PortSIP_NativeMethods.inviteRinging(onInviteRinging);
            _iva = new PortSIP_NativeMethods.inviteAnswered(onInviteAnswered);
            _ivf = new PortSIP_NativeMethods.inviteFailure(onInviteFailure);
            _ivc = new PortSIP_NativeMethods.inviteClosed(onInviteClosed);
            _ivu = new PortSIP_NativeMethods.inviteUpdated(onInviteUpdated);
            _ivsc = new PortSIP_NativeMethods.inviteUASConnected(onInviteUASConnected);
            _ivcc = new PortSIP_NativeMethods.inviteUACConnected(onInviteUACConnected);
            _ivbf = new PortSIP_NativeMethods.inviteBeginingForward(onInviteBeginingForward);
            _rth = new PortSIP_NativeMethods.remoteHold(onRemoteHold);
            _rtu = new PortSIP_NativeMethods.remoteUnHold(onRemoteUnHold);
            _tst = new PortSIP_NativeMethods.transferTrying(onTransferTrying);
            _tsr = new PortSIP_NativeMethods.transferRinging(onTransferRinging);
            _pts = new PortSIP_NativeMethods.PASVTransferSuccess(onPASVTransferSuccess);
            _ptf = new PortSIP_NativeMethods.PASVTransferFailure(onPASVTransferFailure);
            _ats = new PortSIP_NativeMethods.ACTVTransferSuccess(onACTVTransferSuccess);
            _atf = new PortSIP_NativeMethods.ACTVTransferFailure(onACTVTransferFailure);
            _rpm = new PortSIP_NativeMethods.recvPagerMessage(onRecvPagerMessage);
            _spms = new PortSIP_NativeMethods.sendPagerMessageSuccess(onSendPagerMessageSuccess);
            _spmf = new PortSIP_NativeMethods.sendPagerMessageFailure(onSendPagerMessageFailure);
            _asg = new PortSIP_NativeMethods.arrivedSignaling(onArrivedSignaling);
            _ssg = new PortSIP_NativeMethods.sentSignaling(onSentSignaling);
            _wvm = new PortSIP_NativeMethods.waitingVoiceMessage(onWaitingVoiceMessage);
            _wfm = new PortSIP_NativeMethods.waitingFaxMessage(onWaitingFaxMessage);
            _rdt = new PortSIP_NativeMethods.recvDtmfTone(onRecvDtmfTone);
            _prs = new PortSIP_NativeMethods.presenceRecvSubscribe(onPresenceRecvSubscribe);
            _pon = new PortSIP_NativeMethods.presenceOnline(onPresenceOnline);
            _pof = new PortSIP_NativeMethods.presenceOffline(onPresenceOffline);
            _ro = new PortSIP_NativeMethods.recvOptions(onRecvOptions);
            _ri = new PortSIP_NativeMethods.recvInfo(onRecvInfo);
            _rms = new PortSIP_NativeMethods.recvMessage(onRecvMessage);
            _rbm = new PortSIP_NativeMethods.recvBinaryMessage(onRecvBinaryMessage);
            _rbpm = new PortSIP_NativeMethods.recvBinaryPagerMessage(onRecvBinaryPagerMessage);
        }



        // SIP message callback events

        private unsafe Int32 onRegisterSuccess(Int32 callbackObject, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onRegisterSuccess(callbackObject, statusCode, statusText);

            return 0;
        }


        private unsafe Int32 onRegisterFailure(Int32 callbackObject, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onRegisterFailure(callbackObject, statusCode, statusText);

            return 0;
        }

        private unsafe Int32 onInviteIncoming(Int32 callbackObject,
                                             Int32 sessionId,
                                             String caller,
                                             String callerDisplayName,
                                             String callee,
                                             String calleeDisplayName,
                                             String audioCodecName,
                                             String videoCodecName,
                                             Boolean hasVideo)
        {
            _SIPCallbackEvents.onInviteIncoming(callbackObject,
                                              sessionId,
                                              caller,
                                              callerDisplayName,
                                              callee,
                                              calleeDisplayName,
                                              audioCodecName,
                                              videoCodecName,
                                              hasVideo);

            return 0;
        }


        private unsafe Int32 onInviteTrying(Int32 callbackObject, Int32 sessionId, String caller, String callee)
        {
            _SIPCallbackEvents.onInviteTrying(callbackObject, sessionId, caller, callee);

            return 0;
        }

        private unsafe Int32 onInviteRinging(Int32 callbackObject,
                                            Int32 sessionId,
                                            Boolean hasEarlyMedia,
                                            Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName)
        {
            _SIPCallbackEvents.onInviteRinging(callbackObject,
                                      sessionId,
                                      hasEarlyMedia,
                                      hasVideo,
                                      audioCodecName,
                                      videoCodecName);

            return 0;
        }



        private unsafe Int32 onInviteAnswered(Int32 callbackObject,
                                             Int32 sessionId,
                                             Boolean hasVideo,
                                             Int32 statusCode,
                                             String statusText,
                                             String audioCodecName,
                                             String videoCodecName
                                            )
        {
            _SIPCallbackEvents.onInviteAnswered(callbackObject,
                                       sessionId,
                                       hasVideo,
                                       statusCode,
                                       statusText,
                                       audioCodecName,
                                       videoCodecName);

            return 0;
        }


        private unsafe Int32 onInviteFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onInviteFailure(callbackObject, sessionId, statusCode, statusText);
            return 0;
        }


        private unsafe Int32 onInviteClosed(Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onInviteClosed(callbackObject, sessionId);
            return 0;
        }


        private unsafe Int32 onInviteUpdated(Int32 callbackObject,
                                            Int32 sessionId,
                                            Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName
                                            )
        {
            _SIPCallbackEvents.onInviteUpdated(callbackObject,
                                      sessionId,
                                      hasVideo,
                                      audioCodecName,
                                      videoCodecName);

            return 0;
        }


        private unsafe Int32 onInviteUASConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onInviteUASConnected(callbackObject, sessionId, statusCode, statusText);

            return 0;
        }


        private unsafe Int32 onInviteUACConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onInviteUACConnected(callbackObject, sessionId, statusCode, statusText);

            return 0;
        }


        private unsafe Int32 onInviteBeginingForward(Int32 callbackObject, String forwardingTo)
        {
            _SIPCallbackEvents.onInviteBeginingForward(callbackObject, forwardingTo);

            return 0;
        }


        private unsafe Int32 onRemoteHold(Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onRemoteHold(callbackObject, sessionId);

            return 0;
        }

        private unsafe Int32 onRemoteUnHold(Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onRemoteUnHold(callbackObject, sessionId);

            return 0;
        }


        private unsafe Int32 onTransferTrying(Int32 callbackObject, Int32 sessionId, String referTo)
        {
            _SIPCallbackEvents.onTransferTrying(callbackObject, sessionId, referTo);

            return 0;
        }

        private unsafe Int32 onTransferRinging(Int32 callbackObject, Int32 sessionId, Boolean hasVideo)
        {
            _SIPCallbackEvents.onTransferRinging(callbackObject, sessionId, hasVideo);
            return 0;
        }


        private unsafe Int32 onPASVTransferSuccess(Int32 callbackObject, Int32 sessionId, Boolean hasVideo)
        {
            _SIPCallbackEvents.onPASVTransferSuccess(callbackObject, sessionId, hasVideo);
            return 0;
        }

        private unsafe Int32 onPASVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onPASVTransferFailure(callbackObject, sessionId, statusCode, statusText);
            return 0;
        }



        private unsafe Int32 onACTVTransferSuccess(Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onACTVTransferSuccess(callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onACTVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            _SIPCallbackEvents.onACTVTransferFailure(callbackObject, sessionId, statusCode, statusText);
            return 0;
        }


        private unsafe Int32 onRecvPagerMessage(Int32 callbackObject, String from, String fromDisplayName, StringBuilder message)
        {
            _SIPCallbackEvents.onRecvPagerMessage(callbackObject, from, fromDisplayName, message);

            return 0;
        }

        private unsafe Int32 onSendPagerMessageSuccess(Int32 callbackObject,
                                                      String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName
                                                     )
        {
            _SIPCallbackEvents.onSendPagerMessageSuccess(callbackObject,
                                                caller,
                                                callerDisplayName,
                                                callee,
                                                calleeDisplayName);

            return 0;
        }



        private unsafe Int32 onSendPagerMessageFailure(Int32 callbackObject,
                                                       String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName,
                                                      Int32 statusCode,
                                                      String statusText
                                                     )
        {
            _SIPCallbackEvents.onSendPagerMessageFailure(callbackObject,
                                                caller,
                                                callerDisplayName,
                                                callee,
                                                calleeDisplayName,
                                                statusCode,
                                                 statusText);
            return 0;
        }



        private unsafe Int32 onArrivedSignaling(Int32 callbackObject, Int32 sessionId, StringBuilder signaling)
        {
            _SIPCallbackEvents.onArrivedSignaling(callbackObject, sessionId, signaling);
            return 0;
        }


        private unsafe Int32 onSentSignaling(Int32 callbackObject, StringBuilder signaling)
        {
            _SIPCallbackEvents.onSentSignaling(callbackObject, signaling);
            return 0;
        }


        private unsafe Int32 onWaitingVoiceMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {
            _SIPCallbackEvents.onWaitingVoiceMessage(callbackObject, messageAccount, urgentNewMessageCount, urgentOldMessageCount, newMessageCount, oldMessageCount);

            return 0;
        }


        private unsafe Int32 onWaitingFaxMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {
            _SIPCallbackEvents.onWaitingFaxMessage(callbackObject, messageAccount, urgentNewMessageCount, urgentOldMessageCount, newMessageCount, oldMessageCount);


            return 0;
        }



        private unsafe Int32 onRecvDtmfTone(Int32 callbackObject, Int32 sessionId, Int32 tone)
        {
            _SIPCallbackEvents.onRecvDtmfTone(callbackObject, sessionId, tone);
            return 0;
        }


        private unsafe Int32 onPresenceRecvSubscribe(Int32 callbackObject,
                                                    Int32 subscribeId,
                                                    String from,
                                                    String fromDisplayName,
                                                    String subject)
        {
            _SIPCallbackEvents.onPresenceRecvSubscribe(callbackObject,
                                               subscribeId,
                                               from,
                                               fromDisplayName,
                                               subject);

            return 0;
        }


        private unsafe Int32 onPresenceOnline(Int32 callbackObject, String from, String fromDisplayName, String stateText)
        {
            _SIPCallbackEvents.onPresenceOnline(callbackObject, from, fromDisplayName, stateText);
            return 0;
        }

        private unsafe Int32 onPresenceOffline(Int32 callbackObject, String from, String fromDisplayName)
        {
            _SIPCallbackEvents.onPresenceOffline(callbackObject, from, fromDisplayName);

            return 0;
        }


        private unsafe Int32 onRecvOptions(Int32 callbackObject, StringBuilder optionsMessage)
        {
            _SIPCallbackEvents.onRecvOptions(callbackObject, optionsMessage);
            return 0;
        }

        private unsafe Int32 onRecvInfo(Int32 callbackObject, Int32 sessionId, StringBuilder infoMessage)
        {
            _SIPCallbackEvents.onRecvInfo(callbackObject, sessionId, infoMessage);
            return 0;
        }


        private unsafe Int32 onRecvMessage(Int32 callbackObject, Int32 sessionId, StringBuilder message)
        {
            _SIPCallbackEvents.onRecvMessage(callbackObject, sessionId, message);
            return 0;
        }


        private unsafe Int32 onRecvBinaryMessage(Int32 callbackObject,
                                                        Int32 sessionId,
                                                        StringBuilder message,
                                                        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] messageBody,
                                                        Int32 length)
        {
            _SIPCallbackEvents.onRecvBinaryMessage(callbackObject, sessionId, message, messageBody, length);
            return 0;
        }


        private unsafe Int32 onRecvBinaryPagerMessage(Int32 callbackObject,
                                                         StringBuilder from,
                                                         StringBuilder fromDisplayName,
                                                         [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] messageBody,
                                                         Int32 length)
        {
            _SIPCallbackEvents.onRecvBinaryPagerMessage(callbackObject, from, fromDisplayName, messageBody, length);
            return 0;
        }




        private Boolean createSIPCallbackHandle()
        {
            _callbackHandle = PortSIP_NativeMethods.SIPEV_createSIPCallbackHandle();
            if (_callbackHandle == IntPtr.Zero)
            {
                return false;
            }

            return true;
        }


        private void setCallbackHandlers()
        {
            PortSIP_NativeMethods.SIPEV_setRegisterSuccessHandler(_callbackHandle, _rgs, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRegisterFailureHandler(_callbackHandle, _rgf, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteIncomingHandler(_callbackHandle, _ivi, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteTryingHandler(_callbackHandle, _ivt, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteRingingHandler(_callbackHandle, _ivr, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteAnsweredHandler(_callbackHandle, _iva, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteFailureHandler(_callbackHandle, _ivf, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteClosedHandler(_callbackHandle, _ivc, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteUpdatedHandler(_callbackHandle, _ivu, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteUASConnectedHandler(_callbackHandle, _ivsc, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteUACConnectedHandler(_callbackHandle, _ivcc, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setInviteBeginingForwardHandler(_callbackHandle, _ivbf, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRemoteHoldHandler(_callbackHandle, _rth, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRemoteUnHoldHandler(_callbackHandle, _rtu, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setTransferTryingHandler(_callbackHandle, _tst, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setTransferRingingHandler(_callbackHandle, _tsr, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setPASVTransferSuccessHandler(_callbackHandle, _pts, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setACTVTransferSuccessHandler(_callbackHandle, _ats, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setPASVTransferFailureHandler(_callbackHandle, _ptf, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setACTVTransferFailureHandler(_callbackHandle, _atf, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvPagerMessageHandler(_callbackHandle, _rpm, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setSendPagerMessageSuccessHandler(_callbackHandle, _spms, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setSendPagerMessageFailureHandler(_callbackHandle, _spmf, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setArrivedSignalingHandler(_callbackHandle, _asg, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setSentSignalingHandler(_callbackHandle, _ssg, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setWaitingVoiceMessageHandler(_callbackHandle, _wvm, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setWaitingFaxMessageHandler(_callbackHandle, _wfm, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvDtmfToneHandler(_callbackHandle, _rdt, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setPresenceRecvSubscribeHandler(_callbackHandle, _prs, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setPresenceOnlineHandler(_callbackHandle, _pon, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setPresenceOfflineHandler(_callbackHandle, _pof, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvOptionsHandler(_callbackHandle, _ro, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvInfoHandler(_callbackHandle, _ri, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvMessageHandler(_callbackHandle, _rms, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvBinaryMessageHandler(_callbackHandle, _rbm, _callbackObject);
            PortSIP_NativeMethods.SIPEV_setRecvBinaryPagerMessageHandler(_callbackHandle, _rbpm, _callbackObject);
        }




        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        private unsafe Int32 onAudioRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] data,
                                               Int32 dataLength,
                                               Int32 samplingFreqHz)
        {
            _SIPCallbackEvents.onAudioRawCallback(callbackObject,
                                         sessionId,
                                         callbackType,
                                         data,
                                         dataLength,
                                         samplingFreqHz);
            return 0;
        }


        private unsafe Int32 onVideoRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               Int32 width,
                                               Int32 height,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] data,
                                               Int32 dataLength)
        {
            _SIPCallbackEvents.onVideoRawCallback(callbackObject,
                                         sessionId,
                                         callbackType,
                                         width,
                                         height,
                                         data,
                                         dataLength);

            return 0;

        }


        private unsafe Int32 onReceivedRtpPacket(IntPtr callbackObject,
                                               Int32 sessionId,
                                               [MarshalAs(UnmanagedType.I1)] Boolean isAudio,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] RTPPacket,
                                               Int32 packetSize)
        {
            _SIPCallbackEvents.onReceivedRtpPacket(callbackObject,
                                         sessionId,
                                         isAudio,
                                         RTPPacket,
                                         packetSize);


            return 0;
        }


        private unsafe Int32 onSendingRtpPacket(IntPtr callbackObject,
                                               Int32 sessionId,
                                               [MarshalAs(UnmanagedType.I1)] Boolean isAudio,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] RTPPacket,
                                               Int32 packetSize)
        {
            _SIPCallbackEvents.onSendingRtpPacket(callbackObject,
                                          sessionId,
                                          isAudio,
                                          RTPPacket,
                                          packetSize);


            return 0;
        }



        private unsafe Int32 onPlayWaveFileFinished(IntPtr callbackObject, Int32 sessionId, String fileName)
        {
            _SIPCallbackEvents.onPlayWaveFileFinished(callbackObject, sessionId, fileName);
            return 0;
        }

        private unsafe Int32 onPlayAviFileFinished(IntPtr callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onPlayAviFileFinished(callbackObject, sessionId);
            return 0;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private IntPtr _callbackHandle;
        private Int32 _callbackObject;
        private IntPtr _SIPCoreHandle;
    }
}