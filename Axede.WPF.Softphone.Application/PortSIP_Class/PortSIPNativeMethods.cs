
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Axede.WPF.Softphone.Applications.PortSIP_Class
{
    unsafe class PortSIP_NativeMethods
    {

        // The delegate methods for callback functions of SIPEventEX.dll

        public unsafe delegate Int32 registerSuccess(Int32 callbackObject, Int32 statusCode, String statusText);
        public unsafe delegate Int32 registerFailure(Int32 callbackObject, Int32 statusCode, String statusText);

        public unsafe delegate Int32 inviteIncoming(Int32 callbackObject,
                                             Int32 sessionId,
                                             String caller,
                                             String callerDisplayName,
                                             String callee,
                                             String calleeDisplayName,
                                             String audioCodecName,
                                             String videoCodecName,
                                             [MarshalAs(UnmanagedType.I1)] Boolean hasVideo);

        public unsafe delegate Int32 inviteTrying(Int32 callbackObject, Int32 sessionId, String caller, String callee);
        public unsafe delegate Int32 inviteRinging(Int32 callbackObject,
                                            Int32 sessionId,
                                            [MarshalAs(UnmanagedType.I1)] Boolean hasEarlyMedia,
                                            [MarshalAs(UnmanagedType.I1)] Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName);


        public unsafe delegate Int32 inviteAnswered(Int32 callbackObject,
                                             Int32 sessionId,
                                             [MarshalAs(UnmanagedType.I1)] Boolean hasVideo,
                                             Int32 statusCode,
                                             String statusText,
                                             String audioCodecName,
                                             String videoCodecName
                                            );

        public unsafe delegate Int32 inviteFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);
        public unsafe delegate Int32 inviteClosed(Int32 callbackObject, Int32 sessionId);


        public unsafe delegate Int32 inviteUpdated(Int32 callbackObject,
                                            Int32 sessionId,
                                            [MarshalAs(UnmanagedType.I1)] Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName
                                            );

        public unsafe delegate Int32 inviteUASConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);

        public unsafe delegate Int32 inviteUACConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);

        public unsafe delegate Int32 inviteBeginingForward(Int32 callbackObject, String forwardingTo);

        public unsafe delegate Int32 remoteHold(Int32 callbackObject, Int32 sessionId);
        public unsafe delegate Int32 remoteUnHold(Int32 callbackObject, Int32 sessionId);



        public unsafe delegate Int32 transferTrying(Int32 callbackObject, Int32 sessionId, String referTo);
        public unsafe delegate Int32 transferRinging(Int32 callbackObject, Int32 sessionId, [MarshalAs(UnmanagedType.I1)] Boolean hasVideo);

        public unsafe delegate Int32 PASVTransferSuccess(Int32 callbackObject, Int32 sessionId, [MarshalAs(UnmanagedType.I1)] Boolean hasVideo);
        public unsafe delegate Int32 PASVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);


        public unsafe delegate Int32 ACTVTransferSuccess(Int32 callbackObject, Int32 sessionId);
        public unsafe delegate Int32 ACTVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);

        public unsafe delegate Int32 recvPagerMessage(Int32 callbackObject, String from, String fromDisplayName, StringBuilder message);
        public unsafe delegate Int32 sendPagerMessageSuccess(Int32 callbackObject,
                                                      String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName
                                                     );


        public unsafe delegate Int32 sendPagerMessageFailure(Int32 callbackObject,
                                                       String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName,
                                                      Int32 statusCode,
                                                      String statusText
                                                     );


        public unsafe delegate Int32 arrivedSignaling(Int32 callbackObject, Int32 sessionId, StringBuilder signaling);
        public unsafe delegate Int32 sentSignaling(Int32 callbackObject, StringBuilder signaling);

        public unsafe delegate Int32 waitingVoiceMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount);

        public unsafe delegate Int32 waitingFaxMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount);


        public unsafe delegate Int32 recvDtmfTone(Int32 callbackObject, Int32 sessionId, Int32 tone);

        public unsafe delegate Int32 presenceRecvSubscribe(Int32 callbackObject,
                                                    Int32 subscribeId,
                                                    String from,
                                                    String fromDisplayName,
                                                    String subject);

        public unsafe delegate Int32 presenceOnline(Int32 callbackObject, String from, String fromDisplayName, String stateText);
        public unsafe delegate Int32 presenceOffline(Int32 callbackObject, String from, String fromDisplayName);

        public unsafe delegate Int32 recvOptions(Int32 callbackObject, StringBuilder optionsMessage);
        public unsafe delegate Int32 recvInfo(Int32 callbackObject, Int32 sessionId, StringBuilder infoMessage);
        public unsafe delegate Int32 recvMessage(Int32 callbackObject, Int32 sessionId, StringBuilder message);

        public unsafe delegate Int32 recvBinaryMessage(Int32 callbackObject,
                                                        Int32 sessionId,
                                                        StringBuilder message,
                                                        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] messageBody,
                                                       Int32 length);

        public unsafe delegate Int32 recvBinaryPagerMessage(Int32 callbackObject,
                                                        StringBuilder from,
                                                        StringBuilder fromDisplayName,
                                                        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] messageBody,
                                                       Int32 length);


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        // The functions of SIPEventEX.dll
        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr SIPEV_createSIPCallbackHandle();

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_shutdownSIPCallbackHandle(IntPtr callbackHandle);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_releaseSIPCallbackHandle(IntPtr callbackHandle);


        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRegisterSuccessHandler(IntPtr callbackHandle, registerSuccess eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRegisterFailureHandler(IntPtr callbackHandle, registerFailure eventHandler, Int32 callbackObject);


        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteIncomingHandler(IntPtr callbackHandle, inviteIncoming eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteTryingHandler(IntPtr callbackHandle, inviteTrying eventHandler, Int32 callbackObject);


        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteRingingHandler(IntPtr callbackHandle, inviteRinging eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteAnsweredHandler(IntPtr callbackHandle, inviteAnswered eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteFailureHandler(IntPtr callbackHandle, inviteFailure eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteClosedHandler(IntPtr callbackHandle, inviteClosed eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteUpdatedHandler(IntPtr callbackHandle, inviteUpdated eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteUASConnectedHandler(IntPtr callbackHandle, inviteUASConnected eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteUACConnectedHandler(IntPtr callbackHandle, inviteUACConnected eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setInviteBeginingForwardHandler(IntPtr callbackHandle, inviteBeginingForward eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRemoteHoldHandler(IntPtr callbackHandle, remoteHold eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRemoteUnHoldHandler(IntPtr callbackHandle, remoteUnHold eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setTransferTryingHandler(IntPtr callbackHandle, transferTrying eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setTransferRingingHandler(IntPtr callbackHandle, transferRinging eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setPASVTransferSuccessHandler(IntPtr callbackHandle, PASVTransferSuccess eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setACTVTransferSuccessHandler(IntPtr callbackHandle, ACTVTransferSuccess eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setPASVTransferFailureHandler(IntPtr callbackHandle, PASVTransferFailure eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setACTVTransferFailureHandler(IntPtr callbackHandle, ACTVTransferFailure eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvPagerMessageHandler(IntPtr callbackHandle, recvPagerMessage eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setSendPagerMessageSuccessHandler(IntPtr callbackHandle, sendPagerMessageSuccess eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setSendPagerMessageFailureHandler(IntPtr callbackHandle, sendPagerMessageFailure eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setArrivedSignalingHandler(IntPtr callbackHandle, arrivedSignaling eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setSentSignalingHandler(IntPtr callbackHandle, sentSignaling eventHandler, Int32 callbackObject);


        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setWaitingVoiceMessageHandler(IntPtr callbackHandle, waitingVoiceMessage eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setWaitingFaxMessageHandler(IntPtr callbackHandle, waitingFaxMessage eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvDtmfToneHandler(IntPtr callbackHandle, recvDtmfTone eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setPresenceRecvSubscribeHandler(IntPtr callbackHandle, presenceRecvSubscribe eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setPresenceOnlineHandler(IntPtr callbackHandle, presenceOnline eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setPresenceOfflineHandler(IntPtr callbackHandle, presenceOffline eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvOptionsHandler(IntPtr callbackHandle, recvOptions eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvInfoHandler(IntPtr callbackHandle, recvInfo eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvMessageHandler(IntPtr callbackHandle, recvMessage eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvBinaryMessageHandler(IntPtr callbackHandle, recvBinaryMessage eventHandler, Int32 callbackObject);

        [DllImport("SIPEventEX.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void SIPEV_setRecvBinaryPagerMessageHandler(IntPtr callbackHandle, recvBinaryPagerMessage eventHandler, Int32 callbackObject);



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // The delegate methods for callback functions of PortSIPCore.dll
        // These callback functions allows you access the raw audio and video data.


        public unsafe delegate Int32 audioRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] data,
                                               Int32 dataLength,
                                               Int32 samplingFreqHz);


        public unsafe delegate Int32 videoRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               Int32 width,
                                               Int32 height,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] data,
                                               Int32 dataLength);

        public unsafe delegate Int32 receivedRTPCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               [MarshalAs(UnmanagedType.I1)] Boolean isAudio,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] RTPPacket,
                                               Int32 packetSize);

        public unsafe delegate Int32 sendingRTPCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               [MarshalAs(UnmanagedType.I1)] Boolean isAudio,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] RTPPacket,
                                               Int32 packetSize);


        // The delegate methods for callback functions of PortSIPCore.dll
        // These callback functions will be fired when the play the wave file and AVI file to remote side finished.

        public unsafe delegate Int32 playWaveFileToRemoteFinished(IntPtr callbackObject, Int32 sessionId, String fileName);
        public unsafe delegate Int32 playAviFileToRemoteFinished(IntPtr callbackObject, Int32 sessionId);


        //////////////////////////////////////////////////////////////////////////
        // The functions of PortSIPCore.dll
        //////////////////////////////////////////////////////////////////////////

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr PortSIP_initialize(IntPtr callbackEvent,
                                                   Int32 transportType,
                                                   Int32 appLogLevel,
                                                   String logFilePath,
                                                   Int32 maximumLines,
                                                   String agent,
                                                   String STUNSever,
                                                   Int32 STUNServerPort,
                                                   [MarshalAs(UnmanagedType.I1)] Boolean useVirtualAudioDevice,
                                                   [MarshalAs(UnmanagedType.I1)] Boolean useVirtualVideoDevice,
                                                   out Int32 errorCode);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setUserInfo(IntPtr SIPCoreLib,
                                                   String userName,
                                                   String displayName,
                                                   String authName,
                                                   String password,
                                                   String localIp,
                                                   Int32 localSipPort,
                                                   String userDomain,
                                                   String SIPServer,
                                                   Int32 SIPServerPort,
                                                   String outboundServer,
                                                   Int32 outboundServerPort);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_unInitialize(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getLocalIP(IntPtr SIPCoreHandle, Int32 index, StringBuilder ip, Int32 ipLength);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getLocalIP6(IntPtr SIPCoreHandle, Int32 index, StringBuilder ip, Int32 ipLength);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getNICNums(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setLicenseKey(IntPtr SIPCoreHandle, String key);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getVersion(IntPtr SIPCoreHandle, out Int32 majorVersion, out Int32 minorVersion);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableStackLog(IntPtr SIPCoreHandle, String logFilePath);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_enableSessionTimer(IntPtr SIPCoreHandle, Int32 timerSeconds, Int32 refreshMode);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_disableSessionTimer(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setKeepAliveTime(IntPtr SIPCoreHandle, Int32 keepAliveTime);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setReliableProvisionalMode(IntPtr SIPCoreHandle, Int32 mode);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableCheckMwi(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_detectMwi(IntPtr SIPCoreHandle);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setSrtpPolicy(IntPtr SIPCoreHandle, Int32 srtPolicy);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setRtpPortRange(IntPtr SIPCoreHandle,
                                                   Int32 minimumRtpAudioPort,
                                                   Int32 maximumRtpAudioPort,
                                                   Int32 minimumRtpVideoPort,
                                                   Int32 maximumRtpVideoPort);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setRtcpPortRange(IntPtr SIPCoreHandle,
                                                   Int32 minimumRtcpAudioPort,
                                                   Int32 maximumRtcpAudioPort,
                                                   Int32 minimumRtcpVideoPort,
                                                   Int32 maximumRtcpVideoPort);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setRtpKeepAlive(IntPtr SIPCoreHandle,
                                                                [MarshalAs(UnmanagedType.I1)] Boolean state,
                                                                Int32 keepAlivePayloadType,
                                                                Int32 deltaTransmitTimeMS);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setRtpCallback(IntPtr SIPCoreHandle,
                                                                IntPtr callbackObj,
                                                                receivedRTPCallback receivedRTPCallbackHandler,
                                                                sendingRTPCallback sendingRTPCallbackHandler);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_registerServer(IntPtr SIPCoreHandle, Int32 expires);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_unRegisterServer(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_addAudioCodec(IntPtr SIPCoreHandle, Int32 codecType);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_addVideoCodec(IntPtr SIPCoreHandle, Int32 codecType);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setAudioCodecPayloadType(IntPtr SIPCoreHandle, Int32 codecType, Int32 payloadType);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setVideoCodecPayloadType(IntPtr SIPCoreHandle, Int32 codecType, Int32 payloadType);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setAudioCodecParameter(IntPtr SIPCoreHandle, Int32 codecType, String parameter);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setVideoCodecParameter(IntPtr SIPCoreHandle, Int32 codecType, String parameter);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_clearAudioCodec(IntPtr SIPCoreHandle);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_clearVideoCodec(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static unsafe extern Boolean PortSIP_isAudioCodecEmpty(IntPtr SIPCoreHandle);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static unsafe extern Boolean PortSIP_isVideoCodecEmpty(IntPtr SIPCoreHandle);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setAudioDeviceId(IntPtr SIPCoreHandle, Int32 recordingDeviceId, Int32 playoutDeviceId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setVideoDeviceId(IntPtr SIPCoreHandle, Int32 deviceId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setVideoBitrate(IntPtr SIPCoreHandle, Int32 bitrateKbps);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setVideoFrameRate(IntPtr SIPCoreHandle, Int32 frameRate);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setVideoResolution(IntPtr SIPCoreHandle, Int32 resolution);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setLocalVideoWindow(IntPtr SIPCoreHandle, IntPtr localVideoWindow);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setRemoteVideoWindow(IntPtr SIPCoreHandle, Int32 sessionId, IntPtr remoteVideoWindow);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_viewLocalVideo(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_startVideoSending(IntPtr SIPCoreHandle, Int32 sessionId, [MarshalAs(UnmanagedType.I1)] Boolean state);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_call(IntPtr SIPCoreHandle, String callTo, [MarshalAs(UnmanagedType.I1)] Boolean sendSdp, out Int32 errorCode);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_rejectCall(IntPtr SIPCoreHandle, Int32 sessionId, int code, String reason);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_answerCall(IntPtr SIPCoreHandle, Int32 sessionId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_terminateCall(IntPtr SIPCoreHandle, Int32 sessionId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_forwardCall(IntPtr SIPCoreHandle, Int32 sessionId, String forwardTo);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_enableCallForwarding(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean forBusyOnly, String forwardingTo);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_disableCallForwarding(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_updateInvite(IntPtr SIPCoreHandle, Int32 sessionId);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_hold(IntPtr SIPCoreHandle, Int32 sessionId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_unHold(IntPtr SIPCoreHandle, Int32 sessionId);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_refer(IntPtr SIPCoreHandle, Int32 sessionId, String referTo);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_attendedRefer(IntPtr SIPCoreHandle, Int32 sessionId, Int32 replaceSessionId, String referTo);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setAudioJitterBufferLevel(IntPtr SIPCoreHandle, Int32 jitterBufferLevel);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getNetworkStatistics(IntPtr SIPCoreHandle,
                                                                         Int32 sessionId,
                                                                         out Int32 currentBufferSize,
                                                                         out Int32 preferredBufferSize,
                                                                         out Int32 currentPacketLossRate,
                                                                         out Int32 currentDiscardRate,
                                                                         out Int32 currentExpandRate,
                                                                         out Int32 currentPreemptiveRate,
                                                                         out Int32 currentAccelerateRate);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getAudioRtpStatistics(IntPtr SIPCoreHandle,
                                                                         Int32 sessionId,
                                                                         out Int32 averageJitterMs,
                                                                         out Int32 maxJitterMs,
                                                                         out Int32 discardedPackets);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getAudioRtcpStatistics(IntPtr SIPCoreHandle,
                                                                         Int32 sessionId,
                                                                         out Int32 bytesSent,
                                                                         out Int32 packetsSent,
                                                                         out Int32 bytesReceived,
                                                                         out Int32 packetsReceived);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getVideoRtpStatistics(IntPtr SIPCoreHandle,
                                                                         Int32 sessionId,
                                                                         out Int32 bytesSent,
                                                                         out Int32 packetsSent,
                                                                         out Int32 bytesReceived,
                                                                         out Int32 packetsReceived);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableAEC(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableVAD(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableCNG(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableAGC(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setAudioSamples(IntPtr SIPCoreHandle, Int32 ptime, Int32 maxPtime);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setDTMFSamples(IntPtr SIPCoreHandle, Int32 samples);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableDTMFOfRFC2833(IntPtr SIPCoreHandle, Int32 RTPPayloadOf2833);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableDTMFOfInfo(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendDTMF(IntPtr SIPCoreHandle, Int32 sessionId, Char code);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_startAudioRecording(IntPtr SIPCoreHandle,
                                                                     String filePath,
                                                                     String fileName,
                                                                     [MarshalAs(UnmanagedType.I1)] Boolean appendTimestamp,
                                                                     Int32 fileFormat,
                                                                     Int32 recordMode);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_startVideoRecording(IntPtr SIPCoreHandle,
                                                                String filePath,
                                                                String fileName,
                                                                [MarshalAs(UnmanagedType.I1)] Boolean appendTimestamp,
                                                                Int32 codecType,
                                                                Int32 recordMode);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_stopAudioRecording(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_stopVideoRecording(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_muteMicrophone(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean mute);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_muteSpeaker(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean mute);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getExtensionHeaderValue(IntPtr SIPCoreHandle,
                                                                     String sipMessage,
                                                                     String headerName,
                                                                     StringBuilder headerValue,
                                                                     Int32 headerValueLength);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_addExtensionHeader(IntPtr SIPCoreHandle, String headerName, String headerValue);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_clearAddExtensionHeaders(IntPtr SIPCoreHandle);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_modifyHeaderValue(IntPtr SIPCoreHandle, String headerName, String headerValue);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_clearModifyHeaders(IntPtr SIPCoreHandle);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_enableCallbackSentSignaling(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setAudioQos(IntPtr SIPCoreHandle,
                                                               [MarshalAs(UnmanagedType.I1)] Boolean state,
                                                               Int32 DSCPValue,
                                                               Int32 priority);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setVideoQos(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean state, Int32 DSCPValue);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_getDynamicVolumeLevel(IntPtr SIPCoreHandle, out Int32 speakerVolume, out Int32 microphoneVolume);





        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_enableSendPcmStreamToRemote(IntPtr SIPCoreHandle,
                                                                            Int32 sessionId,
                                                                            [MarshalAs(UnmanagedType.I1)] Boolean state,
                                                                            Int32 streamSamplesPerSec);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendPcmStreamToRemote(IntPtr SIPCoreHandle,
                                                                    Int32 sessionId,
                                                                    [MarshalAs(UnmanagedType.LPArray)] byte[] data,
                                                                    Int32 dataLength);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_discardAudio(IntPtr SIPCoreHandle,
                                                                    [MarshalAs(UnmanagedType.I1)] Boolean discardIncomingAudio,
                                                                    [MarshalAs(UnmanagedType.I1)] Boolean discardOutgoingAudio);




        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_setPlayWaveFileToRemote(IntPtr SIPCoreHandle,
                                                                  Int32 sessionId,
                                                                  String waveFile,
                                                                  [MarshalAs(UnmanagedType.I1)] Boolean enableState,
                                                                  Int32 playMode,
                                                                  Int32 fileSamplesPerSec,
                                                                  IntPtr reserve,
                                                                  playWaveFileToRemoteFinished callbackHandler);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setPlayAviFileToRemote(IntPtr SIPCoreHandle,
                                                                 Int32 sessionId,
                                                                 String aviFile,
                                                                 [MarshalAs(UnmanagedType.I1)] Boolean enableState,
                                                                 [MarshalAs(UnmanagedType.I1)] Boolean loop,
                                                                 [MarshalAs(UnmanagedType.I1)] Boolean playAudio,
                                                                 IntPtr reserve,
                                                                 playAviFileToRemoteFinished callbackHandler);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_enableAudioStreamCallback(IntPtr SIPCoreHandle,
                                                                            Int32 sessionId,
                                                                            [MarshalAs(UnmanagedType.I1)] Boolean enable,
                                                                            Int32 callbackType,
                                                                            IntPtr callbackObject,
                                                                            audioRawCallback callbackHandler);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_enableVideoStreamCallback(IntPtr SIPCoreHandle,
                                                                            Int32 sessionId,
                                                                            Int32 callbackType,
                                                                            IntPtr callbackObject,
                                                                            videoRawCallback callbackHandler);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_createConference(IntPtr SIPCoreHandle,
                                                                    IntPtr conferenceVideoWindow,
                                                                    Int32 videoResolution,
                                                                    [MarshalAs(UnmanagedType.I1)] Boolean viewLocalVideoImage);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_createConferenceEx(IntPtr SIPCoreHandle,
                                                                    IntPtr conferenceVideoWindow,
                                                                    Int32 videoResolution,
                                                                    [MarshalAs(UnmanagedType.I1)] Boolean viewLocalVideoImage,
                                                                    [MarshalAs(UnmanagedType.LPArray)] Int32[] sessionIds,
                                                                    Int32 sessionIdNums);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_destroyConference(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_joinToConference(IntPtr SIPCoreHandle, Int32 sessionId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_removeFromConference(IntPtr SIPCoreHandle, Int32 sessionId);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_addSupportedMimeType(IntPtr SIPCoreHandle,
                                                               String methodName,
                                                               String mimeType,
                                                               String subMimeType);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendInfo(IntPtr SIPCoreHandle,
                                                      Int32 sessionId,
                                                      String mimeType,
                                                      String subMimeType,
                                                      String infoContents);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendOptions(IntPtr SIPCoreHandle, String to, String sdp);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendMessage(IntPtr SIPCoreHandle, Int32 sessionId, String mimeType, String subMimeType, String message);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendMessageEx(IntPtr SIPCoreHandle, Int32 sessionId, String mimeType, String subMimeType, [MarshalAs(UnmanagedType.LPArray)] byte[] message, Int32 messageLength);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendOutOfDialogMessage(IntPtr SIPCoreHandle, String to, String mimeType, String subMimeType, String message);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendOutOfDialogMessageEx(IntPtr SIPCoreHandle, String to, String mimeType, String subMimeType, [MarshalAs(UnmanagedType.LPArray)] byte[] message, Int32 messageLength);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_sendPagerMessage(IntPtr SIPCoreHandle, String to, String message);




        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_presenceSubscribeContact(IntPtr SIPCoreHandle, String contact, String subject);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_presenceRejectSubscribe(IntPtr SIPCoreHandle, Int32 subscribeId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_presenceAcceptSubscribe(IntPtr SIPCoreHandle, Int32 subscribeId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_presenceOffline(IntPtr SIPCoreHandle, Int32 subscribeId);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_presenceOnline(IntPtr SIPCoreHandle, Int32 subscribeId, String stateText);





        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getNumOfRecordingDevices(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getNumOfPlayoutDevices(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getRecordingDeviceName(IntPtr SIPCoreHandle,
                                                             Int32 deviceIndex,
                                                             StringBuilder nameUTF8,
                                                             Int32 nameUTF8Length);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getPlayoutDeviceName(IntPtr SIPCoreHandle,
                                                            Int32 deviceIndex,
                                                            StringBuilder nameUTF8,
                                                            Int32 nameUTF8Length);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setSpeakerVolume(IntPtr SIPCoreHandle, Int32 volume);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getSpeakerVolume(IntPtr SIPCoreHandle);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setSystemOutputMute(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean enable);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static unsafe extern Boolean PortSIP_getSystemOutputMute(IntPtr SIPCoreHandle);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setMicVolume(IntPtr SIPCoreHandle, Int32 volume);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getMicVolume(IntPtr SIPCoreHandle);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_setSystemInputMute(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean enable);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static unsafe extern Boolean PortSIP_getSystemInputMute(IntPtr SIPCoreHandle);





        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_playLocalWaveFile(IntPtr SIPCoreHandle,
                                                                    String waveFile,
                                                                    [MarshalAs(UnmanagedType.I1)] Boolean loop);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void PortSIP_audioPlayLoopbackTest(IntPtr SIPCoreHandle, [MarshalAs(UnmanagedType.I1)] Boolean enable);


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getNumOfVideoCaptureDevices(IntPtr SIPCoreHandle);



        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_getVideoCaptureDeviceName(IntPtr SIPCoreHandle,
                                                                           Int32 deviceIndex,
                                                                           StringBuilder uniqueIdUTF8,
                                                                           Int32 uniqueIdUTF8Length,
                                                                           StringBuilder deviceNameUTF8,
                                                                           Int32 deviceNameUTF8Length);




        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern Int32 PortSIP_showVideoCaptureSettingsDialogBox(IntPtr SIPCoreHandle,
                                                                    String uniqueIdUTF8,
                                                                    Int32 uniqueIdUTF8Length,
                                                                    String dialogTitle,
                                                                    IntPtr parentWindow,
                                                                    Int32 x,
                                                                    Int32 y);


        //////////////////////////////////////////////////////////////////////////
        //
        //
        // Extend functions, don't use it
        //
        //
        //////////////////////////////////////////////////////////////////////////


        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 PortSIP_setAMRCodecParameter(IntPtr SIPCoreHandle, Int32 codecType, Int32 changeMode, Int32 fixedRate);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 PortSIP_getAvailableAudioCodecs(IntPtr SIPCoreHandle, Int32 sessionId, Int32[] audioCodecsType, Int32[] localRtpMapValues, Int32[] remoteRtpMapValues, Int32 CodecsLength);

        [DllImport("PortSIPCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 PortSIP_changeSendingAudioCodec(IntPtr SIPCoreHandle, Int32 sessionId, Int32 localRtpmapForSending);

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

    }
}
