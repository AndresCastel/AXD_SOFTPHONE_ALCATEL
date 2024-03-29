﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.WPF.Softphone.Applications.PortSIP_Class
{
    public interface SIPCallbackEvents
    {
        Int32 onRegisterSuccess(Int32 callbackObject, Int32 statusCode, String statusText);


        Int32 onRegisterFailure(Int32 callbackObject, Int32 statusCode, String statusText);

        Int32 onInviteIncoming(Int32 callbackObject,
                                             Int32 sessionId,
                                             String caller,
                                             String callerDisplayName,
                                             String callee,
                                             String calleeDisplayName,
                                             String audioCodecName,
                                             String videoCodecName,
                                             Boolean hasVideo);


        Int32 onInviteTrying(Int32 callbackObject, Int32 sessionId, String caller, String callee);

        Int32 onInviteRinging(Int32 callbackObject,
                                            Int32 sessionId,
                                            Boolean hasEarlyMedia,
                                            Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName);



        Int32 onInviteAnswered(Int32 callbackObject,
                                             Int32 sessionId,
                                             Boolean hasVideo,
                                             Int32 statusCode,
                                             String statusText,
                                             String audioCodecName,
                                             String videoCodecName
                                            );


        Int32 onInviteFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);


        Int32 onInviteClosed(Int32 callbackObject, Int32 sessionId);

        Int32 onInviteUpdated(Int32 callbackObject,
                                            Int32 sessionId,
                                            Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName
                                            );


        Int32 onInviteUASConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);


        Int32 onInviteUACConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);


        Int32 onInviteBeginingForward(Int32 callbackObject, String forwardingTo);


        Int32 onRemoteHold(Int32 callbackObject, Int32 sessionId);

        Int32 onRemoteUnHold(Int32 callbackObject, Int32 sessionId);

        Int32 onTransferTrying(Int32 callbackObject, Int32 sessionId, String referTo);

        Int32 onTransferRinging(Int32 callbackObject, Int32 sessionId, Boolean hasVideo);

        Int32 onPASVTransferSuccess(Int32 callbackObject, Int32 sessionId, Boolean hasVideo);

        Int32 onPASVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);


        Int32 onACTVTransferSuccess(Int32 callbackObject, Int32 sessionId);

        Int32 onACTVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText);

        Int32 onRecvPagerMessage(Int32 callbackObject, String from, String fromDisplayName, StringBuilder message);

        Int32 onSendPagerMessageSuccess(Int32 callbackObject,
                                                      String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName
                                                     );



        Int32 onSendPagerMessageFailure(Int32 callbackObject,
                                                       String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName,
                                                      Int32 statusCode,
                                                      String statusText
                                                     );


        Int32 onArrivedSignaling(Int32 callbackObject, Int32 sessionId, StringBuilder signaling);
        Int32 onSentSignaling(Int32 callbackObject, StringBuilder signaling);

        Int32 onWaitingVoiceMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount);


        Int32 onWaitingFaxMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount);


        Int32 onRecvDtmfTone(Int32 callbackObject, Int32 sessionId, Int32 tone);


        Int32 onPresenceRecvSubscribe(Int32 callbackObject,
                                                    Int32 subscribeId,
                                                    String from,
                                                    String fromDisplayName,
                                                    String subject);


        Int32 onPresenceOnline(Int32 callbackObject, String from, String fromDisplayName, String stateText);
        Int32 onPresenceOffline(Int32 callbackObject, String from, String fromDisplayName);

        Int32 onRecvOptions(Int32 callbackObject, StringBuilder optionsMessage);

        Int32 onRecvInfo(Int32 callbackObject, Int32 sessionId, StringBuilder infoMessage);


        Int32 onRecvMessage(Int32 callbackObject, Int32 sessionId, StringBuilder message);


        Int32 onRecvBinaryMessage(Int32 callbackObject,
                                                        Int32 sessionId,
                                                        StringBuilder message,
                                                        byte[] messageBody,
                                                        Int32 length);


        Int32 onRecvBinaryPagerMessage(Int32 callbackObject,
                                                         StringBuilder from,
                                                         StringBuilder fromDisplayName,
                                                         byte[] messageBody,
                                                         Int32 length);



        Int32 onAudioRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               byte[] data,
                                               Int32 dataLength,
                                               Int32 samplingFreqHz);

        Int32 onVideoRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               Int32 width,
                                               Int32 height,
                                               byte[] data,
                                               Int32 dataLength);

        Int32 onReceivedRtpPacket(IntPtr callbackObject,
                                  Int32 sessionId,
                                  Boolean isAudio,
                                  byte[] RTPPacket,
                                  Int32 packetSize);

        Int32 onSendingRtpPacket(IntPtr callbackObject,
                                  Int32 sessionId,
                                  Boolean isAudio,
                                  byte[] RTPPacket,
                                  Int32 packetSize);


        Int32 onPlayWaveFileFinished(IntPtr callbackObject, Int32 sessionId, String fileName);

        Int32 onPlayAviFileFinished(IntPtr callbackObject, Int32 sessionId);
    }
}
