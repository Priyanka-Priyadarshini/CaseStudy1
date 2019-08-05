using NetMQ;
using NetMQ.Sockets;
using System;

namespace AlertSystemClient
{
    class PatientDataRecord
    {
        public RequestSocket socket;
        public PatientDataRecord()
        {
            socket = new RequestSocket();
            socket.Connect("tcp://localhost:8080");
        }
        public void SendPatientData(string randPatientId, string randSPO2, string randPulse, string randTemp)
        {
            socket.SendMoreFrame(randPatientId)
                .SendMoreFrame(GetTimestamp(DateTime.Now))
                .SendMoreFrame(randSPO2)
                .SendMoreFrame(randPulse)
                .SendFrame(randTemp);
        }
        public string GetTimestamp(DateTime now)
        {
            return now.ToString("dd/MM/yyyy/HH:mm:ss");
        }
        public string ReceiveAcknowledgement()
        {
            var msgReceived = socket.ReceiveFrameString();
            return msgReceived;
        }
    }
}
