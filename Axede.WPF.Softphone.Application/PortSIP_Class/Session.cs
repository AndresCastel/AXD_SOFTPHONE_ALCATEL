using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.WPF.Softphone.Applications.PortSIP_Class
{
    public class Session
    {
        private int mSessionId = 0;
        private bool mHoldState = false;
        private bool mSessionState = false;
        private bool mRecvCallState = false;


        public void reset()
        {
            mSessionId = 0;
            mHoldState = false;
            mSessionState = false;
            mRecvCallState = false;
        }


        public void setSessionId(int sessionId)
        {
            mSessionId = sessionId;
        }


        public int getSessionId()
        {
            return mSessionId;
        }

        public void setHoldState(bool state)
        {
            mHoldState = state;
        }


        public bool getHoldState()
        {
            return mHoldState;
        }

        public void setSessionState(bool state)
        {
            mSessionState = state;
        }


        public bool getSessionState()
        {
            return mSessionState;
        }



        public void setRecvCallState(bool state)
        {
            mRecvCallState = state;
        }


        public bool getRecvCallState()
        {
            return mRecvCallState;
        }

    }
}
